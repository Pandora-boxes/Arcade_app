using System;
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
            int yearStart = int.Parse(joinArray[2]);
            int yearDiff = yearNow - yearStart;
            int monthDiff = monthStart - monthStart;
            int yearToMonth = yearDiff * 12;
            int totalMonths = yearToMonth + monthDiff;
            return totalMonths;
        }
        static string ScoreCheck( List<string> ApplArr)
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
        static void Reader(List<string> applicantDataArr, List<string> successful , List<string> failed )
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

                // dont need with what Pandora has done


                // failed counterpart

                string failedApp = "name: " + name + " age: " + age.ToString() + " high score rank: " + highScoreRank.ToString() + "\n" +
                    "Bowling high score: " + bowlingHS.ToString() + " Average score: " + avgScore.ToString() + "\n" +
                    "Start date as loyal customer: " + startDateAsLoyalCustomer + "\n" +
                    "Number of pizzas since first visit: " + numOfPizzasSinceFirstVisit.ToString() +
                    " Number of Slush-puppys since first visit: " + numOfSlushiesSinceFirstVisit.ToString() + "\n" +
                    "Preffered flavour Sluch-puppy: " + favoriteSlushieFlavour + "\n\n\n";
                failed.Add(failedApp);


            }


           


        }



        static void ApplicantDataEntry(string filepath, List<string> applicantDataEtry)               //This is the Method of etering the new applicant data into a .txt file
        {
            string applicantName, applicantAge, applicantHighScoreRank, applicantStartDate, applicantPizzaTotal,
                applicantBowlHighScore, applicantEmploy, applicantSlushPuppyPref, applicantSlushPuppyTotal; 

            string applicantData;
            List<string> applicantDataArr = new List<string>();                     //list to hold the data as its being entered
            
            bool formatCorrect=false;
            bool enter = true;

           
                                                                                          //a do while loop to contain the user so that if they want to repeat the data
                                                                                        //entry then they can with a simple bool loop
            do
            {
                Console.Clear();
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

                applicantDataEtry.Add(applicantData);                                            //adds the combined string into the next entry of the list to be written into the txt file
                Console.WriteLine("\nDo you still want to add anymore applicants? (Y/N)");
                string choice = Console.ReadLine();
                string choiceUpper = choice.ToUpper();
                if (choiceUpper == "N")
                    enter = false;
                
                

            } while (enter == true);
            File.WriteAllLines(filepath, applicantDataEtry);                                    //writes the applicants in the array and recently added into the file
        }
        

        // Menu creation
        enum Menu
        {
            Capture_details = 1,
            View_token_eligibility,
            Exit_the_program
        }
        enum SubMenu
        {
            View_loyal_customers_that_are_eligable_for_credit = 1,
            View_loyal_customers_that_are_ineligabe_for_credit,
            View_the_score_of_a_customer,
            Return_to_main_menu,
            Exit_the_program
        }

        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory();                          //gets the directory of the running file and puts it into filepath as a string
            List<string> filepatharr = new List<string>(filePath.Split('\\'));          //makes filepath into a list since the directory could be any length

            for (int i = 0; i < filepatharr.Count;i++)                                  //runs through the filepath array to edit the cells
            {                                                                           //to make it properly point to the file
                if (filepatharr[i] == "Arcade_app")                                     //checks for the start of the Arcade_App file
                {
                    filepatharr[i + 1] = "ApplicantData.txt";                           //changes the next cell after the start to the file name
                    filepatharr.Remove("Debug");                                        //removes the debug from the 
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
                    Console.WriteLine("Invalid option, try again");
                    continue;
                }

                //Exiting program
                if (optionChosen == Menu.Exit_the_program)
                {
                    Console.Clear();
                    Console.WriteLine("Thank you, have a nice day\n\n");
                    Console.WriteLine(@"
       /\_/\        (crust)
     =( ^ . ^ )=        (cheese) 
      /  =  =  \        (toppings)
     /  / \ \ \  \  (crust)    
    /_/   \_/   \_\ 
");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
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
                                Console.WriteLine("Invalid option, try again");
                                continue;
                            }

                            //Switch for sub menu
                            switch (subMenuOptionChosen)
                            {
                                case SubMenu.View_loyal_customers_that_are_eligable_for_credit:
                                    Console.Clear();
                                    foreach (string line in successful)
                                    {
                                        Console.WriteLine(line);
                                    }
                                    continue;
                                case SubMenu.View_loyal_customers_that_are_ineligabe_for_credit:
                                    Console.Clear();
                                    foreach (string line in failed)
                                    {
                                        Console.WriteLine(line);
                                    }
                                    continue;
                                case SubMenu.View_the_score_of_a_customer:
                                    Console.Clear();
                                    Console.WriteLine(ScoreCheck(applicantDataArr));
                                    continue;
                                case SubMenu.Return_to_main_menu:
                                    Console.Clear();
                                    subMenuBool = false;
                                    break;
                                case SubMenu.Exit_the_program:
                                    Console.Clear();
                                    Console.WriteLine("Thank you, have a nice day\n\n");
                                    Console.WriteLine(@"
       /\_/\        (crust)
     =( ^ . ^ )=        (cheese) 
      /  =  =  \        (toppings)
     /  / \ \ \  \  (crust)    
    /_/   \_/   \_\ 
");
                                    System.Threading.Thread.Sleep(2000);
                                    Environment.Exit(0);
                                    break;
                            }
                         
                        }
                        break;
                }
            }
        }
    }
}
