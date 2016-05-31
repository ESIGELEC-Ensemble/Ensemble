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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ensemble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = loginEmail.Text;
            string password = loginPassword.Password;
            DBManager.login(email, password);
            FirstPage firstPage = new FirstPage();
            //this will open your child window
            firstPage.Show();
            //this will close parent window. windowOne in this case
            this.Close();
        }

        private void LoginEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "watermark...")
                txtBox.Text = string.Empty;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {

            Window2 twoPage = new Window2();
            //this will open your child window
            twoPage.Show();
            //this will close parent window. windowOne in this case
            this.Close();
        }

       
    }
}
