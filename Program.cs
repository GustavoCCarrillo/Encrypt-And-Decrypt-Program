using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    //In collaboration with Jack, Jason, Jenny, Phil
    public static class EncryptAndDecrypt
    {
        public static string WordAsKey { get; set; } = "CAT";

        public static int SingleKeyValue { get; set; } = 1;

        public static char SingleKeyChar { get; set; } = 'a';



        public static string CleanString(string input)
        {
            string output = "";
            foreach (char i in input)
            {
                if (i >= 97 && i <= 122)
                {
                    output += i.ToString().ToUpper();
                }
                else if (i >= 65 && i <= 90)
                {
                    output += i;
                }
            }
            return output;
        }

        public static int GetKeyValue(char i)
        {
            if (i >= 97 && i <= 122)
            {
                return i - 96;
            }
            if (i >= 65 && i <= 90) 
            { 
                return i - 64; 
            }
            return 0;
        }

        public static void SetCypherKey()
        {
            while (true)
            {
                Console.WriteLine("Enter a single character to be used as a cypher-key:");
                char userInput = Console.ReadLine()[0];
                int value = GetKeyValue(userInput);
                if (value > 0)
                {
                    SingleKeyChar = userInput;
                    SingleKeyValue = value;
                    Console.WriteLine($"Your single key is: {EncryptAndDecrypt.SingleKeyChar} \nYour single key Value is: {EncryptAndDecrypt.SingleKeyValue}");
                    return;
                }
                Console.WriteLine("This entry is not valid...");
            }
        }

        public static void SetCypherWord()
        {
            Console.WriteLine("Enter a word to be used as a cipher word:");
            string word = CleanString(Console.ReadLine());
            WordAsKey = word;
            Console.WriteLine($"Your cipher word is: \"{WordAsKey}\"");

        }

        public static string EncryptWithKey(string input)
        {
            string output = "";
            foreach (char i in input)
            {
                int value = i + SingleKeyValue;
                if (value >= 91) value -= 26;
                output += (char)value;
            }
            return output;
        }

        public static string DecryptWithKey(string input)
        {
            string output = "";
            foreach (char i in input)
            {
                int value = i - SingleKeyValue;
                if (value < 65) 
                { 
                    value += 26;
                }
                output += (char)value;
            }
            return output;
        }

        public static string EncryptWithWord(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int value = input[i] + GetKeyValue(WordAsKey[i % WordAsKey.Length]);
                if (value >= 91) 
                { 
                    value -= 26;
                }
                output += (char)value;
            }
            return output;
        }

        public static string DecryptWithWord(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int value = input[i] - GetKeyValue(WordAsKey[i % WordAsKey.Length]);
                if (value < 65) 
                { 
                    value += 26; 
                }
                output += (char)value;
            }
            return output;
        }

        public static string EncryptWithString(string input)
        {
            string newKey = WordAsKey + input;
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int value = input[i] + GetKeyValue(newKey[i]);
                if (value >= 91) 
                { 
                    value -= 26; 
                }
                output += (char)value;
            }
            return output;
        }

        public static string DecryptWithString(string input)
        {
            string newKey = WordAsKey;
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int value = input[i] - GetKeyValue(newKey[i]);
                if (value < 65) 
                { 
                    value += 26; 
                }
                char n = (char)value;
                newKey += n;
                output += n;
            }
            return output;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string to be encrypted: ");
        string userString = Console.ReadLine();
        string cleanString = EncryptAndDecrypt.CleanString(userString);
        EncryptAndDecrypt.SetCypherKey();
        EncryptAndDecrypt.SetCypherWord();



        Console.WriteLine($"\nYour entry: {cleanString} is in plain text form.");



        string encryptedWithSingleKey = EncryptAndDecrypt.EncryptWithKey(cleanString);
        Console.WriteLine($"\nEncrpted: String with a single key: {encryptedWithSingleKey}");
        Console.WriteLine($"Decrypted: {EncryptAndDecrypt.DecryptWithKey(encryptedWithSingleKey)}");



        string encryptedWithWordKey = EncryptAndDecrypt.EncryptWithWord(cleanString);
        Console.WriteLine($"\nEncrypted: String encrypted with a word: {encryptedWithWordKey}");
        Console.WriteLine($"Decrypted: {EncryptAndDecrypt.DecryptWithWord(encryptedWithWordKey)}");



        string encryptedWithStringKey = EncryptAndDecrypt.EncryptWithString(cleanString);
        Console.WriteLine($"\nEncrypted: String encrypted with a word and a string: {encryptedWithStringKey}");
        Console.WriteLine($"Decrypted: {EncryptAndDecrypt.DecryptWithString(encryptedWithStringKey)}");
    }
}
