using System;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Text;
using System.Collections;
namespace CreateHashPinImage
{
	class MainClass
	{
		static Hashtable ht = new Hashtable();
		public static void Main(string[] args)
		{
			string[] pinsToBeHashed = File.ReadAllLines("pinsToBeHashed.txt");
			pinByteArray(pinsToBeHashed);
			/*byte[] byteArray = new byte[288000];
			ht.Values.CopyTo(byteArray,0);

			Bitmap bmp = new Bitmap(800, 90);
			int count = 0;
			for (int y = 0; y < 90; y++) {
				for (int x = 0; x < 800; x++) {
					byte a = byteArray[count];
					byte r = byteArray[count + 1];
					byte g = byteArray[count + 2];
					byte b = byteArray[count + 3];

					bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
					count += 4;
				}
			}
			saveImage(bmp);*/

			/*Bitmap bmp = new Bitmap("randomImage.png");
			for (int i = 0; i < 800; i++) {
				Console.WriteLine(bmp.GetPixel(i, 0));
			}*/

			foreach (var val in ht.Values) { 
				Console.WriteLine(val);
			}
		}

		static void pinByteArray(string[] input)
		{
			byte[] hash;
			//Write bytes to text file 
			StreamWriter sw = new StreamWriter("pinByteArray.txt");
			int i = 0;
			using (var sha1 = new SHA256CryptoServiceProvider())
			{
				foreach (string line in input)
				{
					hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(line));
					sw.Write("\n");

					foreach (byte b in hash)
					{
						sw.Write(b+",");
						ht.Add(i,b);						
						i++;
					}
				}
			}
			sw.Flush();
			sw.Close();
		}

		static void createImage(byte[] array)
		{
			Console.WriteLine("Creating image...");
			int height = 90;
			int width = 800;
			int len = array.Length;
			Bitmap bmp = new Bitmap(width, height);

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					for (int i = 0; i < len; i += 4)
					{
						byte a = array[i];
						byte r = array[i + 1];
						byte g = array[i + 2];
						byte b = array[i + 3];

						bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
					}

				}
			}
			Console.WriteLine("Image Created... \nSaving image...");
			saveImage(bmp);
			//return bmp;
		}

		static void saveImage(Bitmap bmp)
		{
			try
			{
				if (bmp != null)
				{
					bmp.Save("//Users//Louis-Philip//Desktop//randomImage.png");
					Console.WriteLine("Image saved.");
				}
			}
			catch (Exception e)
			{
				Console.Write("There was a problem saving the file: " + e);
			}
		}
	}
}
