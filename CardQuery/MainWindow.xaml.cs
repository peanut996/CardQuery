
/*
 * 
 * 2019/06/27 14:09 修改Consum表中SNo字段 请在2017数据库自行修改
 *    以及LibRecord中  BReCordSNo字段的更新 
 *    修改LibRecord为LibraryRecord
 *    实现了ListBox和ComboBox 的动态更新
 * 
 * */

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
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
//using CardQueryLibrary;


namespace CardQuery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
            //Function function = new Function();
        
            public MainWindow()
             { 
                InitializeComponent();
            Function function =new Function();

             }


        #region Button_Click

        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow FillRecord = new RecordWindow("ChargeRecord");
            FillRecord.Show();
            
        }
       
        private void Loss_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow LossRecord = new RecordWindow("LossRecord");
            LossRecord.Show();
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow LibraryRecord = new RecordWindow("LibraryRecord");
            LibraryRecord.Show();
        }

        private void Consum_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow ConsumRecord = new RecordWindow("ConsumRecord");
            ConsumRecord.Show();
        }

        private void Dorm_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow DormRecord = new RecordWindow("DormRecord");
            DormRecord.Show();
        }
        #endregion


        /// <summary>
        /// 暂未实现 想法是先创建一个新的Window带有高级权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            if (Function.AdminLoginStatus)
            {
                Function.IsAdvancedModeOn = true;

            }else
            {
                checkBox1.IsChecked = false;

            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Super_Click(object sender, RoutedEventArgs e)
        {
            SuperWindow superWindow = new SuperWindow();
            superWindow.Show();
        }
    }
}
