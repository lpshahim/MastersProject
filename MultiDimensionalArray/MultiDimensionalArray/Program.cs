using System;

namespace MultiDimensionalArray
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			int[,] matrix = new int[800, 90];
			int i = 0;
			for (int y = 0; y < 90; y++) {
				Console.Write("\n");
				for (int x = 0; x < 800; x++) {
					matrix[x, y] = i;
					Console.Write(i + "\t");
					i++;
				}
			}
		}
	}
}
