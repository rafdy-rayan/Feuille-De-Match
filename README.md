# FeuilleDeMatchApp

FeuilleDeMatchApp is a Windows Forms application designed to automate the process of generating an Excel file containing attendance records from PDF documents.

## Features

- **PDF Parsing**: FeuilleDeMatchApp can parse PDF documents to extract player attendance information.
- **Excel Export**: It can generate an Excel file with attendance records, making it easy to manage and analyze data.
- **User Interface**: The application provides a simple user interface with buttons for selecting the input folder and initiating the attendance generation process.
- **Error Handling**: FeuilleDeMatchApp includes error handling mechanisms to handle exceptions gracefully and provide informative messages to the user.

## Usage

1. **Select Input Folder**: Click on the "Browse" button to select the folder containing the PDF documents with attendance records.
2. **Enter Club Name**: Enter the name of the club or organization for which attendance records are being generated.
3. **Generate Attendance**: Click on the "Generate Attendance" button to initiate the process. The application will parse the PDF documents in the selected folder, extract attendance information, and generate an Excel file.
4. **View Results**: Once the process is complete, the application will display a message indicating the success or failure of the operation. If successful, it will provide the location of the generated Excel file.

## Requirements

- Windows operating system
- .NET Framework 8 or later
- Microsoft Excel (to view the generated Excel file)

## Installation

1. Download the latest release of FeuilleDeMatchApp from the [releases page](https://github.com/rafdy-rayan/Feuille-De-Match/releases).
2. Extract the downloaded ZIP file to a location on your computer.
3. Run the `FeuilleDeMatchApp.exe` file to launch the application.

## Contributing

Contributions to FeuilleDeMatchApp are welcome! If you encounter any bugs, have feature requests, or would like to contribute code improvements, please feel free to [open an issue](https://github.com/yourusername/FeuilleDeMatchApp/issues) or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
