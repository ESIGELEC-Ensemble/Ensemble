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
using Microsoft.Win32;
using DatabaseService;

namespace Ensemble
{
    /// <summary>
    /// Interaction logic for showUserInfo.xaml
    /// </summary>
    public partial class showUserInfo : Window
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

        public showUserInfo(int uid)
        {
            InitializeComponent();
            this.userID = uid;
            //show user info
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(120);
            userInfo.ColumnDefinitions.Add(c1);

            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(300);
            userInfo.ColumnDefinitions.Add(c2);

            User u = dbms.getUserByID(userID);

            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri(u.photoURL));
            userPhoto.Source = imageSource;
            userPhoto.Margin = new Thickness(10, 10, 0, 0);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Top;

            Label lName = new Label();
            lName.FontSize = 14;
            lName.Content = "Name:" +u.name;
            lName.HorizontalAlignment = HorizontalAlignment.Left;
            lName.VerticalAlignment = VerticalAlignment.Center;
            lName.Margin = new Thickness(0, 0, 0, 0);

            Label lEmail = new Label();
            lEmail.FontSize = 14;
            lEmail.Content = "Email:" + u.email;
            lEmail.HorizontalAlignment = HorizontalAlignment.Left;
            lEmail.VerticalAlignment = VerticalAlignment.Center;
            lEmail.Margin = new Thickness(0, 0, 0, 0);

            Button btEdit = new Button();
            btEdit.Content = "Edit";
            btEdit.Background = Brushes.LightBlue;
            btEdit.Width = 130;
            btEdit.Height = 25;
            btEdit.FontSize = 14;
            btEdit.Click += new System.Windows.RoutedEventHandler(editButton_Click);
            btEdit.Margin = new Thickness(20, 0, 0, 0);
            btEdit.HorizontalAlignment = HorizontalAlignment.Left;
            btEdit.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetRowSpan(userPhoto, 2);
            Grid.SetColumn(userPhoto, 0);

            Grid.SetRow(lName, 0);
            Grid.SetColumn(lName, 1);
            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            userInfo.RowDefinitions.Add(row1);

            Grid.SetRow(lEmail, 1);
            Grid.SetColumn(lEmail, 1);
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(50);
            userInfo.RowDefinitions.Add(row2);

            Grid.SetRow(btEdit, 3);
            Grid.SetColumn(btEdit, 1);
            RowDefinition row4 = new RowDefinition();
            row4.Height = new GridLength(40);
            userInfo.RowDefinitions.Add(row4);

            RowDefinition row5 = new RowDefinition();
            row5.Height = new GridLength(40);
            userInfo.RowDefinitions.Add(row5);


            RowDefinition row6 = new RowDefinition();
            row6.Height = new GridLength(10);
            userInfo.RowDefinitions.Add(row6);

            userInfo.Children.Add(lName);
            userInfo.Children.Add(userPhoto);
            userInfo.Children.Add(lEmail);
            userInfo.Children.Add(btEdit);

        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            EditUserInfo editPage = new EditUserInfo(userID);
            editPage.Show();
            this.Close();
        }
    }
}