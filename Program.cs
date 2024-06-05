﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Arcade_app
{

    internal class Program
    {

        static int MonthCalc(string startdate, DateTime now)
        {
            string tempstringNow = DateTime.Now.ToString();
            string[] nowArray = tempstringNow.Split('/', ' ');
            string tempstringJoin = startdate;
            string[] joinArray = tempstringJoin.Split('/');
            int monthNow = int.Parse(nowArray[1]);
            int monthStart = int.Parse(joinArray[0]);
            int yearNow = int.Parse(nowArray[0]);
            int yearStart = int.Parse(joinArray[3]);
            int yearDiff = yearNow - yearStart;
            int monthDiff = monthStart - monthStart;
            int yearToMonth = yearDiff * 12;
            int totalMonths = yearToMonth + monthDiff;
            return totalMonths;
        }
        static string ScoreCheck(int searchCustomerAge, string searchCustomerName, string[] ApplArr, int avgHS)
        {

            if (ApplArr[0].ToLower() == searchCustomerName.ToLower() && int.Parse(ApplArr[1]) == searchCustomerAge)
            {
                string display = $"name: {searchCustomerName}" +
                "\n" + $"Age: {searchCustomerAge}" +
                "\n" + $"Your current high score rank: {ApplArr[2]}" +
                "\n " + "=======================================" +
                "\n" + $"Your current bowling high score is: {ApplArr[5]}" +
                "\n=======================================" +
                "\n" + $"Your average high score is: {avgHS}";
                return display;
            }
            else
            {
                return "Either the name and/or age in incorrect or the customer is not registered";
            }
        }
        static string Reader(string filepath)
        {

            string name;
            int age;
            int highScoreRank;
            string startDateAsLoyalCustomer;
            int numOfPizzasSinceFirstVisit;
            int bowlingHS;
            bool isEmployed;
            string favoriteSlushieFlavour;
            int numOfSlushiesSinceFirstVisit;
            int avgScore;
            string[] tempArray;
            List<string> tempList = new List<string>();
            bool applicationApproved = true;
            string successOutput = "Customers that qualify for Credit:";
            DateTime today = DateTime.Now;


            while (filepath != null)
            {
                tempArray = filepath.Split(',');

                name = tempArray[0];
                age = int.Parse(tempArray[1]);
                highScoreRank = int.Parse(tempArray[2]);
                startDateAsLoyalCustomer = tempArray[3];
                numOfPizzasSinceFirstVisit = int.Parse(tempArray[4]);
                bowlingHS = int.Parse(tempArray[5]);
                isEmployed = bool.Parse(tempArray[6]);
                favoriteSlushieFlavour = tempArray[7];
                numOfSlushiesSinceFirstVisit = int.Parse(tempArray[8]);
                avgScore = (bowlingHS + highScoreRank) / 2;
                int loyalCustomerMonths = MonthCalc(startDateAsLoyalCustomer, today);


                //condition cheacks
                if (isEmployed == false)
                {
                    applicationApproved = false;
                }
                if (applicationApproved == true && loyalCustomerMonths < 24)
                {
                    applicationApproved = false;
                }
                if (applicationApproved == true)
                {
                    if (highScoreRank <= 2000 || bowlingHS <= 1500 || avgScore <= 1200)
                    {
                        applicationApproved = false;
                    }
                }
                if (applicationApproved == true && numOfPizzasSinceFirstVisit / loyalCustomerMonths < 3)
                {
                    applicationApproved = false;
                }
                if (applicationApproved == true && numOfSlushiesSinceFirstVisit / loyalCustomerMonths < 4)
                {
                    applicationApproved = false;
                }
                if (applicationApproved == true && favoriteSlushieFlavour == "Gooey Gulp Galore")
                {
                    applicationApproved = false;
                }
                if (applicationApproved == true)
                {


                    string successfulApp = "name: " + name + " age: " + age.ToString() + " high score rank: " + highScoreRank.ToString() + "\n" +
                    "Bowling high score: " + bowlingHS.ToString() + " Average score: " + avgScore.ToString() + "\n" +
                    "Start date as loyal customer: " + startDateAsLoyalCustomer + "\n" +
                    "Number of pizzas since first visit: " + numOfPizzasSinceFirstVisit.ToString() +
                    " Number of Slush-puppys since first visit: " + numOfSlushiesSinceFirstVisit.ToString() + "\n" +
                    "Preffered flavour Sluch-puppy: " + favoriteSlushieFlavour + "\n\n\n";

                    tempList.Add(successOutput);


                }



                // failed counterpart



                // if you want to search for an applicant

                int searchCustomerAge;
                string searchCustomerName;
                Console.WriteLine("What is the customer's name?:");
                searchCustomerName = Console.ReadLine();
                Console.WriteLine("How old is the Customer?:");
                searchCustomerAge = int.Parse(Console.ReadLine());
                Console.Write(ScoreCheck(searchCustomerAge, searchCustomerName, tempArray, avgScore));

            }


            return "the text file is empty";
        }
        


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

                Console.WriteLine("\nWhat is the applicant's age?");
                while (formatCorrect == false)
                {
                    applicantAge = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantAge, out int Age);            //A tryParse into a throwaway variable like age to
                                                                                        //check if the string can be properly formatted into it's intended data type
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write an age. Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantAge);
                        
                }                                                                       //all of these while loops are for format checking
                                                                                        //so that if the user gets the data format wrong they can redo the data entry

                formatCorrect = false;
                Console.WriteLine("\nWhat is the applicant's high score rank?");
                while (formatCorrect == false)
                { 
                    applicantHighScoreRank = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantHighScoreRank, out int HighScoreRank);
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write an number rank.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantHighScoreRank);
                }

                formatCorrect = false;
                Console.WriteLine("\nWhat is the applicant's start date?: mm/dd/yyyy");
                while (formatCorrect == false)
                {
                    applicantStartDate = Console.ReadLine();
                    formatCorrect = DateTime.TryParse(applicantStartDate, out DateTime StartDate);
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write a date.Try again");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantStartDate);
                }

                formatCorrect = false;
                Console.WriteLine("\nWhat is the applicant's employment status?" +
                    " If the applicant is under 18 then use the parents employment status." +
                    "\nWrite true for employed or false for unemployed");
                while (formatCorrect == false)
                {
                    applicantEmploy = Console.ReadLine();
                    formatCorrect = bool.TryParse(applicantEmploy, out bool Employ);
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write true or false.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantEmploy);
                }

                Console.WriteLine("\nWhat is the applicant's favorite slush-puppy flavor?");
                applicantSlushPuppyPref = Console.ReadLine();
                applicantDataArr.Add(applicantSlushPuppyPref);

                formatCorrect = false;
                Console.WriteLine("\nHow many sulsh-puppies has the applicant had since first visit?");
                while (formatCorrect == false)
                {
                    applicantSlushPuppyTotal = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantSlushPuppyTotal, out int SlushPuppyTotal);
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write an number. Try again.");
                    else if(formatCorrect == true)
                        applicantDataArr.Add(applicantSlushPuppyTotal);
                }

                formatCorrect = false;
                Console.WriteLine("\nWhat is the applicant's bowling high score rank?");
                while (formatCorrect == false)
                {
                    applicantBowlHighScore = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantBowlHighScore, out int BowlHighScore);
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write an number rank.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantBowlHighScore);
                }

                formatCorrect = false;
                Console.WriteLine("\nHow many pizzas has the applicant eaten since first visit?");
                while (formatCorrect == false)
                {
                    applicantPizzaTotal = Console.ReadLine();
                    formatCorrect = int.TryParse(applicantPizzaTotal, out int Total);
                    if (formatCorrect == false)
                        Console.WriteLine("\nThe format is wrong you need to write an number.Try again.");
                    else if (formatCorrect == true)
                        applicantDataArr.Add(applicantPizzaTotal);
                }
              

                applicantData = string.Join(",", applicantDataArr);                                 //This makes the Entered data into a combined string with a "," seperating them
               
                applicantDataArrEtry.Add(applicantData);                                            //adds the combined string into the next entry of the list to be written into the txt file
                Console.WriteLine("\nDo you still want to add anymore applicants? Y for yes and N for no");
                string choice = Console.ReadLine();
                string choiceUpper = choice.ToUpper();
                if (choiceUpper == "N")
                    enter = false;
                File.WriteAllLines(filepath, applicantDataArrEtry);
                Console.Clear();

            } while (enter == true);
            
        }

        // Menu creation
        enum Menu
        {
            Capture_details = 1,
            View_token_eligibility,
            Exit_the_program
        }

        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory();
            List<string> filepatharr = new List<string>(filePath.Split('\\'));

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
            // Perpetual loop to keep program running unless choosing exit
            while (true)
            {
                Console.WriteLine("=======================================================================================================================");

                // Choosing from menu
                Console.WriteLine("\nMenu:");
                foreach (Menu option in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{(int)option}. {option.ToString().Replace('_', ' ')}");
                }

                // Making sure user input is valid
                if (!Enum.TryParse(Console.ReadLine(), out Menu optionChosen) || !Enum.IsDefined(typeof(Menu), optionChosen))
                {
                    Console.WriteLine("Invalid option, try again");
                    continue;
                }

                //Exiting program
                if (optionChosen == Menu.Exit_the_program)
                {
                    Console.WriteLine("Thank you, have a nice day");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }

                Console.WriteLine("=======================================================================================================================");

                // Switch case to use the menu option chosen
                switch (optionChosen)
                {
                    case Menu.Capture_details:
                        ApplicantDataEntry(filePath);
                        break;
                    case Menu.View_token_eligibility:
                        break;
                }
            }
        }
    }
}
