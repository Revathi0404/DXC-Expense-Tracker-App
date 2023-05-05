using System;
using System.Collections.Generic;


namespace Expense_Tracker
{
    class Program
    {
        static List<Dictionary<string, object>> transactions = new List<Dictionary<string, object>>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Expense Tracker Menu");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Check Available Balance");
                Console.Write("\nEnter your choice (1-4): ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("\nWrong Choice Entered. Try Again!\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddTransaction();
                        break;
                    case 2:
                        ViewTransactions("Expense");
                        break;
                    case 3:
                        ViewTransactions("Income");
                        break;
                    case 4:
                        Console.WriteLine("\nAvailable Balance: {0}\n", GetBalance());
                        break;
                }
            }
        }

        static void AddTransaction()
        {
            Console.Write("\nEnter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("\nInvalid amount entered. Try Again!\n");
                return;
            }

            Console.Write("Enter Date (MM/DD/YYYY): ");
            DateTime date;
            if (!DateTime.TryParse(Console.ReadLine(), out date) || date.Month < 1 || date.Month > 12 || date.Year > 2023)
            {
                Console.WriteLine("\nInvalid date entered. Try Again!\n");
                return;
            }

            string type = amount < 0 ? "Expense" : "Income";

            transactions.Add(new Dictionary<string, object>
            {
                { "Title", title },
                { "Description", description },
                { "Amount", amount },
                { "Date", date },
                { "Type", type }
            });

            Console.WriteLine("\nTransaction added successfully.\n");
        }

        static void ViewTransactions(string type)
        {
            string header = type == "Expense" ? "Expense Transactions" : "Income Transactions";
            Console.WriteLine("\n{0}\n", header);

            foreach (Dictionary<string, object> transaction in transactions)
            {
                if ((string)transaction["Type"] == type)
                {
                    Console.WriteLine("Title: {0}\nDescription: {1}\nAmount: {2}\nDate: {3}\n",
                                      transaction["Title"], transaction["Description"], transaction["Amount"], ((DateTime)transaction["Date"]).ToShortDateString());
                }
            }
        }

        static decimal GetBalance()
        {
            decimal income = 0;
            decimal expenses = 0;

            foreach (Dictionary<string, object> transaction in transactions)
            {
                if ((string)transaction["Type"] == "Income")
                {
                    income += Convert.ToDecimal(transaction["Amount"]);
                }
                else
                {
                    expenses += Convert.ToDecimal(transaction["Amount"]);
                }
            }

            return income + expenses;
        }
    }
}
