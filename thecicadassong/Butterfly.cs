using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace thecicadassong
{
    public class ButterflyButton : Control
    {
        private readonly Bitmap _img;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 33 };
        private float _t;
        private bool _hover;

        public ButterflyButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;

            _img = CropToContent(Properties.Resources.butterfly);

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _timer.Tick += (s, e) => { _t += 0.08f; Invalidate(); };
                _timer.Start();
            }
        }

        private static Bitmap CropToContent(Bitmap src)
        {
            var bmp = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
                g.DrawImage(src, 0, 0, src.Width, src.Height);

            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int stride = data.Stride;
            var bytes = new byte[stride * bmp.Height];
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            bmp.UnlockBits(data);

            int minX = int.MaxValue, minY = int.MaxValue, maxX = -1, maxY = -1;
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    if (bytes[y * stride + x * 4 + 3] > 16)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }

            if (maxX < minX) return bmp;
            return bmp.Clone(new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1),
                PixelFormat.Format32bppArgb);
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; base.OnMouseLeave(e); }

        private void DrawAlpha(Graphics g, RectangleF r, float alpha)
        {
            var m = new ColorMatrix { Matrix33 = alpha };
            using var ia = new ImageAttributes();
            ia.SetColorMatrix(m);
            g.DrawImage(_img, Rectangle.Round(r), 0, 0, _img.Width, _img.Height, GraphicsUnit.Pixel, ia);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            float scale = Math.Min((Width - 24f) / _img.Width, (Height - 24f) / _img.Height);
            float k = _hover ? 1.08f : 1f;
            float w = _img.Width * scale * k, h = _img.Height * scale * k;

            g.TranslateTransform(Width / 2f, Height / 2f + (float)Math.Sin(_t) * 4f);
            g.RotateTransform((float)Math.Sin(_t * 0.7f) * 4f);

            var rect = new RectangleF(-w / 2, -h / 2, w, h);

            if (_hover)
                for (int i = 4; i >= 1; i--)
                    DrawAlpha(g, RectangleF.Inflate(rect, i * 5, i * 5), 0.06f);

            DrawAlpha(g, rect, _hover ? 1f : 0.85f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { _timer.Dispose(); _img.Dispose(); }
            base.Dispose(disposing);
        }
    }

    public class EasterEggForm : Form
    {
        public EasterEggForm()
        {
            var gif = Properties.Resources.erika;

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            Padding = new Padding(3);
            ClientSize = new Size(gif.Width + 6, gif.Height + 6);
            KeyPreview = true;

            var pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = gif,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            pic.Click += (s, e) => Close();
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            Controls.Add(pic);
        }
    }
}