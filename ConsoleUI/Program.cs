using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        //variables
        private static Dictionary<int, string> guests = new Dictionary<int, string>();

        //private static int min = 1000;

        //private static int max = 9999;

        private static int raffleNumber;

        private static Random rdm = new Random();

        private static string raffleName;

        private static string nameToAdd;



        /*1. Create a method that return a string and name it GetUserInput. This method
         *  will take a string parameter called message.
         * i. This method should make it easier for you to get input from the user.*/
        static string GetUserInput(string message)
        {

            Console.Write(message);
            string name = Console.ReadLine();
            return name;
        }

        /*2. Create a method that returns nothing and name it GetUserInfo().
         * i. Inside your method use a loop to ask the user to enter the name of the guest.
         *  You should call the method GetUserInput and store it in a variable call** name
         *  ex: string name = GetUserInput("Please enter your name ");
         * ii. The loop will end when the user type "yes". use a separate variable to store the input
         *  ex: otherGuest = GetUserInput("Do you want to add another name? ").ToLower();
         *  Remember that you have to declare this variable outside of the loop and inside the
         *  body of your function so you can use it in the while condition
         *  ex: do{//....}while(otherGuest == 'yes');
         * iii. create a 4 digits random number by calling the GenerateRandomNumber()
         *  (see description for this method below) and store it in the raffleNumber variable that
         *  you created.
         * iv. Validate your input. Keep asking for the guest's name if user enter an empty string.
         * v. Same thing for your random number, validate the data.You can't have the same
         *  raffleNumber. You can use a loop to keep calling GenerateRandomNumber() method.
         * vi. Add both the raffleNumber and guest name in the dictionary by calling
         *  AddGuestsInRaffle() method (see description below)*/
        static void GetUserInfo()
        {


            string yesOrNo;

            do
            {
                raffleName = GetUserInput("Please enter your name:  ");
                yesOrNo = GetUserInput("Do you want to add another name? (y or n)  ").ToLower();
                nameToAdd = raffleName;
                raffleNumber = GenerateRandomNumber();
                AddGuestsInRaffle(raffleNumber, nameToAdd);

                if (yesOrNo == "y") continue;

                else if (yesOrNo == "")
                {
                    yesOrNo = GetUserInput("Do you want to add another name?  ").ToLower();
                    nameToAdd = raffleName;
                    raffleNumber = GenerateRandomNumber();
                    foreach (KeyValuePair<int, string> kvp in guests)
                    {
                        if (raffleNumber == kvp.Key)
                        {
                            raffleNumber = GenerateRandomNumber();
                        }
                    }
                    AddGuestsInRaffle(raffleNumber, nameToAdd);
                }

                else if (yesOrNo == "n") break;

                else
                {
                    Console.WriteLine("You have not entered \"yes\" or \"no\", or" +
                        " you have entered it incorrectly. No more names will be taken.");
                    break;
                }

            }
            while (yesOrNo == "y");
        }

        /*3. Create a method that return an integer and name it GenerateRandomNumber().
         * i. This method should take 2 parameters int min, and int max, This method should return
         *  an integer number between min and max.*/
        static int GenerateRandomNumber(int min = 1000, int max = 9999)
        {
            return rdm.Next(min, max);
        }

        /*4. Create a method that returns nothing and name it AddGuestsInRaffle(). This method
         *  should take 2 parameters int raffleNumber and string guest.
         * i. Add the raffleNumber and the guest name that you collect from the user and store
         *  them in the guests dictionary that you created.*/
        static void AddGuestsInRaffle(int raffleNumber, string nameToAdd)
        {
            guests.Add(raffleNumber, nameToAdd);
        }

        /*5. Create a void method and name it PrintGuestsName()
         * i. Use a loop to print the name of all your guests with their assigned raffleNumber.*/
        static void PrintGuestsName()
        {
            foreach (KeyValuePair<int, string> kvp in guests)
            {
                Console.WriteLine($"{kvp.Value} : {kvp.Key}");
            }
        }

        /*6. Create a method and call it GetRaffleNumber(). This method should take a
         *  Dictionary<int, string> as parameters you can call it anything, people for example.
         * i. This method should allow you to get a random key value from the dictionary and
         *  return it as winner number.*/
        private static int GetRaffleNumber()
        {
            List<int> keyList = new List<int>(guests.Keys);

            Random rand = new Random();
            int randomKey = keyList[rand.Next(keyList.Count)];

            return randomKey;
        }

        /*7. Create a void static method and name it PrintWinner()
         * i. This method should print the name of the winner and the raffleNumber.
         * ii. You should be able to call the GetRaffleNumber(). method and store it in an int
         *  variable named winnerNumber.
         * iii.You should be able to get the winnerName once you have the winnerNumber.*/
        static void PrintWinner()
        {
            int winnerNumber = GetRaffleNumber();

            foreach (KeyValuePair<int, string> kvp in guests)
                if (winnerNumber == kvp.Key)
                {
                    string winnerName = kvp.Value;
                    Console.WriteLine($"The Winner is: {winnerName} with" +
                        $" the #{winnerNumber}");
                }

        }



        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Party!!");
            GetUserInfo();
            Thread.Sleep(2000);
            MultiLineAnimation();
            PrintGuestsName();
            PrintWinner();

        }

        //Start writing your code here






        static void MultiLineAnimation() // Credit: https://www.michalbialecki.com/2018/05/25/how-to-make-you-console-app-look-cool/
        {
            var counter = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.Clear();

                switch (counter % 4)
                {
                    case 0:
                        {
                            Console.WriteLine("         ╔════╤╤╤╤════╗");
                            Console.WriteLine("         ║    │││ \\   ║");
                            Console.WriteLine("         ║    │││  O  ║");
                            Console.WriteLine("         ║    OOO     ║");
                            break;
                        };
                    case 1:
                        {
                            Console.WriteLine("         ╔════╤╤╤╤════╗");
                            Console.WriteLine("         ║    ││││    ║");
                            Console.WriteLine("         ║    ││││    ║");
                            Console.WriteLine("         ║    OOOO    ║");
                            break;
                        };
                    case 2:
                        {
                            Console.WriteLine("         ╔════╤╤╤╤════╗");
                            Console.WriteLine("         ║   / │││    ║");
                            Console.WriteLine("         ║  O  │││    ║");
                            Console.WriteLine("         ║     OOO    ║");
                            break;
                        };
                    case 3:
                        {
                            Console.WriteLine("         ╔════╤╤╤╤════╗");
                            Console.WriteLine("         ║    ││││    ║");
                            Console.WriteLine("         ║    ││││    ║");
                            Console.WriteLine("         ║    OOOO    ║");
                            break;
                        };
                }

                counter++;
                Thread.Sleep(200);
            }
        }
    }
}