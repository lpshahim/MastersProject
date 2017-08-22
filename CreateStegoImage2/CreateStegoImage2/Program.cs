using System;
using System.IO;
using System.Drawing;

namespace CreateStegoImage2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Bitmap bmp = new Bitmap(800, 90);

			for (int i = 0; i < 90; i++) {
				for (int j = 0; j < 800; j++) {
					int a = 0;
					int r = 0;
					int g = 0;
					int b = 0;

					bmp.SetPixel(j, i, Color.FromArgb(a, r, g, b));
				}
			}

			bmp.Save("stegoImage2.png");
		}
	}
}
