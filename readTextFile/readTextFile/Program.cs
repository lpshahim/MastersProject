using System;
using System.Collections.Generic;
using System.IO;

namespace readTextFile
{
	class readTextFile
	{
		static void Main()
		{
			string[] lines;
			var list = new List<string>();
			var fileStream = new FileStream(@"/Users/Louis-Philip/Desktop/guest.txt", FileMode.Open, FileAccess.Read);
			using (var streamReader = new StreamReader(fileStream))
			{
				string line;
				while ((line = streamReader.ReadLine()) != null)
				{
					if (line.Length == 8)
					{
						list.Add(line);
					}

				}
			}



			lines = list.ToArray();
			var streamWriter = new StreamWriter(@"/Users/Louis-Philip/Desktop/8_guest.txt");
			foreach (string s in lines)
			{
				streamWriter.WriteLine(s);
			}
		}
	}
}
