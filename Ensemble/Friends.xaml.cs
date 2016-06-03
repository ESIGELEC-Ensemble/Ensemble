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
    /// <summary>
    /// Interaction logic for Friends.xaml
    /// </summary>
    public partial class Friends : Window
    {
        public Friends()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++) {          

            Image image1 = new Image();
            ImageSource imageSource = new BitmapImage(new Uri("C:\\Users\\j.li.15.INTRANET\\Desktop\\images.png"));
            image1.Source = imageSource;
            image1.Margin = new Thickness(10, 10, 10, 10);

            Grid.SetRow(image1, i);
            Grid.SetColumn(image1, 0);

            RowDefinition rowDef1 = new RowDefinition();
            rowDef1.Height = new GridLength(90);
            showPicture.RowDefinitions.Add(rowDef1);

            showPicture.Children.Add(image1);

            }


            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sv.Content = show;
            //another part

            for (int i = -2; i < 3; i++)
            {
                Label l1 = new Label();
                l1.FontSize = 14;
                l1.Content = "Name: Frank";
                l1.Margin = new Thickness(0, 0, 0, 0);

                Label l2 = new Label();
                l2.FontSize = 14;
                l2.Content = "Joined acrivities: code game";

                Button b1 = new Button();
                b1.Content = "unfollowed";
                b1.HorizontalAlignment = HorizontalAlignment.Left;
                b1.Margin = new Thickness(0, 0, 0, 0);
                b1.Height = 20;
                b1.Width = 100;
                b1.Background = Brushes.Tomato;
                b1.Foreground = Brushes.White;

                Grid.SetRow(l1, showInfo.RowDefinitions.Count);
                Grid.SetColumn(l1, 0);
                RowDefinition rowDef5 = new RowDefinition();
                rowDef5.Height = new GridLength(30);
                showInfo.RowDefinitions.Add(rowDef5);

                //RowDefinition rowDef5 = new RowDefinition();
                //rowDef5.Height = new GridLength(30);
                //showInfo.RowDefinitions.Add(rowDef5);

                Grid.SetRow(l2, showInfo.RowDefinitions.Count);
                Grid.SetColumn(l2, 0);
                RowDefinition rowDef6 = new RowDefinition();
                rowDef6.Height = new GridLength(30);
                showInfo.RowDefinitions.Add(rowDef6);

                Grid.SetRow(b1, showInfo.RowDefinitions.Count);
                Grid.SetColumn(b1, 0);
                RowDefinition rowDef4 = new RowDefinition();
                rowDef4.Height = new GridLength(30);
                showInfo.RowDefinitions.Add(rowDef4);

                showInfo.Children.Add(l1);
                showInfo.Children.Add(l2);
                showInfo.Children.Add(b1);

            }
        }
    }
}
