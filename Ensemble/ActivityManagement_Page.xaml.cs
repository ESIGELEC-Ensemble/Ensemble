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

namespace Ensemble
{
    /// <summary>
    /// Interaction logic for ActivityManagement_Page.xaml
    /// </summary>
    public partial class ActivityManagement_Page : Window
    {
        public ActivityManagement_Page()
        {
            InitializeComponent();

            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri("C:\\Users\\j.li.15.INTRANET\\Desktop\\profile.jpg"));
            userPhoto.Source = imageSource;
            userPhoto.Height = 55;
            userPhoto.Margin = new Thickness(30,4,0,10);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetColumn(userPhoto, 4);
            bar.Children.Add(userPhoto);


            for (int i = 0; i < 5; i++)
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
                ImageSource imageSource2 = new BitmapImage(new Uri("C:\\Users\\j.li.15.INTRANET\\Desktop\\panda.jpg"));
                activityPhoto.Source = imageSource2;
                activityPhoto.Margin = new Thickness(0, 0, 0, 0);
                activityPhoto.HorizontalAlignment = HorizontalAlignment.Left;
                activityPhoto.VerticalAlignment = VerticalAlignment.Top;

                Label lTitle = new Label();
                lTitle.FontSize = 14;
                lTitle.Content = "Title: ";
                lTitle.HorizontalAlignment = HorizontalAlignment.Left;
                lTitle.VerticalAlignment = VerticalAlignment.Center;
                lTitle.Margin = new Thickness(0, 0, 0, 0);

                Label lTime = new Label();
                lTime.FontSize = 14;
                lTime.Content = "Time: ";
                lTime.HorizontalAlignment = HorizontalAlignment.Left;
                lTime.VerticalAlignment = VerticalAlignment.Center;
                lTime.Margin = new Thickness(0, 0, 0, 0);

                Label lLocation = new Label();
                lLocation.FontSize = 14;
                lLocation.Content = "Location: ";
                lLocation.HorizontalAlignment = HorizontalAlignment.Left;
                lLocation.VerticalAlignment = VerticalAlignment.Center;
                lLocation.Margin = new Thickness(0, 0, 0, 0);

                Label lMoney = new Label();
                lMoney.FontSize = 14;
                lMoney.Content = "Money: ";
                lMoney.HorizontalAlignment = HorizontalAlignment.Left;
                lMoney.VerticalAlignment = VerticalAlignment.Center;
                lMoney.Margin = new Thickness(0, 0, 0, 0);

                Label lSponsor = new Label();
                lSponsor.FontSize = 14;
                lSponsor.Content = "Sponser: ";
                lSponsor.HorizontalAlignment = HorizontalAlignment.Left;
                lSponsor.VerticalAlignment = VerticalAlignment.Center;
                lSponsor.Margin = new Thickness(0, 0, 0, 0);

              
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
                showActivity.Children.Add(activityPhoto);
            }

        }

         

    }
}
