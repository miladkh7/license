using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseDB;
namespace LicenseDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "5e0c5fac2b88e41892f7dd511abb5469b1a09";
            string dataBaseName = "gholami-c537";
            string collection = "license";
            License myLisence = new License(dataBaseName, collection, apiKey);
            //60b5e06f6a5d621100014665
            myLisence.CheckHardWareID(dataBaseName, collection,"123");
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");


            myLisence.RegisterNewLisence("my test");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
