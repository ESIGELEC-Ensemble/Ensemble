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
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class EditUserInfo : Window
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


        public EditUserInfo(int uid)
        {
            InitializeComponent();
            this.userID = uid;

            //show user's photo
            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri(dbms.getUserImage(userID)));
            userPhoto.Source = imageSource;
            userPhoto.Height = 55;
            userPhoto.Margin = new Thickness(30, 4, 0, 10);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetColumn(userPhoto, 4);
            bar.Children.Add(userPhoto);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image files (*.jpg;*png;*jpeg)|*.jpg;*png;*jpeg";
            openFileDialog.Title = "Select activity image";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                path.Content = openFileDialog.FileName;
            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            string newName = name.Text;
            string oldP = oldPassword.Password;
            string newP = newPassword.Password;
            string newImage = path.Content.ToString();

            if (oldP != newP)
            {
                if (dbms.editMyInfo(userID, newName, newP, newImage) != 0)
                {
                    System.Console.WriteLine("success");
                }
                else
                {
                    System.Console.WriteLine("Edit failed!");
                }
            }
            else
            {
                System.Console.WriteLine("The new password should not be the same like the old one!");
            }

            FirstPage firstPage = new FirstPage(userID);
            firstPage.Show();
            this.Close();

        }



    }
}
