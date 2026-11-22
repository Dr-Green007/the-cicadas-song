using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thecicadassong
{
    public partial class FormPrologue : Form
    {
        private GameState _state;
        private int brightness = 0;      // начинаем с чёрного (невидимый текст)
        private bool fadingIn = true;    // сначала появление
        private const int Step = 3;      // скорость: меньше шаг = медленнее

        public FormPrologue() : this(new GameState()) { }

        public FormPrologue(GameState state)
        {
            InitializeComponent();
            _state = state ?? new GameState();
            prologue_text.ForeColor = Color.Black;   // текст невидим с самого начала
        }

        private void FormPrologue_Load(object sender, EventArgs e)
        {
            timer1.Start();   // запускаем таймер для анимации
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fadingIn)
            {
                brightness += Step;
                if (brightness >= 255)
                {
                    brightness = 255;
                    fadingIn = false;
                    timer1.Stop();
                    _ = HoldThenFadeOut();   // пауза, потом затухание
                }
            }
            else
            {
                brightness -= Step;
                if (brightness <= 0)
                {
                    brightness = 0;
                    timer1.Stop();

                    // переход
                    Transition.Open("prologue", _state, this);
                    return;
                }
            }

            prologue_text.ForeColor = Color.FromArgb(brightness, brightness, brightness);
        }

        private async Task HoldThenFadeOut()
        {
            await Task.Delay(4000);   // текст держится 4 секунды
            timer1.Start();           // запускаем затухание
        }
    }
}