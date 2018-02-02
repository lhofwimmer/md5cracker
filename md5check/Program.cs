using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace md5check
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckforResult(md5result);
            Console.ReadKey();
        }
        static string md5result = "38959589619129abc573d5130445cb6d";

        private static void CheckforResult(string md5result)
        {
            var path = $@"{Environment.CurrentDirectory}\wordlist.txt";
            var array = File.ReadAllLines(path).ToList();

            CalcHashes(array);
        }

        private static void CalcHashes(List<string> array)
        {

            int counter = 0;
            var resFound = false;

            for (int i= 0;i < array.Count();i++)
            {
                for (int j=i;j<array.Count();j++)
                {
                    var testword = array[i] + array[j];
                    string result = CalculateMD5Hash(testword);
                    counter++;
                    Console.WriteLine(counter);
                    if (result.Equals(md5result))
                    {
                        Console.WriteLine(testword + " | " + counter);
                        resFound = true;
                    }
                    if (resFound) break;
                }
                if (resFound) break;
            }
        }

        private static string CalculateMD5Hash(string testword)
        {
            var md5hash = "";

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputbytes = System.Text.Encoding.ASCII.GetBytes(testword);
                byte[] computehash = md5.ComputeHash(inputbytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < computehash.Length; i++)
                {
                    sb.Append(computehash[i].ToString("X2"));
                }
                md5hash = sb.ToString();
            }

            return md5hash;
        }
    }
}
