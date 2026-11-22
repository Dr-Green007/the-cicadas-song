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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            testtext = new Label();
            btnSave = new Button();
            btnLoad = new Button();
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
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(30, 30, 50);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("SimSun-ExtG", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = SystemColors.ButtonFace;
            btnSave.Location = new Point(12, 20);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(220, 50);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить игру";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.FromArgb(30, 30, 50);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("SimSun-ExtG", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoad.ForeColor = SystemColors.ButtonFace;
            btnLoad.Location = new Point(245, 20);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(220, 50);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Загрузить игру";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // FormPrologue01
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1008, 729);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(testtext);
            Name = "FormPrologue01";
            Text = "FormPrologue01";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label testtext;
        private Button btnSave;
        private Button btnLoad;
    }
}