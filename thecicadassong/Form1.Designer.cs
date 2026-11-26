namespace thecicadassong
{
    partial class Form1
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
            butterfly = new ButterflyButton();
            logo = new TitleLogo();
            newgame = new MenuButton();
            loadgame = new MenuButton();
            leadership = new MenuButton();
            developers = new MenuButton();
            settings = new MenuButton();
            quit = new MenuButton();
            db_test = new MenuButton();
            SuspendLayout();
            // 
            // butterfly
            // 
            butterfly.BackColor = Color.Transparent;
            butterfly.Location = new Point(900, 25);
            butterfly.Name = "butterfly";
            butterfly.Size = new Size(110, 110);
            butterfly.TabIndex = 8;
            butterfly.Click += butterfly_Click;
            // 
            // logo
            // 
            logo.BackColor = Color.Transparent;
            logo.Location = new Point(700, 140);
            logo.Name = "logo";
            logo.Size = new Size(480, 90);
            logo.TabIndex = 7;
            // 
            // newgame
            // 
            newgame.BackColor = Color.Transparent;
            newgame.Font = new Font("Georgia", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            newgame.Location = new Point(760, 260);
            newgame.Name = "newgame";
            newgame.Size = new Size(340, 38);
            newgame.TabIndex = 0;
            newgame.Text = "Новая игра";
            newgame.Click += newgame_Click;
            // 
            // loadgame
            // 
            loadgame.BackColor = Color.Transparent;
            loadgame.Font = new Font("Georgia", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            loadgame.Location = new Point(760, 300);
            loadgame.Name = "loadgame";
            loadgame.Size = new Size(340, 38);
            loadgame.TabIndex = 1;
            loadgame.Text = "Загрузить игру";
            loadgame.Click += loadgame_Click;
            // 
            // leadership
            // 
            leadership.BackColor = Color.Transparent;
            leadership.Font = new Font("Georgia", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            leadership.Location = new Point(760, 340);
            leadership.Name = "leadership";
            leadership.Size = new Size(340, 38);
            leadership.TabIndex = 2;
            leadership.Text = "Таблица лидеров";
            leadership.Click += leadership_Click;
            // 
            // developers
            // 
            developers.BackColor = Color.Transparent;
            developers.Font = new Font("Georgia", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            developers.Location = new Point(760, 380);
            developers.Name = "developers";
            developers.Size = new Size(340, 38);
            developers.TabIndex = 3;
            developers.Text = "О разработчиках";
            // 
            // settings
            // 
            settings.BackColor = Color.Transparent;
            settings.Font = new Font("Georgia", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            settings.Location = new Point(760, 420);
            settings.Name = "settings";
            settings.Size = new Size(340, 38);
            settings.TabIndex = 6;
            settings.Text = "Настройки";
            settings.Click += settings_Click;
            // 
            // quit
            // 
            quit.BackColor = Color.Transparent;
            quit.Font = new Font("Georgia", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            quit.Location = new Point(760, 460);
            quit.Name = "quit";
            quit.Size = new Size(340, 38);
            quit.TabIndex = 4;
            quit.Text = "Выйти";
            quit.Click += quit_Click;
            // 
            // db_test
            // 
            db_test.BackColor = Color.Transparent;
            db_test.Font = new Font("Georgia", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            db_test.Location = new Point(10, 10);
            db_test.Name = "db_test";
            db_test.Size = new Size(260, 30);
            db_test.TabIndex = 5;
            db_test.Text = "кнопка для проверки бд";
            db_test.Click += db_test_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1196, 532);
            Controls.Add(db_test);
            Controls.Add(quit);
            Controls.Add(settings);
            Controls.Add(developers);
            Controls.Add(leadership);
            Controls.Add(loadgame);
            Controls.Add(newgame);
            Controls.Add(butterfly);
            Controls.Add(logo);
            Name = "Form1";
            Text = "Песнь цикад";
            ResumeLayout(false);
        }

        #endregion

        private ButterflyButton butterfly;
        private TitleLogo logo;
        private MenuButton newgame;
        private MenuButton loadgame;
        private MenuButton leadership;
        private MenuButton developers;
        private MenuButton settings;
        private MenuButton quit;
        private MenuButton db_test;
    }
}