using System;
using System.IO;
using System.Drawing;
using System.Collections;

namespace EightPixelsPerUser
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			int count = 0;
			int[] argb = new int[72000];
			Hashtable ht = new Hashtable();
			Bitmap bmp = new Bitmap("randomImage.png");
			for (int y = 0; y < 90; y++) {
				for (int x = 0; x < 800; x++) {
					Color pixel = bmp.GetPixel(x, y);
					argb[count] = pixel.A;
					//Console.WriteLine(pixel);
					ht.Add(count, pixel);
					count++;
				}
			}

			foreach (var i in argb) {
				Console.WriteLine(i);
			}

		}
	}
}
