using System;

namespace Fibonacci_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaring int variables
            int loop_number, x = 0, y = 1, z;
            
            //Welcomes User then gets their input
            Console.WriteLine("Welcome to Fibonacci Loop");
            Console.Write("Please enter how many Fibonacci numbers you would like?:"); 
            string u_i = Console.ReadLine();
            
            //Changes input string into loop number
            int.TryParse(u_i, out loop_number);
            
            //checks if input is valid and loops until it recieves a valid loop number
            while(loop_number <= 0 || !int.TryParse(u_i, out loop_number))
            {
                Console.WriteLine("Input invalid, please try again");
                u_i = Console.ReadLine();
                int.TryParse(u_i, out loop_number);
            }
            
            //If the loop number only equals 1 then just display 0 and no more
            if(loop_number == 1)
            {
                Console.WriteLine("Fibonacci numbers: 0");
            }
            //Otherwise perform the loop and print the new number every time for the amount of times entered by the user
            else
            {
                Console.Write("Fibonacci numbers: " + x);

                for (int loop = 2; loop <= loop_number; loop++)
                {
                    Console.Write(", " + y);
                    z = x + y;
                    x = y;
                    y = z;
                }
            }
            
            //Thank the User
            Console.WriteLine("");
            Console.WriteLine("Thank you for using Fibonacci Loop");
        }
    }
}
