using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Vector_Confidence_Match
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			int[] arr1 = { 1, 2, 3, 4, 5 };
			int[] arr2 = { 1, 7, 9, 9, 11 };
			bool check = checkConfidence(arr1, arr2);
			Console.WriteLine(check);
			string strArr1 = "";
			for (int i = 0; i < arr1.Length; i++) {
				strArr1 += arr1[i].ToString() + ",";
			}
			generateHash(strArr1);
			Console.WriteLine("\n\n\n");
			string strArr2 = "";
			for (int i = 0; i<arr2.Length; i++) {
				strArr2 += arr2[i].ToString() + ",";
			}
			generateHash(strArr2);
		}

		public static bool checkConfidence(int[] arr1, int[] arr2) {
			int count = 0;
			for (int i = 0; i < arr1.Length; i++)
			{
				int low = arr2[i] - 5;
				int high = arr2[i] + 5;

				if (arr1[i] >= low && arr1[i] <= high) {
					count++;
				}
			}

			if (count >= 3)
			{
				return true;
			}
			else 
			{
				return false;
			}

		}

		static void generateHash(string input)
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
		}

		static bool checkHashMatch(int[] arr) {


			return false;
		}
	}
}
