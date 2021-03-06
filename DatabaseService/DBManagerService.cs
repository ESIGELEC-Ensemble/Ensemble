﻿using System;
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
        //private MySqlConnection conn = new MySqlConnection("Server=localhost;Database=ensemble;Uid=root;Pwd=;");

        public static string defaultProfileImage = "X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\profile.jpg";

        public string getDefaultProfileImage()
        {
            return "X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\profile.jpg";
        }

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

        private MySqlConnection connect()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=ensemble;Uid=root;Pwd=;");
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Connection goes wrong: " + e.Message);
            }
            return conn;
        }

        public void disConnect(MySqlConnection conn)
        {
            conn.Close();
        }

        private bool isUserExist(string e)
        {
            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM user WHERE email = @t_email";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_email", e);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    this.disConnect(conn);
                    return true;
                }
            }
            this.disConnect(conn);
            return false;
        }

        public string login(string e, string p)
        {
            string return_str = "";
            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
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
            this.disConnect(conn);

            return return_str;
        }

        public int getUID(string e)
        {
            int uid = -1;
            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "SELECT id FROM user WHERE email = @t_email";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@t_email", e);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    uid = reader.GetInt32("id");
                }
            }
            this.disConnect(conn);
            return uid;
        }

        public User getUserByID(int uid)
        {
            User user = null;

            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM user WHERE id=@uid";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@uid", uid);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString("name");
                    int id = reader.GetInt32("id");
                    string email = reader.GetString("email");
                    string password = reader.GetString("password");
                    string photo = reader.GetString("photo");

                    user = new User(id, email, name, password, photo);
                }
            }
            this.disConnect(conn);
            return user;
        }

        public string getUserImage(int uid)
        {
            string url = "";
            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "SELECT photo FROM user WHERE id=@uid";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@uid", uid);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    url = reader.GetString("photo");
                }
            }
            this.disConnect(conn);


            return url;
        }

        private string hashPassword(string p)
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

        public string register(User u)
        {
            string photoURL = u.photoURL;
            string email = u.email;
            string password = u.password;
            string name = u.name;

            string return_str = "";
            if (photoURL == null)
            {// default photo url
                photoURL = "";
            }
            MySqlConnection conn = this.connect();
            if (!this.isUserExist(email))//check if the user is exist
            {
                this.connect();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    password = hashPassword(password);
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO user(email, name, password, photo) VALUES(@t_email,@t_name, @t_password, @t_photo)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_email", email);
                    cmd.Parameters.AddWithValue("@t_name", name);
                    cmd.Parameters.AddWithValue("@t_password", password);
                    cmd.Parameters.AddWithValue("@t_photo", photoURL);
                    cmd.ExecuteNonQuery();

                    return_str = "success";
                }
            }
            else
            {
                return_str = "exist";
            }
            this.disConnect(conn);
            return return_str;
        }

        public bool createActivity(Activity a)
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
            string url = a.actPicURL;
            string tag = a.tag;
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd1 = new MySqlCommand())
                {
                    cmd1.Connection = conn;
                    cmd1.CommandText = "INSERT INTO activity(act_name, act_date, act_startTime, act_endTime, budget, introduction, " +
                        "createdUserID, city, act_location, tag, picURL) VALUES(@t_name, @t_start_date, @t_start_time, @t_end_time, @t_budget," +
                        "@t_intro,@t_createdUser, @t_city, @t_act_location, @t_tag, @t_url)";
                    cmd1.Prepare();
                    cmd1.Parameters.AddWithValue("@t_name", name);
                    cmd1.Parameters.AddWithValue("@t_start_date", start_date);
                    cmd1.Parameters.AddWithValue("@t_start_time", start_time);
                    cmd1.Parameters.AddWithValue("@t_end_time", end_time);
                    cmd1.Parameters.AddWithValue("@t_budget", budget);
                    cmd1.Parameters.AddWithValue("@t_intro", introduction);
                    cmd1.Parameters.AddWithValue("@t_createdUser", created_userID);
                    cmd1.Parameters.AddWithValue("@t_city", city);
                    cmd1.Parameters.AddWithValue("@t_act_location", location);
                    cmd1.Parameters.AddWithValue("@t_tag", tag);
                    cmd1.Parameters.AddWithValue("@t_url", url);
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("can not insert to table for some reason: " + e.Message);
                this.disConnect(conn);
                return false;
            }
            this.disConnect(conn);
            return true;
        }

        public bool deleteActivity(int activityID)
        {
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "DELETE FROM activity WHERE id=@act_id;";
                    query += "DELETE FROM comment_table WHERE activityId=@act_id;";
                    query += "DELETE FROM liked_table WHERE activityId=@act_id;";
                    query += "DELETE FROM joined_table WHERE actId=@act_id;";
                    cmd.CommandText = query;
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@act_id", activityID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Someting wrong, can not delete the activity: " + e.Message);
                this.disConnect(conn);
                return false;
            }
            this.disConnect(conn);
            return true;
        }

        public List<Activity> getAllActivities()
        {
            List<Activity> activities = new List<Activity>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM activity";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string actName = reader.GetString("act_name");
                        DateTime date = reader.GetDateTime("act_date");
                        string startTime = reader.GetString("act_startTime");
                        string endTime = reader.GetString("act_endTime");
                        int budget = reader.GetInt32("budget");
                        string introduction = reader.GetString("introduction");
                        int createdUserID = reader.GetInt32("createdUserID");
                        string city = reader.GetString("city");
                        string act_location = reader.GetString("act_location");
                        string tag = reader.GetString("tag");
                        string picURL = reader.GetString("picURL");

                        Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                            createdUserID, city, act_location, picURL, tag);
                        activities.Add(activity);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Get data wrong!" + e.Message);
            }

            this.disConnect(conn);
            return activities;
        }

        private User getUserById(int userId)
        {
            MySqlConnection conn = this.connect();
            try{

                using (MySqlCommand temp_cmd = new MySqlCommand())
                {
                    temp_cmd.Connection = conn;

                    temp_cmd.CommandText = "SELECT * FROM user WHERE id=@uid";
                    temp_cmd.Parameters.AddWithValue("@uid", userId);
                    MySqlDataReader reader = temp_cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string email = reader.GetString("email");
                        string name = reader.GetString("name");
                        string password = reader.GetString("password");
                        string photoURL = reader.GetString("photo");

                        User user = new User(id, email, name, password, photoURL);
                        temp_cmd.Dispose();
                        this.disConnect(conn);
                        return user;
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);
            return null;
        }

        public List<User> getMyFriends(int userId)
        {
            List<User> friends = new List<User>();
            MySqlConnection conn = this.connect();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM relationship WHERE userId=@uid";
                    cmd.Parameters.AddWithValue("@uid", userId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int friendID = reader.GetInt32("friendId");
                        User friend = getUserById(friendID);
                        friends.Add(friend);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            disConnect(conn);
            return friends;
        }

        public List<Activity> filterActivity(string c, string t, string key_word)
        {
            List<Activity> activities = new List<Activity>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM activity WHERE city=@t_city and tag=@t_tag and act_name LIKE @t_name";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_tag", t);
                    cmd.Parameters.AddWithValue("@t_city", c);
                    cmd.Parameters.AddWithValue("@t_name", "%" + key_word + "%");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string actName = reader.GetString("act_name");
                            DateTime date = reader.GetDateTime("act_date");
                            string startTime = reader.GetString("act_startTime");
                            string endTime = reader.GetString("act_endTime");
                            int budget = reader.GetInt32("budget");
                            string introduction = reader.GetString("introduction");
                            int createdUserID = reader.GetInt32("createdUserID");
                            string city = reader.GetString("city");
                            string act_location = reader.GetString("act_location");
                            string tag = reader.GetString("tag");
                            string picURL = reader.GetString("picURL");

                            Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                                createdUserID, city, act_location, picURL, tag);
                            activities.Add(activity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("searching wrong! " + e.Message);
            }

            this.disConnect(conn);
            return activities;
        }

        public List<Activity> searchActivityByTag(string t)
        {
            List<Activity> activities = new List<Activity>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM activity WHERE tag=@t_tag";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_tag", t);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string actName = reader.GetString("act_name");
                            DateTime date = reader.GetDateTime("act_date");
                            string startTime = reader.GetString("act_startTime");
                            string endTime = reader.GetString("act_endTime");
                            int budget = reader.GetInt32("budget");
                            string introduction = reader.GetString("introduction");
                            int createdUserID = reader.GetInt32("createdUserID");
                            string city = reader.GetString("city");
                            string act_location = reader.GetString("act_location");
                            string tag = reader.GetString("tag");
                            string picURL = reader.GetString("picURL");

                            Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                                createdUserID, city, act_location, picURL, tag);
                            activities.Add(activity);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                System.Console.WriteLine("searching wrong! " + e.Message);
            }
            this.disConnect(conn);
            return activities;
        }

        public List<Activity> searchActivityByCity(string c)
        {
            List<Activity> activities = new List<Activity>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM activity WHERE city=@t_city";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_city", c);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string actName = reader.GetString("act_name");
                            DateTime date = reader.GetDateTime("act_date");
                            string startTime = reader.GetString("act_startTime");
                            string endTime = reader.GetString("act_endTime");
                            int budget = reader.GetInt32("budget");
                            string introduction = reader.GetString("introduction");
                            int createdUserID = reader.GetInt32("createdUserID");
                            string city = reader.GetString("city");
                            string act_location = reader.GetString("act_location");
                            string tag = reader.GetString("tag");
                            string picURL = reader.GetString("picURL");

                            Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                                createdUserID, city, act_location, picURL, tag);
                            activities.Add(activity);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                System.Console.WriteLine("searching wrong! " + e.Message);
            }
            this.disConnect(conn);
            return activities;
        }

        public List<Activity> searchActivityByKey(string key_word)
        {
            List<Activity> activities = new List<Activity>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM activity WHERE act_name LIKE @t_name";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_name", "%" + key_word + "%");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string actName = reader.GetString("act_name");
                            DateTime date = reader.GetDateTime("act_date");
                            string startTime = reader.GetString("act_startTime");
                            string endTime = reader.GetString("act_endTime");
                            int budget = reader.GetInt32("budget");
                            string introduction = reader.GetString("introduction");
                            int createdUserID = reader.GetInt32("createdUserID");
                            string city = reader.GetString("city");
                            string act_location = reader.GetString("act_location");
                            string tag = reader.GetString("tag");
                            string picURL = reader.GetString("picURL");

                            Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                                createdUserID, city, act_location, picURL, tag);
                            activities.Add(activity);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                System.Console.WriteLine("searching wrong! " + e.Message);
            }
            this.disConnect(conn);
            return activities;
        }

        public List<Activity> getMyActivities(int userId)
        {
            List<Activity> activities = new List<Activity>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM activity WHERE createdUserID=@t_uid";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_uid", userId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string actName = reader.GetString("act_name");
                            DateTime date = reader.GetDateTime("act_date");
                            string startTime = reader.GetString("act_startTime");
                            string endTime = reader.GetString("act_endTime");
                            int budget = reader.GetInt32("budget");
                            string introduction = reader.GetString("introduction");
                            int createdUserID = reader.GetInt32("createdUserID");
                            string city = reader.GetString("city");
                            string act_location = reader.GetString("act_location");
                            string tag = reader.GetString("tag");
                            string picURL = reader.GetString("picURL");

                            Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                                createdUserID, city, act_location, picURL, tag);
                            activities.Add(activity);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                System.Console.WriteLine("searching wrong! " + e.Message);
            }
            this.disConnect(conn);
            return activities;
        }

        public void likeActivity(int userId, int activityId)
        {
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO liked_table VALUE(@u_id, @act_id)";
                    cmd.Parameters.AddWithValue("@u_id", userId);
                    cmd.Parameters.AddWithValue("@act_id", activityId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("You already liked the activity" + e.Message);
            }
            this.disConnect(conn);
        }

        public void followFriend(int userId, int followId)
        {
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO relationship VALUE(@u_id,@f_id)";
                    cmd.Parameters.AddWithValue("@u_id", userId);
                    cmd.Parameters.AddWithValue("@f_id", followId);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("you have already followed this friend: " + e.Message);
            }
            this.disConnect(conn);
        }

        public void joinActivity(int userId, int activityId)
        {
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO joined_table VALUE(@u_id, @act_id)";
                    cmd.Parameters.AddWithValue("@u_id", userId);
                    cmd.Parameters.AddWithValue("@act_id", activityId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("You already joined the activity" + e.Message);
            }
            this.disConnect(conn);
        }

        public void commentActivity(int userId, int activityId, string comment)
        {
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO comment_table VALUE(@u_id, @act_id, @comment, @date)";
                    cmd.Parameters.AddWithValue("@u_id", userId);
                    cmd.Parameters.AddWithValue("@act_id", activityId);
                    cmd.Parameters.AddWithValue("@comment", comment);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("You already commented the activity" + e.Message);
            }
            this.disConnect(conn);
        }
        
        public int editMyInfo(int currentId, string newName, string newP, string newUrl)
        {
            int state = 0;

            string new_password = hashPassword(newP);
            MySqlConnection conn = this.connect();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = "UPDATE user SET name=@newN, password = @newP, photo = @newURL WHERE id = @id";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@newP", new_password);
                    cmd.Parameters.AddWithValue("@newN", newName);
                    cmd.Parameters.AddWithValue("@newURL", newUrl);
                    cmd.Parameters.AddWithValue("@id", currentId);
                    state = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("update wrong" + e.Message);
            }
            this.disConnect(conn);
            return state;
        }

        public void unfollow(int userId, int followId)
        {
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM relationship WHERE userId = @u_id AND  friendId = @f_id";
                    cmd.Parameters.AddWithValue("@u_id", userId);
                    cmd.Parameters.AddWithValue("@f_id", followId);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("you have already followed this friend: " + e.Message);
            }
            this.disConnect(conn);
        }

        public List<string> getAllTags()
        {
            List<string> tags = new List<string>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM tag";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tag = reader.GetString("tagName");
                        tags.Add(tag);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Get data wrong!" + e.Message);
            }

            this.disConnect(conn);
            return tags;
        }

        public List<string> getAllCities()
        {
            List<string> cities = new List<string>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM city";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tag = reader.GetString("cityName");
                        cities.Add(tag);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Get data wrong!" + e.Message);
            }

            this.disConnect(conn);
            return cities;
        }
        
        public bool hasLiked(int userID, int actID)
        {
            bool isExist = false;

            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM liked_table WHERE userId = @u_id and activityId = @act_id";
                    cmd.Parameters.AddWithValue("@u_id", userID);
                    cmd.Parameters.AddWithValue("@act_id", actID);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        isExist = true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);

            return isExist;
        }

        public bool hasJoined(int userID, int actID)
        {
            bool isExist = false;

            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM joined_table WHERE userId = @u_id and actId = @act_id";
                    cmd.Parameters.AddWithValue("@u_id", userID);
                    cmd.Parameters.AddWithValue("@act_id", actID);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        isExist = true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);

            return isExist;
        }

        public List<Activity> getActAscend()
        {
            List<Activity> activities = new List<Activity>();

            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM activity ORDER BY act_date ASC";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string actName = reader.GetString("act_name");
                        DateTime date = reader.GetDateTime("act_date");
                        string startTime = reader.GetString("act_startTime");
                        string endTime = reader.GetString("act_endTime");
                        int budget = reader.GetInt32("budget");
                        string introduction = reader.GetString("introduction");
                        int createdUserID = reader.GetInt32("createdUserID");
                        string city = reader.GetString("city");
                        string act_location = reader.GetString("act_location");
                        string tag = reader.GetString("tag");
                        string picURL = reader.GetString("picURL");

                        Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                            createdUserID, city, act_location, picURL, tag);
                        activities.Add(activity);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Get data wrong!" + e.Message);
            }

            this.disConnect(conn);

            return activities;
        }

        public List<Activity> getActDescend()
        {
            List<Activity> activities = new List<Activity>();

            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM activity ORDER BY act_date DESC";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string actName = reader.GetString("act_name");
                        DateTime date = reader.GetDateTime("act_date");
                        string startTime = reader.GetString("act_startTime");
                        string endTime = reader.GetString("act_endTime");
                        int budget = reader.GetInt32("budget");
                        string introduction = reader.GetString("introduction");
                        int createdUserID = reader.GetInt32("createdUserID");
                        string city = reader.GetString("city");
                        string act_location = reader.GetString("act_location");
                        string tag = reader.GetString("tag");
                        string picURL = reader.GetString("picURL");

                        Activity activity = new Activity(id, actName, date, startTime, endTime, budget, introduction,
                            createdUserID, city, act_location, picURL, tag);
                        activities.Add(activity);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Get data wrong!" + e.Message);
            }

            this.disConnect(conn);

            return activities;
        }

        public Activity getActByID(int actID)
        {
            Activity act = null;

            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM activity WHERE id=@actID";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@actID", actID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString("act_name");
                    DateTime act_date = reader.GetDateTime("act_date");
                    string actStartTime = reader.GetString("act_startTime");
                    string actEndTime = reader.GetString("act_endTime");
                    int budget = reader.GetInt32("budget");
                    string intro = reader.GetString("introduction");
                    int creatUID = reader.GetInt32("createdUserID");
                    string city = reader.GetString("city");
                    string location = reader.GetString("act_location");
                    string tag = reader.GetString("tag");
                    string picURL = reader.GetString("picURL");
                    act = new Activity(actID, name, act_date, actStartTime, actEndTime, budget, intro, creatUID, city, location, picURL, tag);
                }
        }
            this.disConnect(conn);
            return act;
        }

        public List<Comment> getActComment(int actID)
        {
            List<Comment> comments = new List<Comment>();

            MySqlConnection conn = this.connect();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM comment_table WHERE activityId = @actID";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@actID", actID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string commentStr = reader.GetString("comment");
                    int userID = reader.GetInt32("userId");
                    int activityID = reader.GetInt32("activityId");
                    DateTime createdDate = reader.GetDateTime("createdDate");

                    Comment comment = new Comment(userID, activityID, createdDate, commentStr);
                    comments.Add(comment);
                }
            }
            this.disConnect(conn);

            return comments;
        }

        public List<User> searchFriendsByName(string n)
        {
            List<User> friends = new List<User>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM user WHERE name LIKE @t_name";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@t_name", "%" + n + "%");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string email = reader.GetString("email");
                            int id = reader.GetInt32("id");
                            string password = reader.GetString("password");
                            string name = reader.GetString("name");
                            string url = reader.GetString("photo");

                            User f = new User(id, email, name, password, url);
                            friends.Add(f);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("searching wrong! " + e.Message);
            }
            this.disConnect(conn);
            return friends;
        }

        public List<User> searchFriendsByEmail(string e)
        {
            List<User> friends = new List<User>();

            return friends;
        }

        public bool isFollowed(int userID, int friendID)
        {
            bool isFollow = false;

            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM relationship WHERE userId = @u_id and friendID = @fri_id";
                    cmd.Parameters.AddWithValue("@u_id", userID);
                    cmd.Parameters.AddWithValue("@fri_id", friendID);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        isFollow = true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);

            return isFollow;
        }

        public List<Activity> getMyJoinedActivities(int userID)
        {
            List<Activity> acts = new List<Activity>();

            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM joined_table WHERE userId = @u_id";
                    cmd.Parameters.AddWithValue("@u_id", userID);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int actID = reader.GetInt32("actId");
                            Activity actitity = this.getActByID(actID);
                            acts.Add(actitity);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);
            return acts;
        }

        public int getTotalJoinByAID(int aid)
        {
            int total = 0;
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM joined_table WHERE actId = @aid";
                    cmd.Parameters.AddWithValue("@aid", aid);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            total++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);
            return total;
        }

        public int getTotalLikeByAID(int aid)
        {
            int total = 0;
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM liked_table WHERE activityId = @aid";
                    cmd.Parameters.AddWithValue("@aid", aid);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            total++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            this.disConnect(conn);
            return total;
        }

        public List<User> getAllUsersExceptMe(int uid)
        {
            List<User> users = new List<User>();
            MySqlConnection conn = this.connect();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM user WHERE id <> @uid";
                    cmd.Parameters.AddWithValue("@uid", uid);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string email = reader.GetString("email");
                        string password = reader.GetString("password");
                        string name = reader.GetString("name");
                        string photo = reader.GetString("photo");
                        User user = new User(id, email, name, password, photo);
                        users.Add(user);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Get data wrong!" + e.Message);
            }

            this.disConnect(conn);
            return users;
        }

        public User getUserObject(int t_id, string t_email, string t_name, string t_password, string t_photoURL)
        {
            return new User(t_id, t_email, t_name, t_password, t_photoURL);
        }

        public Activity getActivityObeject1(string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro, int t_uid, string t_city, string t_location, string url, string t_tag)
        {
            return new Activity( t_name,  t_date,  t_start_time,  t_end_time,  t_budget,  t_intro,  t_uid,  t_city,  t_location,  url,  t_tag);
        }

        public Activity getActivityObeject2(int id, string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro, int t_uid, string t_city, string t_location, string url, string t_tag)
        {
            return new Activity(id, t_name, t_date, t_start_time, t_end_time, t_budget, t_intro, t_uid, t_city, t_location, url, t_tag);
        }

        public Comment getCommentObject(int uID, int aID, DateTime cd, string comm)
        {
            return new Comment(uID, aID, cd, comm);
        }
    }
}
