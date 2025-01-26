namespace EasyModern.Core.Effects
{
    using EasyModern.Core.Capture;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;

    public class TextureEffectManager : IDisposable
    {
        private readonly object _lock = new object();
        private SharpDX.Direct3D9.Texture _currentTexture = null;
        private Thread _processingThread;
        private bool _isRunning = false;

        private Func<Bitmap, Bitmap> _effectFunction;

        // Variables para el cálculo de FPS
        private int _framesProcessed = 0;
        private Stopwatch _fpsTimer = new Stopwatch();

        /// <summary>
        /// Obtiene la textura actual procesada con el efecto.
        /// </summary>
        public SharpDX.Direct3D9.Texture Texture
        {
            get
            {
                lock (_lock)
                {
                    return _currentTexture;
                }
            }
        }

        /// <summary>
        /// Obtiene los FPS actuales del efecto.
        /// </summary>
        public float FPS
        {
            get
            {
                if (_fpsTimer.ElapsedMilliseconds == 0) return 0;
                return _framesProcessed / (_fpsTimer.ElapsedMilliseconds / 1000.0f);
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase TextureEffectManager.
        /// </summary>
        /// <param name="effectFunction">Función que aplica el efecto a una imagen Bitmap.</param>
        public TextureEffectManager(Func<Bitmap, Bitmap> effectFunction)
        {
            _effectFunction = effectFunction ?? throw new ArgumentNullException(nameof(effectFunction));
        }

        /// <summary>
        /// Inicia el procesamiento de efectos.
        /// </summary>
        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            _fpsTimer.Start();
            _processingThread = new Thread(ProcessEffectLoop)
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest
            };
            _processingThread.Start();
        }

        /// <summary>
        /// Detiene el procesamiento de efectos y libera los recursos.
        /// </summary>
        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;
            _processingThread?.Join();

            // Libera la textura actual
            lock (_lock)
            {
                _currentTexture?.Dispose();
                _currentTexture = null;
            }

            _fpsTimer.Stop();
        }

        private void ProcessEffectLoop()
        {
            while (_isRunning)
            {
                try
                {
                    if (!Core.Instances.Settings.InGameEffects) continue;

                    Bitmap image = CaptureImage();
                    if (image == null) continue;

                    // Aplica el efecto utilizando la función proporcionada
                    Bitmap processedImage = _effectFunction(image);
                    if (processedImage == null) continue;

                    SharpDX.Direct3D9.Texture newTexture = Core.Utils.TextureHelper.CreateTextureFromBitmap(Core.Instances.OverlayWindow.D3DDevice, processedImage);
                    image.Dispose();
                    processedImage.Dispose();

                    // Asigna la nueva textura de manera segura
                    lock (_lock)
                    {
                        _currentTexture?.Dispose();
                        _currentTexture = newTexture;
                    }

                    // Incrementa el contador de cuadros procesados
                    Interlocked.Increment(ref _framesProcessed);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones opcional
                    Console.WriteLine($"Error en ProcessEffectLoop: {ex.Message}");
                }
            }
        }

        private Bitmap CaptureImage()
        {
            System.Drawing.Bitmap image = null;
            if (Core.Instances.OverlayMode != OverlayMode.Normal)
            {
                image = GameCapture.CaptureClientRegionPrintWindow(Core.Instances.GameProcess.MainWindowHandle, Core.Instances.OverlayWindow.RestoreBounds);

            }
            else
            {
                Rectangle rectangle = new Rectangle(Core.Instances.OverlayWindow.Location, Core.Instances.OverlayWindow.Size);

                Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(
                        rectangle.X,
                        rectangle.Y,
                        0,
                        0,
                        new Size(rectangle.Width, rectangle.Height),
                        CopyPixelOperation.SourceCopy
                    );
                }

                image = bmp;
            }
            return image;
        }

        /// <summary>
        /// Libera todos los recursos asociados con la clase.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
