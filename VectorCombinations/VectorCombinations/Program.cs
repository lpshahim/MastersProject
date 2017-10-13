using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VectorCombinations
{
	class MainClass
	{
		static HashSet<int> handGeo = new HashSet<int>();
		static HashSet<int> current = new HashSet<int>();
		static string checkTest;
		//HASH SET FOR CALCULATED COMBOS
		static HashSet<string> combos = new HashSet<string>();
		static Dictionary<string, string> dictionary = new Dictionary<string, string>();
		static StreamWriter sw = new StreamWriter("combos.txt");

		public static void Main(string[] args)
		{
			checkTest = generateHash("11,19,31,40,49");
			Console.WriteLine(checkTest);

			int[] vector = { 10, 20, 30, 40, 50};
			Console.WriteLine("Original vector:");
			foreach (int v in vector)
				Console.Write(v + " ");
			Console.WriteLine();
			recFunc(vector,0);
			recurseArray(vector, 0);

			foreach (string val in dictionary.Values)
			{
				if (val.Equals(checkTest))
				   Console.WriteLine("MATCH FOUND FOR {0}", val);
			}

			writeDictionary(dictionary);
			
			sw.Flush();
			sw.Close();
		}

		static void writeDictionary(Dictionary<string, string> dic) 
		{
			StreamWriter writeDic = new StreamWriter("Dictionary.txt");
			foreach (var i in dic)
				writeDic.WriteLine("Combo: {0} \nHash: {1}", i.Key, i.Value);

			writeDic.Flush();
			writeDic.Close();

		}
		//WRITE FUNCTION THAT SUBTRACTS ONE FROM EACH ARRAY ELEMENT AND ACCEPTS COUNTER. 
		//RECURSE FUNCTION UNTIL COUNTER VALUE IS SIZE OF ARRAY - 1

		static void recurseArray(int[] arr, int count) 
		{
			int[] low = new int[arr.Length];
			int[] high = new int[arr.Length];
			if (count == arr.Length)
				return;
			recurseArray(arr, count + 1);
			foreach (int val in arr)
			{
				Array.Copy(arr, low, arr.Length);
				Array.Copy(arr, high, arr.Length);
				low[count] = arr[count] - 1;
				high[count] = arr[count] + 1;
				printOutArrays(low, high);
				recurseArray(low, count+1);
				recurseArray(high, count+1);
			}
		}

		static void recFunc(int[] vector, int count)
		{
			//int count = 0;
			int[] highVector = new int[5];
			int[] lowVector = new int[5];

			foreach (int v in vector) {
				Array.Copy(vector, lowVector, 5);
				Array.Copy(vector, highVector, 5);
				highVector[count] = vector[count] + 1;
				lowVector[count] = vector[count] - 1;
				printOutArrays(lowVector, highVector);
				count++;
			}
		}

		//GENERATE HASH FUNCTION
		static string generateHash(string input)
		{
			byte[] hash;
			string finalHash = "";

			//CHECK DUPLICATES IN COMBOS HASHSET
			checkDuplicates(input);

			using (var sha1 = new SHA256CryptoServiceProvider())
			{
				hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));

				foreach (byte b in hash)
				{
					finalHash += b + " ";
					handGeo.Add(b);
				}
			}

			compareHashsets(handGeo);

			return finalHash;
		}

		static bool compareHashsets(HashSet<int> curr) 
		{
			if (curr.SetEquals(current))
			{
				Console.WriteLine("MATCH FOUND!");
				return true;
			}
			else 
			{
				return false;
			}
		}

		//CONVERT INT ARRAY TO STRING EACH TIME USING FUNCTION AND HASH EACH TIME FOR OUTPUT

		static void printOutArrays(int[] low, int[] high) {
			string lowArr = "";
			string highArr = "";

			foreach (int l in low)
			{
				lowArr += Convert.ToString(l) + ",";
			}

			foreach (int h in high)
			{
				highArr += Convert.ToString(h) + ",";
			}

			string arr1 = generateHash(lowArr.TrimEnd(','));
			//Console.WriteLine(arr1);
			string arr2 = generateHash(highArr.TrimEnd(','));
			//Console.WriteLine(arr2);

		}

		static void checkDuplicates(string input) 
		{
			if (!combos.Contains(input))
			{
				Console.WriteLine(input);
				combos.Add(input);
				sw.WriteLine(input);
				dictionary.Add(input, generateHash(input));
			}
		}
		//CHECK DUPLICATES FOR HASH VALUES OF EACH INPUT
	}
}
