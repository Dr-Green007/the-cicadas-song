using Microsoft.VisualBasic;
using static System.Windows.Forms.AxHost;

namespace thecicadassong
{
    public partial class Form1 : Form
    {
        private GameState _state = new();
        public Form1()
        {
            InitializeComponent();
            // а тут ничего нету (дополнение от лёхи: а тут что-то нахуй надо?)
        }

        private void quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newgame_Click(object sender, EventArgs e)
        {
            FormPrologue fpro = new FormPrologue(); // этот проклятый код будет состоятся на 96 процентов... худший кошмар
            this.Hide();
            fpro.Show();
        }

        private void loadgame_Click(object sender, EventArgs e)
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
