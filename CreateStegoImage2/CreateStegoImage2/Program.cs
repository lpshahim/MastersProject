using System;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace CreateStegoImage2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Enter hand geometry\n");
			string handgeo = Console.ReadLine();
			Console.WriteLine(generateHash(handgeo));
		}

		static string generateHash(string input)
		{
			byte[] hash;

			using (var sha1 = new SHA256CryptoServiceProvider())
			{
				hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
				foreach (byte b in hash)
				{
					Console.Write(b + " ");
				}
			}

			setPixels(hash);

			//HASH TO STRING
			var sb = new StringBuilder();

			foreach (byte b in hash)
			{
				sb.AppendFormat("{0:X2}", b);
			}

			return sb.ToString();
		}

		static void setPixels(byte[] h) {
			int x = 0;
			int y = 0;

			Image img = Image.FromFile("stegoImage2.png");
			Bitmap bmp = new Bitmap(img);

			for (int i = 0; i < h.Length;i+=4){
				//Console.WriteLine(h[i] + " " + h[i+1] + " " + h[i+2] + " " + h[i+3] + "\n");
				bmp.SetPixel(x, y, Color.FromArgb(h[i], h[i+1], h[i+2], h[i+3]));
				x++;
			}
			bmp.Save("stegoImage2a.png");
		}

		void createStegoImage() { 
			Bitmap bmp = new Bitmap(800, 90);

			for (int i = 0; i< 90; i++) {
				for (int j = 0; j< 800; j++) {
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
