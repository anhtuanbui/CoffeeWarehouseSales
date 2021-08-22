// Project: ICT272 Web Design and Development
// Assignment 2: CREATE A BASIC C# CONSOLE APPLICATION 
// ID: 11900962
// Author: Anh Tuan bui

using System;
using System.Collections.Generic;
using System.Linq;

namespace ICT272___Assessment2
{
    class Program
    {
        static string[] yesAnswers = { "Yes", "yes", "y", "Y" };
        static string[] noAnswers = { "No", "no", "n", "N" };
        static string line = "----------------------------------------------------------------------------------------------------------------------";
        static List<Sale> sales = new List<Sale>();

        class Sale
        {
            public string customerName { get; set; }
            public int numberOfBags { get; set; }
            public string isReseller { get; set; }
            public double total { get; set; }

            // Overloading initialise the object
            public Sale() { }

            public Sale(string customerName, int numberOfBags, string isReseller)
            {
                this.customerName = customerName;
                this.numberOfBags = numberOfBags;
                this.isReseller = isReseller;
            }

            // get total, return in 2 decimal after the point ex: 1.12
            public string getTotalFormated()
            {
                return String.Format("{0:0.00}", this.total);
            }

            public void caculatingTotal()
            {
                double price;
                float discount = 0;

                // set price with different quantities
                if (numberOfBags <= 5)
                {
                    price = 36;
                }
                else if (numberOfBags <= 15)
                {
                    price = 34.5;
                }
                else
                {
                    price = 32.7;
                }

                //  check if customer is reseller, open discount
                if (yesAnswers.Contains(isReseller))
                {
                    discount = 0.2f;
                }
                else if (noAnswers.Contains(isReseller))
                {
                    discount = 0;
                }

                // caculating total
                this.total = numberOfBags*price - numberOfBags * price * discount;
            }
        }

        // set the message center of the window
        public static void centerText(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        // require user to input a number. Ask to input again if wrong input type
        public static int inputNumberOfBags()
        {
            int numberOfBags = 1;

            Console.Write("Number of coffee bags: ");

            if (int.TryParse(Console.ReadLine(), out numberOfBags) && numberOfBags >= 1 && numberOfBags <= 200)
            {
                return numberOfBags;
            }
            return -1;
        }

        // check if users fill in the right answer, if yes assign it to the answer
        public static string ynQuestion(string text)
        {
            string[] answers = { "Yes", "yes", "y", "Y", "No", "no", "n", "N" };
            Console.Write(text);
            string answer = Console.ReadLine();

            while (!answers.Contains(answer))
            {
                Console.WriteLine();
                Console.WriteLine("(Fill in 'Yes' or 'No')");
                answer = Console.ReadLine();
            }
            return answer;
        }

        // table of summary after finishing all sale sections
        // the table is arrange with white space ideal for 2 word names. Other than that, the look is broken
        public static void summary()
        {
            List<Sale> salesOrdered = sales.OrderBy(sale => sale.total).ToList();
            Sale minSale = salesOrdered[0];
            Sale maxSale = salesOrdered[salesOrdered.Count - 1];

            centerText("Summary of sales");
            Console.WriteLine(line); // border line
            Console.WriteLine(line);
            Console.WriteLine("Name                 Quantity            Reseller            Charge");

            foreach (Sale sale in sales)
            {
                Console.WriteLine($"{sale.customerName}               {sale.numberOfBags}                 {sale.isReseller}                {sale.getTotalFormated()}");
            }

            Console.WriteLine(line);
            Console.WriteLine(line);

            Console.WriteLine($"The customer spending most is {maxSale.customerName} ${maxSale.getTotalFormated()}");
            Console.WriteLine($"The customer spending least is {minSale.customerName} ${minSale.getTotalFormated()}");
        }

        public static void section()
        {
            Sale sale = new Sale();

            //  get customer name
            Console.Write("Enter your Name: ");
            sale.customerName = Console.ReadLine();

            // get number of coffee bags
            // using while loop to ask user to fill the right number
            int numberOfBags = inputNumberOfBags();
            while (numberOfBags == -1)
            {
                Console.WriteLine(); //  a break line
                Console.WriteLine("(Make sure you enter a number between 1 and 200)");
                numberOfBags = inputNumberOfBags();
            }
            sale.numberOfBags = numberOfBags;

            // check if the customer is reseller
            sale.isReseller = ynQuestion("Reseller (Yes/No): ");

            // caculate the total then print out the total
            // calling the function caculatingTotal() to caculate total
            sale.caculatingTotal();
            Console.WriteLine($"The total sales to {sale.customerName} is ${sale.getTotalFormated()}");

            // add this customer to the sales list
            sales.Add(sale);

            // ask user for starting another section
            string userContinue = ynQuestion("Do you want to continue?(Yes/No): ");

            //  break line
            Console.WriteLine(line);

            if (yesAnswers.Contains(userContinue))
            {
                section();
            }
            else if (noAnswers.Contains(userContinue))
            {
                summary();
                centerText("Thank you for purchasing with us!");
            }

        }

        static void Main(string[] args)
        {
            // set align welcom text to the center
            string strWelcome = "Welcome to Sydney Coffee Warehouse";
            string strInstruction = "(Fill out the field and enter for the next command)";
            centerText(strWelcome);

            // program instruction
            centerText(strInstruction);
                 
            // sale sections
            section();

            // after compiling the application does not stop to read without readline
            // adding readline line at the end of the program
            Console.ReadLine();
        }
    }
}