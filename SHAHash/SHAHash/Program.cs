using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace SHAHash
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string input = "";
			while (input != "exit")
			{
				Console.WriteLine("Enter string to be hashed:");
				input = Console.ReadLine();
				Console.WriteLine("Enter salt:");
				string salt = Console.ReadLine();

				string hashed = generateHash(input + salt);
				Console.WriteLine("\n\nHashed value:\n" + hashed);
				//splitString(input);
				//splitHashIntoSixes(hashed);
				//Console.WriteLine("Image creation\n");
				//createImage();
			}

			//read all lines for pins to be hashed
			//string[] pinsToBeHashed = File.ReadAllLines("pinsToBeHashed.txt");
			//hashAllPins(pinsToBeHashed);
			//generatePinBytes(pinsToBeHashed);
			//pinByteArray(pinsToBeHashed);
		}

		//create hashed pins for the image
		static void hashAllPins(string[] pins){ 
			//writer to write out hashed pins
			StreamWriter sw = new StreamWriter("HashedPinsForImage.txt");

			foreach (string line in pins) {
				sw.WriteLine(generateHash(line));
			}
			sw.Flush();
			sw.Close();

		}

		static void pinByteArray(string[] input) { 
			byte[] hash;
			//Write bytes to text file 
			StreamWriter sw = new StreamWriter("pinByteArray.txt");

			using (var sha1 = new SHA256CryptoServiceProvider())
			{
				foreach (string line in input)
				{
					hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(line));
					sw.Write("\n");
					foreach (byte b in hash) {
						sw.Write("\t" + b);
					}
					//bmp = createImage(hash);
					CopyDataToBitmap(hash);
				}
			}
			//saveImage(bmp);

			sw.Flush();
			sw.Close();
		}


		static string generateHash(string input)
		{
			byte[] hash;

			using (var sha1 = new SHA256CryptoServiceProvider())
			{
				hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
				foreach (byte b in hash) {
					Console.Write(b + " ");
				}
			}

			/*createImageMatrix(hash);

			//Write bytes to text file 
			StreamWriter sw = new StreamWriter("byteArray.txt");
			foreach (byte b in hash) {
				sw.WriteLine(b);
			}
			sw.Flush();
			sw.Close();

			//Split byte array into 8
			sw = new StreamWriter("StartPixels.txt");
			for (int i = 0; i < hash.Length; i += 4) {
				sw.WriteLine(hash[i]);
			}
			sw.Flush();
			sw.Close();

			//CONVERT BYTE ARRAY TO 256 bits TO BE STORED IN THE PIXELS
			//8 PIXELS PER USER = 32BPP = 256 BITS

			var bits = new BitArray(hash);
			int T = 1;
			int F = 0;
			int count = 0;

			sw = new StreamWriter("bitArray.txt");
			foreach (bool bit in bits) {

				switch (bit)
				{
					case true:
						//Console.WriteLine(T);
						sw.WriteLine(T);
						count++;
						break;
					case false:
						//Console.WriteLine(F);
						sw.WriteLine(F);
						count++;
						break;
				}
			}
			sw.Flush();
			sw.Close();*/

			//HASH TO STRING
			var sb = new StringBuilder();

			foreach (byte b in hash)
			{
				sb.AppendFormat("{0:X2}", b);
			}

				return sb.ToString();
        }

		static void splitString(string input)
		{
			string[] handGeo = input.Split(',');

			foreach (string hand in handGeo) {
				Console.WriteLine(hand);
			}
		}

		static void splitHashIntoSixes(string hash) {
			
			for (int i = 0; i < hash.Length ; i += 6) {
				if (i + 6 < hash.Length)
				{
					string sub = hash.Substring(i, 6);
					Console.WriteLine(sub);
				}
				else 
				{
					string sub = hash.Substring(hash.Length - 4);
					Console.WriteLine(sub + "11");
				}
			}
		}

		//WRITE BYTE ARRAY TO BITMAP IMAGE
		static Bitmap CopyDataToBitmap(byte[] data)
		{
			//Here create the Bitmap to the know height, width and format
			Bitmap bmp = new Bitmap(800, 90);

			//Create a BitmapData and Lock all pixels to be written 
			BitmapData bmpData = bmp.LockBits(
								 new Rectangle(0, 0, bmp.Width, bmp.Height),
								 ImageLockMode.WriteOnly, bmp.PixelFormat);

			//Copy the data from the byte array into BitmapData.Scan0
			Marshal.Copy(data, 0, bmpData.Scan0, data.Length);


			//Unlock the pixels
			bmp.UnlockBits(bmpData);


			//Return the bitmap 
			return bmp;
		}

		//CREATE IMAGE WITH HEIGHT OF 90 AND WIDTH OF 800 PIXELS
		static void createPinImage(byte b) {
			int height = 90;
			int width = 800;
			Bitmap bmp = new Bitmap(width, height);

			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					bmp.SetPixel(x, y, Color.FromArgb(b, b, b, b));
				}
			}
			try
			{
				if (bmp != null) { 
					bmp.Save("//Users//Louis-Philip//Desktop//randomBytePinImage.png");
					Console.WriteLine("Image saved.");		
				}
			}
			catch (Exception e) {
				Console.Write("There was a problem saving the file: " + e);
			}
		}

		static Bitmap createImage(byte[] array) {
			int height = 90;
			int width = 800;
			Bitmap bmp = new Bitmap(width, height);

			for (int y = 0; y < height; y++) { 
				for (int x = 0; x < width; x++) {
					for (int i = 0; i< array.Length; i += 4) {
						int a = array[i];
						int r = array[i + 1];
						int g = array[i + 2];
						int b = array[i + 3];

						bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));	
					}

				}
			}

			return bmp;
		}

		static void saveImage(Bitmap bmp) { 
			try
			{
				if (bmp != null) { 
					bmp.Save("//Users//Louis-Philip//Desktop//randomImage.png");
					Console.WriteLine("Image saved.");		
				}
			}
			catch (Exception e) {
				Console.Write("There was a problem saving the file: " + e);
			}
		}

		static void createImageMatrix(byte[] byteArray) {
			int height = 32;
			int width = 32;
			Bitmap bmp = new Bitmap(width, height);
			StreamWriter sw = new StreamWriter("byteMatrix.txt");

			for (int y = 0; y < height; y++) {
				sw.Write("\n");
				for (int x = 0; x < width; x++) {
					sw.Write(byteArray[x] + "\t");
					bmp.SetPixel(x, y, Color.FromArgb(byteArray[x],byteArray[x],byteArray[x],byteArray[x]));
				}
			}

			try
			{
				if (bmp != null) { 
					bmp.Save("//Users//Louis-Philip//Desktop//randomImage.png");
					//Console.WriteLine("Image saved.");		
				}
			}
			catch (Exception e) {
				Console.Write("There was a problem saving the file: " + e);
			}
			sw.Flush();
			sw.Close();
		}
	}
}
