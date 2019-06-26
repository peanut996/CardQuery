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
//using CardQueryLibrary;


namespace CardQuery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }




        #region Button_Click

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow FillRecord = new RecordWindow("FillRecord");
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
            MessageBox.Show("HelloWorld");
        }
    }
}
