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
        public static void CreateEmptyFile(string path)
        {
            string tempFilePath = Path.Combine(Path.GetDirectoryName(path),
                Guid.NewGuid().ToString());
            using (File.Create(tempFilePath)) { }
            File.Move(tempFilePath, path);
        }
        static void Main(string[] args)
        {
            string apiKey = "60b8910e318a330b62f58a27";
            string dataBaseName = "mylicenseserver-0137";
            string collection = "license";
            string filename = @"C:\testLicense";
            
            string userLicense;
            License myLisence = new License(dataBaseName, collection, apiKey);
            Console.WriteLine("License Manager");
            while (true)
            {
               
                Console.WriteLine("please enter your license");
                userLicense = Console.ReadLine();
                if (myLisence.CheckLicense(userLicense))
                {
                    CreateEmptyFile(filename);
                    Console.WriteLine("your product acitve succeslly");
                    break;
                }
            }
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
