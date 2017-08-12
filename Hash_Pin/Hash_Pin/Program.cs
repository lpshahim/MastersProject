using System;
using System.Security.Cryptography;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace Hash_Pin
{
	class MainClass
	{
		public static void Main(string[] args)
		{

			Console.WriteLine("Enter pin:");
			string pin = Console.ReadLine();

			using (MD5 md5Hash = MD5.Create())
			{
				string hash = GetMd5Hash(md5Hash, pin);

				Console.WriteLine("The MD5 hash of " + pin + " is: " + hash + ".");

				Console.WriteLine("Verifying the hash...");

				if (VerifyMd5Hash(md5Hash, pin, hash))
				{
					Console.WriteLine("The hashes are the same.");
				}
				else
				{
					Console.WriteLine("The hashes are not same.");
				}
			}
		}

		static string GetMd5Hash(MD5 md5Hash, string input)
		{

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
		{
			// Hash the input.
			string hashOfInput = GetMd5Hash(md5Hash, input);

			// Create a StringComparer an compare the hashes.
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
