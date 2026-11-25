using NAudio.Wave;

namespace thecicadassong
{
    //Зацикливание звука
    public class LoopStream : WaveStream
    {
        private readonly WaveStream _source;
        public LoopStream(WaveStream source) { _source = source; }
        public override WaveFormat WaveFormat => _source.WaveFormat;
        public override long Length => _source.Length;
        public override long Position
        {
            get => _source.Position;
            set => _source.Position = value;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            int total = 0;
            while (total < count)
            {
                int read = _source.Read(buffer, offset + total, count - total);
                if (read == 0)
                {
                    if (_source.Position == 0) break;
                    _source.Position = 0;
                }
                total += read;
            }
            return total;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) _source.Dispose();
            base.Dispose(disposing);
        }
    }

    //Общий менеджер музыки для всей игры
    public static class MusicManager
    {
        private static WaveOutEvent? _output;
        private static int _volume = 70;
        private static bool _muted = false;

        public static int Volume
        {
            get => _volume;
            set { _volume = Math.Clamp(value, 0, 100); Apply(); }
        }

        public static bool Muted
        {
            get => _muted;
            set { _muted = value; Apply(); }
        }

        public static void Play()
        {
            if (_output != null) return;   //уже играет
            try
            {
                var reader = new WaveFileReader(Properties.Resources.menu);
                _output = new WaveOutEvent();
                _output.Init(new LoopStream(reader));
                Apply();
                _output.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось запустить музыку:\n" + ex.Message,
                    "Песнь цикад", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void Stop()
        {
            _output?.Stop();
            _output?.Dispose();
            _output = null;
        }

        private static void Apply()
        {
            if (_output != null)
                _output.Volume = _muted ? 0f : _volume / 100f;
        }
    }
}