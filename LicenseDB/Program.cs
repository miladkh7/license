using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseDB;
using System.IO;
namespace LicenseDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "60b8910e318a330b62f58a27";
            string dataBaseName = "mylicenseserver-0137";
            string collection = "license";
            string filename = @"C:\Users\Milad\Desktop\testLi";
            string userLicense;
            License myLisence = new License(dataBaseName, collection, apiKey);

            Console.WriteLine("License Manager");
            Console.WriteLine("please enter your license");
            userLicense = Console.ReadLine();
            if (myLisence.CheckLicense(userLicense))
            {
                using (File.Create(filename)) ;
                Console.WriteLine("your product acitve succeslly");
            }
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
