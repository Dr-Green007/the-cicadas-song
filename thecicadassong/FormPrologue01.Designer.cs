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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrologue01));
            btnSave = new Button();
            btnLoad = new Button();
            pictureBox1 = new PictureBox();
            text01 = new RichTextBox();
            cont = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
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
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(1, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1004, 625);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // text01
            // 
            text01.BackColor = SystemColors.GradientActiveCaption;
            text01.BorderStyle = BorderStyle.None;
            text01.Font = new Font("Georgia", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            text01.ForeColor = SystemColors.ControlText;
            text01.Location = new Point(163, 345);
            text01.Name = "text01";
            text01.Size = new Size(619, 206);
            text01.TabIndex = 4;
            text01.Text = "Октябрь 1986 года. \nОстров Роккэндзима. \nВдали от материка, на скале над морем, стоит поместье рода Усиромия. \nВ этом году тайфун придёт раньше, чем семья успеет разъехаться.";
            // 
            // cont
            // 
            cont.BackColor = Color.FromArgb(30, 30, 50);
            cont.FlatStyle = FlatStyle.Flat;
            cont.Font = new Font("SimSun-ExtG", 14.25F);
            cont.ForeColor = SystemColors.ButtonFace;
            cont.Location = new Point(812, 505);
            cont.Name = "cont";
            cont.Size = new Size(177, 55);
            cont.TabIndex = 5;
            cont.Text = "Продолжить";
            cont.UseVisualStyleBackColor = false;
            cont.Click += cont_Click_1;
            // 
            // FormPrologue01
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1001, 563);
            Controls.Add(cont);
            Controls.Add(text01);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(pictureBox1);
            Name = "FormPrologue01";
            Text = "FormPrologue01";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnSave;
        private Button btnLoad;
        private PictureBox pictureBox1;
        private RichTextBox text01;
        private Button cont;
    }
}