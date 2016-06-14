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
    /// Interaction logic for CreateActivity.xaml
    /// </summary>
    public partial class CreateActivity : Window
    {
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            FirstPage mainPage = new FirstPage();
            mainPage.Show();
            this.Close();
        }

        private void Activitylink_Click(object sender, RoutedEventArgs e)
        {
            ActivityManagement_Page activityPage = new ActivityManagement_Page();
            activityPage.Show();
            this.Close();
        }
        private void Friendlink_Click(object sender, RoutedEventArgs e)
        {
            Friends friendsPage = new Friends();
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
            showUserInfo shwoPage = new showUserInfo();
            shwoPage.Show();
            this.Close();
        }

        public CreateActivity()
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


            //upload code

            // In the Click event of Browse button, I used System.Windows.Forms.OpenFileDialog control to browse images, display the image's name in TextBox. 


//            private void Button_Click(object sender, RoutedEventArgs e) 
//{ 
//            OpenFileDialog FileDialog = new System.Windows.Forms.OpenFileDialog(); 
//            FileDialog.Title = "Select A File"; 
//            FileDialog.InitialDirectory = ""; 
//            FileDialog.Filter = "Image Files (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png"; 
//            FileDialog.FilterIndex = 1; 
 
//            if (FileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
//            { 
//                TextBox1.Text = FileDialog.FileName; 
//                Label1.Content = GetFileNameNoExt(TextBox1.Text.Trim()); 
//                BitmapImage bmp = new BitmapImage(new Uri(TextBox1.Text.Trim())); 
//                image1.Source = bmp; 
//            } 
//            else 
//            { 
//                Label1.Content = "You didn't select any image file...."; 
//            } 
//} 
 
//public string GetFileNameNoExt(string FilePathFileName) 
//{ 
//            return System.IO.Path.GetFileNameWithoutExtension(FilePathFileName); 
//}



            //In the Click event of Upload button, note that the process of Entity Framework:
            //private void SaveImage_Click(object sender, RoutedEventArgs e) 
            //{
            //    FileStream Stream = new FileStream(TextBox1.Text, FileMode.Open, FileAccess.Read);
            //    StreamReader Reader = new StreamReader(Stream);
            //    Byte[] ImgData = new Byte[Stream.Length - 1];
            //    Stream.Read(ImgData, 0, (int)Stream.Length - 1);
            //    using (dbtestEntities db = new dbtestEntities())
            //    {
            //        imageinfo o = db.imageinfoes.Create();
            //        o.Name = GetFileNameNoExt(TextBox1.Text);
            //        o.ImgData = ImgData;
            //        db.imageinfoes.Add(o);
            //        db.SaveChanges();
            //    }

            //    Label1.Content = GetFileNameNoExt(TextBox1.Text.Trim()) + " Stored Successfully....";
            //}

        }

    }
}
