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
using Ensemble.Model;

namespace Ensemble
{
    public partial class FirstPage
    {
        int UID = -1;
        public FirstPage(int uid)
        {
            InitializeComponent();
            UID = uid;

            for (int i = -2; i < 3; i++)
            {
                Image image1 = new Image();
                //set to relative path later
                ImageSource imageSource = new BitmapImage(new Uri("X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\panda.jpg"));
                image1.Source = imageSource;
                image1.Margin = new Thickness(10, 10, 10, 10);

                Label l1 = new Label();
                l1.FontSize = 14;
                l1.Content = "Frank";
                l1.HorizontalAlignment = HorizontalAlignment.Center;
                l1.Margin = new Thickness(0, 0, 0, 0);

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

            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sv.Content = showFriends;

            for (int i = 0; i < 5; i++)
            {
                Image image1 = new Image();
                //set to relative later
                ImageSource imageSource = new BitmapImage(new Uri("X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\panda.jpg"));
                image1.Source = imageSource;
                image1.Margin = new Thickness(10, 10, 10, 10);

                Label l1 = new Label();
                l1.FontSize = 14;
                l1.Content = "pandapanda";
                l1.HorizontalAlignment = HorizontalAlignment.Center;
                l1.Margin = new Thickness(0, 0, 0, 0);

                Grid.SetRow(image1, showFriends.RowDefinitions.Count);
                Grid.SetColumn(image1, 0);

                RowDefinition r1 = new RowDefinition();
                r1.Height = new GridLength(150);
                showActivityPhoto.RowDefinitions.Add(r1);

                Grid.SetRow(l1, showFriends.RowDefinitions.Count);
                Grid.SetColumn(l1, 0);

                RowDefinition r2 = new RowDefinition();
                r2.Height = new GridLength(10);
                showActivityPhoto.RowDefinitions.Add(r2);

                showActivityPhoto.Children.Add(l1);
                showActivityPhoto.Children.Add(image1);

            }

        }

        //just for testing 
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            CreateActivity createActivity = new CreateActivity(UID);
            createActivity.Show();
            this.Close();
        }
    }
}
