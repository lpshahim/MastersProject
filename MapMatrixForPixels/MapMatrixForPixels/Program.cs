using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace MapMatrixForPixels
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Dictionary<int,string> d = new Dictionary<int,string>();
			int count = 0;
			for (int y = 0; y < 90; y++) {
				for (int x = 0; x < 800; x++) {
					string X = Convert.ToString(x);
					string Y = Convert.ToString(y);
					string combo = X +","+ Y;
					d.Add(count,combo);
					count++;
				}
			}

			StreamWriter sw = new StreamWriter("MatrixMap.txt");
			int index = 0;
			foreach (var i in d.Values) {
				//Console.WriteLine(i);
				sw.WriteLine(i + "; " + index);
				index++;
			}
			sw.Flush();
			sw.Close();

			//MAP NEW MATRIX WITH 8 PIXELS PER USER
			StreamWriter newSW = new StreamWriter("newMatrixMap.txt");
			foreach(var i in d.Values){
				newSW.WriteLine(i);
			}
			newSW.Flush();
			newSW.Close();

			string[] allLines = File.ReadAllLines("newMatrixMap.txt");
			string[] ids = File.ReadAllLines("8MapMatrix.txt");
			string[] newLines = new string[allLines.Length];


			for (int i = 0; i < allLines.Length; i++) {
				newLines[i] = allLines[i] + "; " + ids[i];
			}
			File.WriteAllLines("newMatrixMap.txt", newLines);
			//*************************************

			//new dictionary for search
			Dictionary<string, string> newD = new Dictionary<string, string>();
			for (int i = 0; i < 72000; i++) {
				newD.Add(allLines[i],ids[i]);
			}

			foreach (var i in newD) {
				Console.WriteLine(i);
			}

			Console.WriteLine("\nCheck old dictionary\n");

			foreach (var i in d) {
				Console.WriteLine(i);
			}

			//*************************************


			Console.WriteLine("\nEnter pixel id:");
			string pixelId = Console.ReadLine();

			Bitmap bmp = new Bitmap("randomImage.png");

			if (d.ContainsKey(Convert.ToInt32(pixelId)))
			{
				//get value from pixel id i.e. 0,0 from 0
				string xy = d[Convert.ToInt32(pixelId)];
				Console.WriteLine("Value: " + d[Convert.ToInt32(pixelId)]);
				string[] values = Regex.Split(xy, ",");
				Color pixel = bmp.GetPixel(Convert.ToInt16(values[0]), Convert.ToInt16(values[1]));
				Console.WriteLine(bmp.GetPixel(Convert.ToInt16(values[0]), Convert.ToInt16(values[1])));
				Console.Write("x: " + values[0] + " y: " + values[1]);
				Console.WriteLine("\nPixel Value to argb: a: {0} r: {1} g: {2} b: {3}", pixel.A, pixel.R,pixel.G, pixel.B);
			}
			else 
			{
				Console.WriteLine("Does not contain pixel");
			}


			//get argb values from pixels in image
			for (int i = 0; i<bmp.Width; i++)
			{
				for (int j = 0; j<bmp.Height; j++)
			    {
					//Color pixel = bmp.GetPixel(i, j);

			    }
			} 

		}
	}
}
