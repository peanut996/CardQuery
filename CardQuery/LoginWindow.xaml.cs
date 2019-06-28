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

namespace CardQuery
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
         public MainWindow mainWindow = new MainWindow(); 

        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            String[] acconut = new String[] { "Hydrogen", "Oxygen", "Nitrogen" ,"sa","Administrator","Admin"};
            string password = "849421294";
            if (acconut.Contains(Account.Text))
            {
                if (Password.Text.Equals(password))
                {
                    Function.AdminLoginStatus = true;
                    MessageBox.Show("Login Success! ", "Success");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password is incorrect. ", "Failed");
                }

            }
            else
            {
                MessageBox.Show("This Account is no exist.  ");
            }
            if (Function.AdminLoginStatus)
            {
                Function.IsAdvancedModeOn = true;
                mainWindow. checkBox1.IsChecked = true;
                this.Show();
            }
            else
            {
                //checkBox1.IsChecked = false;

            }


        }


    }
}
