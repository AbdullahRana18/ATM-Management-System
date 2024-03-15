using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Project
{
    internal class Program
    {
        private static string loggedInUser;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\n\n\t\t\t\t\t--------------------------------");
            Console.WriteLine("\t\t\t\t\tWelCome To ATM Management System");
            Console.WriteLine("\t\t\t\t\t--------------------------------\n");
            Console.WriteLine("\t\t\t\t\tPress Enter to Continue\n\n");
            ConsoleKeyInfo KeyInfo = Console.ReadKey(true);
            if (KeyInfo.Key == ConsoleKey.Enter)
            {
                Mnue();
            }
            else
            {
                Console.WriteLine("Invalid Key...Please press Enter To Continue");
                ConsoleKeyInfo ReTryKeyInfo = Console.ReadKey(true);
                if (ReTryKeyInfo.Key == ConsoleKey.Enter)
                {
                    Mnue();
                }
                else
                {
                    Console.WriteLine("Second Attempt Failed ...Exiting Programm");
                    Console.ReadLine();
                }
            }

        }
        static void Mnue()
        {
            while (true)     //for (; ; )
            {
                Console.WriteLine("\n\t\t\t\t\t------------------------");
                Console.WriteLine("\t\t\t\t\tWhat You Want To Do?");
                Console.WriteLine("\t\t\t\t\t1. Login");
                Console.WriteLine("\t\t\t\t\t2. SignUp");
                Console.WriteLine("\t\t\t\t\t3. Delete Account");
                Console.WriteLine("\t\t\t\t\t4. Admin Login");
                Console.WriteLine("\t\t\t\t\t5. Exit");
                Console.WriteLine("\t\t\t\t\t------------------------");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Login();
                        break;


                    case 2:
                        SignUp();
                        break;

                    case 3:
                        Delete();
                        break;

                    case 4:
                        Admin();
                        break;

                    case 5:
                        Console.WriteLine("Exiting Programm .....Good Bye!");
                        break;

                    default:
                        Console.WriteLine("InValid Option...Please Try Again");
                        break;
                }
            }
        }
        static void SignUp()
        {

            string name, uname, pass, contact;

            Console.WriteLine("\t\t\t\t\tEnter UserName");
            uname = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\tEnter Your Full Name");
            name = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\tSet PIN");
            pass = (Console.ReadLine());
            Console.WriteLine("\t\t\t\t\tEnter Your Contact Number");
            contact = (Console.ReadLine());
            using (StreamWriter sw = File.AppendText("e:\\Project\\output.txt"))
            {
                sw.WriteLine(uname);
                sw.WriteLine(name);
                sw.WriteLine(pass);
                sw.WriteLine(contact);
                sw.WriteLine("Balance: 0");
                sw.Flush();

            }
            Console.WriteLine("\t\t\t\t\tSignUp Successful");
        }
        static void Login()
        {




            Console.WriteLine("\t\t\t\t\t User Name ");

            string usernameinput = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\tEnter PIN");
            string passwordinput = Console.ReadLine();
            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            bool userFound = false;
            for (int i = 0; i < lines.Length; i += 5)
            {
                string uname = lines[i];
                string pin = lines[i + 2];
                string name = lines[i + 1];


                if (uname == usernameinput && pin == passwordinput)
                {
                    userFound = true;
                    loggedInUser = uname; // Set the currently logged-in user
                    Console.WriteLine("\n\t\t\t\t\t-------------------------");
                    Console.WriteLine("\t\t\t\t\tLogin Successful!");
                    Console.WriteLine("\t\t\t\t\tWelcome back " + name );
                    Console.WriteLine("\t\t\t\t\t-------------------------\n");
                    ShowOptions();
                    break;
                }
            }

            if (!userFound)
            {
                Console.WriteLine("\t\t\t\t\tIncorrect UserName or Password...");
                Console.WriteLine("\t\t\t\t\tPlease Try Again");
            }
        }














        static void ShowOptions()
        {

            Console.WriteLine("\t\t\t\t\tWhat You Want To do");
            Console.WriteLine("\t\t\t\t\t1. Show Balance");
            Console.WriteLine("\t\t\t\t\t2. WithDraw");
            Console.WriteLine("\t\t\t\t\t3. Deposit");
            Console.WriteLine("\t\t\t\t\t4. LogOut");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowBalance();
                    break;

                case 2:
                    WithDraw();
                    break;
                case 3:
                    Deposit();
                    break;
                    
                case 4:
                    logout();
                    break;
                default:
                    Console.WriteLine("\t\t\t\t\tInvalid Choice...Please Try Again");
                    break;
            }
        }
        static void ShowBalance()
        {
            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            for (int i = 0; i < lines.Length; i += 5)
            {
                string name = lines[i];
                string balanceline = lines[i + 4];
                if (name == loggedInUser)
                {
                    string[] balanceTokkens = balanceline.Split(':');
                    string balance = balanceTokkens[1].Trim();
                    Console.WriteLine("\t\t\t\t\tYour Current Balance is :" + balance + " RS");
                    ShowOptions();
                }
            }
        }
        static void WithDraw()
        {
            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            for (int i = 0; i < lines.Length; i += 5)
            {
                string name = lines[i];
                if (name == loggedInUser)
                {
                    string balanceline = lines[i + 4];
                    string[] balanceTokkens = balanceline.Split(':');
                    string balanceString = balanceTokkens[1].Trim();
                    int balance = int.Parse(balanceString);

                    Console.WriteLine("\t\t\t\t\tEnter Amount to WithDraw");
                    int amount = int.Parse(Console.ReadLine());

                    if (amount <= balance)
                    {
                        balance -= amount;
                        lines[i + 4] = "Balance: " + balance.ToString();
                        File.WriteAllLines("e:\\project\\output.txt", lines);
                        Console.WriteLine("\t\t\t\t\tWithDraw Successful. Your New Balance is: " + balance + "RS");
                        ShowOptions();
                    }
                    else
                    {
                        Console.WriteLine("\t\t\t\t\tInsfficient Balance.");
                        ShowOptions();
                    }
                    break;
                }
            }
        }
        static void Deposit()
        {
            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            for (int i = 0; i < lines.Length; i += 5)
            {
                string name = lines[i];
                if (name == loggedInUser)
                {
                    string balanceline = lines[i + 4];
                    string[] balanceTokens = balanceline.Split(':');
                    string balanceString = balanceTokens[1].Trim();
                    int balance = int.Parse(balanceString);
                    Console.WriteLine("\t\t\t\t\tEnter Amount to Deposite:");
                    int amout = int.Parse(Console.ReadLine());
                    balance += amout;
                    lines[i += 4] = "Balance: " + balance.ToString();
                    File.WriteAllLines("e:\\Project\\output.txt", lines);
                    Console.WriteLine("\t\t\t\t\t-------------------------");
                    Console.WriteLine("\t\t\t\t\tDeposite Successful. Your New Balance is " + balance + " RS");
                    Console.WriteLine("\t\t\t\t\t-------------------------");
                    ShowOptions();


                }
            }

        }
        static void logout()
        {
            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            for (int i = 0; i < lines.Length; i += 5)
            {
                string name = lines[i + 1];
                Console.WriteLine("\n\t\t\t\t\t-------------------------");
                Console.WriteLine("\t\t\t\t\tLogOut SuccessFul....\n\t\t\t\t\tGood Bye " );
                Console.WriteLine("\t\t\t\t\t-------------------------\n");
            }
        }
        static void Delete()
        {
            Console.WriteLine("\t\t\t\t\tEnter User Name to delete the account:");
            string username = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\tEnter PIN:");
            string password = Console.ReadLine();

            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            bool userFound = false;

            for (int i = 0; i < lines.Length; i += 5)
            {
                string name = lines[i];
                string pass = lines[i + 2];

                if (name == username && pass == password)
                {
                    userFound = true;
                    // Remove the lines associated with the user
                    lines[i] = ""; // Username
                    lines[i + 1] = ""; // Full Name
                    lines[i + 2] = ""; // PIN
                    lines[i + 3] = ""; // Contact Number
                    lines[i + 4] = ""; // Balance

                    File.WriteAllLines("e:\\Project\\output.txt", lines);


                    Console.WriteLine("\t\t\t\t\tAccount deleted successfully.");
                    Mnue();
                    break;
                }
            }

            if (!userFound)
            {
                Console.WriteLine("\t\t\t\t\tUser not found. Account deletion failed.");
            }
        }
        static void Admin()
        {
            Console.WriteLine("\t\t\t\t\tAdmin Name");
            string name= Console.ReadLine();
            Console.WriteLine("\t\t\t\t\tPassword");
            string pass = Console.ReadLine();

            if (name == "Admin" && pass == "Admin123")
            {
                Console.WriteLine("\n\t\t\t\t\t------------------------");
                Console.WriteLine("\t\t\t\t\tAdmin Login Successful...");
                
                Display();
               
            }
            else
            {
                Console.WriteLine("\t\t\t\t\tIn Correct Admin Name Or Password");
                Console.WriteLine("Please Try Again");
            }
        }
        static void Display()
        {
            string[] lines = File.ReadAllLines("e:\\Project\\output.txt");
            Console.WriteLine("\t\t\t\t\tAccount Details");
            Console.WriteLine("\t\t\t\t\t------------------------");
            for (int i =0; i < lines.Length; i+=5)
            {
                Console.WriteLine("\t\t\t\t\t-------------------------");
                Console.WriteLine("\t\t\t\t\tUser Name:" + lines[i]);
                Console.WriteLine("\t\t\t\t\tName:" + lines[i+1]);
                Console.WriteLine("\t\t\t\t\tPIN:" + lines[i+2]);
                Console.WriteLine("\t\t\t\t\tContact Number:" + lines[i+3]);
                Console.WriteLine("\t\t\t\t\tBalance: " + lines[i+4] + "RS");
                Console.WriteLine("\t\t\t\t\t-------------------------");
            }
            Mnue();
        }
    }

}


