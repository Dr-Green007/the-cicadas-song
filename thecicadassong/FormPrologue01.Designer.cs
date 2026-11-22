namespace thecicadassong
{
    partial class FormPrologue01
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            testtext = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // testtext
            // 
            testtext.AutoSize = true;
            testtext.Font = new Font("SimSun-ExtB", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            testtext.ForeColor = SystemColors.ButtonFace;
            testtext.Location = new Point(160, 266);
            testtext.Name = "testtext";
            testtext.Size = new Size(731, 29);
            testtext.TabIndex = 0;
            testtext.Text = "*ТЕСТОВЫЙ ТЕКСТ! ЗДЕСЬ НИЧЕГО ПОКА ЧТО НЕТУ*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("SimSun-ExtG", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(12, 26);
            label1.Name = "label1";
            label1.Size = new Size(235, 35);
            label1.TabIndex = 1;
            label1.Text = "Сохранить игру";
            // 
            // FormPrologue01
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1008, 729);
            Controls.Add(label1);
            Controls.Add(testtext);
            Name = "FormPrologue01";
            Text = "FormPrologue01";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label testtext;
        private Label label1;
    }
}