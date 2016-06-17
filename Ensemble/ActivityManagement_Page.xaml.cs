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
using DatabaseService;

namespace Ensemble
{
    /// <summary>
    /// Interaction logic for ActivityManagement_Page.xaml
    /// </summary>
    public partial class ActivityManagement_Page : Window
    {
        int userID = -1;
        DBManagerService dbms = new DBManagerService();

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

        public ActivityManagement_Page(int uid)
        {
            InitializeComponent();
            userID = uid;
            string imageURI = dbms.getUserImage(uid);


            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri(imageURI));
            userPhoto.Source = imageSource;
            userPhoto.Height = 55;
            userPhoto.Margin = new Thickness(30,4,0,10);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetColumn(userPhoto, 4);
            bar.Children.Add(userPhoto);

            List<Activity> activities = dbms.getMyActivities(userID);

            foreach (Activity activity in activities)
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
                ImageSource imageSource2 = new BitmapImage(new Uri(activity.actPicURL));
                activityPhoto.Source = imageSource2;
                activityPhoto.Margin = new Thickness(0, 0, 0, 0);
                activityPhoto.HorizontalAlignment = HorizontalAlignment.Left;
                activityPhoto.VerticalAlignment = VerticalAlignment.Top;

                Label lTitle = new Label();
                lTitle.FontSize = 14;
                lTitle.Content = "Title:" + activity.name;
                lTitle.HorizontalAlignment = HorizontalAlignment.Left;
                lTitle.VerticalAlignment = VerticalAlignment.Center;
                lTitle.Margin = new Thickness(0, 0, 0, 0);

                Label lTime = new Label();
                lTime.FontSize = 14;
                lTime.Content = "Time:" + activity.start_time;
                lTime.HorizontalAlignment = HorizontalAlignment.Left;
                lTime.VerticalAlignment = VerticalAlignment.Center;
                lTime.Margin = new Thickness(0, 0, 0, 0);

                Label lLocation = new Label();
                lLocation.FontSize = 14;
                lLocation.Content = "Location:" + activity.location;
                lLocation.HorizontalAlignment = HorizontalAlignment.Left;
                lLocation.VerticalAlignment = VerticalAlignment.Center;
                lLocation.Margin = new Thickness(0, 0, 0, 0);

                Label lMoney = new Label();
                lMoney.FontSize = 14;
                lMoney.Content = "Budget:" + activity.budget;
                lMoney.HorizontalAlignment = HorizontalAlignment.Left;
                lMoney.VerticalAlignment = VerticalAlignment.Center;
                lMoney.Margin = new Thickness(0, 0, 0, 0);

                int createdUID = activity.created_userID;
                User u = dbms.getUserByID(createdUID);
                Label lSponsor = new Label();
                lSponsor.FontSize = 14;
                lSponsor.Content = "Sponser:" + u.name;
                lSponsor.HorizontalAlignment = HorizontalAlignment.Left;
                lSponsor.VerticalAlignment = VerticalAlignment.Center;
                lSponsor.Margin = new Thickness(0, 0, 0, 0);

                Button btDelete = new Button();
                btDelete.Content = "Delete";
                btDelete.HorizontalAlignment = HorizontalAlignment.Center;
                btDelete.VerticalAlignment = VerticalAlignment.Center;
                btDelete.Click += (sender, eventArg) =>
                {
                    dbms.deleteActivity(activity.Id);
                    ActivityManagement_Page actmpage = new ActivityManagement_Page(userID);
                    actmpage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    actmpage.Show();
                    this.Close();
                };


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

                RowDefinition r4 = new RowDefinition();
                r4.Height = new GridLength(30);
                showActivity.RowDefinitions.Add(r4);

                Grid.SetRow(lSponsor, showActivity.RowDefinitions.Count);
                Grid.SetColumn(lSponsor, 1);

                Grid.SetRow(btDelete, showActivity.RowDefinitions.Count);
                Grid.SetColumn(btDelete, 2);
               

                RowDefinition r5 = new RowDefinition();
                r5.Height = new GridLength(30);
                showActivity.RowDefinitions.Add(r5);

                RowDefinition r6 = new RowDefinition();
                r6.Height = new GridLength(20);
                showActivity.RowDefinitions.Add(r6);

                showActivity.Children.Add(lTitle);
                showActivity.Children.Add(lTime);
                showActivity.Children.Add(lLocation);
                showActivity.Children.Add(lMoney);
                showActivity.Children.Add(lSponsor);
                showActivity.Children.Add(btDelete);
                showActivity.Children.Add(activityPhoto);
            }

        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateActivity createPage = new CreateActivity(userID);
            createPage.Show();
            this.Close();
        }

         

    }
}
