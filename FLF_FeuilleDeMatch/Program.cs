using iText.Kernel.Pdf;
using System.Text.RegularExpressions;
using OfficeOpenXml;

class Program
{
    static void Main(string[] args)
    {
        string folderPath = @"C:\Feuille_de_match";

        Dictionary<string, List<string>> playerAttendance = new Dictionary<string, List<string>>();
        List<string> allGameDates = new List<string>();

        string[] folders = Directory.GetDirectories(folderPath);
        foreach (string folder in folders)
        {
            string[] pdfFiles = Directory.GetFiles(folder, "*.pdf");
            foreach (string pdfFile in pdfFiles)
            {
                using (PdfReader pdfReader = new PdfReader(pdfFile))
                {
                    PdfDocument pdfDocument = new PdfDocument(pdfReader);

                    int numPages = pdfDocument.GetNumberOfPages();
                    for (int pageNum = 1; pageNum <= numPages; pageNum++)
                    {
                        string pageText = ExtractTextFromPage(pdfDocument, pageNum);
                        if (pageText.Contains("Club A: Red Star Merl-Belair") || pageText.Contains("Club B: Red Star Merl-Belair"))
                        {
                            string gameDate = ExtractGameDate(pageText);
                            if (!string.IsNullOrEmpty(gameDate) && !allGameDates.Contains(gameDate))
                            {
                                allGameDates.Add(gameDate);
                            }
                            ExtractPlayerDetails(playerAttendance, pageText, gameDate);
                            break; // No need to process further pages once the relevant page is found
                        }
                    }
                }
            }
        }

        CreateExcelFile(allGameDates, playerAttendance);
        Console.WriteLine("Excel file generated successfully.");
    }

    static void CreateExcelFile(List<string> allGameDates, Dictionary<string, List<string>> playerAttendance)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Create a new Excel package
        using (var excelPackage = new ExcelPackage())
        {
            // Add a new worksheet
            var worksheet = excelPackage.Workbook.Worksheets.Add("Attendance");

            // Write headers in the first row
            worksheet.Cells[1, 1].Value = "Names";
            worksheet.Cells[1, 2].Value = "License Number";
            worksheet.Cells[1, 3].Value = "Birthdate";
            int lastCol = allGameDates.Count + 4; // Offset for additional columns

            // Write game dates as titles for the columns starting from the fourth column
            int col = 4;
            foreach (var gameDate in allGameDates)
            {
                worksheet.Cells[1, col].Value = gameDate;
                col++;
            }
            worksheet.Cells[1, lastCol].Value = "Occurrences"; // Add column for occurrences

            // Write player names, license numbers, and birthdates
            int row = 2;
            foreach (var player in playerAttendance)
            {
                string[] playerDetails = player.Key.Split('|');
                worksheet.Cells[row, 1].Value = playerDetails[0]; // Player name
                worksheet.Cells[row, 2].Value = playerDetails[1]; // License number
                worksheet.Cells[row, 3].Value = playerDetails[2]; // Birthdate

                // Mark attendance with "x" and count occurrences
                int occurrences = 0;
                col = 4;
                foreach (var gameDate in allGameDates)
                {
                    bool present = player.Value.Contains(gameDate);
                    worksheet.Cells[row, col].Value = present ? "x" : "";
                    if (present)
                        occurrences++;
                    col++;
                }
                worksheet.Cells[row, lastCol].Value = occurrences; // Write occurrences
                row++;
            }

            // Save the Excel package
            FileInfo excelFile = new FileInfo(@"C:\Feuille_de_match\Attendance.xlsx");
            excelPackage.SaveAs(excelFile);
        }
    }

    static string ExtractGameDate(string pdfText)
    {
        Regex regex = new Regex(@"Date: (.+?)\s");
        Match match = regex.Match(pdfText);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return null;
    }

    static string ExtractTextFromPage(PdfDocument pdfDocument, int pageNumber)
    {
        PdfPage page = pdfDocument.GetPage(pageNumber);
        return iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(page);
    }

    static void ExtractPlayerDetails(Dictionary<string, List<string>> playerAttendance, string text, string gameDate)
    {
        int startIndex = text.IndexOf("Nom & Prénom");
        int endIndex = text.IndexOf("Fonctions");

        if (startIndex != -1 && endIndex != -1)
        {
            string playersSection = text.Substring(startIndex, endIndex - startIndex);
            string[] lines = playersSection.Split('\n');

            for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip headers
            {
                string line = lines[i].Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    parts = parts.Where(part => part != "Oui").ToArray();
                    if (parts.Length > 4 && parts.Length < 14)
                    {
                        string playerName = string.Join(" ", parts.SkipWhile(part => !Regex.IsMatch(part, "[a-zA-Z]")).TakeWhile(part => !Regex.IsMatch(part, @"\d")));
                        string licenseNumber = null;
                        string birthdate = null;
                        foreach (var part in parts)
                        {
                            if (Regex.IsMatch(part, @"\d{2}/\d{2}/\d{4}"))
                                birthdate = part;
                            else if (Regex.IsMatch(part, @"\d+"))
                                licenseNumber = part;
                        }
                        string key = $"{playerName}|{licenseNumber ?? ""}|{birthdate ?? ""}";
                        if (!playerAttendance.ContainsKey(key))
                        {
                            playerAttendance.Add(key, new List<string>());
                        }
                        playerAttendance[key].Add(gameDate);
                    }
                }
            }
        }
    }
}
