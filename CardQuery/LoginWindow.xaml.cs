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
         

        public LoginWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            String[] acconut = new String[] { "Hydrogen", "Oxygen", "Nitrogen" ,"sa","Administrator","Admin"};
            string password = "849421294";
            if (acconut.Contains(Account.Text))
            {
                if (passWordBox.Password.Equals(password))
                {
                    Function.AdminLoginStatus = true;
                    MessageBox.Show("Login Success! ", "Success",MessageBoxButton.OK,MessageBoxImage.Information);
                    
                }
                else
                {
                    MessageBox.Show("Password is incorrect. ", "Failed",MessageBoxButton.OK,MessageBoxImage.Stop);
                }

            }
            else
            {
                MessageBox.Show("This Account is no exist.  ");
            }



        }


    }
}
