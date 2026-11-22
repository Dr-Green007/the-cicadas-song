namespace thecicadassong
{
    public partial class Form1 : Form
    {
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
    }
}
