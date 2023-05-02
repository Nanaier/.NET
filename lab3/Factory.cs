using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lab3
{
    abstract class Ciphers
    {
        public abstract Encrypt CreateEncrypt();
        public abstract Decrypt CreateDecrypt();
    }

    class Cesar : Ciphers
    {
        public override Encrypt CreateEncrypt()
        {
            return new EncryptCesar();
        }
        public override Decrypt CreateDecrypt()
        {
            return new DecryptCesar();
        }
    }

    class Vigenere : Ciphers
    {
        public override Encrypt CreateEncrypt()
        {
            return new EncodeVigenere();
        }
        public override Decrypt CreateDecrypt()
        {
            return new DecryptVigenere();
        }
    }

    class RailFence : Ciphers
    {
        public override Encrypt CreateEncrypt()
        {
            return new EncodeRailFence();
        }
        public override Decrypt CreateDecrypt()
        {
            return new DecryptRailFence();
        }
    }

    abstract class Encrypt
    {
        public abstract string Encrypting(string plaintext, string key);
    }

    abstract class Decrypt
    {
        public abstract string Decrypting(string ciphertext, string key);
    }

    class EncryptCesar : Encrypt
    {
        public override string Encrypting(string plaintext, string key)
        {
            int shift = int.Parse(key);
            StringBuilder ciphertext = new StringBuilder();

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    char shifted = (char)((((c + shift) - baseChar) % 26) + baseChar);
                    ciphertext.Append(shifted);
                }
                else
                {
                    ciphertext.Append(c);
                }
            }

            return ciphertext.ToString();
        }
    }

    class DecryptCesar : Decrypt
    {
        public override string Decrypting(string ciphertext, string key)
        {
            int shift = int.Parse(key);
            StringBuilder plaintext = new StringBuilder();

            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    char shifted = (char)((((c - shift) - baseChar + 26) % 26) + baseChar);
                    plaintext.Append(shifted);
                }
                else
                {
                    plaintext.Append(c);
                }
            }

            return plaintext.ToString();
        }
    }

    class EncodeVigenere : Encrypt
    {
        public override string Encrypting(string plaintext, string key)
        {
            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                    return null;

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < plaintext.Length; ++i)
            {
                if (char.IsLetter(plaintext[i]))
                {
                    bool cIsUpper = char.IsUpper(plaintext[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    char ch = (char)((Mod(((plaintext[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += plaintext[i];
                    ++nonAlphaCharCount;
                }
            }

            return output;
        }
        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }
    }

    class DecryptVigenere : Decrypt
    {
        public override string Decrypting(string ciphertext, string key)
        {
            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                    return null;

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < ciphertext.Length; ++i)
            {
                if (char.IsLetter(ciphertext[i]))
                {
                    bool cIsUpper = char.IsUpper(ciphertext[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = -k;
                    char ch = (char)((Mod(((ciphertext[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += ciphertext[i];
                    ++nonAlphaCharCount;
                }
            }

            return output;
        }
        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }
    }


    class EncodeRailFence : Encrypt
    {
        public override string Encrypting(string plaintext, string key)
        {
            int _rails = int.Parse(key);
            var railStr = new string[_rails];
            var trackIdx = 0;
            var direction = 1;
            var directionCount = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                railStr[trackIdx] += plaintext[i];
                trackIdx += direction;
                directionCount++;
                if (directionCount == _rails - 1)
                {
                    direction *= -1;
                    directionCount = 0;
                }
            }
            return railStr.Aggregate("", (x, y) => x + y);
        }
    }

    class DecryptRailFence : Decrypt
    {
        public override string Decrypting(string ciphertext, string key)
        {
            int _rails = int.Parse(key);

            int[] trackLen = CalculateTrackLen(ciphertext, _rails);
            var list = FillTextToRails(ciphertext, trackLen, _rails);
            var decodeStr = string.Empty;
            var trackIdx = 0;
            var direction = 1;
            var directionCount = 0;
            for (int i = 0; i < ciphertext.Length; i++)
            {
                decodeStr += char.ToString(list[trackIdx].First());
                list[trackIdx] = list[trackIdx].Remove(0, 1);
                trackIdx += direction;
                directionCount++;
                if (directionCount == _rails - 1)
                {
                    direction *= -1;
                    directionCount = 0;
                }
            }
            return decodeStr;
        }

        private List<string> FillTextToRails(string input, int[] trackLen, int _rails)
        {
            var list = new List<string>();
            var cur = 0;
            foreach (var i in Enumerable.Range(0, _rails))
            {
                var k = input.Substring(cur, trackLen[i]);
                list.Add(k);
                cur += trackLen[i];
            }
            return list;
        }
        private int[] CalculateTrackLen(string input, int _rails)
        {
            var dumb = new string[_rails];
            var indexTrack = 0;
            var direction = 1;
            var directionCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                dumb[indexTrack] += input[i];
                indexTrack += direction;
                directionCount++;
                if (directionCount == _rails - 1)
                {
                    direction *= -1;
                    directionCount = 0;
                }
            }
            return dumb.Select(s => s.Length).ToArray();
        }
    }

    class Text
    {
        private Encrypt _encrypt;
        private Decrypt _decrypt;

        public Text(Ciphers cipher)
        {
            _encrypt = cipher.CreateEncrypt();
            _decrypt = cipher.CreateDecrypt();
        }

        public string Encrypt(string plaintext, string key)
        {
            return _encrypt.Encrypting(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return _decrypt.Decrypting(ciphertext, key);
        }
    }

    public class CipherFactory
    {
        public void CreateCipher(string cipherType, string input, string key)
        {
            switch (cipherType)
            {
                case "caesar":
                    Ciphers cipher = new Cesar();
                    Text text = new Text(cipher);
                    string encrypted = text.Encrypt(input, key);
                    string decrypted = text.Decrypt(encrypted, key);
                    Console.WriteLine($"Encrypted text using {cipherType} cipher: {encrypted}");
                    Console.WriteLine($"Decrypted text using {cipherType} cipher: {decrypted}");
                    break;
                case "vigenere":
                    cipher = new Vigenere();
                    text = new Text(cipher);
                    encrypted = text.Encrypt(input, key);
                    decrypted = text.Decrypt(encrypted, key);
                    Console.WriteLine($"Encrypted text using {cipherType} cipher: {encrypted}");
                    Console.WriteLine($"Decrypted text using {cipherType} cipher: {decrypted}");
                    break;
                case "railfence":
                    cipher = new RailFence();
                    text = new Text(cipher);
                    encrypted = text.Encrypt(input, key);
                    decrypted = text.Decrypt(encrypted, key);
                    Console.WriteLine($"Encrypted text using {cipherType} cipher: {encrypted}");
                    Console.WriteLine($"Decrypted text using {cipherType} cipher: {decrypted}");
                    break;
                default:
                    throw new ArgumentException("Invalid cipher type");
            }
        }
    }
}

