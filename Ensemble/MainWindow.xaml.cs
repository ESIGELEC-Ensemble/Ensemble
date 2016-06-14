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
using DatabaseService;

namespace Ensemble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBManagerService dbms = new DBManagerService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = loginEmail.Text;
            string password = loginPassword.Password;

            //string info = DBManager.login(email, password);
            DBManagerService dbs = new DBManagerService();
            string info = dbs.login(email, password);


            if (info == "loged_in")
            {
                int userID = dbs.getUID(email);
                //ActivityManagement_Page firstPage = new ActivityManagement_Page();              
                //firstPage.Show();               
                //this.Close();

                //Friends c = new Friends();
               //c.Show();
               // this.Close();

                //CreateActivity createPage = new CreateActivity(userID);
                //createPage.Show();
                //this.Close();

                UserInfo userInfo = new UserInfo();
                userInfo.Show();
                this.Close();


                //FirstPage firstPage = new FirstPage(userID);
                //firstPage.Show();
                //this.Close();

            }
            else if (info == "wrong_password")
            {
                info1.Content = "Password is wrong";
            }
            else if (info == "not_exist")
            {
                info1.Content = "Email not exists";
            }
            else
            {
                info1.Content = "Database connection wrong";
            }

        }

        private void LoginEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "watermark...")
                txtBox.Text = string.Empty;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string name = registerName.Text;
            string email = registerEmail.Text;
            string password = registerPassword.Password;
            User user = new User(0,email,name,password, null);
            string info = dbms.register(user);

            if (info == "success")
            {
                //Window2 twoPage = new Window2();
                ////this will open your child window
                //twoPage.Show();
                ////this will close parent window. windowOne in this case
                //this.Close();
            }
            else if (info == "exist")
            {
                info2.Content = "Email exists";
            }
            else
            {
                info2.Content = "Connection wrong";
            }

        }
    }
}
