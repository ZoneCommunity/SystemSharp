// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Mosa.DeviceSystem.Graphics;
using Mosa.DeviceSystem.Mouse;

namespace System;

internal static class MosaLogo
{
	//Size in tiles
	private const uint Width = 23;

	private const uint Height = 7;
    
    public static void Draw()
    {
        var positionX = Display.Width / 2 - Width / 2;
        var positionY = Display.Height / 2 - Height / 2;

        var data = File.ReadAllBytes("cur.bmp");
        var image = Bitmap.CreateImage(data);

        for (uint y = 0; y < (uint)image.Height; y++)
        {
            for (uint x = 0; x < (uint)image.Width; x++)
            {
                Color pixelColor = image.GetPixel((int)x, (int)y);
                if (pixelColor.A != 0) // Check if the pixel is not fully transparent
                {
                    Display.DrawPoint(positionX + x, positionY + y, pixelColor);
                }
            }
        }
    }


}