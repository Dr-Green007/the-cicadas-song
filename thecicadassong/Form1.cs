using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Text;
using static System.Windows.Forms.AxHost;

namespace thecicadassong
{
    public partial class Form1 : Form
    {
        private const string MasterConnString =
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

        private const string ConnString =
            @"Server=(localdb)\MSSQLLocalDB;Database=sessions_db;Trusted_Connection=True;TrustServerCertificate=True;";
        private GameState _state = new();
        //Паслхалка для Алексея Фомина
        private void butterfly_Click(object? sender, EventArgs e)
        {
            using var f = new EasterEggForm();
            f.ShowDialog(this);
        }
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            BackgroundImage = Properties.Resources.menu_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            // а тут ничего нету (дополнение от лёхи: а тут что-то нахуй надо?)
            // вот это надо
        }

        //Музыка включается, когда форма видна, и выключается, когда скрыта
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible) MusicManager.Play();
            else MusicManager.Stop();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            MusicManager.Stop();
            base.OnFormClosing(e);
        }

        private void settings_Click(object? sender, EventArgs e)
        {
            using var f = new SettingsForm();
            f.ShowDialog(this);
        }

        private void newgame_Click(object? sender, EventArgs e)
        {
            var state = new GameState();
            Transition.Open("splash", state, this);   //заставка
        }

        //ЗАГРУЗИТЬ ИГРУ
        private void loadgame_Click(object? sender, EventArgs e)
        {
            var saves = SaveManager.ListSaves();

            if (saves.Count == 0)
            {
                MessageBox.Show("Нет сохранений.", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var lines = new List<string>();
            for (int i = 0; i < saves.Count; i++)
                lines.Add($"{i + 1}. {saves[i]}");
            var list = string.Join("\n", lines);

            var input = Interaction.InputBox(
                $"Выберите сохранение (номер или имя):\n\n{list}",
                "Загрузить игру", "1");

            if (string.IsNullOrWhiteSpace(input)) return;
            input = input.Trim();

            string chosen;
            if (int.TryParse(input, out var num) && num >= 1 && num <= saves.Count)
                chosen = saves[num - 1];
            else if (saves.Contains(input))
                chosen = input;
            else
            {
                MessageBox.Show("Такого сохранения нет.", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = SaveManager.Load(chosen, out var err);
            if (data is null)
            {
                MessageBox.Show($"Не удалось загрузить:\n{err}", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //переход
            Transition.Open(data.CurrentSceneId, data, this);
        }

        //ВЫЙТИ
        private void quit_Click(object sender, EventArgs e)
        {
            MusicManager.Stop();
            Application.Exit();
        }


        private void db_test_Click(object sender, EventArgs e)
        {
            testdb switches = new testdb();
            this.Hide();
            switches.Show();
        }
        private static void EnsureDatabase()
        {
            using var conn = new SqlConnection(MasterConnString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "CREATE DATABASE sessions_db";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) when (ex.Number == 1801)
            {
                // 1801 = "Database already exists": база есть, ничего делать не нужно
            }
        }
        private void leadership_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureDatabase();

                using var conn = new SqlConnection(ConnString);
                conn.Open();

                // Если таблицы ещё нет, значит, сохранений не было
                using (var check = conn.CreateCommand())
                {
                    check.CommandText = "SELECT OBJECT_ID('dbo.sessions', 'U')";
                    if (check.ExecuteScalar() is DBNull or null)
                    {
                        MessageBox.Show("Сохранений пока нет.", "Загрузка",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT TOP 10 name, duration_seconds, created_at
FROM dbo.sessions
ORDER BY duration_seconds ASC";

                var sb = new StringBuilder();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        string name = reader.GetString(0);
                        var duration = TimeSpan.FromSeconds(reader.GetInt32(1));
                        DateTime created = reader.GetDateTime(2);

                        sb.AppendLine(
                            $"#{name}  |  {duration:hh\\:mm\\:ss}  |  {created:dd.MM.yyyy HH:mm}");
                    }
                }
                if (sb.Length == 0)
                {
                    MessageBox.Show("Сохранений пока нет.", "Загрузка",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show(sb.ToString(), "Последние сохранения (10)",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось загрузить данные:\n" + ex.Message,
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}