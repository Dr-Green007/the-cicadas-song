using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace thecicadassong
{
    public partial class FormPrologue01 : Form
    {
        private GameState _state;

        //реплики по порядку: (кто говорит, текст). Пустое имя = рассказчик
        private readonly (string Speaker, string Text)[] _pages =
        {
            ("", "Октябрь 1986 года.\nОстров Роккэндзима.\nВдали от материка, на скале над морем, стоит поместье рода Усиромия.\nВ этом году тайфун придёт раньше, чем семья успеет разъехаться."),
            ("", "Ты узнаешь об этом позже. А пока ты просто плывёшь домой, в место, которого не видел шесть лет.")
        };
        private int _pageIndex = 0;

        public FormPrologue01() : this(new GameState()) { }

        public FormPrologue01(GameState state)
        {
            InitializeComponent();
            _state = state ?? new GameState();

            DoubleBuffered = true;
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                {
                    Advance();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            ShowPage();
        }

        private void ShowPage()
        {
            var p = _pages[_pageIndex];
            dialog.SetMessage(p.Speaker, p.Text);
        }

        // ДАЛЬШЕ (клик, пробел, Enter)
        private void Advance()
        {
            if (dialog.IsTyping)
            {
                dialog.Complete();   // первый клик допечатывает текст
                return;
            }

            _pageIndex++;
            if (_pageIndex < _pages.Length)
            {
                ShowPage();
            }
            else
            {
                _pageIndex = _pages.Length - 1;
                // здесь будет код перехода на следующую форму (главу)
            }
        }

        private void dialog_Click(object? sender, EventArgs e) => Advance();

        //НАСТРОЙКИ
        private void btnSettings_Click(object? sender, EventArgs e)
        {
            using var f = new SettingsForm();
            f.ShowDialog(this);
        }

        //В МЕНЮ
        private void btnMenu_Click(object? sender, EventArgs e)
        {
            var ok = MessageBox.Show(
                "Выйти в главное меню? Несохранённый прогресс будет потерян.",
                "Песнь цикад", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok != DialogResult.Yes) return;

            var menu = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (menu == null || menu.IsDisposed) menu = new Form1();
            menu.Show();   // Form1 сама включит музыку при показе
            Close();
        }

        //СОХРАНИТЬ
        private void btnSave_Click(object? sender, EventArgs e)
        {
            var name = Interaction.InputBox(
                "Имя сохранения (1–32 символа):",
                "Сохранить игру",
                _state.PlayerName);

            if (string.IsNullOrWhiteSpace(name)) return;

            name = name.Trim();
            if (name.Length > 32)
            {
                MessageBox.Show("Имя: 1–32 символа.", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Имя содержит недопустимые символы.", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _state.PlayerName = name;

            if (SaveManager.Exists(name))
            {
                var ok = MessageBox.Show(
                    $"Сохранение «{name}» уже есть. Перезаписать?",
                    "Песнь цикад", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ok != DialogResult.Yes) return;
            }

            if (SaveManager.Save(_state, out var err))
                MessageBox.Show("Игра сохранена.", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"Не удалось сохранить:\n{err}", "Песнь цикад",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //ЗАГРУЗИТЬ
        private void btnLoad_Click(object? sender, EventArgs e)
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

            _state = data;

            // переход
            Transition.Open(_state.CurrentSceneId, _state, this);
        }
    }
}