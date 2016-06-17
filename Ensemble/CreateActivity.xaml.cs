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
    /// Interaction logic for CreateActivity.xaml
    /// </summary>
    public partial class CreateActivity : Window
    {
        DBManagerService database = new DBManagerService();

        int userID = -1;

        string profileImage = "X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\profile.jpg";

        public CreateActivity(int uid)
        {
            InitializeComponent();

            userID = uid;

            profileImage = database.getUserImage(uid);
            //show user's photo
            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri(profileImage));
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
            string act_name = name.Text;
            DateTime act_date = (DateTime)date.SelectedDate;
            string act_start = start.Text;
            string act_end = end.Text;
            string act_budget_str = budget.Text;
            int act_budget = 0;
            Int32.TryParse(act_budget_str, out act_budget);

            string act_city = (string)cityValue.SelectedItem;

            string act_location = location.Text;

            string imgURL;
            if (path.Content.ToString() == "image")
            {
                imgURL = null;
            }
            else
            {
                imgURL = path.Content.ToString();
            }

            string act_tag = (string)tagValue.SelectedItem;

            string act_intro = intro.Text;

            Activity act = new Activity(act_name, act_date, act_start, act_end, act_budget, act_intro, userID, act_city, act_location
                , imgURL, act_tag);

            database.createActivity(act);

            FirstPage firstPage = new FirstPage(userID);
            firstPage.Show();
            this.Close();
        }

        private void upload_Click(object sender, RoutedEventArgs e)
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

        private void load_city(object sender, RoutedEventArgs e)
        {
            List<string> cities = database.getAllCities();
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = cities;
            comboBox.SelectedIndex = 0;
        }

        private void load_tag(object sender, RoutedEventArgs e)
        {
            List<string> tags = database.getAllTags();
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = tags;
            comboBox.SelectedIndex = 0;

        }




        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            FirstPage mainPage = new FirstPage(userID);
            mainPage.Show();
            this.Close();
        }
        private void Activitylink_Click(object sender, RoutedEventArgs e)
        {
            ActivityManagement_Page activityPage = new ActivityManagement_Page(userID);
            activityPage.Show();
            this.Close();
        }
        private void Friendlink_Click(object sender, RoutedEventArgs e)
        {
            Friends friendsPage = new Friends(userID);
            friendsPage.Show();
            this.Close();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow welPage = new MainWindow();
            welPage.Show();
            this.Close();
        }
        private void Userinfo_Click(object sender, RoutedEventArgs e)
        {
            showUserInfo shwoPage = new showUserInfo(userID);
            shwoPage.Show();
            this.Close();
        }

    }
}
