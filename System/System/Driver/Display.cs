using System.Drawing;
using Mosa.DeviceSystem.Fonts;
using Mosa.DeviceSystem.Graphics;
using System.IO;

namespace System.Driver
{
    public static class Display
    {
        private static FrameBuffer32 DisplayFrame { get; set; }
        private static FrameBuffer32 BackFrame1 { get; set; }
        private static FrameBuffer32 BackFrame2 { get; set; }

        public static IGraphicsDevice Driver { get; private set; }
        public static uint Width { get; private set; }
        public static uint Height { get; private set; }
        public static ISimpleFont DefaultFont { get; set; }

        private static int currentBackBuffer = 0;

        public static bool Initialize()
        {
            Width = 1280;
            Height = 720;

            Driver = Program.DeviceService.GetFirstDevice<IGraphicsDevice>().DeviceDriver as IGraphicsDevice;

            if (Driver == null)
                return false;

            Driver.SetMode((ushort)Width, (ushort)Height);

            DisplayFrame = Driver.FrameBuffer;
            BackFrame1 = DisplayFrame.Clone();
            BackFrame2 = DisplayFrame.Clone();

            return true;
        }

        public static void DrawMosaLogo(uint v)
        {
            MosaLogo.Draw(v);
        }

        public static void DrawWallpaper()
        {
            var datax = File.ReadAllBytes("logo.bmp");
            var imagae = Bitmap.CreateImage(datax);
            FrameBuffer32 bitmapFrameBuffer = Bitmap.CreateImage(datax);

            DrawBuffer(1, 1, bitmapFrameBuffer, true);
        }

        public static void DrawPoint(uint x, uint y, Color color)
        {
            GetCurrentBackFrame().SetPixel((uint)color.ToArgb(), x, y);
        }

        public static void DrawBuffer(uint x, uint y, FrameBuffer32 buffer, bool drawWithAlpha = false)
        {
            GetCurrentBackFrame().DrawBuffer(buffer, x, y, drawWithAlpha);
        }

        public static void DrawString(uint x, uint y, string text, ISimpleFont font, Color color)
        {
            font.DrawString(GetCurrentBackFrame(), (uint)color.ToArgb(), x, y, text);
        }

        public static void DrawRectangle(uint x, uint y, uint width, uint height, Color color, bool fill)
        {
            if (fill)
            {
                GetCurrentBackFrame().FillRectangle((uint)color.ToArgb(), x, y, width, height);
            }
            else
            {
                GetCurrentBackFrame().DrawRectangle((uint)color.ToArgb(), x, y, width, height, 1);
            }
        }

        public static bool IsInBounds(uint x1, uint x2, uint y1, uint y2, uint width, uint height)
        {
            return x1 >= x2 && x1 <= x2 + width && y1 >= y2 && y1 <= y2 + height;
        }

        public static void Clear(Color color)
        {
            GetCurrentBackFrame().ClearScreen((uint)color.ToArgb());
        }

        public static void Update()
        {
            // Swap back buffers
            currentBackBuffer = (currentBackBuffer + 1) % 2;

            // Copy the back buffer to the display buffer
            DisplayFrame.CopyFrameBuffer(GetCurrentBackFrame());

            // Update the display
            Driver.Update(0, 0, Width, Height);
        }

        // Helper method to get the current back buffer
        private static FrameBuffer32 GetCurrentBackFrame()
        {
            return currentBackBuffer == 0 ? BackFrame1 : BackFrame2;
        }
    }
}
