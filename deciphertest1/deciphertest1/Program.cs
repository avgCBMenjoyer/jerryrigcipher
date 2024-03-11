using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace deciphertest1
{
    class Program
    {
        static void Main(string[] args)
        {
            char delimiter = Convert.ToChar(65300); //Separating char

            //READ FILE
            
            string location = "";
            string file = "";
            bool exception = false;
            while (exception != true)
            {
                try
                {
                    Console.WriteLine("Enter file path of encrypted text file: ");
                    location = Console.ReadLine();
                    StreamReader read = new StreamReader(location);
                    file = read.ReadToEnd();
                    read.Close();
                    exception = true;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine("File path incorrect.");
                    exception = false;
                }
                catch(System.ArgumentException ex)
                {
                    Console.WriteLine("File path incorrect.");
                    exception = false;
                }
            }
            string[] division = file.Split(delimiter);
            string publicKey = division[1];
            //int keycount = publicKey.Length;
            //Console.WriteLine(keycount);
            string cipher = division[0];
            char[] PublicKeyArray = publicKey.ToCharArray();
            char[] PrivateKeyArray = new char[128];
            int textLength = division[0].Length;
            char[] cleartext = new char[textLength];


            bool valid = false;
            int enterLimit = 10;
            while (valid != true && enterLimit > 0)    //VALIDATION LOOP
            {
                bool overflowFlag = true;
                while (overflowFlag != false)
                {
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();
                    char[] Password = password.ToCharArray();
                    int y = 0;
                    int lim1 = 0;
                    int lim2 = 0;
                    int lim3 = 0;
                    foreach (char help in Password)
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
                    try
                    {
                        foreach (char boredom in PublicKeyArray)
                        {
                            int value = Convert.ToInt32(boredom) - (lim1 * 5) + (lim2) - lim3;
                            //MATH

                            PrivateKeyArray[c] = Convert.ToChar(value);

                            c++;
                            overflowFlag = false;
                        }






                        //DECRYPTION OF FILE

                        char[] Cipher = cipher.ToCharArray();

                        int j = 0; //Array count
                        int i = 0;
                        foreach (char item in Cipher)
                        {
                            int itemNumber = Convert.ToInt32(item);
                            //MATH
                            int scrambleValue = itemNumber - PrivateKeyArray[j] + PrivateKeyArray[j + 1] - PrivateKeyArray[j + 2] - PrivateKeyArray[j + 3] - 2 * (PrivateKeyArray[j + 4]) - PrivateKeyArray[j + 5] - PrivateKeyArray[j + 6] - PrivateKeyArray[j + 7] - 2 * (PrivateKeyArray[j + 2]);




                            cleartext[i] = Convert.ToChar(scrambleValue);
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
                    }
                    catch (System.OverflowException ex)
                    {
                        Console.WriteLine("Invalid password.");
                        enterLimit = enterLimit - 1;
                        Console.WriteLine("After " + enterLimit + " more attempts, the data will be destroyed.");
                        overflowFlag = true;

                    }

                }

                string check = "";
                int checkLimit = 0;
                if (textLength < 15)
                {
                    checkLimit = textLength;
                }
                else
                {
                    checkLimit = 15;
                }
                Console.WriteLine("As the password validation is not 100% accurate, the user has to check whether the decrypted data is not scrambled.");
                Console.WriteLine(checkLimit + " CHARACTER CHECK");
                for (int l = 0; l < checkLimit; l++)
                {
                    check = check + cleartext[l];
                }
                Console.WriteLine("CLEARTEXT: " + check);

                Console.WriteLine("Is the data clear? (Y/N)");
                string response = Console.ReadLine();
            
                if (response.ToUpper() == "Y")
                {
                    valid = true;
                }
                
                else
                {
                    enterLimit=enterLimit-1;
                    Console.WriteLine("After "+enterLimit+" more attempts, the data will be destroyed.");
                }
                
            }
            StreamWriter sw = new StreamWriter(location, false, Encoding.UTF8);
            if (enterLimit == 0)
            {
                sw.Write(" ");
                Console.WriteLine("The data has been destroyed.");
            }
            else
            {
                string ClearText = new string(cleartext);
                sw.Write(ClearText);
                Console.WriteLine("Decryption Complete.");
            }

            sw.Close();
        }
    }
}
