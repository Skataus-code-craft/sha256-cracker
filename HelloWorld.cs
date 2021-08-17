using Internal;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HelloWorld
{
    public class Cracker
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: mono HelloWorld.exe [hashfile] [dictionary]");
                Console.ReadLine();
                Environment.Exit(1);
            }
            string hashfile = args[0];
            string dictionary = args[1];
            string[] hashes = File.ReadAllLines(hashfile);
            string[] wordlist = File.ReadAllLines(dictionary);
            using (SHA256 algorithm = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                foreach (string hash in hashes)
                {
                    bool cracked = false;
                    foreach (string word in wordlist)
                    {
                        Console.WriteLine(word);
                        StringBuilder Sb = new StringBuilder();
                        Byte[] result = algorithm.ComputeHash(enc.GetBytes(word));
                        foreach (Byte b in result)
                        {
                            Sb.Append(b.ToString("x2"));
                        }
                        if (Sb.ToString() == hash)
                        {
                            cracked = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Cracked! {Environment.NewLine}");
                            Console.ResetColor();
                            break;
                        }
                    }
                    if (cracked == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Not in dictionary. {Environment.NewLine}");
                        Console.ResetColor();
                    }
                }
            }
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
