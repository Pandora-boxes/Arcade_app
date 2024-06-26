﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Security.Policy;
using System.Web;
using System.CodeDom.Compiler;

//Fourie Jooste       (602075)
//Pandora Greyling    (602369)
//Matthew Bisset      (602166)

namespace Arcade_app
{

    internal class Program
    {

        static int MonthCalc(string startdate, DateTime now)          // calculating the total monthes      
        {
            string tempstringNow = DateTime.Now.ToString();
            string[] nowArray = tempstringNow.Split('/', ' ');
            string tempstringJoin = startdate;
            string[] joinArray = tempstringJoin.Split('/');
            int monthNow = int.Parse(nowArray[1]);
            int monthStart = int.Parse(joinArray[0]);
            int yearNow = int.Parse(nowArray[0]);
            int yearStart = int.Parse(joinArray[2]);
            int yearDiff = yearNow - yearStart;
            int monthDiff = monthStart - monthStart;
            int yearToMonth = yearDiff * 12;
            int totalMonths = yearToMonth + monthDiff;
            return totalMonths;
        }
        static string ScoreCheck( List<string> ApplArr)     // Searching for a specific person
        {
            int searchCustomerAge;
            string searchCustomerName;
            Console.WriteLine("What is the customer's name?:");
            searchCustomerName = Console.ReadLine();
            Console.WriteLine("How old is the Customer?:");
            searchCustomerAge = int.Parse(Console.ReadLine());
            List<string> tempApplArr = new List<string>();



            foreach (string var in ApplArr)
            {
                List<string> array = new List<string>(var.Split(','));

                if (array[0].ToLower() == searchCustomerName.ToLower() && int.Parse(array[1]) == searchCustomerAge)
                {
                    string display = $"name: {searchCustomerName}" +
                    "\n" + $"Age: {searchCustomerAge}" +
                    "\n" + $"Your current high score rank: {array[2]}" +
                    "\n " + "=======================================" +
                    "\n" + $"Your current bowling high score is: {array[5]}" +
                    "\n=======================================" +
                    "\n" + $"Your average high score is: {(array[2]+ array[5])}";
                    
                    return display;
                    
                }             
                
            }
            return "Either the name and/or age in incorrect or the customer is not registered";
        }
        static void MaxMinAge(List<string> ApplArr)
        {
            int maxAge = 0;
            int minAge = 1000;
            int i = 0;
            foreach(string var in ApplArr)
            {
                i++;
                string[] tempArr = var.Split(',');
                int ApplAge = int.Parse(tempArr[1]);
                if(ApplAge > maxAge)
                {
                    maxAge = ApplAge;
                }
                if(ApplAge < minAge)
                {
                    minAge = ApplAge;
                }              
            }
            if (i == 0)
            {
                Console.WriteLine("The list of aplicants is empty please add users and try again");
            }
            else
            {
                Console.WriteLine("The youngest Customer is: {0}", minAge);
                Console.WriteLine("The oldest Customer is: {0}", maxAge);
            }

        } 
        static void Reader(List<string> applicantDataArr, List<string> successful , List<string> failed )

            //method for all the condition checks
        {
            Console.Clear();

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
            
            List<string> tempList = new List<string>();
            
            string successOutput = "Customers that qualify for Credit:";
            DateTime today = DateTime.Now;

            foreach (string line in applicantDataArr)
            {

                tempList = new List<string>(line.Split(','));

                name = tempList[0];
                age = int.Parse(tempList[1]);
                highScoreRank = int.Parse(tempList[2]);
                startDateAsLoyalCustomer = tempList[3];
                isEmployed = bool.Parse(tempList[4]);
                favoriteSlushieFlavour = tempList[5];
                numOfSlushiesSinceFirstVisit = int.Parse(tempList[6]);
                bowlingHS = int.Parse(tempList[7]);
                numOfPizzasSinceFirstVisit = int.Parse(tempList[8]);
                bool applicationApproved = true;

                avgScore = (bowlingHS + highScoreRank) / 2;
                int loyalCustomerMonths = MonthCalc(startDateAsLoyalCustomer, today);
                string successfulApp = "name: " + name + "\n" + "age: " + age.ToString() + "\n" +  " high score rank: " +
                    highScoreRank.ToString() + "\n" +"Bowling high score: " + bowlingHS.ToString() + "\n" + "Average score: " + avgScore.ToString() + "\n" +
                    "Start date as loyal customer: " + startDateAsLoyalCustomer + "\n" +
                    "Number of pizzas since first visit: " + numOfPizzasSinceFirstVisit.ToString() + "\n" +
                    "Number of Slush-puppys since first visit: " + numOfSlushiesSinceFirstVisit.ToString() + "\n" +
                    "Preffered flavour Sluch-puppy: " + favoriteSlushieFlavour + "\n\n\n";



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
                    successful.Add(successfulApp);
                }
                else
                {
                    failed.Add(successfulApp);
                }
            }
        }

        //Create a method that will calculate the average number of pizzas consumed per first visit.
        //• Make sure the data has been formatted appropriately.
        //• Your method should return only the average
        static double AveragePizza(List<string> dataArray)
        {
            int total = 0;
            foreach (string line in dataArray)                              //goes through each entry in the list to add the total of the pizzas consumed
            { 
                List<string> data = new List<string>(line.Split(','));
                total += int.Parse(data[8]);
            }
            if (dataArray.Count == 0 && total == 0)
            {
                return 0;
            }
            int average = total / dataArray.Count;                          //divides the total with the amount of entries in the array to get the avarage
            return average;
        }

        //Create a method to check if the applicant qualifies for a long-term loyalty award
        //If the applicant has been a loyal customer for 10 years, they receive an unlimited number of credits
        static string LongLoyaltyCheck(List<string> dataArray,string name, string age)  //method that returns a string value of true, false, or invalid for long loyalty 
        {
            string unlimited = null;
            string applicant = null;
            

            foreach (string line in dataArray)
            {
                if (line.Contains(age) && line.Contains(name))                              //checks the array for the certain applicant
                {   
                    applicant = line;
                    break;
                }
            }
            
            if (applicant == null)
            {
                unlimited = "invalid";                  //it returns invalid if the user isn't found in the sustem
                return unlimited;
            }

            List<string> data = new List<string>(applicant.Split(','));
            string startdate = data[3];
            int monthsTotal = MonthCalc(startdate,DateTime.Now);
            if (monthsTotal >= 120) 
            {
                unlimited = "true";                     //it returns true if the candadite qualifies
                return unlimited;
            }
            else
            {
                unlimited = "false";                    //it returns false if they aren't qualifying
                return unlimited;
            }            
        }

        static string ApplicantDataValidation()
        {

            string applicantName, applicantAge, applicantHighScoreRank, applicantStartDate, applicantPizzaTotal,
                applicantBowlHighScore, applicantEmploy, applicantSlushPuppyPref, applicantSlushPuppyTotal;

            string applicantData;
            List<string> applicantDataArr = new List<string>();
            bool formatCorrect = false;

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
                "\nY/N");
            string employ;
            while (formatCorrect == false)
            {
                applicantEmploy = Console.ReadLine();
                applicantEmploy = applicantEmploy.ToUpper();
                if (applicantEmploy == "N")
                {
                    employ = "false";
                    applicantDataArr.Add(employ);
                    formatCorrect = true;
                }
                else if (applicantEmploy == "Y")
                {
                    employ = "true";
                    applicantDataArr.Add(employ);
                    formatCorrect = true;
                }
                else
                {
                    Console.WriteLine("The format is wrong you need to write Y or N\nTry again:");
                }

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
                else if (formatCorrect == true)
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
            return applicantData;
        }

        static void ApplicantDataEntry(string filepath, List<string> applicantDataEtry)               //This is the Method of etering the new applicant data into a .txt file
        {
          
            string applicantData;
            List<string> applicantDataArr = new List<string>();                     //list to hold the data as its being entered
            
            bool enter = true;
           
                                                                                          //a do while loop to contain the user so that if they want to repeat the data
                                                                                        //entry then they can with a simple bool loop
            do
            {
                Console.Clear();
                applicantDataArr.Clear();

                applicantData = ApplicantDataValidation();
                applicantDataEtry.Add(applicantData);                                            //adds the combined string into the next entry of the list to be written into the txt file

                Console.WriteLine("\nDo you still want to add anymore applicants? (Y/N)");
                string choice = Console.ReadLine();
                string choiceUpper = choice.ToUpper();
                if (choiceUpper == "N")
                    enter = false;

            } while (enter == true);

            StartAnimation();
            File.WriteAllLines(filepath, applicantDataEtry);                                    //writes the applicants in the array and recently added into the file
            StopAnimation();
        }
        

        static string UpdateData(List<string> dataArray, string filepath, string name, string age)
        {
            for(int i = 0; i < dataArray.Count ; i++) 
            {
                List<string> lineArr = new List<string>(dataArray[i].Split(','));
                if (lineArr.Contains<string>(name) == true)
                    if (lineArr.Contains<string>(age) == true) 
                    {
                        string newData = ApplicantDataValidation();
                        dataArray[i] = newData;
                        StartAnimation();
                        File.WriteAllLines(filepath, dataArray);
                        StopAnimation() ;
                        return "The data has been updated sucessfully.";
                    }
            }
            return "The user hasn't been found in the data.";
        }

        // Menu creation
        enum Menu
        {
            Capture_details = 1,
            Update_details,
            View_token_eligibility,
            Exit_the_program
        }
        enum SubMenu
        {
            View_loyal_customers_that_are_eligable_for_credit = 1,
            View_loyal_customers_that_are_ineligabe_for_credit,
            View_the_score_of_a_customer,
            View_average_pizzas_consumed,
            View_long_term_loyalty_eligability,
            View_youngest_and_oldest_customer,
            Return_to_main_menu,
            Exit_the_program
        }

        private static bool stopAnimatioBool = false;

        private static Thread animationThread;

        static void Main(string[] args)
        {
            Welcome();
            string filePath = Directory.GetCurrentDirectory();                          //gets the directory of the running file and puts it into filepath as a string
            List<string> filepatharr = new List<string>(filePath.Split('\\'));          //makes filepath into a list since the directory could be any length

            Console.Clear();

            for (int i = 0; i < filepatharr.Count;i++)                                  //runs through the filepath array to edit the cells
            {                                                                           //to make it properly point to the file
                if (filepatharr[i] == "Arcade_app")                                     //checks for the start of the Arcade_App file
                {
                    filepatharr[i + 1] = "ApplicantData.txt";                           //changes the next cell after the start to the file name
                    filepatharr.Remove("Debug");                                        //removes the debug from the directory
                    break;
                }

            }

            filePath = string.Join("\\", filepatharr);                                  //joins the file path array back into a string amking it accessable by functions
            List<string> applicantDataArr = new List<string>();                         //array for storing the values in the txt file

            foreach (string line in File.ReadAllLines(filePath))                        //runs through each line of the array and puts it into a array
            {
                applicantDataArr.Add(line);                                             //to get all data already in the txt file into the applicantDataArrEtry list
            }

            List<string>successful = new List<string>();
            List<string>failed = new List<string>();
            if (applicantDataArr.Count > 0)
            {
                Reader(applicantDataArr, successful, failed);
            }
            // Perpetual loop to keep program running unless choosing exit
            while (true)
            {
                // Choosing from menu
                Console.WriteLine("Main menu\nChoose a option:\n");
                foreach (Menu option in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{(int)option}. {option.ToString().Replace('_', ' ')}");
                }

                // Making sure user input is valid
                if (!Enum.TryParse(Console.ReadLine(), out Menu optionChosen) || !Enum.IsDefined(typeof(Menu), optionChosen))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid option, try again\n=========================\n");
                    continue;
                }

                //Exiting program
                if (optionChosen == Menu.Exit_the_program)
                {
                    Console.Clear();
                    ByeBye();
                }

                // Switch case to use the menu option chosen
                switch (optionChosen)
                {
                    case Menu.Capture_details:

                        
                        ApplicantDataEntry(filePath, applicantDataArr);
                        if (applicantDataArr.Count > 0)
                        {
                            Reader(applicantDataArr, successful, failed);
                        }
                        
                        continue;
                    case Menu.Update_details:
                        StartAnimation();
                        StopAnimation();

                        Console.WriteLine("What is the previous name of the applicant?");
                        string updateName = Console.ReadLine();

                        Console.WriteLine( "What is the previous age of the applicant?");
                        string updateAge = Console.ReadLine();

                        Console.WriteLine(UpdateData(applicantDataArr,filePath,updateName, updateAge));
                        break;
                    case Menu.View_token_eligibility:
                        Console.Clear();

                        bool subMenuBool = true;

                        while (subMenuBool == true)
                        {
                            //Displaying Sub Menu
                            Console.WriteLine("Menu\nChoose a option:\n");
                            foreach (SubMenu option in Enum.GetValues(typeof(SubMenu)))
                            {
                                Console.WriteLine($"{(int)option}. {option.ToString().Replace('_', ' ')}");
                            }

                            // Making sure user input is valid
                            if (!Enum.TryParse(Console.ReadLine(), out SubMenu subMenuOptionChosen) || !Enum.IsDefined(typeof(SubMenu), subMenuOptionChosen))
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid option, try again\n=========================\n");
                                continue;
                            }

                            //Switch for sub menu
                            switch (subMenuOptionChosen)
                            {
                                case SubMenu.View_loyal_customers_that_are_eligable_for_credit:
                                    Console.Clear();

                                    StartAnimation();
                                    StopAnimation();

                                    foreach (string line in successful)
                                    {
                                        Console.WriteLine(line);
                                    }
                                    continue;
                                case SubMenu.View_loyal_customers_that_are_ineligabe_for_credit:
                                    Console.Clear();

                                    StartAnimation();
                                    StopAnimation();

                                    foreach (string line in failed)
                                    {
                                        Console.WriteLine(line);
                                    }
                                    continue;
                                case SubMenu.View_the_score_of_a_customer:
                                    Console.Clear();

                                    StartAnimation();
                                    StopAnimation();

                                    Console.WriteLine(ScoreCheck(applicantDataArr));
                                    continue;
                                case SubMenu.View_average_pizzas_consumed:
                                    Console.Clear();
                                    StartAnimation();
                                    double avgPizza = Math.Round(AveragePizza(applicantDataArr),3);
                                    StopAnimation();
                                    Console.WriteLine($"The average amount of pizzas is: {avgPizza}");

                                    continue;
                                case SubMenu.View_long_term_loyalty_eligability:
                                    Console.Clear();

                                    Console.WriteLine("What is the name of the appliciant?");
                                    string name = Console.ReadLine();
                                    Console.WriteLine("What is the age of the appliciant?");
                                    string age = Console.ReadLine();
                                    StartAnimation();
                                    string longLoyalty = LongLoyaltyCheck(applicantDataArr,name,age);
                                    StopAnimation();
                                    if (longLoyalty == "true")
                                    {
                                        Console.WriteLine("This applicant is eligable for the Long Time Loyalty.");
                                        continue;
                                    }
                                    else if (longLoyalty == "false")
                                    {
                                        Console.WriteLine("This applicant is not eligable for the Long Time Loyalty.");
                                        continue;
                                    }
                                    else if(longLoyalty == "invalid")
                                    {
                                        Console.WriteLine("Either the name or age is wrong. Please try again.");
                                        continue;
                                    }
                                    continue;
                                case SubMenu.View_youngest_and_oldest_customer:
                                    Console.Clear();
                                    MaxMinAge(applicantDataArr);
                                    continue;
                                case SubMenu.Return_to_main_menu:
                                    Console.Clear();
                                    subMenuBool = false;
                                    break;
                                case SubMenu.Exit_the_program:
                                    Console.Clear();
                                    ByeBye();
                                    break;
                            }
                         
                        }
                        break;
                }
            }
        }

        static void StartAnimation()
        {
            if (animationThread != null && animationThread.IsAlive)
            {
                stopAnimatioBool = true;
                animationThread.Join();
            }

            stopAnimatioBool = false;
            animationThread = new Thread(thinkingAnimation);
            animationThread.Start();
        }

        static void StopAnimation()
        {
            stopAnimatioBool = true;
            if (animationThread != null)
            {
                animationThread.Join();
                animationThread = null;
            }
        }

        static void thinkingAnimation()
        {
            string[] animationFrames = { ">", "=>", "==>", "===>", "====>", "=====>", "======>", "=======>" };

            Random randomLoadTimes = new Random();

            int randomFrameIndex = randomLoadTimes.Next(4, 11);
            int animationFrameCounter = 0;


            for (animationFrameCounter = 0; animationFrameCounter < randomFrameIndex; animationFrameCounter++)
            {
                Console.Write($"\r{animationFrames[animationFrameCounter % animationFrames.Length]}");
                System.Threading.Thread.Sleep(100);
                Console.Clear();
            }
        }

        static void ByeBye ()
        {
            string[] pizzaJokes = { "Pizza perfect", "Life happens, pizza helps", "Cheese the day with pizza", "In queso emergency, eat pizza", "Don’t worry, be happy, eat pizza", "You’ve stolen a pizza my heart", "Tossing up some fun – pizza style", "Slice, slice, baby", "I’m feeling grate, thanks to pizza", "I’m on a roll with this pizza dough", "I’m having a pizza party in my mouth", "A pizza a day keeps the sadness away", "Slice to the chase – it’s pizza time", "You want a pizza me? Come and get it", "I knead pizza every hour of every day", "A good pizza pun always circles back", "In dough we crust, in pizza we believe", "I’m in a saucy mood – must be the pizza", "Crust me, I’m practically a pizza scholar", "Pizza: the official sponsor of happiness", "When life gives you pizzas, make a feast", "A slice of advice: never share your pizza", "A little slice of heaven: now that’s amore", "I’m saucing up my life, one pizza at a time", "I’m a pizzaterian. I survive on pizza alone", "This pizza slice is the yeast of my worries", "Saucy, cheesy, and downright easy", "You’ve got a pizzazz that just can’t be topped", "I’m a pizza enthusiast, I’m really crustworthy", "I like my puns how I like my pizza: extra cheesy" };

            Console.WriteLine(  @"   _______ _                 _                          " + "\r\n" +
                                @"  |__   __| |               | |                         " + "\r\n" +
                                @"     | |  | |__   __ _ _ __ | | __    _   _  ___  _   _ " + "\r\n" +
                                @"     | |  | '_ \ / _` | '_ \| |/ /   | | | |/ _ \| | | |" + "\r\n" +
                                @"     | |  | | | | (_| | | | |   <    | |_| | (_) | |_| |" + "\r\n" +
                                @"     |_|  |_| |_|\__,_|_| |_|_|\_\    \__, |\___/ \__,_|" + "\r\n" +
                                @"                                       __/ |            " + "\r\n" +
                                @"                                      |___/             ");

            System.Threading.Thread.Sleep(900);

            Console.WriteLine(  @"  _    _                                     _                  _             " + "\r\n" +
                                @" | |  | |                                   (_)                | |            " + "\r\n" +
                                @" | |__| | __ ___   _____      __ _     _ __  _  ___ ___      __| | __ _ _   _ " + "\r\n" + 
                                @" |  __  |/ _` \ \ / / _ \    / _` |   | '_ \| |/ __/ _ \    / _` |/ _` | | | |" + "\r\n" +
                                @" | |  | | (_| |\ V /  __/   | (_| |   | | | | | (_|  __/   | (_| | (_| | |_| |" + "\r\n" +
                                @" |_|  |_|\__,_| \_/ \___|    \__,_|   |_| |_|_|\___\___|    \__,_|\__,_|\__, |" + "\r\n" +
                                @"                                                                         __/ |" + "\r\n" +
                                @"                                                                        |___/ ");

            System.Threading.Thread.Sleep(1250);
            Console.Clear();

            Random randomNum = new Random();
            int randomArrIndex = randomNum.Next(0, 31);
            Console.WriteLine("");
            Console.WriteLine(pizzaJokes[randomArrIndex]);
;

            Console.WriteLine(  "                                        \r\n" +
                                "       ..::==============::..           \r\n" +
                                "   .:::=======================:::..     \r\n" +
                                "   ===-=**********#%%%%%%%#***++===--.  \r\n" +
                                "   -%%%%%###+=-++*#%%%%%%%%#++++***#+   \r\n" +
                                "    -##**##*=--+++%#**+#####++***#%#.   \r\n" +
                                "     =%#####+=-+*=####%###*++=+*#%*     \r\n" +
                                "      +%#*#*=#----==++**++-=--+*##      \r\n" +
                                "       +%%#***+-=*+=-:--:=--::==#:      \r\n" +
                                "        +%##***##*=-==+=-====+--.       \r\n" +
                                "         +#*###*#=-=++---==-==-.        \r\n" +
                                "          +######*+==+###%#**+:         \r\n" +
                                "           +*#***+**#%#####%%-          \r\n" +
                                "            +*+===+#####**#%=           \r\n" +
                                "             :::--=*##%%%%%=            \r\n" +
                                "             .---::-+###%#+             \r\n" +
                                "               **+==+++++=              \r\n" +
                                "               .--=---=++               \r\n" +
                                "                -=*+=+==-               \r\n" +
                                "                  +%%%%#-               \r\n" +
                                "                   +#**.                \r\n" +
                                "                    =*                  \r\n");

            System.Threading.Thread.Sleep(3500);
            Environment.Exit(0);
        }

        static void Welcome()
        {
            Console.WriteLine(  @" /$$      /$$           /$$                                            " + "\r\n" +
                                @"| $$  /$ | $$          | $$                                            " + "\r\n" +
                                @"| $$ /$$$| $$  /$$$$$$ | $$  /$$$$$$$  /$$$$$$  /$$$$$$/$$$$   /$$$$$$ " + "\r\n" +
                                @"| $$/$$ $$ $$ /$$__  $$| $$ /$$_____/ /$$__  $$| $$_  $$_  $$ /$$__  $$" + "\r\n" +
                                @"| $$$$_  $$$$| $$$$$$$$| $$| $$      | $$  \ $$| $$ \ $$ \ $$| $$$$$$$$" + "\r\n" +
                                @"| $$$/ \  $$$| $$_____/| $$| $$      | $$   |$$| $$ | $$ | $$| $$_____/" + "\r\n" +
                                @"| $$/   \  $$|  $$$$$$$| $$|  $$$$$$$|  $$$$$$/| $$ | $$ | $$| $$$$$$$"  + "\r\n" +
                                @"|__/     \__/ \_______/|__/ \_______/ \______/ |__/ |__/ |__/ \_____/"   + "\r\n");

            System.Threading.Thread.Sleep (500);

            Console.WriteLine(  @"    /$$              " + "\r\n" +
                                @"   | $$              " + "\r\n" +
                                @"  /$$$$$$    /$$$$$$ " + "\r\n" +
                                @" |_  $$_/   /$$__  $$" + "\r\n" +
                                @"   | $$    | $$  \ $$" + "\r\n" +
                                @"   | $$ /$$| $$  | $$" + "\r\n" +
                                @"   | $$$$/ |  $$$$$$/" + "\r\n" +
                                @"    \___/   \______/" + "\r\n");

            System.Threading.Thread.Sleep(500);

            Console.WriteLine(  @" /$$$$$$$$ /$$                " + "\r\n" +
                                @"|__  $$__/| $$                " + "\r\n" +
                                @"   | $$   | $$$$$$$   /$$$$$$ " + "\r\n" + 
                                @"   | $$   | $$__  $$ /$$__  $$" + "\r\n" +
                                @"   | $$   | $$  \ $$| $$$$$$$$" + "\r\n" + 
                                @"   | $$   | $$  | $$| $$_____/" + "\r\n" +
                                @"   | $$   | $$  | $$|  $$$$$$$" + "\r\n" + 
                                @"   |__/   |__/  |__/ \_______/" + "\r\n");
            System.Threading.Thread.Sleep(600);

            Console.Clear();

            System.Threading.Thread.Sleep(150);

            for(int loop =0; loop < 2; loop++)
            {
                Console.WriteLine(                                                                                                      "\r\n" +
                                    @" /$$$$$$$              /$$                                /$$$$$$  /$$ /$$                    " + "\r\n" +
                                    @"| $$__  $$            | $$                               /$$__  $$| $$|__/                    " + "\r\n" +
                                    @"| $$  \ $$  /$$$$$$  /$$$$$$    /$$$$$$   /$$$$$$       | $$  \__/| $$ /$$  /$$$$$$$  /$$$$$$ " + "\r\n" +
                                    @"| $$$$$$$/ /$$__  $$|_  $$_/   /$$__  $$ /$$__  $$      |  $$$$$$ | $$| $$ /$$_____/ /$$__  $$" + "\r\n" +
                                    @"| $$__  $$| $$$$$$$$  | $$    | $$  \__/| $$  \ $$       \____  $$| $$| $$| $$      | $$$$$$$$" + "\r\n" +
                                    @"| $$  \ $$| $$_____/  | $$ /$$| $$      | $$  | $$       /$$  \ $$| $$| $$| $$      | $$_____/" + "\r\n" +
                                    @"| $$  | $$|  $$$$$$$  |  $$$$/| $$      |  $$$$$$/      |  $$$$$$/| $$| $$|  $$$$$$$|  $$$$$$$" + "\r\n" +
                                    @"|__/  |__/ \_______/   \___/  |__/       \______/        \______/ |__/|__/ \_______/ \_______/");

                System.Threading.Thread.Sleep(500);
                Console.Clear();
                System.Threading.Thread.Sleep(100);
            }



            Console.WriteLine(                                                                                                      "\r\n" +
                                @" /$$$$$$$              /$$                                /$$$$$$  /$$ /$$                    " + "\r\n" +
                                @"| $$__  $$            | $$                               /$$__  $$| $$|__/                    " + "\r\n" +
                                @"| $$  \ $$  /$$$$$$  /$$$$$$    /$$$$$$   /$$$$$$       | $$  \__/| $$ /$$  /$$$$$$$  /$$$$$$ " + "\r\n" +
                                @"| $$$$$$$/ /$$__  $$|_  $$_/   /$$__  $$ /$$__  $$      |  $$$$$$ | $$| $$ /$$_____/ /$$__  $$" + "\r\n" +
                                @"| $$__  $$| $$$$$$$$  | $$    | $$  \__/| $$  \ $$       \____  $$| $$| $$| $$      | $$$$$$$$" + "\r\n" +
                                @"| $$  \ $$| $$_____/  | $$ /$$| $$      | $$  | $$       /$$  \ $$| $$| $$| $$      | $$_____/" + "\r\n" +
                                @"| $$  | $$|  $$$$$$$  |  $$$$/| $$      |  $$$$$$/      |  $$$$$$/| $$| $$|  $$$$$$$|  $$$$$$$" + "\r\n" +
                                @"|__/  |__/ \_______/   \___/  |__/       \______/        \______/ |__/|__/ \_______/ \_______/");

            System.Threading.Thread.Sleep(750);

            Console.WriteLine(                                                                                                            "\r\n" +
                                @" /$$                                     /$$   /$$                      /$$$$$$                     " + "\r\n" +
                                @"| $$                                    | $$  | $$                     /$$__  $$                    " + "\r\n" +
                                @"| $$        /$$$$$$  /$$   /$$  /$$$$$$ | $$ /$$$$$$   /$$   /$$      | $$  \ $$  /$$$$$$   /$$$$$$ " + "\r\n" +
                                @"| $$       /$$__  $$| $$  | $$ |____  $$| $$|_  $$_/  | $$  | $$      | $$$$$$$$ /$$__  $$ /$$__  $$" + "\r\n" +
                                @"| $$      | $$  \ $$| $$  | $$  /$$$$$$$| $$  | $$    | $$  | $$      | $$__  $$| $$  \ $$| $$  \ $$" + "\r\n" +
                                @"| $$      | $$  | $$| $$  | $$ /$$__  $$| $$  | $$ /$$| $$  | $$      | $$__  $$| $$  \ $$| $$  \ $$" + "\r\n" +
                                @"| $$$$$$$$|  $$$$$$/|  $$$$$$$|  $$$$$$$| $$  |  $$$$/|  $$$$$$$      | $$  | $$| $$$$$$$/| $$$$$$$/" + "\r\n" +
                                @"|________/ \______/  \____  $$ \_______/|__/   \___/   \____  $$      |__/  |__/| $$____/ | $$____/ " + "\r\n" +
                                @"                     /$$  | $$                         /$$  | $$                | $$      | $$      " + "\r\n" +
                                @"                    |  $$$$$$/                        |  $$$$$$/                | $$      | $$      " + "\r\n" +
                                @"                     \______/                          \______/                 |__/      |__/      ");

            System.Threading.Thread.Sleep(1350);


        }
    }
}
 