using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Arcade_app
{
    internal class Program
    {
        static void ApplicantDataEntry(string filepath)
        {

            bool enter = true;
            do
            {

            } while (enter ==true);
        }

        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory();
            List<string> filepatharr = new List<string>(filePath.Split('\\'));

            Console.WriteLine(filePath);

            for (int i = 0; i < filepatharr.Count;i++)
            {
                if (filepatharr[i] == ("Arcade_app"))
                {
                    filepatharr[i + 1] = "ApplicantData.txt";
                    filepatharr.Remove("Debug");
                    break;
                }

            }
            
            foreach (var path in filepatharr)
            {
                Console.WriteLine(path);
            }
            filePath = string.Join("\\", filepatharr);

            Console.ReadKey();
        }
    }
}
