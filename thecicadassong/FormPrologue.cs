using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thecicadassong
{
    public partial class FormPrologue : Form
    {
        private int brightness = 0;      // начинаем с чёрного (невидимый текст)
        private bool fadingIn = true;    // сначала появление
        private const int Step = 3;      // скорость меньше шаг = медленнее

        public FormPrologue()
        {
            InitializeComponent();
            prologue_text.ForeColor = Color.Black;   // текст невидим с самого начала
        }

        private void FormPrologue_Load(object sender, EventArgs e)
        {
            timer1.Start();   // запускаем таймер для появление анимаций
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fadingIn) // если фолс, то не робит
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
                if (brightness <= 0) // если меньше 0, то затухание происходит
                {
                    brightness = 0;
                    timer1.Stop();
                    FormPrologue01 fpro01 = new FormPrologue01();
                    this.Hide();
                    fpro01.Show();
                }
            }

            prologue_text.ForeColor = Color.FromArgb(brightness, brightness, brightness); // задаём параметры
        }

        private async Task HoldThenFadeOut()
        {
            await Task.Delay(7000);   // текст держится 7 секунд
            timer1.Start();           // запускаем затухание
        }
    }
}