using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ciphertest1
{
    class Program
    {
        static void Main(string[] args)
        {
            char delimiter = Convert.ToChar(65300); //Separating character for data and key (MATH UNDER 65300)


            //KEY GENERATOR

            DateTime now = DateTime.Now;
            Random rnd = new Random();
            char[] privatekey = new char[128]; //length of key can be adjusted here (within multiples of 4)
            for (int x = 0; x < 128; x++)
            {
                int day = now.Day;
                int milli = now.Millisecond;
                int value = ((milli + day - rnd.Next(0, 28)) * rnd.Next(1, 6)) + rnd.Next(1, 30);

                int positiveValue = System.Math.Abs(value);

                privatekey[x] = Convert.ToChar(positiveValue);

            }




            //ENCRYPTION OF DATA
            //Console.WriteLine("Enter text to be encrypted");
            string location = "";
            string example = "";

            bool exception = false;
            while (exception != true) {
                try
                {
                    Console.WriteLine("Enter file path (\\file.txt): ");
                    location = Console.ReadLine();
                    StreamReader read = new StreamReader(location);
                    example = read.ReadToEnd();
                    read.Close();
                    exception = true;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine("File path incorrect.");
                    exception = false;
                }
                catch (System.ArgumentException ex)
                {
                    Console.WriteLine("File path incorrect.");
                    exception = false;
                }


            }
            char[] exampleArray = example.ToCharArray();
            int exampleLength = example.Length;
            char[] resultArray = new char[exampleLength];

            int j = 0; //Array count
            int i = 0;
            foreach (char item in exampleArray)
            {
                int itemNumber = Convert.ToInt32(item);  //MATH
                int scrambleValue = itemNumber + privatekey[j] - privatekey[j + 1] + privatekey[j + 2] + privatekey[j + 3] + 2 * (privatekey[j + 4]) + privatekey[j + 5] + privatekey[j + 6] + privatekey[j + 7] + 2 * (privatekey[j + 2]);




                resultArray[i] = Convert.ToChar(scrambleValue);
                i++;
                j = j + 8;
                if (j == 128)
                {
                    j = 0;
                }
                if (scrambleValue == 65300)
                {
                    Console.WriteLine("Undesired overflow");

                }
            }
            string result = new string(resultArray);


            //PASSWORD GENERATOR
            //int charNumber = rnd.Next(1,9);

            // string testPass = "";
            //for (int w = 0; w < charNumber; w++)
            {
                // int charValue = rnd.Next(1, 256);
                //testPass = testPass + Convert.ToChar(charValue);

            }
            // Console.WriteLine(testPass);





            //PASSWORD LOCK
            Console.WriteLine("Enter desired password: ");
            string userInput = Console.ReadLine();
            int inputLength = userInput.Length;
            char[] Input = userInput.ToCharArray();
            char[] Cipher = new char[128];
            int y = 0;
            int lim1 = 0;
            int lim2 = 0;
            int lim3 = 0;
            foreach (char help in Input)
            {
                if (y <= 4)
                {
                    lim1 = lim1 + Convert.ToInt32(help);
                }
                else if (y <= 6)
                {
                    lim2 = lim2 + Convert.ToInt32(help);
                }
                else
                {
                    lim3 = lim3 + Convert.ToInt32(help);
                }
                y++;
            }
            int c = 0;
            foreach (char boredom in privatekey)
            {
                int value = Convert.ToInt32(boredom) + (lim1 * 5) - (lim2) + lim3;
                //MATH

                Cipher[c] = Convert.ToChar(value);

                c++;

            }
            string cipher = new string(Cipher);





            StreamWriter write = new StreamWriter(location, false, Encoding.UTF8);
            write.Write(result);
            write.Write(delimiter);
            write.Write(cipher);
            write.Close();
            Console.WriteLine("Encryption Complete.");

        }
    }
}
