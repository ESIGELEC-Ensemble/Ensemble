using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensemble.Model
{
    class Activity
    {
        private int Id
        {
            get;
            set;
        }
        private string name
        {
            get;
            set;
        }
        private DateTime start_date
        {
            get;
            set;
        }
        private string start_time
        {
            get;
            set;
        }
        private int budget
        {
            get;
            set;
        }
        private string introduction
        {
            get;
            set;
        }
        private int created_userID
        {
            get;
            set;
        }
        private string city
        {
            get;
            set;
        }
        private string location
        {
            get;
            set;
        }

        public Activity(string t_name, DateTime t_date, string t_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location)
        {
            name = t_name;
            start_date = t_date;
            start_time = t_time;
            budget = t_budget;
            introduction = t_intro;
            created_userID = t_uid;
            city = t_city;
            location = t_location;
        }


        public Activity(int t_id, string t_name, DateTime t_date, string t_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location)
        {
            Id = t_id;
            name = t_name;
            start_date = t_date;
            start_time = t_time;
            budget = t_budget;
            introduction = t_intro;
            created_userID = t_uid;
            city = t_city;
            location = t_location;
        }


    }
}
