using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace AppADO.net
{
    class AdminRepository
    {
        private string name;
        private string password;
        private string flightName;
        private string flightNumber;
        public AdminRepository()
        {
            name = "sridhar";
            password = "adminsri";
        }
        public bool VerifyAdmin(string checkName, string checkPassword)
        {
            if (checkName.Equals(name) && checkPassword.Equals(password))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect Admin login");
                return false;
            }
        }
        public void AddFlightDetail()
        {
            Console.WriteLine("enter the Flight name :");
            flightName = Console.ReadLine();
            Console.WriteLine("enter the Flight Number :");
            flightNumber = Console.ReadLine();
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string sql = "FLIGHT_ADD";

            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@flightName", flightName);
                sqlCommand.Parameters.Add(param);
                param = new SqlParameter("@flightNumber", flightNumber);
                sqlCommand.Parameters.Add(param);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = sqlCommand;
                int retRows = sqlDataAdapter.InsertCommand.ExecuteNonQuery();

                if (retRows >= 1)
                {
                    Console.WriteLine("Flight Added...");
                }
                else
                {
                    Console.WriteLine("Flight does not Added...");
                }
                sqlCommand.Dispose();
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
        public void DeleteFlightDetail()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            Console.Write("Enter flightId : ");
            int flightId = Int32.Parse(Console.ReadLine());
            string sql = "DELETE FROM flightdb WHERE flightId =@flightId ";
            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                SqlParameter param = new SqlParameter("@flightId", flightId);
                sqlCommand.Parameters.Add(param);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.DeleteCommand = sqlCommand;
                int retRows = sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
                if (retRows >= 1)
                {
                    Console.WriteLine("flight details Deleted...");
                }
                else
                {
                    Console.WriteLine("flight does not exist...");
                }
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }
        public void DisplayUserDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string sql = "USER_PROC_DISPLAY";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine("\nUserId   : " + dataReader.GetValue(0));
                        Console.WriteLine("Name     : " + dataReader.GetString(1));
                        Console.WriteLine("M.No     : " + dataReader.GetString(2));
                        Console.WriteLine("DOB      : " + dataReader.GetDateTime(3));
                        Console.WriteLine("AGE      : " + dataReader.GetValue(5));
                        Console.WriteLine("sex      : " + dataReader.GetString(4));
                        Console.WriteLine("Address  : " + dataReader.GetString(6));
                    }
                }
                dataReader.Dispose();
                sqlConnection.Close();
            }
        }
    }
}
