using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using OfficeOpenXml;

namespace FeuilleDeMatchApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string folderPath;
        public string clubName;
        private void button2_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Do something with the selected folder path
                    folderPath = folderBrowserDialog.SelectedPath;
                    MessageBox.Show("Selected folder: " + folderPath);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<(string, string), List<DateTime>> playerAttendance = new Dictionary<(string name, string category), List<DateTime>>();
            List<DateTime> allGameDates = new List<DateTime>();
            clubName = clubNameTextBox.Text;
            try
            {
                string[] folders = Directory.GetDirectories(folderPath);
                foreach (string folder in folders)
                {
                    string[] pdfFiles = Directory.GetFiles(folder, "*.pdf");
                    foreach (string pdfFile in pdfFiles)
                    {
                        try
                        {
                            using (PdfReader pdfReader = new PdfReader(pdfFile))
                            {
                                PdfDocument pdfDocument = new PdfDocument(pdfReader);

                                int numPages = pdfDocument.GetNumberOfPages();
                                bool foundClubName = false;
                                for (int pageNum = 1; pageNum <= numPages; pageNum++)
                                {
                                    string pageText = ExtractTextFromPage(pdfDocument, pageNum);
                                    if (pageText.Contains($"Club A: {clubName}") || pageText.Contains($"Club B: {clubName}"))
                                    {
                                        foundClubName = true;
                                        string[] formats = { "dd/MM/yyyy", "yyyy-MM-dd" };
                                        DateTime gameDate = DateTime.ParseExact(
                                            ExtractGameDate(pageText), 
                                            formats, 
                                            System.Globalization.CultureInfo.InvariantCulture, 
                                            System.Globalization.DateTimeStyles.None);

                                        if (!allGameDates.Contains(gameDate))
                                        {
                                            allGameDates.Add(gameDate);
                                        }
                                        ExtractPlayerDetails(playerAttendance, pageText, gameDate);
                                        break; // No need to process further pages once the relevant page is found
                                    }
                                }
                                if (!foundClubName)
                                {
                                    messageBox.Text += ($"Error finding {clubName} on PDF file {pdfFile}\n");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            messageBox.Text += ($"Error reading PDF file {pdfFile}: {ex.Message}\n");
                        }
                    }
                }

                if (playerAttendance.Count > 0)
                {
                    allGameDates.Sort();
                    CreateExcelFile(allGameDates, playerAttendance);
                    messageBox.Text += ($"Excel file generated successfully.\n Location: {folderPath}\\Attendance.xlsx");
                }
                else
                {
                    messageBox.Text += ("No player attendance data to generate Excel file.\n");
                }
            }
            catch (Exception ex)
            {
                messageBox.Text += ($"An error occurred: {ex.Message}\n");
            }
        }


        static void CreateExcelFile(List<DateTime> allGameDates, Dictionary<(string, string), List<DateTime>> playerAttendance)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Create a new Excel package
                using (var excelPackage = new ExcelPackage())
                {
                    // Add a new worksheet
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Attendance");

                    // Write headers in the first row
                    worksheet.Cells[1, 1].Value = "Names";
                    worksheet.Cells[1, 2].Value = "Category";
                    worksheet.Cells[1, 3].Value = "License Number";
                    worksheet.Cells[1, 4].Value = "Birthdate";
                    int lastCol = allGameDates.Count + 5; // Offset for additional columns

                    // Write game dates as titles for the columns starting from the fourth column
                    int col = 5;
                    foreach (var gameDate in allGameDates)
                    {
                        worksheet.Cells[1, col].Value = gameDate.ToShortDateString();
                        col++;
                    }
                    worksheet.Cells[1, lastCol].Value = "Occurrences"; // Add column for occurrences
                    worksheet.Cells[1, 2].Value = "Category"; // Add column for category

                    // Write player names, license numbers, and birthdates
                    int row = 2;
                    foreach (var player in playerAttendance)
                    {
                        string[] playerDetails = player.Key.Item1.Split('|');
                        string category = player.Key.Item2;

                        worksheet.Cells[row, 1].Value = playerDetails[0]; // Player name
                        worksheet.Cells[row, 2].Value = category; // Category
                        worksheet.Cells[row, 3].Value = playerDetails[1]; // License number (assuming it's the same for all game dates)
                        worksheet.Cells[row, 4].Value = playerDetails[2] ; // Birthdate (assuming it's the same for all game dates)

                        // Mark attendance with "x" and count occurrences
                        int occurrences = 0;
                        col = 5;
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
                    FileInfo excelFile = new FileInfo(Path.Combine(folderPath, "Attendance.xlsx"));
                    excelPackage.SaveAs(excelFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating Excel file: {ex.Message}");
            }
        }

        static string ExtractGameDate(string pdfText)
        {
            try
            {
                Regex regex = new(@"Date: (.+?)\s");
                Match match = regex.Match(pdfText);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error extracting game date: {ex.Message}");
                return null;
            }
        }

        static string ExtractTextFromPage(PdfDocument pdfDocument, int pageNumber)
        {
            try
            {
                PdfPage page = pdfDocument.GetPage(pageNumber);
                return iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(page);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error extracting text from PDF page: {ex.Message}");
                return string.Empty;
            }
        }

        static void ExtractPlayerDetails(Dictionary<(string, string), List<DateTime>> playerAttendance, string text, DateTime gameDate)
        {
            try
            {
                int startIndex = text.IndexOf("Nom & Prénom");
                int endIndex = text.IndexOf("Fonctions");
                int categoryIndex = text.IndexOf("Catégorie:");

                if (startIndex != -1 && endIndex != -1 && categoryIndex != -1)
                {
                    string playersSection = text.Substring(startIndex, endIndex - startIndex);
                    string categoryPattern = @"Catégorie:\s*(\S+)";
                    Match categoryMatch = Regex.Match(text, categoryPattern);
                    string category = categoryMatch.Success ? categoryMatch.Groups[1].Value : "";

                    string[] lines = playersSection.Split('\n');

                    for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip headers
                    {
                        string line = lines[i].Trim();
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            parts = parts.Where(part => part != "Oui" && part != "IN" && part != "OUT").ToArray();
                            if (parts.Length > 4 && parts.Length < 14)
                            {
                                string playerName = string.Join(" ", parts.SkipWhile(part => !Regex.IsMatch(part, "[a-zA-Z]")).TakeWhile(part => !Regex.IsMatch(part, @"\d")));
                                string licenseNumber = null;
                                string birthdate = null;
                                foreach (var part in parts)
                                {
                                    if (Regex.IsMatch(part, @"\d{2}/\d{2}/\d{4}"))
                                        birthdate = part;
                                    else if (Regex.IsMatch(part, @"^\d+"))
                                    {
                                        Match match = Regex.Match(part, @"^\d+");
                                        if (match.Success)
                                        {
                                            licenseNumber = match.Value;
                                        }
                                    }
                                }
                                var key = ($"{playerName}|{licenseNumber ?? ""}|{birthdate ?? ""}", category);
                                if (!playerAttendance.ContainsKey(key))
                                {
                                    playerAttendance.Add(key, new List<DateTime>());
                                }
                                playerAttendance[key].Add(gameDate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while extracting player details: {ex.Message}");
            }
        }

        private void clubNameTextBox_TextChanged(object sender, EventArgs e)
        {
            clubName = clubNameTextBox.Text;
        }
    }
}
