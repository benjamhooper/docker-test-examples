using Greet;
using System;

namespace learnacr
{
    class Program
    {
        static void Main(string[] args)
        {
	        var gr = new GreetMe();
            Console.WriteLine(gr.Greet("Container World"));
        }
    }
}
