using System;
using System.Collections.Generic;
using System.Text;

namespace AppADO.net
{
    class MainWindow
    {
        public enum UserOrAdmin
        {
            admin = 1,
            user,
            exit
        }
        public enum UserOperation
        {
            registration = 1,
            login,
            exit
        };
        public enum AdminOperation
        {
            add = 1,
            view,
            delete,
            userdetail,
            exit
        };
        public void MainOperation()
        {
            Console.WriteLine("WELCOME TO FLIGHT BOOKING");
            try
            {
                int i = 0;
                while (i == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine(GetEnumUserOrAdmin());
                    Console.WriteLine("enter the option :");
                    string option = Console.ReadLine();
                    UserOrAdmin opration = (UserOrAdmin)Enum.Parse(typeof(UserOrAdmin), option);
                    switch (opration)
                    {
                        case UserOrAdmin.admin:
                            OperationAdmin();
                            break;
                        case UserOrAdmin.user:
                            OperationUser();
                            break;
                        case UserOrAdmin.exit:
                            Console.WriteLine("");
                            i = 1;
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void OperationUser()
        {
            try
            {
                int i = 1;
                UserRepository user = new UserRepository();
                user.ViewFlightDetails();
                while (i != 0)
                {
                    Console.WriteLine(GetEnumsUserOperation());
                    Console.WriteLine("enter the option :");
                    string option = Console.ReadLine();
                    UserOperation opration = (UserOperation)Enum.Parse(typeof(UserOperation), option);
                    switch (opration)
                    {
                        case UserOperation.registration:
                            user.AddUser();
                            break;
                        case UserOperation.login:
                            user.GetDetail();
                            break;
                        case UserOperation.exit:
                            Console.WriteLine("");
                            i = 0;
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void OperationAdmin()
        {
            AdminRepository admin = new AdminRepository();
            Console.WriteLine("enter the name :");
            string checkName = Console.ReadLine();
            Console.WriteLine("enter the password :");
            string checkPassword = Console.ReadLine();
            try
            {
                bool checkAdmin = admin.VerifyAdmin(checkName, checkPassword);
                if (checkAdmin == true)
                {
                    int i = 0;
                    while (i != 1)
                    {
                        Console.WriteLine(GetEnumsAdminOperation());
                        Console.WriteLine("enter the option :");
                        string option = Console.ReadLine();
                        AdminOperation opration = (AdminOperation)Enum.Parse(typeof(AdminOperation), option);
                        switch (opration)
                        {
                            case AdminOperation.add:
                                admin.AddFlightDetail();
                                break;
                            case AdminOperation.delete:
                                admin.DeleteFlightDetail();
                                break;
                            case AdminOperation.view:
                                admin.ViewFlightDetails();
                                break;
                            case AdminOperation.userdetail:
                                admin.DisplayUserDetails();
                                break;
                            case AdminOperation.exit:
                                Console.WriteLine("");
                                i = 1;
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public string GetEnumsUserOperation()
        {
            int i = 1;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (UserOperation userEnum in Enum.GetValues(typeof(UserOperation)))
            {
                stringBuilder.Append(i + " " + userEnum + "\n");
                i++;
            }
            return stringBuilder.ToString();
        }
        public string GetEnumsAdminOperation()
        {
            int i = 1;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (AdminOperation adminEnum in Enum.GetValues(typeof(AdminOperation)))
            {
                stringBuilder.Append(i + " " + adminEnum + "\n");
                i++;
            }
            return stringBuilder.ToString();
        }
        public string GetEnumUserOrAdmin()
        {
            int i = 1;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (UserOrAdmin userEnum in Enum.GetValues(typeof(UserOrAdmin)))
            {
                stringBuilder.Append(i + " " + userEnum + "\n");
                i++;
            }
            return stringBuilder.ToString();
        }
    }
}
