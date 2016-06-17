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
using DatabaseService;

namespace Ensemble
{
    /// <summary>
    /// Interaction logic for ActivityDetail.xaml
    /// </summary>
    public partial class ActivityDetail : Window
    {

        int userID = -1;
        int actID = -1;
        DBManagerService dbms = new DBManagerService();

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


        public ActivityDetail(int uid, int actID)
        {
            InitializeComponent();
            this.userID = uid;
            this.actID = actID;
            string userImage = dbms.getUserImage(userID);

            //show user's photo
            Image userPhoto = new Image();
            ImageSource imageSource = new BitmapImage(new Uri(userImage));
            userPhoto.Source = imageSource;
            userPhoto.Height = 55;
            userPhoto.Margin = new Thickness(30, 4, 0, 10);
            userPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            userPhoto.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(userPhoto, 0);
            Grid.SetColumn(userPhoto, 4);
            bar.Children.Add(userPhoto);

            //show friends
            List<User> friends = dbms.getMyFriends(userID);
            foreach(User u in friends)
            {
                Image image1 = new Image();
                ImageSource imageSource2 = new BitmapImage(new Uri(u.photoURL));
                image1.Source = imageSource2;
                image1.Margin = new Thickness(10, 10, 10, 10);

                Label l1 = new Label();
                l1.FontSize = 14;
                l1.Content = u.name;
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

            //show activity info
            Activity activity = dbms.getActByID(actID);

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
            ImageSource imageSource3 = new BitmapImage(new Uri(activity.actPicURL));
            activityPhoto.Source = imageSource3;
            activityPhoto.Margin = new Thickness(0, 0, 0, 0);
            activityPhoto.HorizontalAlignment = HorizontalAlignment.Left;
            activityPhoto.VerticalAlignment = VerticalAlignment.Top;

            Label lTitle = new Label();
            lTitle.FontSize = 14;
            lTitle.Content = "Title:" + activity.name;
            lTitle.HorizontalAlignment = HorizontalAlignment.Left;
            lTitle.VerticalAlignment = VerticalAlignment.Center;
            lTitle.Margin = new Thickness(0, 0, 0, 0);

            Label lTime = new Label();
            lTime.FontSize = 14;
            lTime.Content = "Time:" ;
            lTime.HorizontalAlignment = HorizontalAlignment.Left;
            lTime.VerticalAlignment = VerticalAlignment.Center;
            lTime.Margin = new Thickness(0, 0, 0, 0);

            Label lLocation = new Label();
            lLocation.FontSize = 14;
            lLocation.Content = "Location:" + activity.location;
            lLocation.HorizontalAlignment = HorizontalAlignment.Left;
            lLocation.VerticalAlignment = VerticalAlignment.Center;
            lLocation.Margin = new Thickness(0, 0, 0, 0);

            Label lMoney = new Label();
            lMoney.FontSize = 14;
            lMoney.Content = "Budget:" + activity.budget;
            lMoney.HorizontalAlignment = HorizontalAlignment.Left;
            lMoney.VerticalAlignment = VerticalAlignment.Center;
            lMoney.Margin = new Thickness(0, 0, 0, 0);

            Label lSponsor = new Label();
            lSponsor.FontSize = 14;
            lSponsor.Content = "Sponser:" + activity.created_userID;
            lSponsor.HorizontalAlignment = HorizontalAlignment.Left;
            lSponsor.VerticalAlignment = VerticalAlignment.Center;
            lSponsor.Margin = new Thickness(0, 0, 0, 0);

            Button btLike = new Button();
            btLike.Content = "Like";
            btLike.HorizontalAlignment = HorizontalAlignment.Center;
            btLike.VerticalAlignment = VerticalAlignment.Center;
            //check if the current activity is liked or not
            if (dbms.hasLiked(userID, activity.Id))
            {
                btLike.IsEnabled = false;
            }
            else
            {
                btLike.Click += (sender, eventArgs) =>
                {
                    dbms.likeActivity(userID, activity.Id);
                    btLike.IsEnabled = false;
                };
            }

            Button btJoin = new Button();
            btJoin.Content = "Join";
            btJoin.HorizontalAlignment = HorizontalAlignment.Center;
            btJoin.VerticalAlignment = VerticalAlignment.Center;
            //check if the current activity is joined or not
            if (dbms.hasJoined(userID, activity.Id))
            {
                btJoin.IsEnabled = false;
            }
            else
            {
                btJoin.Click += (sender, eventArgs) =>
                {
                    dbms.joinActivity(userID, activity.Id);
                    btJoin.IsEnabled = false;
                };
            }


            Grid.SetRow(activityPhoto, 0);
            Grid.SetRowSpan(activityPhoto, 5);
            Grid.SetColumn(activityPhoto, 0);

            Grid.SetRow(lTitle, 0);
            Grid.SetColumn(lTitle, 1);
            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(30);
            showActivity.RowDefinitions.Add(row1);

            Grid.SetRow(lTime, 1);
            Grid.SetColumn(lTime, 1);
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(30);
            showActivity.RowDefinitions.Add(row2);

            Grid.SetRow(lLocation, 2);
            Grid.SetColumn(lLocation, 1);
            RowDefinition row3 = new RowDefinition();
            row3.Height = new GridLength(30);
            showActivity.RowDefinitions.Add(row3);

            Grid.SetRow(lMoney, 3);
            Grid.SetColumn(lMoney, 1);

            Grid.SetRow(btLike, 3);
            Grid.SetColumn(btLike, 2);
            RowDefinition row4 = new RowDefinition();
            row4.Height = new GridLength(30);
            showActivity.RowDefinitions.Add(row4);

            Grid.SetRow(lSponsor, 4);
            Grid.SetColumn(lSponsor, 1);

            Grid.SetRow(btJoin, 4);
            Grid.SetColumn(btJoin, 2);

            RowDefinition row5 = new RowDefinition();
            row5.Height = new GridLength(30);
            showActivity.RowDefinitions.Add(row5);


            RowDefinition row6 = new RowDefinition();
            row6.Height = new GridLength(10);
            showActivity.RowDefinitions.Add(row6);

            showActivity.Children.Add(lTitle);
            showActivity.Children.Add(lTime);
            showActivity.Children.Add(lLocation);
            showActivity.Children.Add(lMoney);
            showActivity.Children.Add(lSponsor);
            showActivity.Children.Add(btLike);
            showActivity.Children.Add(btJoin);
            showActivity.Children.Add(activityPhoto);

            //show introduction
            Label lIntroduction = new Label();
            lIntroduction.FontSize = 18;
            lIntroduction.Content = " " + "Introduction:";
            lIntroduction.HorizontalAlignment = HorizontalAlignment.Left;
            lIntroduction.VerticalAlignment = VerticalAlignment.Center;
            lIntroduction.Margin = new Thickness(0, 0, 0, 0);


            Grid.SetRow(lIntroduction, 0);
            Grid.SetColumn(lIntroduction, 0);

            RowDefinition row7 = new RowDefinition();
            row7.Height = new GridLength(30);
            introduction.RowDefinitions.Add(row7);


            TextBlock lIntroductionContent = new TextBlock();
            lIntroductionContent.FontSize = 14;
            lIntroductionContent.TextWrapping = TextWrapping.Wrap;
            lIntroductionContent.Text = "    "+ activity.introduction;
            lIntroductionContent.HorizontalAlignment = HorizontalAlignment.Left;
            lIntroductionContent.VerticalAlignment = VerticalAlignment.Center;
            lIntroductionContent.Margin = new Thickness(0, 0, 0, 0);

            Grid.SetRow(lIntroductionContent, 1);
            Grid.SetColumn(lIntroductionContent, 0);

            RowDefinition row8 = new RowDefinition();
            row8.Height = new GridLength(30);
            introduction.RowDefinitions.Add(row8);

            introduction.Children.Add(lIntroduction);
            introduction.Children.Add(lIntroductionContent);

            
            ////show comments
            List<Comment> comments = dbms.getActComment(activity.Id);
            foreach(Comment c in comments)
            {
                ColumnDefinition c4 = new ColumnDefinition();
                c4.Width = new GridLength(150);
                comment.ColumnDefinitions.Add(c4);

                ColumnDefinition c5 = new ColumnDefinition();
                c5.Width = new GridLength(500);
                comment.ColumnDefinitions.Add(c5);

                Label lCommentTitle = new Label();
                lCommentTitle.FontSize = 14;
                lCommentTitle.Content = " Comments:";
                lCommentTitle.HorizontalAlignment = HorizontalAlignment.Center;
                lCommentTitle.VerticalAlignment = VerticalAlignment.Center;
                lCommentTitle.Margin = new Thickness(0, 0, 0, 0);

                Image userCommentImage = new Image();
                userCommentImage.Source = imageSource;
                userCommentImage.Margin = new Thickness(0, 0, 0, 0);
                userCommentImage.HorizontalAlignment = HorizontalAlignment.Center;
                userCommentImage.VerticalAlignment = VerticalAlignment.Top;


                User user = dbms.getUserByID(c.userID);
                Label lCommentName = new Label();
                String name = user.name;
                lCommentName.FontSize = 13;
                lCommentName.Content = name + " said:";
                lCommentName.HorizontalAlignment = HorizontalAlignment.Left;
                lCommentName.VerticalAlignment = VerticalAlignment.Top;
                lCommentName.Margin = new Thickness(0, 0, 0, 0);

                Label lCommentTime = new Label();
                lCommentTime.FontSize = 13;
                lCommentTime.Content = "Time:" + c.commentDate;
                lCommentTime.HorizontalAlignment = HorizontalAlignment.Left;
                lCommentTime.VerticalAlignment = VerticalAlignment.Top;
                lCommentTime.Margin = new Thickness(0, 0, 0, 0);

                Label lComments = new Label();
                lComments.FontSize = 13;
                lComments.Content = c.comment;
                lComments.HorizontalAlignment = HorizontalAlignment.Left;
                lComments.VerticalAlignment = VerticalAlignment.Top;
                lComments.Margin = new Thickness(0, 0, 0, 0);

                Grid.SetRow(lCommentTitle, comment.RowDefinitions.Count);
                Grid.SetColumn(lCommentTitle, 0);

                RowDefinition r1 = new RowDefinition();
                r1.Height = new GridLength(25);
                comment.RowDefinitions.Add(r1);

                Grid.SetRow(userCommentImage, comment.RowDefinitions.Count);
                Grid.SetRowSpan(userCommentImage, 2);
                Grid.SetColumn(userCommentImage, 0);

                Grid.SetRow(lCommentName, comment.RowDefinitions.Count);
                Grid.SetColumn(lCommentName, 1);
                RowDefinition r2 = new RowDefinition();
                r2.Height = new GridLength(25);
                comment.RowDefinitions.Add(r2);

                Grid.SetRow(lCommentTime, comment.RowDefinitions.Count);
                Grid.SetColumn(lCommentTime, 1);
                RowDefinition r3 = new RowDefinition();
                r3.Height = new GridLength(25);
                comment.RowDefinitions.Add(r3);

                Grid.SetRow(lComments, comment.RowDefinitions.Count);
                Grid.SetColumn(lComments, 1);
                RowDefinition r4 = new RowDefinition();
                r4.Height = new GridLength(40);
                comment.RowDefinitions.Add(r4);

                comment.Children.Add(lCommentTitle);
                comment.Children.Add(userCommentImage);
                comment.Children.Add(lCommentName);
                comment.Children.Add(lCommentTime);
                comment.Children.Add(lComments);

            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            string commt = commentBT.Text;
            dbms.commentActivity(userID, actID, commt);
            
        }

    }
}