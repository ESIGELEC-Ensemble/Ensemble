﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;

namespace DatabaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDBManagerService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string getDefaultProfileImage();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string login(string e, string p);

        [OperationContract]
        int getUID(string e);

        [OperationContract]
        User getUserByID(int id);

        [OperationContract]
        List<User> getMyFriends(int userId);

        [OperationContract]
        string register(User u);

        [OperationContract]
        int editMyInfo(int currentId, string newName, string newP, string newUrl);

        [OperationContract]
        bool createActivity(Activity a);

        [OperationContract]
        bool deleteActivity(int activityID);

        [OperationContract]
        List<Activity> getAllActivities();

        [OperationContract]
        List<Activity> filterActivity(string tag, string city, string key_word);

        [OperationContract]
        List<Activity> searchActivityByTag(string tag);

        [OperationContract]
        List<Activity> searchActivityByCity(string city);

        [OperationContract]
        List<Activity> searchActivityByKey(string key_word);

        [OperationContract]
        List<Activity> getMyActivities(int userId);

        [OperationContract]
        List<Activity> getMyJoinedActivities(int userID);

        [OperationContract]
        void likeActivity(int userId, int activityId);

        [OperationContract]
        void followFriend(int userId, int followId);

        [OperationContract]
        void unfollow(int userId, int followId);

        [OperationContract]
        void joinActivity(int userId, int activityId);

        [OperationContract]
        void commentActivity(int userId, int activityId, string comment);

        [OperationContract]
        List<string> getAllTags();

        [OperationContract]
        List<string> getAllCities();

        [OperationContract]
        string getUserImage(int uid);

        [OperationContract]
        bool hasLiked(int user, int actID);

        [OperationContract]
        bool hasJoined(int user, int actID);

        [OperationContract]
        List<Activity> getActAscend();

        [OperationContract]
        List<Activity> getActDescend();

        [OperationContract]
        Activity getActByID(int actID);

        [OperationContract]
        List<Comment> getActComment(int actID);

        [OperationContract]
        List<User> searchFriendsByName(string n);

        [OperationContract]
        List<User> searchFriendsByEmail(string e);

        [OperationContract]
        bool isFollowed(int userID, int friendID);

        [OperationContract]
        int getTotalJoinByAID(int aid);

        [OperationContract]
        int getTotalLikeByAID(int aid);

        [OperationContract]
        List<User> getAllUsersExceptMe(int uid);

        [OperationContract]
        User getUserObject(int t_id, string t_email, string t_name, string t_password, string t_photoURL);

        [OperationContract]
        Activity getActivityObeject1(string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location, string url, string t_tag);

        [OperationContract]
        Activity getActivityObeject2(int id, string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location, string url, string t_tag);

        [OperationContract]
        Comment getCommentObject(int uID, int aID, DateTime cd, string comm);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "DatabaseService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class Activity
    {
        [DataMember]
        public int Id
        {
            get;
            private set;
        }
        [DataMember]
        public string name
        {
            get;
            private set;
        }
        [DataMember]
        public DateTime start_date
        {
            get;
            private set;
        }
        [DataMember]
        public string start_time
        {
            get;
            private set;
        }
        [DataMember]
        public string end_time
        {
            get;
            private set;
        }
        [DataMember]
        public int budget
        {
            get;
            private set;
        }
        [DataMember]
        public string introduction
        {
            get;
            private set;
        }
        [DataMember]
        public int created_userID
        {
            get;
            private set;
        }
        [DataMember]
        public string city
        {
            get;
            private set;
        }
        [DataMember]
        public string location
        {
            get;
            private set;
        }
        [DataMember]
        public string actPicURL
        {
            get;
            private set;
        }
        [DataMember]
        public string tag;
        
        public Activity(string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location, string url, string t_tag)
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
            actPicURL = url;
            tag = t_tag;
        }

        public Activity(int t_id, string t_name, DateTime t_date, string t_start_time, string t_end_time, int t_budget, string t_intro,
            int t_uid, string t_city, string t_location, string url, string t_tag)
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
            actPicURL = url;
            tag = t_tag;
        }
        
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int id
        {
            get;
            private set;
        }

        [DataMember]
        public string email
        {
            get;
            private set;
        }

        [DataMember]
        public string name
        {
            get;
            private set;
        }

        [DataMember]
        public string password
        {
            get;
            private set;
        }

        [DataMember]
        public string photoURL
        {
            get;
            private set;
        }

        public User(int t_id, string t_email, string t_name, string t_password, string t_photoURL)
        {
            id = t_id;
            email = t_email;
            name = t_name;
            password = t_password;
            photoURL = t_photoURL;
        }




    }

    [DataContract]
    public class Comment
    {
        [DataMember]
        public string comment;

        [DataMember]
        public DateTime commentDate;

        [DataMember]
        public int actID;

        [DataMember]
        public int userID;

        public Comment(int uID, int aID, DateTime cd, string comm)
        {
            this.actID = aID;
            this.comment = comm;
            this.commentDate = cd;
            this.userID = uID;
        }
    

    }

}
