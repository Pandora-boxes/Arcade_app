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
        static void ApplicantDataEntry(string filepath)                             //This is the Method of etering the new applicant data into a .txt file
        {
            string applicantName, applicantAge, applicantHighScoreRank, applicantStartDate, applicantPizzaTotal,
                applicantBowlHighScore, applicantEmploy, applicantSlushPuppyPref, applicantSlushPuppyTotal; 

            string applicantData;
            List<string> applicantDataArr = new List<string>();                     //list to hold the data as its being entered
            List<string> applicantDataArrEtry = new List<string>();                 //list to hold all the data from the txt file and the properly formatted new data
            bool formatCorrect=false;
            bool enter = true;

            foreach (string line in File.ReadAllLines(filepath))
            {
                applicantDataArrEtry.Add(line);                                        //to get all data already in the txt file into the applicantDataArrEtry list
            }
                                                                                          //a do while loop to contain the user so that if they want to repeat the data
                                                                                        //entry then they can with a simple bool loop
            do
            {
                applicantDataArr.Clear();
                Console.WriteLine("What is the applicant's name?");
                applicantName = Console.ReadLine();
                applicantDataArr.Add(applicantName);
                formatCorrect = false;

                Console.WriteLine("What is the applicant's age?");
                while (formatCorrect == false)
                {
                    applicantAge = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantAge, out int Age);            //A tryParse into a throwaway variable like age to
                                                                                        //check if the string can be properly formatted into it's intended data type
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write an age.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantAge);
                        
                }                                                                       //all of these while loops are for format checking
                                                                                        //so that if the user gets the data format wrong they can redo the data entry

                formatCorrect = false;
                Console.WriteLine("What is the applicant's high score rank?");
                while (formatCorrect == false)
                { 
                    applicantHighScoreRank = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantHighScoreRank, out int HighScoreRank);
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write an number rank.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantHighScoreRank);
                }

                formatCorrect = false;
                Console.WriteLine("What is the applicant's start date?: mm/dd/yyy");
                while (formatCorrect == false)
                {
                    applicantStartDate = Console.ReadLine();
                    formatCorrect = DateTime.TryParse(applicantStartDate, out DateTime StartDate);
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write a date.Try again");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantStartDate);
                }

                formatCorrect = false;
                Console.WriteLine("What is the applicant's employment status?" +
                    " If the applicant is under 18 then use the parents employment status." +
                    "\nWrite true for employed or false for unemployed");
                while (formatCorrect == false)
                {
                    applicantEmploy = Console.ReadLine();
                    formatCorrect = bool.TryParse(applicantEmploy, out bool Employ);
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write true or false.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantEmploy);
                }

                Console.WriteLine("What is the applicant's favorite slush-puppy flavor?");
                applicantSlushPuppyPref = Console.ReadLine();
                applicantDataArr.Add(applicantSlushPuppyPref);

                formatCorrect = false;
                Console.WriteLine("How many sulsh-puppies has the applicant had since first visit?");
                while (formatCorrect == false)
                {
                    applicantSlushPuppyTotal = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantSlushPuppyTotal, out int SlushPuppyTotal);
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write an number. Try again.");
                    else if(formatCorrect == true)
                        applicantDataArr.Add(applicantSlushPuppyTotal);
                }

                formatCorrect = false;
                Console.WriteLine("What is the applicant's bowling high score rank?");
                while (formatCorrect == false)
                {
                    applicantBowlHighScore = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantBowlHighScore, out int BowlHighScore);
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write an number rank.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantBowlHighScore);
                }

                formatCorrect = false;
                Console.WriteLine("How many pizzas has the applicant eaten since first visit?");
                while (formatCorrect == false)
                {
                    applicantPizzaTotal = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantPizzaTotal, out int Total);
                    if (formatCorrect == false)
                        Console.WriteLine("The format is wrong you need to write an number.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantPizzaTotal);
                }
              

                applicantData = string.Join(",", applicantDataArr);                                 //This makes the Entered data into a combined string with a "," seperating them
               
                applicantDataArrEtry.Add(applicantData);                                            //adds the combined string into the next entry of the list to be written into the txt file
                Console.WriteLine("Do you still want to add anymore applicants? Y for yes and N for no");
                string choice = Console.ReadLine();
                if (choice == "N")
                    enter = false;
                File.WriteAllLines(filepath, applicantDataArrEtry);
                Console.Clear();

            } while (enter == true);
            
        }

        // Menu creation
        enum Menu
        {
            CaptureDetails = 1,
            CheckTokenQualification,
            Exit
        }

        static void Main(string[] args)
        {
            // Perpetual loop to keep program running unless choosing exit
            while (true)
            {
                // Choosing from menu
                Console.WriteLine("What would you like to do?" +
                    "\nType the corrosponding number" +
                    "\n1: Capture details" +
                    "\n2: See customer token qualification" +
                    "\n3: Exit program");

                // Making sure user input is valid
                if (!Enum.TryParse(Console.ReadLine(), out Menu optionChosen) || !Enum.IsDefined(typeof(Menu), optionChosen))
                {
                    Console.WriteLine("Invalid option, try again");
                    continue;
                }

                //Exiting program
                if (optionChosen == Menu.Exit)
                {
                    break;
                }

                // Switch case to use the menu option chosen
            }


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

            filePath = string.Join("\\", filepatharr);

            Console.ReadKey();
        }
    }
}
