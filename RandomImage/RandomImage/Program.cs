using System;
using System.IO;

namespace RandomImage
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			StreamWriter sr = new StreamWriter(@"Matrix.txt");
			Console.WriteLine("Matrix!");
			int id = 1;
			for (int height = 0; height < 30; height++) {
//				Console.Write("\n" + height +"\t");
				for (int width = 0; width < 30; width++) {
					Console.Write(width + "\t");
					sr.Write(width + "\t");
					id++;
				}
			}

			sr.Flush();
			sr.Close();
		}
	}
}
