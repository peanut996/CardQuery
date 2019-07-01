
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
using System.Threading;
//using CardQueryLibrary;


namespace CardQuery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        //Function function = new Function();
        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
            Function function = new Function();
            this.Loaded += new RoutedEventHandler(WindowLoaded);
            //数据库未连接自动关闭


        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {

            if (App.Dic.ContainsKey("InitWindow"))
            {
                InitWindow initWindow = App.Dic["InitWindow"] as InitWindow;
                initWindow.Dispatcher.Invoke((Action)(() => initWindow.Close()));//在initWindow的线程上关闭InitWindow
       
            }
            if (!Function.IsConnectSQL)
            {
                this.Close();

            }
        }


        #endregion


        #region 各种Button_Click

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

        private void Super_Click(object sender, RoutedEventArgs e)
        {
            SuperWindow superWindow = new SuperWindow();
            superWindow.Show();
        }

        #endregion

        #region 各种Checkbox

        /// <summary>
        /// 高级模式开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            //注意checkBox一旦被点击即为改变状态 

            LoginWindow loginWindow = new LoginWindow();
            Function.AdminLoginStatus = false;

            ///未选中点击后的操作
            if (checkBox1.IsChecked == true)
            {
                if (loginWindow.ShowDialog() == false)  //检测窗口关闭状态
                {

                    if (Function.AdminLoginStatus)
                    {
                        
                        checkBox1.IsChecked = true;

                    }
                    else
                    {
                        checkBox1.IsChecked = false;
                     

                    }
                }
            }



        }


        /// <summary>
        /// 语言设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            /////未选中点击后的操作
            //if (checkBox2.IsChecked == true)
            //{

            //}

            //// 选中后点击的操作
            //if (checkBox2.IsChecked == true)
            //{

            //}
            MessageBox.Show("Coming Soon...","Sorry",MessageBoxButton.OK,MessageBoxImage.Warning);
            checkBox2.IsChecked = false;
        }

        #endregion


        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/peanut996");

        }

        private void Blog_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://onepeanut.me");

        }
    }
}
