using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using Ensemble.Model;

namespace Ensemble
{
    class DBManager
    {
        private static MySqlConnection conn = new MySqlConnection("Server=localhost;Database=ensemble;Uid=root;Pwd=;");
        private static int ReturnCode = 0;


        public static int connect()
        {
            try
            {
                ReturnCode = 0;
                conn.Open();
            }
            catch (Exception e)
            {
                ReturnCode = 1;
                System.Console.WriteLine("Connection goes wrong: " + e.Message);
            }
            return ReturnCode;
        }

        public static void disconnect()
        {
            conn.Close();
        }

        public static string hashPassword(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i]);
            }
            return strBuilder.ToString();
        }

        public static void register(string email, string name, string password, string photoURL)
        {
            if(photoURL == null){// default photo url
                photoURL = "";
            }
            if (DBManager.connect() == 0)
            {
                MySqlCommand cmd = new MySqlCommand();
                password = hashPassword(password);
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO user(email, name, password, photo) VALUES(@t_email,@t_name, @t_password, @t_photo)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_email", email);
                cmd.Parameters.AddWithValue("@t_name", name);
                cmd.Parameters.AddWithValue("@t_password", password);
                cmd.Parameters.AddWithValue("@t_photo", photoURL);
                cmd.ExecuteNonQuery();
            }
            DBManager.disconnect();
        }

        public static void login(string e, string p)
        {
            if (DBManager.connect() == 0)
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
                        System.Console.WriteLine("loged in");
                    }
                    else
                    {
                        System.Console.WriteLine("wrong password");
                    }
                }

            }
            DBManager.disconnect();
        }

        public static List<Activity> get_all_activities()
        {
            List<Activity> activities = new List<Activity>();

            return activities;
        }

        public static List<Activity> get_activities(string key_word)
        {
            List<Activity> activities = new List<Activity>();

            if (DBManager.connect() == 0)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM activity WHERE act_name LIKE %@t_name%";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_name", key_word);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string email = reader.GetString("");
                    }
                }

            }

            return activities;
        }

    }
}
