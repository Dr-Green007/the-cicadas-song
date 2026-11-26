using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace thecicadassong
{
    //Кнопка меню прозрачная
    public class MenuButton : Control
    {
        private bool _hover;

        public MenuButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var gold = Color.FromArgb(255, 214, 120);
            var textColor = _hover ? gold : Color.FromArgb(235, 235, 235);
            float x = _hover ? 26 : 12;

            using var fmt = new StringFormat { LineAlignment = StringAlignment.Center };
            using var shadow = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
            using var brush = new SolidBrush(textColor);

            g.DrawString(Text, Font, shadow, new RectangleF(x + 2, 2, Width - x, Height), fmt);
            g.DrawString(Text, Font, brush, new RectangleF(x, 0, Width - x, Height), fmt);

            if (_hover)
            {
                float cy = Height / 2f;
                using var diamond = new SolidBrush(gold);
                g.FillPolygon(diamond, new[]
                {
                    new PointF(6, cy), new PointF(12, cy - 6),
                    new PointF(18, cy), new PointF(12, cy + 6)
                });
                using var pen = new Pen(Color.FromArgb(120, gold), 1);
                g.DrawLine(pen, 10, Height - 3, Width - 10, Height - 3);
            }
        }
    }

    //Логотип Umineko When They Cry / Answer Arcs
    public class TitleLogo : Control
    {
        private static readonly StringFormat Sf = CreateFormat();

        private static StringFormat CreateFormat()
        {
            var f = (StringFormat)StringFormat.GenericTypographic.Clone();
            f.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            return f;
        }

        public TitleLogo()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
        }

        private static SizeF Measure(Graphics g, string t, Font f) => g.MeasureString(t, f, 10000, Sf);

        private static float FitSize(Graphics g, string text, string family, FontStyle style, float targetWidth)
        {
            using var f = new Font(family, 20f, style);
            return 20f * targetWidth / Measure(g, text, f).Width;
        }

        private static void DrawRuns(Graphics g, Font font, float y, Color outline, float outlineW,
                                     params (string Text, float X, Color Fill)[] runs)
        {
            float em = font.SizeInPoints * g.DpiY / 72f;
            var paths = new List<GraphicsPath>();
            foreach (var r in runs)
            {
                var p = new GraphicsPath();
                p.AddString(r.Text, font.FontFamily, (int)font.Style, em, new PointF(r.X, y), Sf);
                paths.Add(p);
            }
            using (var pen = new Pen(outline, outlineW) { LineJoin = LineJoin.Round })
                foreach (var p in paths) g.DrawPath(pen, p);
            for (int i = 0; i < paths.Count; i++)
            {
                using var b = new SolidBrush(runs[i].Fill);
                g.FillPath(b, paths[i]);
                paths[i].Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var red = Color.FromArgb(225, 20, 20);
            float pad = 8;
            const string a = "Umineko The Cicadas ", b = "S", c = "ong";
            var style = FontStyle.Bold | FontStyle.Italic;
            float size1 = FitSize(g, a + b + c, "Arial", style, Width - pad * 2);
            using var f1 = new Font("Arial", size1, style);
            float em1 = size1 * g.DpiY / 72f;
            float wA = Measure(g, a, f1).Width;
            float wB = Measure(g, b, f1).Width;
            float titleH = Measure(g, a, f1).Height;
            DrawRuns(g, f1, 0, Color.Black, em1 * 0.2f,
                (a, pad, Color.White), (b, pad + wA, red), (c, pad + wA + wB, Color.White));

            string aa = "Question Arc";
            float sizeAA = FitSize(g, aa, "Arial", style, Width * 0.40f);
            using var f3 = new Font("Arial", sizeAA, style);
            float emAA = sizeAA * g.DpiY / 72f;
            float wAA = Measure(g, aa, f3).Width;
            DrawRuns(g, f3, titleH * 0.80f, Color.White, emAA * 0.14f, (aa, Width - wAA - pad - 4, red));
        }
    }
}