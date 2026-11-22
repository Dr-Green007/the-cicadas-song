using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace thecicadassong
{
    public partial class FormPrologue01 : Form
    {
        private GameState _state = new();

        public FormPrologue01()
        {
            InitializeComponent();
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
                "Загрузить игру",
                "1");

            if (string.IsNullOrWhiteSpace(input)) return;

            input = input.Trim();

            // определить, что ввели: номер или имя
            string chosen;
            if (int.TryParse(input, out var num) && num >= 1 && num <= saves.Count)
            {
                chosen = saves[num - 1];
            }
            else if (saves.Contains(input))
            {
                chosen = input;
            }
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

            MessageBox.Show(
                $"Загружено «{chosen}».\n" +
                $"Сцена: {_state.CurrentSceneId}\n" +
                $"Воля: {_state.Will}  Вера: {_state.Faith}  Теории: {_state.Theories}\n" +
                $"Сохранено: {_state.SavedAt}",
                "Песнь цикад", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}