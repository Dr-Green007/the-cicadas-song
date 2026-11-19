namespace thecicadassong
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
            newgame = new Label();
            loadgame = new Label();
            leadership = new Label();
            developers = new Label();
            quit = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // newgame
            // 
            newgame.AutoSize = true;
            newgame.Font = new Font("Javanese Text", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            newgame.ForeColor = SystemColors.ButtonFace;
            newgame.Location = new Point(861, 39);
            newgame.Name = "newgame";
            newgame.Size = new Size(148, 43);
            newgame.TabIndex = 0;
            newgame.Text = "Новая игра";
            // 
            // loadgame
            // 
            loadgame.AutoSize = true;
            loadgame.Font = new Font("Javanese Text", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loadgame.ForeColor = SystemColors.ButtonFace;
            loadgame.Location = new Point(861, 82);
            loadgame.Name = "loadgame";
            loadgame.Size = new Size(153, 34);
            loadgame.TabIndex = 1;
            loadgame.Text = "Загрузить игру";
            // 
            // leadership
            // 
            leadership.AutoSize = true;
            leadership.Font = new Font("Javanese Text", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            leadership.ForeColor = SystemColors.ButtonFace;
            leadership.Location = new Point(863, 116);
            leadership.Name = "leadership";
            leadership.Size = new Size(146, 29);
            leadership.TabIndex = 2;
            leadership.Text = "Таблица лидеров";
            // 
            // developers
            // 
            developers.AutoSize = true;
            developers.Font = new Font("Javanese Text", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            developers.ForeColor = SystemColors.ButtonFace;
            developers.Location = new Point(863, 145);
            developers.Name = "developers";
            developers.Size = new Size(141, 29);
            developers.TabIndex = 3;
            developers.Text = "О разработчиках";
            // 
            // quit
            // 
            quit.AutoSize = true;
            quit.Font = new Font("Javanese Text", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            quit.ForeColor = SystemColors.ButtonFace;
            quit.Location = new Point(886, 174);
            quit.Name = "quit";
            quit.Size = new Size(94, 43);
            quit.TabIndex = 4;
            quit.Text = "Выйти";
            quit.Click += quit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(174, 410);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 5;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1008, 729);
            Controls.Add(label1);
            Controls.Add(quit);
            Controls.Add(developers);
            Controls.Add(leadership);
            Controls.Add(loadgame);
            Controls.Add(newgame);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label newgame;
        private Label loadgame;
        private Label leadership;
        private Label developers;
        private Label quit;
        private Label label1;
    }
}
