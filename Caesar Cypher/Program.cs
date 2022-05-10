using System;
using System.IO; //need this to read from and write to files

namespace Caesar_Cypher
{
    class Program
    {
        static void Main(string[] args)
        {
            //declares and defines strings used for file names and paths used in reading and writing
            string _filePath = @"C:/ciphers/";
            string _inputFileName = "CipherInput.txt";
            string _outputFileName = "CipherOutput.txt";

            //declares strings used to store deciphered and un-deciphered code 
            string _fileContent;
            string _decipheredText;

            //the number used to decide how far along a cipher will shift
            int _shiftNumber;

            //The string _fileContent will equal the return value of the function LoadFileText()
            //LoadFileText() uses the _filePath and _inputFileName variables
            _fileContent = LoadFileText(_filePath, _inputFileName);

            //The int _shiftNumber will equal the return value of the function GetValidShiftNumber()
            _shiftNumber = GetValidShiftNumber();

            //The string _decipheredText will equal the return value of the function DecipherText()
            //DecipherText() uses the _fileContent and _shiftNumber variables
            _decipheredText = DecipherText(_fileContent, _shiftNumber);

            //Runs the SaveFileText() function which uses the _filePath, _outputFileName and _decipheredText variables
            SaveFileText(_filePath, _outputFileName, _decipheredText);




            //LoadFileText() is a function the uses a file name and path to return text form the chosen file
            string LoadFileText(string path, string name)
            {
                //creates a variable to store file text
                string text;
                
                //reads text from a file using the file path and name, then stores it in the text variable
                text = File.ReadAllText(path + name);

                //returns the value of the text variable
                return text;
            }

            //GetValidShiftNumber() get's a number from the user, validates it and returns it as an int variable
            int GetValidShiftNumber()
            {
                //Declares an int to store the input number
                int input;

                //Prompts user to enter shift value
                Console.WriteLine("Welcome to Caesar Cypher. Please enter the shift value below: \n");

                //Uses a while loop that runs until a valid input that can be converted into a number is recieved
                while(!int.TryParse(Console.ReadLine(), out input)) Console.WriteLine("Input invalid please try again");

                //A while Loop that runs until the input number is between -25 and 25
                while(input < -25 || input > 25)
                {
                    //if the input is less than -25 then add 26 to input
                    if (input < -25) input += 26;
                    //else -26 from input
                    else input -= 26;
                }

                //return valid input number
                return input;
            }

            //DecipherText() takes the text from the file and the valid user input number and
            //uses it to return the deciphered text
            string DecipherText(string inputText, int shiftNumber)
            {
                //Declares a string to use as an output and gives it an initial value so that characters can be added to it
                string outputText = "";

                //Creates a for loop to go through every character in the inputText and convert it using the shift number into a new character
                //that can now be added to the output text. If it's not a valid character it's added to the output vithout conversion.
                for(int character = 0; character < inputText.Length; character++)
                {
                    // If character is in range of upper case 
                    if(inputText[character] >= 65 && inputText[character] <= 90)
                    {
                        //if character number plus shift number is outside of range then correct it and add to output
                        if((inputText[character] + shiftNumber) < 65)
                        {
                            outputText += (char)((inputText[character] + shiftNumber) + 26);
                        }
                        else if((inputText[character] + shiftNumber) > 90)
                        {
                            outputText += (char)((inputText[character] + shiftNumber) - 26);
                        }
                        //Else add character that is the sum of both to output
                        else
                        {
                            outputText += (char)(inputText[character] + shiftNumber);
                        }                           
                    }
                    // If character is in range of lower case
                    else if (inputText[character] >= 97 && inputText[character] <= 122)
                    {
                        //if character number plus shift number is outside of range then correct it and add to output
                        if ((inputText[character] + shiftNumber) < 97)
                        {
                            outputText += (char)((inputText[character] + shiftNumber) + 26);
                        }
                        else if ((inputText[character] + shiftNumber) > 122)
                        {
                            outputText += (char)((inputText[character] + shiftNumber) - 26);
                        }
                        //Else add character that is the sum of both to output
                        else
                        {
                            outputText += (char)(inputText[character] + shiftNumber);
                        }
                    }
                    //else since character is not a letter just add it to output
                    else
                    {
                        outputText += inputText[character];
                    }
                }
                
                //Return the output
                return outputText;
            }

            //SaveFileText() uses a file name, path, and a string of text
            //It uses this info to save the string of text to the file of the name and path specified
            void SaveFileText(string path, string name, string text)
            {
                //Basically does what I already said but I made it a function because it looks nicer this way
                File.WriteAllText(path + name, text);
            }
        }
    }
}
