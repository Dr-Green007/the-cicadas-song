using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace thecicadassong
{
    public partial class testdb : Form
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly System.Windows.Forms.Timer _timer;
        private readonly Label _lblTimer;
        private bool _saved;

        // сначала подключаемся к master, чтобы создать базу, если её нет
        private const string MasterConnString =
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

        private const string ConnString =
            @"Server=(localdb)\MSSQLLocalDB;Database=sessions_db;Trusted_Connection=True;TrustServerCertificate=True;";

        public testdb()
        {
            InitializeComponent();

            // метка со временем сеанса
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

            label1.Click += SaveGame_Click;   // "сохранить бд"
            label2.Click += LoadGame_Click;   // "загрузить бд"
        }

        private void testdb_Load(object sender, EventArgs e)
        {
            _stopwatch.Restart();
            _timer.Start();
        }

        // нажали "сохранить бд" (я ебал этот адо нет, только пётру это не показывайте)
        private void SaveGame_Click(object sender, EventArgs e)
        {
            if (_saved) return;

            // останавливаем время
            _stopwatch.Stop();
            _timer.Stop();
            var elapsed = _stopwatch.Elapsed;
            _lblTimer.Text = elapsed.ToString(@"hh\:mm\:ss");

            // спрашиваем имя
            string name = Interaction.InputBox("Введите ваше имя:", "Сохранение игры");

            // Отмена или пустое имя: продолжаем отсчёт, ничего не сохраняем
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Имя не введено, игра продолжается.", "Сохранение отменено",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _stopwatch.Start();
                _timer.Start();
                return;
            }

            // сохраняем в БД
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
                // Время остановлено, можно нажать «сохранить бд» ещё раз
            }
        }
        // Нажали "загрузить бд": показываем последние сохранения
        private void LoadGame_Click(object sender, EventArgs e)
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
SELECT TOP 10 id, name, duration_seconds, created_at
FROM dbo.sessions
ORDER BY created_at DESC";

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

                MessageBox.Show(sb.ToString(), "Последние сохранения (10)",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось загрузить данные:\n" + ex.Message,
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Создаёт базу sessions_db в LocalDB, если её ещё нет
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

        private void SaveSession(string name, TimeSpan elapsed)
        {
            EnsureDatabase();

            using var conn = new SqlConnection(ConnString);
            conn.Open();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
CREATE TABLE dbo.sessions (
    id               INT IDENTITY(1,1) PRIMARY KEY,
    name             NVARCHAR(100) NOT NULL,
    duration_seconds INT           NOT NULL,
    created_at       DATETIME2     NOT NULL DEFAULT SYSDATETIME()
)";
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex) when (ex.Number == 2714)
                {
                    // 2714 = "объект уже существует": таблица есть, идём дальше
                }
            }

            using var insert = conn.CreateCommand();
            insert.CommandText =
                "INSERT INTO dbo.sessions (name, duration_seconds) VALUES (@name, @dur)";
            insert.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
            insert.Parameters.Add("@dur", SqlDbType.Int).Value = (int)elapsed.TotalSeconds;
            insert.ExecuteNonQuery();
        }
    }
}