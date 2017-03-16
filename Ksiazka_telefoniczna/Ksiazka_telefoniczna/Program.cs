using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.IO;

namespace Ksiazka_telefoniczna
{
    class Menu
    {
        static int choice;
        public static void MakeAChoice()
        {
            Console.WriteLine("Wybierz:\n\t- 1, aby dodać nową pozycję w książce,\n\t- 2, aby wyszukać numer telefonu w książce,\n\t- 3, aby zamknąć program.");
            choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                Console.Clear();
                Addition.AddNewRecord();
            }
            else if (choice == 2) 
            {

            }
            else if (choice == 3) 
            {
                
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("Dokonałeś błędnego wyboru. Spróbuj ponownie.\n\n");
                MakeAChoice();
            }
        }
    }
    class Search
    {
        static void SearchRecord()
        {

        }
    }
    class Addition
    {
        static string reserved;
        public static void AddNewRecord(string firstName = "", string lastName = "", int number = 0) 
        {
            if(firstName == "") 
            {
                Console.WriteLine("Wprowadź imię: ");
                firstName = Console.ReadLine();
                Console.WriteLine("Wprowadź nazwisko: ");
                lastName = Console.ReadLine();
            }
            else 
            {
                Console.WriteLine("Wprowadź imię: " + firstName);
                Console.WriteLine("Wprowadź nazwisko: " + lastName);
            }
            if (number == 0)
            {
                Console.WriteLine("Wprowadź numer telefonu: ");
                number = Convert.ToInt32(Console.ReadLine());
                if (number < 10000 || number > 1000000000) 
                {
                    Console.Clear();
                    Console.WriteLine("Wpisałeś błędny numer. Uzupełnij dane ponownie.\n\n");
                    AddNewRecord(firstName, lastName);
                }
            }
            else 
            {
                Console.WriteLine("Wprowadź numer telefonu: " + Convert.ToString(number));
            }
            Console.WriteLine("Czy numer ma być zastrzeżony? Wprowadź true lub false: ");
            reserved = Console.ReadLine();
            if (reserved == "false" || reserved == "true") 
            {
                AddData(firstName, lastName, number, reserved);
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("Wprowadziłeś błędne dane. Uzupełnij pole ponownie.\n\n");
                AddNewRecord(firstName, lastName, number);
            }
        }
        private static void AddData(string firstName, string lastName, int number, string reserved) 
        {
            string data = "datasource=PhoneBook.sdf; password=PhoneBook";
            SqlCeConnection connection = new SqlCeConnection(data);
            connection.Open();
            SqlCeCommand insert = new SqlCeCommand("INSERT INTO Base (FirstName, LastName, Number, Reserved) VALUES ('" + firstName + "', '" + lastName + "', " + number + ", '" + reserved + "')", connection);
            insert.ExecuteNonQuery();
            connection.Close();
            Menu.MakeAChoice();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Create phone book database
            string data = "datasource=PhoneBook.sdf; password=PhoneBook";
            SqlCeEngine engine = new SqlCeEngine(data);
            SqlCeConnection connection = new SqlCeConnection(data);
            if (File.Exists("PhoneBook.sdf") == false)
            {
                engine.CreateDatabase();
                connection.Open();
                SqlCeCommand create = new SqlCeCommand("CREATE TABLE Base(Id INT PRIMARY KEY IDENTITY(1,1), FirstName NVARCHAR(20), LastName NVARCHAR(20), Number INT, Reserved NVARCHAR(5))", connection);
                create.ExecuteNonQuery();
                connection.Close();
            }
            Menu.MakeAChoice();
            Console.ReadLine();
        }
    }
}
