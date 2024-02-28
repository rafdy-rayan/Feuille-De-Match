namespace FeuilleDeMatchApp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            clubNameTextBox = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button2 = new Button();
            label3 = new Label();
            messageBox = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(12, 185);
            button1.Name = "button1";
            button1.Size = new Size(776, 34);
            button1.TabIndex = 0;
            button1.Text = "Générez le fichier Excel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(619, 25);
            label1.TabIndex = 1;
            label1.Text = "Veuillez séléctionner le dossier dans lequel se trouvent les feuilles de matches";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 120);
            label2.Name = "label2";
            label2.Size = new Size(570, 25);
            label2.TabIndex = 2;
            label2.Text = "Entrez le nom de votre club, tel que indiqué sur les feuilles de matches:";
            // 
            // clubNameTextBox
            // 
            clubNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            clubNameTextBox.Location = new Point(12, 148);
            clubNameTextBox.Name = "clubNameTextBox";
            clubNameTextBox.Size = new Size(776, 31);
            clubNameTextBox.TabIndex = 3;
            clubNameTextBox.Text = "Red Star Merl-Belair";
            clubNameTextBox.TextChanged += clubNameTextBox_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(12, 63);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 4;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 222);
            label3.Name = "label3";
            label3.Size = new Size(91, 25);
            label3.TabIndex = 5;
            label3.Text = "Message: ";
            // 
            // messageBox
            // 
            messageBox.Dock = DockStyle.Bottom;
            messageBox.Location = new Point(0, 250);
            messageBox.Multiline = true;
            messageBox.Name = "messageBox";
            messageBox.ReadOnly = true;
            messageBox.ScrollBars = ScrollBars.Both;
            messageBox.Size = new Size(800, 212);
            messageBox.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 462);
            Controls.Add(messageBox);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(clubNameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Generator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private TextBox clubNameTextBox;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button button2;
        private Label label3;
        private TextBox messageBox;
    }
}
