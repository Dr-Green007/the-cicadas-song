using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.ComponentModel;

namespace thecicadassong
{
    // Диалоговое окно как в Umineko
    public class DialogBox : Control
    {
        private const int PadX = 60;
        private static readonly StringFormat Sf = StringFormat.GenericTypographic;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 28 };
        private List<string> _lines = new();
        private string _message = "";
        private int _total, _shown, _ticks;
        private bool _blinkOn = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Speaker { get; set; } = "";
        public bool IsTyping => _shown < _total;

        public DialogBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _timer.Tick += (s, e) =>
                {
                    if (IsTyping) { _shown++; Invalidate(); }
                    else if (++_ticks >= 18) { _ticks = 0; _blinkOn = !_blinkOn; Invalidate(); }
                };
                _timer.Start();
            }
        }

        public void SetMessage(string speaker, string text)
        {
            Speaker = speaker ?? "";
            _message = text ?? "";
            _shown = 0;
            _ticks = 0;
            _blinkOn = true;
            Rewrap();
            Invalidate();
        }

        public void Complete() { _shown = _total; Invalidate(); }

        private void Rewrap()
        {
            _lines = Wrap(_message, Width - 2 * PadX);
            _total = _lines.Sum(l => l.Length);
            _shown = Math.Min(_shown, _total);
        }

        protected override void OnResize(EventArgs e) { base.OnResize(e); Rewrap(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); Rewrap(); }

        // Заранее режем текст на строки, чтобы слова не перепрыгивали при печати
        private List<string> Wrap(string text, float maxWidth)
        {
            var result = new List<string>();
            using var g = Graphics.FromHwnd(IntPtr.Zero);
            foreach (var para in text.Replace("\r", "").Split('\n'))
            {
                string line = "";
                foreach (var word in para.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    string test = line.Length == 0 ? word : line + " " + word;
                    if (line.Length > 0 && g.MeasureString(test, Font, int.MaxValue, Sf).Width > maxWidth)
                    {
                        result.Add(line);
                        line = word;
                    }
                    else line = test;
                }
                result.Add(line);
            }
            return result;
        }

        private static void DrawText(Graphics g, string s, Font f, float x, float y)
        {
            using var shadow = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
            g.DrawString(s, f, shadow, x + 2, y + 2, Sf);
            g.DrawString(s, f, Brushes.White, x, y, Sf);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width < 2 || Height < 2) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            // Полупрозрачная тёмно-красная плашка
            var rect = new Rectangle(0, 0, Width, Height);
            using (var bg = new LinearGradientBrush(rect,
                Color.FromArgb(205, 90, 28, 48), Color.FromArgb(220, 42, 12, 28), 90f))
                g.FillRectangle(bg, rect);

            // Линии сверху и снизу
            using (var thick = new Pen(Color.FromArgb(220, 255, 228, 228), 2f))
            using (var thin = new Pen(Color.FromArgb(90, 255, 228, 228), 1f))
            {
                g.DrawLine(thick, 0, 2, Width, 2);
                g.DrawLine(thick, 0, Height - 3, Width, Height - 3);
                g.DrawLine(thin, 0, 8, Width, 8);
                g.DrawLine(thin, 0, Height - 9, Width, Height - 9);
            }

            float y = 22;
            if (Speaker.Length > 0)
            {
                using var nameFont = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Regular);
                DrawText(g, Speaker, nameFont, PadX - 10, y);
                y += nameFont.GetHeight(g) + 12;
            }

            float lh = Font.GetHeight(g) * 1.2f;
            int left = _shown;
            float lastX = PadX, lastY = y;
            foreach (var l in _lines)
            {
                if (left <= 0) break;
                string part = l.Length <= left ? l : l.Substring(0, left);
                left -= part.Length;
                DrawText(g, part, Font, PadX, y);
                lastX = PadX + g.MeasureString(part, Font, int.MaxValue, Sf).Width;
                lastY = y;
                y += lh;
            }

            // Мигающий треугольник ▼, когда текст допечатан
            if (!IsTyping && _blinkOn && _total > 0)
            {
                float cy = lastY + lh / 2f - 2;
                g.FillPolygon(Brushes.White, new[]
                {
                    new PointF(lastX + 8, cy - 4),
                    new PointF(lastX + 20, cy - 4),
                    new PointF(lastX + 14, cy + 6)
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _timer.Dispose();
            base.Dispose(disposing);
        }
    }

    // Кнопка верхней панели: скруглённая, полупрозрачная, золото при наведении
    public class HudButton : Control
    {
        private bool _hover;

        public HudButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            TabStop = false;
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }

        private static GraphicsPath RoundRect(Rectangle r, int d)
        {
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width < 4 || Height < 4) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var gold = Color.FromArgb(255, 214, 120);
            using var path = RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 10);
            using var fill = new SolidBrush(_hover ? Color.FromArgb(215, 85, 30, 50) : Color.FromArgb(165, 25, 10, 22));
            using var pen = new Pen(_hover ? gold : Color.FromArgb(150, 255, 228, 200), 1.5f);
            g.FillPath(fill, path);
            g.DrawPath(pen, path);

            using var fmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using var txt = new SolidBrush(_hover ? gold : Color.White);
            g.DrawString(Text, Font, txt, new RectangleF(0, 0, Width, Height), fmt);
        }
    }
}