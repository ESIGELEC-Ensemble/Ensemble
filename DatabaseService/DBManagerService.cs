using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace DatabaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DBManagerService : IDBManagerService
    {

        private int ReturnCode;
        private MySqlConnection conn = new MySqlConnection("Server=localhost;Database=ensemble;Uid=root;Pwd=;");

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int connect()
        {
            try
            {
                ReturnCode = 1;
                conn.Open();
            }
            catch (Exception e)
            {
                ReturnCode = 0;
                System.Console.WriteLine("Connection goes wrong: " + e.Message);
            }
            return ReturnCode;
        }

        public void disConnect()
        {
            conn.Close();
        }

        public bool isUserExist(string e)
        {
            if (conn != null)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM user WHERE email = @t_email";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_email", e);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    return true;
            }
            else if (conn == null)
            {
                if (this.connect() == 1)//connect success
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM user WHERE email = @t_email";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_email", e);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }
            }
            this.disConnect();
            return false;
        }

        public string login(string e, string p)
        {
            string return_str = "";
            if (this.connect() == 1)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user WHERE email = @t_email";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_email", e);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    string password = "";
                    while (reader.Read())
                    {
                        password = reader.GetString("password");
                    }
                    if (hashPassword(p) == password)
                    {
                        return_str = "loged_in";
                    }
                    else
                    {
                        return_str = "wrong_password";
                    }
                }
                else//user name do not exist
                {
                    return_str = "not_exist";
                }

            }
            this.disConnect();

            return return_str;
        }


        public string hashPassword(string p)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(p));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i]);
            }
            return strBuilder.ToString();
        }
    }
}
