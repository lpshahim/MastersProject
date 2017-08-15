using System;
using System.IO;
using System.Text;

namespace Read8Pixels
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			int[] num = { 1, 2, 3, 4, 5 };
			int[] user = new int[5];
			string input = "";
			int match = 0;
			double percentage = 0.0;

			for (int a = 0; a<5;a++){
				input = Console.ReadLine();
				user[a] = Convert.ToInt32(input);
			}

			for (int i = 0; i < num.Length; i++) {
				if (num[i] == user[i])
				{
					Console.WriteLine("Match");
					match++;
					percentage = (Convert.ToDouble(match) / Convert.ToDouble(user.Length));
				}
				else {
					Console.WriteLine("No match");
				}
			}

			Console.WriteLine("Percentage match: " + percentage);

			//TODO: READ 8 PIXELS AT A TIME


		}
	}
}
