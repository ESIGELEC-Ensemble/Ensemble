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
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        public UserInfo()
        {
            InitializeComponent();

            //show user's photo
            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri("C:\\Users\\j.li.15.INTRANET\\Desktop\\profile.jpg"));
            userPhoto.Source = imageSource;
            userPhoto.Height = 55;
            userPhoto.Margin = new Thickness(30, 4, 0, 10);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetColumn(userPhoto, 4);
            bar.Children.Add(userPhoto);

        }
    }
}
