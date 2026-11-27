namespace thecicadassong
{
    partial class FormPrologue01
    {
        private System.ComponentModel.IContainer components = null;

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
            pictureBox1 = new PictureBox();
            dialog = new DialogBox();
            btnSave = new HudButton();
            btnLoad = new HudButton();
            btnSettings = new HudButton();
            btnMenu = new HudButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pictureBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Controls.Add(btnMenu);
            pictureBox1.Controls.Add(btnSettings);
            pictureBox1.Controls.Add(btnLoad);
            pictureBox1.Controls.Add(btnSave);
            pictureBox1.Controls.Add(dialog);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1001, 563);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += dialog_Click;
            // 
            // dialog
            // 
            dialog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dialog.Font = new Font("Georgia", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dialog.Location = new Point(0, 355);
            dialog.Name = "dialog";
            dialog.Size = new Size(1001, 190);
            dialog.TabIndex = 0;
            dialog.Click += dialog_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Font = new Font("Georgia", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSave.Location = new Point(489, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoad.Font = new Font("Georgia", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLoad.Location = new Point(617, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(120, 36);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Загрузить";
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSettings.Font = new Font("Georgia", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSettings.Location = new Point(745, 12);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(120, 36);
            btnSettings.TabIndex = 3;
            btnSettings.Text = "Настройки";
            btnSettings.Click += btnSettings_Click;
            // 
            // btnMenu
            // 
            btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMenu.Font = new Font("Georgia", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnMenu.Location = new Point(873, 12);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(120, 36);
            btnMenu.TabIndex = 4;
            btnMenu.Text = "В меню";
            btnMenu.Click += btnMenu_Click;
            // 
            // FormPrologue01
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1001, 563);
            Controls.Add(pictureBox1);
            Name = "FormPrologue01";
            Text = "Песнь цикад";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pictureBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private DialogBox dialog;
        private HudButton btnSave;
        private HudButton btnLoad;
        private HudButton btnSettings;
        private HudButton btnMenu;
    }
}