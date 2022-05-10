using System;
using System.IO;

namespace Csv_Test
{
    class Program
    {


        


        static void Main(string[] args)
        {

            int _numberOfEvents = 8;

            string _filePath = "../";
            string _fileName = @"GenericEvents.csv";
            
            string[,] _storage = new string[8, 26];


            string[] _input = File.ReadAllLines(_filePath + _fileName);
            


            for (int line = 1; line <= _numberOfEvents; line ++)
            {
                int cell = 0;                

                for (int character = 0; character < _input[line].Length; character++)
                {
                    if (_input[line][character] == ',')
                    {
                        cell++;
                    }
                    else if (_input[line][character] == '@')
                    {
                        _storage[line - 1, cell] += ',';
                    }
                    else if (_input[line][character] == '%')
                    {
                        _storage[line - 1, cell] += '"';
                    }
                    else 
                    {
                        _storage[line - 1, cell] += _input[line][character];
                    }

                }
                
            }

            Console.WriteLine(_storage[4, 3]);
        }
    }
}
