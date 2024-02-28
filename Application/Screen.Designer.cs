namespace Application
{
    partial class Screen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            clubNameTextBox = new TextBox();
            folderLabel = new Label();
            clubNames = new Label();
            excelGeneratorButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // clubNameTextBox
            // 
            clubNameTextBox.Location = new Point(12, 201);
            clubNameTextBox.Name = "clubNameTextBox";
            clubNameTextBox.Size = new Size(533, 31);
            clubNameTextBox.TabIndex = 0;
            // 
            // folderLabel
            // 
            folderLabel.AutoSize = true;
            folderLabel.Location = new Point(12, 59);
            folderLabel.Name = "folderLabel";
            folderLabel.Size = new Size(550, 25);
            folderLabel.TabIndex = 1;
            folderLabel.Text = "Sélectionnez le folder dans lequel se trouvent les feuilles de matches";
            // 
            // clubNames
            // 
            clubNames.AutoSize = true;
            clubNames.Location = new Point(12, 173);
            clubNames.Name = "clubNames";
            clubNames.Size = new Size(533, 25);
            clubNames.TabIndex = 2;
            clubNames.Text = "Entrez le nom du club (comme induqé sur les feuilles de matches):";
            // 
            // excelGeneratorButton
            // 
            excelGeneratorButton.Location = new Point(12, 316);
            excelGeneratorButton.Name = "excelGeneratorButton";
            excelGeneratorButton.Size = new Size(597, 34);
            excelGeneratorButton.TabIndex = 3;
            excelGeneratorButton.Text = "Générez le fichier Excel";
            excelGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 406);
            Controls.Add(excelGeneratorButton);
            Controls.Add(clubNames);
            Controls.Add(folderLabel);
            Controls.Add(clubNameTextBox);
            Name = "Form1";
            Text = "VdL Excel Generator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox clubNameTextBox;
        private Label folderLabel;
        private Label clubNames;
        private Button excelGeneratorButton;
        private OpenFileDialog openFileDialog1;
    }
}
