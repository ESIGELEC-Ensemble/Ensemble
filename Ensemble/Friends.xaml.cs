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
    /// Interaction logic for Friends.xaml
    /// </summary>
    public partial class Friends : Window
    {
        int userID = -1;
        DBManagerService dbms = new DBManagerService();

        TextBox tbEmail = new TextBox();

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

        public Friends(int uid)
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


            //serch friends
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(80);
            serachFriend.ColumnDefinitions.Add(c1);

            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(200);
            serachFriend.ColumnDefinitions.Add(c2);

            Label lTitle = new Label();
            lTitle.FontSize = 22;
            var bc = new BrushConverter();
            lTitle.Foreground = (Brush)bc.ConvertFrom("#FF7E3737");
            lTitle.Content = "Search Friend";
            lTitle.FontFamily = new FontFamily("Lucida Handwriting"); 
            lTitle.HorizontalAlignment = HorizontalAlignment.Left;
            lTitle.VerticalAlignment = VerticalAlignment.Center;
            lTitle.Margin = new Thickness(20, 0, 0, 0);

            Label lEmail = new Label();
            lEmail.FontSize = 14;
            lEmail.Content = "Name: ";
            lEmail.FontFamily = new FontFamily("Lucida Handwriting"); 
            lEmail.Margin = new Thickness(20, 0, 0, 0);
            lEmail.HorizontalAlignment = HorizontalAlignment.Left;
            lEmail.VerticalAlignment = VerticalAlignment.Center;


            tbEmail.Height = 30;
            tbEmail.Width = 160;
            tbEmail.FontFamily = new FontFamily("Lucida Handwriting"); 
            tbEmail.HorizontalAlignment = HorizontalAlignment.Center;
            tbEmail.VerticalAlignment = VerticalAlignment.Center;

            Button btSearch = new Button();
            btSearch.Content = "Search";
            btSearch.Background = Brushes.LightBlue;
            btSearch.Width = 100;
            btSearch.FontFamily = new FontFamily("Lucida Handwriting"); 
            btSearch.Height = 30;
            btSearch.FontSize = 14;
            btSearch.Click += new System.Windows.RoutedEventHandler(searchButton_Click);
            btSearch.Margin = new Thickness(30, 0, 0, 0);
            btSearch.HorizontalAlignment = HorizontalAlignment.Center;
            btSearch.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(lTitle, 0);
            Grid.SetColumnSpan(lTitle, 2);
            Grid.SetColumn(lTitle, 0);

            Grid.SetRow(lEmail, 1);
            Grid.SetColumn(lEmail, 0);
            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            serachFriend.RowDefinitions.Add(row1);

            Grid.SetRow(tbEmail, 1);
            Grid.SetColumn(tbEmail, 1);
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(50);
            serachFriend.RowDefinitions.Add(row2);

            Grid.SetRow(btSearch, 3);
            Grid.SetColumnSpan(btSearch, 2);
            Grid.SetColumn(btSearch, 0);
            RowDefinition row4 = new RowDefinition();
            row4.Height = new GridLength(10);
            serachFriend.RowDefinitions.Add(row4);

            RowDefinition row5 = new RowDefinition();
            row5.Height = new GridLength(60);
            serachFriend.RowDefinitions.Add(row5);

            serachFriend.Children.Add(lTitle);
            serachFriend.Children.Add(lEmail);
            serachFriend.Children.Add(tbEmail);
            serachFriend.Children.Add(btSearch);

            List<User> friends = dbms.getMyFriends(userID);

            loadFriends(friends);

        }

        private void loadFriends(List<User> friends)
        {
            showInfo.Children.Clear();
            showInfo.ColumnDefinitions.Clear();
            showInfo.RowDefinitions.Clear();
            showInfo.ColumnDefinitions.Clear();
            showInfo.RowDefinitions.Clear();
            showInfo.Children.Clear();

            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(130);
            showInfo.ColumnDefinitions.Add(c1);

            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(360);
            showInfo.ColumnDefinitions.Add(c2);



            foreach(User friend in friends)
            {

                Image image1 = new Image();
                ImageSource imageSource2 = new BitmapImage(new Uri(friend.photoURL));
                image1.Source = imageSource2;
                image1.Margin = new Thickness(10, 10, 10, 10);

                Label l1 = new Label();
                l1.FontSize = 14;
                l1.Content = "Name: " + friend.name;
                l1.Margin = new Thickness(0, 0, 0, 0);

                Label l2 = new Label();
                l2.FontSize = 14;

                Button b1 = new Button();
                if (dbms.isFollowed(userID, friend.id))
                {
                    b1.Content = "unfollow";
                    b1.HorizontalAlignment = HorizontalAlignment.Left;
                    b1.Margin = new Thickness(0, 0, 0, 0);
                    b1.Height = 22;
                    b1.Width = 100;
                    b1.Background = Brushes.LightPink;
                    b1.Foreground = Brushes.White;
                    b1.Click += (sender, eventArgs) =>
                    {
                        dbms.unfollow(userID, friend.id);
                        b1.IsEnabled = false;
                    };
                }
                else
                {
                    b1.Content = "follow";
                    b1.HorizontalAlignment = HorizontalAlignment.Left;
                    b1.Margin = new Thickness(0, 0, 0, 0);
                    b1.Height = 20;
                    b1.Width = 100;
                    b1.Background = Brushes.LightPink;
                    b1.Foreground = Brushes.White;
                    b1.Click += (sender, eventArgs) =>
                    {
                        dbms.followFriend(userID, friend.id);
                        b1.IsEnabled = false;
                    };
                }

                Grid.SetRow(image1, showInfo.RowDefinitions.Count);
                Grid.SetRowSpan(image1, 3);
                Grid.SetColumn(image1, 0);

                Grid.SetRow(l1, showInfo.RowDefinitions.Count);
                Grid.SetColumn(l1, 1);
                RowDefinition rowDef5 = new RowDefinition();
                rowDef5.Height = new GridLength(30);
                showInfo.RowDefinitions.Add(rowDef5);

                Grid.SetRow(l2, showInfo.RowDefinitions.Count);
                Grid.SetColumn(l2, 1);
                RowDefinition rowDef6 = new RowDefinition();
                rowDef6.Height = new GridLength(30);
                showInfo.RowDefinitions.Add(rowDef6);

                Grid.SetRow(b1, showInfo.RowDefinitions.Count);
                Grid.SetColumn(b1, 1);

                List<Activity> activities = dbms.getMyJoinedActivities(friend.id);

                if (activities.Count >= 1)
                {
                    l2.Content = "Latest joined: " + activities[activities.Count - 1].name;
                    Button btMore = new Button();
                    btMore.Content = "More joined activities";
                    btMore.Click += (s, e) =>
                    {
                        ShowFriendsActivities showFriendACT = new ShowFriendsActivities(userID,friend.id);
                        showFriendACT.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        showFriendACT.Show();
                        this.Close();
                    };
                    btMore.HorizontalAlignment = HorizontalAlignment.Left;
                    btMore.Margin = new Thickness(130, 0, 0, 0);
                    btMore.Height = 22;
                    btMore.Width = 150;
                    btMore.Background = Brushes.LightBlue;
                    btMore.Foreground = Brushes.White;

                    Grid.SetRow(btMore, showInfo.RowDefinitions.Count);
                    Grid.SetColumn(btMore, 1);
                    showInfo.Children.Add(btMore);
                }
                else
                {
                    l2.Content = "No joined activity";
                }

                RowDefinition rowDef4 = new RowDefinition();
                rowDef4.Height = new GridLength(30);
                showInfo.RowDefinitions.Add(rowDef4);

                showInfo.Children.Add(l1);
                showInfo.Children.Add(l2);
                showInfo.Children.Add(b1);
                showInfo.Children.Add(image1);

            }

            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sv.Content = show;

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string key_name = tbEmail.Text;
            
            if (key_name != "")
            {
                List<User> friends = dbms.searchFriendsByName(key_name);
                if (friends.Count > 0)
                {
                    loadFriends(friends);
                }
                else
                {
                    showInfo.RowDefinitions.Clear();
                    showInfo.ColumnDefinitions.Clear();
                    showInfo.Children.Clear();
                    showInfo.ColumnDefinitions.Clear();
                    showInfo.RowDefinitions.Clear();
                    showInfo.Children.Clear();
                }
            }
            else
            {
                showInfo.RowDefinitions.Clear();
                showInfo.ColumnDefinitions.Clear();
                showInfo.Children.Clear();
                showInfo.ColumnDefinitions.Clear();
                showInfo.RowDefinitions.Clear();
                showInfo.Children.Clear();
            }
            
        }
    }
}
