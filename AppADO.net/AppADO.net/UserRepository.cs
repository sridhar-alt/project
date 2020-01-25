using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.SqlClient;

namespace AppADO.net
{
    public class UserRepository
    {
        public string name;
        public string mobile;
        public DateTime dob;
        public string mail;
        public int age;
        public string userAddress;
        public string sex;
        public int userId=1;
        public string password;
        public void AddUser()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                name = ValidateName();
                mobile = ValidateMobile();
                dob = ValidateDob();
                mail = ValidateMail();
                userAddress = ValidateAddress();
                sex = ValidateSex();
                password = ValidatePassword();
                string sql = "USER_PROC_INSERT";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter("@NAME", name);
                    sqlCommand.Parameters.Add(param);
                    param = new SqlParameter("@MOBILENUMBER", mobile);
                    sqlCommand.Parameters.Add(param);
                    param = new SqlParameter("@DOB", dob);
                    sqlCommand.Parameters.Add(param);
                    param = new SqlParameter("@SEX", sex);
                    sqlCommand.Parameters.Add(param);
                    param = new SqlParameter("@AGE", age);
                    sqlCommand.Parameters.Add(param);
                    param = new SqlParameter("@USERADDRESS", userAddress);
                    sqlCommand.Parameters.Add(param);
                    param = new SqlParameter("@PASSWORD", password);
                    sqlCommand.Parameters.Add(param);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.InsertCommand = sqlCommand;
                    int retRows = sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                    if (retRows >= 1)
                    {
                        Console.WriteLine("Customer Added...");
                    }
                    else
                    {
                        Console.WriteLine("Customer Does not Added...");
                    }
                    sqlCommand.Dispose();
                }
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                sqlConnection.Close();
            }
        }
        private string ValidateAddress()
        {
            Console.WriteLine("Enter the user address: ");
            userAddress = Console.ReadLine();
            return userAddress;
        }
        //validate name is done here...
        private string ValidateSex()
        {
            Console.WriteLine("Enter the Sex of the user : ");
            sex = Console.ReadLine();
            if (sex.Equals("m") == false && sex.Equals("f") == false)
            {
                ValidateSex();
            }
            return sex;
        }

        private string ValidateName()
        {
            Console.WriteLine("Enter the Name of the user : ");
            name = Console.ReadLine();
            if (name.Length < 4)
            {
                Console.WriteLine("The name is not valid try again..");
                name = ValidateName();
            }
            Regex check = new Regex(@"^[A-Za-z]+$", RegexOptions.IgnoreCase);
            bool valid = check.IsMatch(name);
            char[] charName = name.ToCharArray();
            int i;
            for (i = 0; i < charName.Length - 2; i++)
            {
                if ((charName[i] == charName[i + 1]) && (charName[i + 1] == charName[i + 2]))
                {

                    break;
                }
            }
            if ((valid == false) && (i != (name.Length - 2)))
            {
                Console.WriteLine("Name is Invalid..");
                name = ValidateName();
            }
            return name;
        }
        private string ValidateMobile()
        {
            Console.WriteLine("Enter the Mobile Number of the user : ");
            mobile = Console.ReadLine();
            Regex check = new Regex(@"^[0-9]");
            bool valid = check.IsMatch(mobile);
            if ((mobile.Length == 10) && (valid == true))
            {
                return mobile;
            }
            else
            {
                Console.WriteLine("Mobile Number is Invalid..");
                mobile = ValidateMobile();
            }
            return mobile;
        }
        private DateTime ValidateDob()
        {
            Console.WriteLine("Enter the Date Of Birth : ");
            try
            {
                dob = Convert.ToDateTime(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid Date of birth..");
                dob = ValidateDob();
            }
            DateTime today = DateTime.Today;
            age = today.Year - dob.Year;
            if (age < 0 || age > 80)
            {
                Console.WriteLine("Invalid Date of birth..");
                dob = ValidateDob();
            }
            return dob;
        }
        private string ValidateMail()
        {
            Console.WriteLine("Enter the Mail of the user : ");
            mail = Console.ReadLine();
            try
            {
                MailAddress mailid = new MailAddress(mail);
                return mail;
            }
            catch
            {
                mail = ValidateMail();
            }
            return mail;
        }
        private string ValidatePassword()
        {
            password = Convert.ToString(age) + name;
            Console.WriteLine("password : " + password);
            return password;
        }
        public void GetDetail()
        {
            Console.WriteLine("enter the mobile number :");
            mobile = Console.ReadLine();
            Console.WriteLine("enter the password :");
            password = Console.ReadLine();
            checkDetail(name, password);
        }
        private void checkDetail(string name, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string sql = "USER_PROC_LOGIN";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            SqlParameter param = new SqlParameter("@mobilenumber", mobile);
            sqlCommand.Parameters.Add(param);
            param = new SqlParameter("@passwords", password);
            sqlCommand.Parameters.Add(param);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine("login successful..");
                        Console.WriteLine("\nUserId   : " + dataReader.GetValue(0));
                        Console.WriteLine("Name     : " + dataReader.GetString(1));
                        Console.WriteLine("M.No     : " + dataReader.GetString(2));
                        Console.WriteLine("DOB      : " + dataReader.GetDateTime(3));
                        Console.WriteLine("sex      : " + dataReader.GetString(5));
                        Console.WriteLine("Address  : " + dataReader.GetString(6));
                    }
                }
                else
                {
                    Console.WriteLine("login failed..");
                }
                dataReader.Dispose();
                sqlConnection.Close();
            }
        }
        public void ViewFlightDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string sql = "FLIGHT_DISPLAY";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine("\nflightId   : " + dataReader.GetValue(0));
                        Console.WriteLine("Flight Name     : " + dataReader.GetValue(1));
                        Console.WriteLine("Flight Number      : " + dataReader.GetValue(2));
                    }
                }
            }
            sqlConnection.Close();
        }
    }
    
}


