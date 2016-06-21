using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using DatabaseService;
using System.Windows.Controls.Primitives;

namespace Ensemble
{
    public partial class FirstPage
    {
        int userID = -1;

        DBManagerService dbms = new DBManagerService();

        public FirstPage(int uid)
        {
            InitializeComponent();
            userID = uid;
            string imageURI = dbms.getUserImage(userID);

            //show user's photo
            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri(imageURI));
            userPhoto.Source = imageSource;
            userPhoto.Height = 55;
            userPhoto.Margin = new Thickness(30, 4, 0, 10);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetColumn(userPhoto, 4);
            bar.Children.Add(userPhoto);

            //show friends
            List<User> users = dbms.getAllUsersExceptMe(userID);
            loadFriends(users);

            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sv.Content = showFriends;

            //show activities
            List<Activity> activities = dbms.getAllActivities();
            loadActivities(activities);



        }

        private void loadFriends(List<User> users)
        {
            if (users.Count == 0)
            {

            }
            else
            {
                foreach (User u in users)
                {
                    Image image1 = new Image();


                    ImageSource imageSource2 = new BitmapImage(new Uri(u.photoURL));
                    image1.Source = imageSource2;
                    image1.Margin = new Thickness(10, 10, 10, 10);

                    Label l1 = new Label();
                    l1.FontSize = 14;
                    l1.Content = u.name;
                    l1.HorizontalAlignment = HorizontalAlignment.Center;
                    l1.Margin = new Thickness(0, 0, 0, 0);
                    l1.MouseEnter += (sender, eventArgs) =>
                    {
                        l1.Foreground = new SolidColorBrush(Colors.Blue);
                        this.Cursor = Cursors.Hand;
                    };
                    l1.MouseLeave += (sender, eventArgs) =>
                    {
                        l1.Foreground = new SolidColorBrush(Colors.Black);
                        this.Cursor = null;
                    };

                    l1.MouseLeftButtonDown += (sender, eventArgs) =>
                    {
                        ShowFriendInfo friendDetail = new ShowFriendInfo(userID,u.id);
                        friendDetail.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        friendDetail.Show();
                        this.Close();
                    };

                    Grid.SetRow(image1, showFriends.RowDefinitions.Count);
                    Grid.SetColumn(image1, 0);

                    RowDefinition r1 = new RowDefinition();
                    r1.Height = new GridLength(80);
                    showFriends.RowDefinitions.Add(r1);

                    Grid.SetRow(l1, showFriends.RowDefinitions.Count);
                    Grid.SetColumn(l1, 0);

                    RowDefinition r2 = new RowDefinition();
                    r2.Height = new GridLength(30);
                    showFriends.RowDefinitions.Add(r2);

                    showFriends.Children.Add(l1);
                    showFriends.Children.Add(image1);
                }
            }

        }

        private void loadActivities(List<Activity> activities)
        {
            showActivity.RowDefinitions.Clear();
            showActivity.ColumnDefinitions.Clear();
            showActivity.Children.Clear();
            

            if (activities.Count == 0)
            {
                //to be implemented
            }
            else
            {
                foreach (Activity a in activities)
                {
                    ColumnDefinition c1 = new ColumnDefinition();
                    c1.Width = new GridLength(130);
                    showActivity.ColumnDefinitions.Add(c1);

                    ColumnDefinition c2 = new ColumnDefinition();
                    c2.Width = new GridLength(460);
                    showActivity.ColumnDefinitions.Add(c2);

                    ColumnDefinition c3 = new ColumnDefinition();
                    c3.Width = new GridLength(60);
                    showActivity.ColumnDefinitions.Add(c3);
                    
                    Image activityPhoto = new Image();
                    ImageSource imageSource2 = new BitmapImage(new Uri(a.actPicURL));
                    activityPhoto.Source = imageSource2;
                    activityPhoto.Margin = new Thickness(0, 0, 0, 0);
                    activityPhoto.HorizontalAlignment = HorizontalAlignment.Left;
                    activityPhoto.VerticalAlignment = VerticalAlignment.Top;

                    Label lTitle = new Label();
                    lTitle.FontSize = 14;
                    lTitle.Content = "Title: " + a.name;
                    lTitle.HorizontalAlignment = HorizontalAlignment.Left;
                    lTitle.VerticalAlignment = VerticalAlignment.Center;
                    lTitle.Margin = new Thickness(0, 0, 0, 0);
                    lTitle.MouseEnter += (sender, eventArgs) =>
                    {
                        lTitle.Foreground = new SolidColorBrush(Colors.Blue);
                        this.Cursor = Cursors.Hand;
                    };
                    lTitle.MouseLeave += (sender, eventArgs) =>
                    {
                        lTitle.Foreground = new SolidColorBrush(Colors.Black);
                        this.Cursor = null;
                    };

                    lTitle.MouseLeftButtonDown += (sender, eventArgs) =>
                    {
                        ActivityDetail activityDetail = new ActivityDetail(userID, a.Id);
                        activityDetail.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        activityDetail.Show();
                        this.Close();
                    };


                    Label lTime = new Label();
                    lTime.FontSize = 14;
                    lTime.Content = "Date: " + a.start_date + " Start at:" + a.start_time + " End at:" + a.end_time;
                    lTime.HorizontalAlignment = HorizontalAlignment.Left;
                    lTime.VerticalAlignment = VerticalAlignment.Center;
                    lTime.Margin = new Thickness(0, 0, 0, 0);

                    Label lLocation = new Label();
                    lLocation.FontSize = 14;
                    lLocation.Content = "Location: " + a.location;
                    lLocation.HorizontalAlignment = HorizontalAlignment.Left;
                    lLocation.VerticalAlignment = VerticalAlignment.Center;
                    lLocation.Margin = new Thickness(0, 0, 0, 0);

                    Label lMoney = new Label();
                    lMoney.FontSize = 14;
                    lMoney.Content = "Budget: " + a.budget;
                    lMoney.HorizontalAlignment = HorizontalAlignment.Left;
                    lMoney.VerticalAlignment = VerticalAlignment.Center;
                    lMoney.Margin = new Thickness(0, 0, 0, 0);

                    User u = dbms.getUserByID(a.created_userID);
                    Label lSponsor = new Label();
                    lSponsor.FontSize = 14;
                    lSponsor.Content = "Sponser: " + u.name;
                    lSponsor.HorizontalAlignment = HorizontalAlignment.Left;
                    lSponsor.VerticalAlignment = VerticalAlignment.Center;
                    lSponsor.Margin = new Thickness(0, 0, 0, 0);


                    Button btLike = new Button();
                    btLike.Content = "Like";
                    btLike.HorizontalAlignment = HorizontalAlignment.Center;
                    btLike.VerticalAlignment = VerticalAlignment.Center;
                    //check if the current activity is liked or not
                    if (dbms.hasLiked(userID, a.Id))
                    {
                        btLike.IsEnabled = false;
                    }
                    else
                    {
                        btLike.Click += (sender, eventArgs) =>
                        {
                            dbms.likeActivity(userID, a.Id);
                            btLike.IsEnabled = false;
                        };
                    }

                    Button btJoin = new Button();
                    btJoin.Content = "Join";
                    btJoin.HorizontalAlignment = HorizontalAlignment.Center;
                    btJoin.VerticalAlignment = VerticalAlignment.Center;
                    //check if the current activity is joined or not
                    if (dbms.hasJoined(userID, a.Id))
                    {
                        btJoin.IsEnabled = false;
                    }
                    else
                    {
                        btJoin.Click += (sender, eventArgs) =>
                        {
                            dbms.joinActivity(userID, a.Id);
                            btJoin.IsEnabled = false;
                        };
                    }

                    Grid.SetRow(activityPhoto, showActivity.RowDefinitions.Count);
                    Grid.SetRowSpan(activityPhoto, 5);
                    Grid.SetColumn(activityPhoto, 0);

                    Grid.SetRow(lTitle, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(lTitle, 1);
                    RowDefinition r1 = new RowDefinition();
                    r1.Height = new GridLength(30);
                    showActivity.RowDefinitions.Add(r1);

                    Grid.SetRow(lTime, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(lTime, 1);
                    RowDefinition r2 = new RowDefinition();
                    r2.Height = new GridLength(30);
                    showActivity.RowDefinitions.Add(r2);

                    Grid.SetRow(lLocation, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(lLocation, 1);
                    RowDefinition r3 = new RowDefinition();
                    r3.Height = new GridLength(30);
                    showActivity.RowDefinitions.Add(r3);

                    Grid.SetRow(lMoney, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(lMoney, 1);

                    Grid.SetRow(btLike, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(btLike, 2);
                    RowDefinition r4 = new RowDefinition();
                    r4.Height = new GridLength(30);
                    showActivity.RowDefinitions.Add(r4);

                    Grid.SetRow(lSponsor, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(lSponsor, 1);

                    Grid.SetRow(btJoin, showActivity.RowDefinitions.Count);
                    Grid.SetColumn(btJoin, 2);

                    RowDefinition r5 = new RowDefinition();
                    r5.Height = new GridLength(30);
                    showActivity.RowDefinitions.Add(r5);


                    RowDefinition r6 = new RowDefinition();
                    r6.Height = new GridLength(10);
                    showActivity.RowDefinitions.Add(r6);

                    showActivity.Children.Add(lTitle);
                    showActivity.Children.Add(lTime);
                    showActivity.Children.Add(lLocation);
                    showActivity.Children.Add(lMoney);
                    showActivity.Children.Add(lSponsor);
                    showActivity.Children.Add(btLike);
                    showActivity.Children.Add(btJoin);
                    showActivity.Children.Add(activityPhoto);
                }


            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            FirstPage mainPage = new FirstPage(userID);
            mainPage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            mainPage.Show();
            this.Close();
        }
        private void Activitylink_Click(object sender, RoutedEventArgs e)
        {
            ActivityManagement_Page activityPage = new ActivityManagement_Page(userID);
            activityPage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            activityPage.Show();
            this.Close();
        }
        private void Friendlink_Click(object sender, RoutedEventArgs e)
        {
            Friends friendsPage = new Friends(userID);
            friendsPage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            friendsPage.Show();
            this.Close();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow welPage = new MainWindow();
            welPage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            welPage.Show();
            this.Close();
        }
        private void Userinfo_Click(object sender, RoutedEventArgs e)
        {
            showUserInfo shwoPage = new showUserInfo(userID);
            shwoPage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            shwoPage.Show();
            this.Close();
        }

        private void search(object sender, RoutedEventArgs e)
        {
            string c = city.Text;
            string t = Tag.Text;
            string key = SearchTermTextBox.Text;

            if (c != "City")
            {
                List<Activity> activities = dbms.searchActivityByCity(c);
                if (activities.Count != 0)
                {
                    loadActivities(activities);
                }
                else
                {
                    showActivity.RowDefinitions.Clear();
                    showActivity.ColumnDefinitions.Clear();
                    showActivity.Children.Clear();
                }
            }
            else if (t != "Tag")
            {
                List<Activity> activities = dbms.searchActivityByTag(t);
                if (activities.Count != 0)
                {
                    loadActivities(activities);
                }
                else
                {
                    showActivity.RowDefinitions.Clear();
                    showActivity.ColumnDefinitions.Clear();
                    showActivity.Children.Clear();
                }
            }
            else if (key != "")
            {
                List<Activity> activities = dbms.searchActivityByKey(key);
                if (activities.Count != 0)
                {
                    loadActivities(activities);
                }
                else
                {
                    showActivity.RowDefinitions.Clear();
                    showActivity.ColumnDefinitions.Clear();
                    showActivity.Children.Clear();
                }
            }
        }

        private void ascendByTime(object sender, RoutedEventArgs e)
        {
            List<Activity> activities = dbms.getActAscend();
            if (activities.Count != 0)
            {
                loadActivities(activities);
            }
            else
            {
                showActivity.RowDefinitions.Clear();
                showActivity.ColumnDefinitions.Clear();
                showActivity.Children.Clear();
            }
        }

        private void descendByTime(object sender, RoutedEventArgs e)
        {
            List<Activity> activities = dbms.getActDescend();
            if (activities.Count != 0)
            {
                loadActivities(activities);
            }
            else
            {
                showActivity.RowDefinitions.Clear();
                showActivity.ColumnDefinitions.Clear();
                showActivity.Children.Clear();
            }
        }
    
        
    
    }

}
