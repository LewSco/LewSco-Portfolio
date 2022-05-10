using System;

namespace Fibonacci_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            int loop_number, x = 0, y = 1, z;

            Console.WriteLine("Welcome to Fibonacci Loop");
            Console.Write("Please enter how many Fibonacci numbers you would like?:"); 
            string u_i = Console.ReadLine();

            int.TryParse(u_i, out loop_number);

            while(loop_number <= 0 || !int.TryParse(u_i, out loop_number))
            {
                Console.WriteLine("Input invalid, please try again");
                u_i = Console.ReadLine();
                int.TryParse(u_i, out loop_number);
            }
            
            if(loop_number == 1)
            {
                Console.WriteLine("Fibonacci numbers: 0");
            }
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
            
            Console.WriteLine("");
            Console.WriteLine("Thank you for using Fibonacci Loop");
        }
    }
}
