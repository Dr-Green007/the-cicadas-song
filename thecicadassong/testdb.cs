using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace thecicadassong
{
    public partial class testdb : Form
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly System.Windows.Forms.Timer _timer;
        private readonly Label _lblTimer;
        private bool _saved;

        // бедный мой домашний пк будет хостится(((
        private const string ConnString =
    "Server=153.52.95.81,1433;Database=sessions_db;" +
    "User ID=game_user;Password=1234;" +
    "Encrypt=True;TrustServerCertificate=True;Connection Timeout=15;";

        public testdb()
        {
            InitializeComponent();

            _lblTimer = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                ForeColor = SystemColors.ButtonFace,
                Location = new Point(20, 20),
                Text = "00:00:00"
            };
            Controls.Add(_lblTimer);

            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += (s, e) =>
                _lblTimer.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");

            // Load уже подписан в testdb.Designer.cs, здесь его НЕ подписываем
            label1.Click += SaveGame_Click;   // "сохранить бд"
            label2.Click += LoadGame_Click;   // "загрузить бд"
        }

        private void testdb_Load(object sender, EventArgs e)
        {
            _stopwatch.Restart();
            _timer.Start();
        }

        // Нажали "сохранить бд"
        private void SaveGame_Click(object sender, EventArgs e)
        {
            if (_saved) return;

            _stopwatch.Stop();
            _timer.Stop();
            var elapsed = _stopwatch.Elapsed;
            _lblTimer.Text = elapsed.ToString(@"hh\:mm\:ss");

            string name = Interaction.InputBox("Введите ваше имя:", "Сохранение игры");

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Имя не введено, игра продолжается.", "Сохранение отменено",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _stopwatch.Start();
                _timer.Start();
                return;
            }

            try
            {
                SaveSession(name.Trim(), elapsed);
                _saved = true;

                MessageBox.Show(
                    $"Игра сохранена!\nИгрок: {name.Trim()}\nВремя: {elapsed:hh\\:mm\\:ss}",
                    "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось сохранить сессию:\n" + ex.Message,
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Нажали "загрузить бд"
        private void LoadGame_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(ConnString);
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT TOP 10 id, name, duration_seconds, created_at
FROM dbo.sessions
ORDER BY duration_seconds ASC, id ASC";

                var sb = new StringBuilder();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        var duration = TimeSpan.FromSeconds(reader.GetInt32(2));
                        DateTime created = reader.GetDateTime(3);

                        sb.AppendLine(
                            $"#{id}  {name}  |  {duration:hh\\:mm\\:ss}  |  {created:dd.MM.yyyy HH:mm}");
                    }
                }

                if (sb.Length == 0)
                {
                    MessageBox.Show("Сохранений пока нет.", "Загрузка",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show(sb.ToString(), "Лучшие результаты (10)",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось загрузить данные:\n" + ex.Message,
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSession(string name, TimeSpan elapsed)
        {
            using var conn = new SqlConnection(ConnString);
            conn.Open();

            using var insert = conn.CreateCommand();
            insert.CommandText =
                "INSERT INTO dbo.sessions (name, duration_seconds) VALUES (@name, @dur)";
            insert.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
            insert.Parameters.Add("@dur", SqlDbType.Int).Value = (int)elapsed.TotalSeconds;
            insert.ExecuteNonQuery();
        }
    }
}
