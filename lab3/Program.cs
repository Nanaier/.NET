using System;
using System.Text.RegularExpressions;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            CipherFactory factory = new CipherFactory();

            Console.WriteLine("Enter the cipher name(caesar, vigenere or railfence): ");
            string cipherType = Console.ReadLine();
            while (cipherType != "caesar" && cipherType != "railfence" && cipherType != "vigenere")
            {
                Console.WriteLine("Enter the cipher name(caesar, vigenere or railfence): ");
                cipherType = Console.ReadLine();
            }

            Console.WriteLine("Enter the input text: ");
            string input = Console.ReadLine();
            while (!Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Enter the input text: ");
                input = Console.ReadLine();
            }

            Console.WriteLine("Enter the key: ");
            string key = Console.ReadLine();
            while ((cipherType == "caesar" || cipherType == "railfence") && !int.TryParse(key, out _))
            {
                Console.WriteLine("Enter the key: ");
                key = Console.ReadLine();
            }

            while (cipherType == "vigenere" && !Regex.IsMatch(key, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Enter the key: ");
                key = Console.ReadLine();
            }

            factory.CreateCipher(cipherType, input, key);

        }
    }
}
