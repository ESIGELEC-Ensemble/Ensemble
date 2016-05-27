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


        public static bool isUserExist(string email)
        {
            if (conn != null)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM user WHERE email = @t_email";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_email", email);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    return true;
            }
            else if(conn == null)
            {
                if (DBManager.connect() == 0)
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM user WHERE email = @t_email";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_email", email);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                }
            }

            return false;
        }


        public static void register(string email, string name, string password, string photoURL)
        {
            if(photoURL == null){// default photo url
                photoURL = "";
            }
            if (DBManager.connect() == 0)
            {
                if (!DBManager.isUserExist(email))//check if the user is exist
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
                else
                {
                    System.Console.WriteLine("Email exist!");
                }
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
                else//user name do not exist
                {
                    System.Console.WriteLine("the email is not exist in database");
                }

            }
            DBManager.disconnect();
        }


        public static bool add_activity(Activity a)
        {
            string name = a.name;
            DateTime start_date = a.start_date;
            string start_time = a.start_time;
            string end_time = a.end_time;
            int budget = a.budget;
            string introduction = a.introduction;
            int created_userID = a.created_userID;
            string city = a.city;
            string location = a.location;

            try { 
                if (DBManager.connect() == 0)
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO activity(act_name, act_date, act_startTime, act_endTime, budget, introduction, "+
                        "createdUserID, city, act_location) VALUES(@t_name, @t_start_date, @t_start_time, @t_end_time, @t_budget,"+
                        "@t_intro,@t_createdUser, @t_city, @t_act_location)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_name", name);
                    cmd.Parameters.AddWithValue("@t_start_date", start_date);
                    cmd.Parameters.AddWithValue("@t_start_time", start_time);
                    cmd.Parameters.AddWithValue("@t_end_time", end_time);
                    cmd.Parameters.AddWithValue("@t_budget", budget);
                    cmd.Parameters.AddWithValue("@t_intro", introduction);
                    cmd.Parameters.AddWithValue("@t_createdUser", created_userID);
                    cmd.Parameters.AddWithValue("@t_city", city);
                    cmd.Parameters.AddWithValue("@t_act_location", location);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("can not insert to table for some reason: " + e.Message);
                return false;
            }
            return true;
        }

        public static bool delet_activity(string activityID) //implement later
        {
            if(DBManager.connect() == 0)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM activity ";
            }

            return true;
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


        public static void follow(int friendID)
        {

        }

    }
}
