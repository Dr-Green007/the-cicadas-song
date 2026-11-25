namespace thecicadassong
{
    public class SettingsForm : Form
    {
        public SettingsForm()
        {
            Text = "Настройки";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(320, 170);

            var lbl = new Label
            {
                Text = "Громкость музыки",
                AutoSize = true,
                Location = new Point(20, 20)
            };
            var valueLbl = new Label
            {
                Text = MusicManager.Volume + "%",
                AutoSize = true,
                Location = new Point(260, 20)
            };
            var bar = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                TickFrequency = 10,
                Value = MusicManager.Volume,
                Location = new Point(12, 45),
                Width = 295
            };
            var mute = new CheckBox
            {
                Text = "Выключить музыку",
                AutoSize = true,
                Checked = MusicManager.Muted,
                Location = new Point(20, 95)
            };
            var close = new Button
            {
                Text = "Закрыть",
                DialogResult = DialogResult.OK,
                Location = new Point(210, 125),
                Width = 90
            };

            bar.ValueChanged += (s, e) =>
            {
                MusicManager.Volume = bar.Value;
                valueLbl.Text = bar.Value + "%";
            };
            mute.CheckedChanged += (s, e) => MusicManager.Muted = mute.Checked;

            Controls.AddRange(new Control[] { lbl, valueLbl, bar, mute, close });
            AcceptButton = close;
        }
    }
}