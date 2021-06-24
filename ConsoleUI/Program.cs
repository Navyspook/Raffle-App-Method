using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Party!!");
            GetUserInfo();
            PrintGuestsName();
            Thread.Sleep(2000);
            Animate();
            PrintGuestsName();
            PrintWinner();



        }

        //Start writing your code here
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
                yesOrNo = GetUserInput("Do you want to add another name?  ").ToLower();
                nameToAdd = raffleName;
                raffleNumber = GenerateRandomNumber();
                AddGuestsInRaffle(raffleNumber, nameToAdd);

                if (yesOrNo == "yes") continue;

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

                else if (yesOrNo == "no") break;

                else
                {
                    Console.WriteLine("You have not entered \"yes\" or \"no\", or" +
                        " you have entered it incorrectly. No more names will be taken.");
                    break;
                }

            }
            while (yesOrNo == "yes");
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
            Console.WriteLine();
            foreach (KeyValuePair<int, string> kvp in guests)
            {
                Console.WriteLine($"{kvp.Value}'s number is **{kvp.Key}**");
            }
            Console.WriteLine();
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
                    Console.WriteLine($"The Winner is: **{winnerName}** with" +
                        $" the **#{winnerNumber}**");
                }

        }




        // CREDIT for animation: http://newbcoding.blogspot.com/2014/11/drawing-and-animating-ascii-art-in-c.html
        // CREDIT for ASCII: https://ascii.co.uk/art/steelers
        static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
        {
            if (x > Console.WindowWidth) return;
            if (y > Console.WindowHeight) return;

            var trimLeft = x < 0 ? -x : 0;
            int index = y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            var linesToPrint =
                from line in lines
                let currentIndex = index++
                where currentIndex > 0 && currentIndex < Console.WindowHeight
                select new
                {
                    Text = new String(line.Skip(trimLeft).Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft)).ToArray()),
                    X = x,
                    Y = y++
                };

            Console.Clear();
            foreach (var line in linesToPrint)
            {
                Console.SetCursorPosition(line.X, line.Y);
                Console.Write(line.Text);
            }
        }
        static void Animate()
        {
            var arr = new[]
            {
                    @"                                   .-*******-.                                          ",
                    @"                                 .'       __  \_                                        ",
                    @"                                /        /  \/  \                                       ",
                    @"                               |         \_0/\_0/______                                 ",
                    @"                               |:.          .'       oo`\                               ",
                    @"                               |:.         /             \                              ",
                    @"                               | ' ;       |             |                              ",
                    @"                               |:..   .     \_______     |                              ",
                    @"                               |::.| '    ,  \,_____\   /                               ",
                    @"                               |:::.; ' | .  '| ====)_ /===;===========;()              ",
                    @"                               |::; | | ; ; | |            # # # #::::::                ",
                    @"                              /::::.|-| |_|-|, \           # # # #::::::                ",
                    @"                             / '-=-'`  '-'  '--'\          # # # #::::::                ",
                    @"                            /                    \         # # # #::::::                ",
                    @"                                                           # # # # # # #                ",
                    @"                                   H A P P Y               # # # # # # #                ",
                    @"                                                           # # # # # # #                ",
                    @"                             F O U R T H O F J U L Y       # # # # # # #                ",
                    @"                                                           # # # # # # #                ",
                    @"                                                           # # # # # # #                ",
            };
            Console.WindowWidth = 160;
            Console.WriteLine("\n\n");
            var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (int y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
            {
                ConsoleDraw(arr, x, y);
                Thread.Sleep(100);
            }
        }



    }
}
