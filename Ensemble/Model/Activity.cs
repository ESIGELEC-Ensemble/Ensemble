using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensemble.Model
{
    class Activity
    {
        public int Id
        {
            get;
            private set;
        }

        public string name
        {
            get;
            private set;
        }
        public DateTime start_date
        {
            get;
            private set;
        }
        public string start_time
        {
            get;
            private set;
        }
        public string end_time
        {
            get;
            private set;
        }
        public int budget
        {
            get;
            private set;
        }
        public string introduction
        {
            get;
            private set;
        }
        public int created_userID
        {
            get;
            private set;
        }
        public string city
        {
            get;
            private set;
        }
        public string location
        {
            get;
            private set;
        }

        public Activity(string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location)
        {
            name = t_name;
            start_date = t_date;
            start_time = t_start_time;
            end_time = t_end_time;
            budget = t_budget;
            introduction = t_intro;
            created_userID = t_uid;
            city = t_city;
            location = t_location;
        }


        public Activity(int t_id, string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location)
        {
            Id = t_id;
            name = t_name;
            start_date = t_date;
            start_time = t_start_time;
            end_time = t_end_time;
            budget = t_budget;
            introduction = t_intro;
            created_userID = t_uid;
            city = t_city;
            location = t_location;
        }

        


    }
}
