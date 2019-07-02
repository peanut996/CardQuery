using System;
using System.Collections;
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
    /// RecordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RecordWindow : Window
    {
        public int recordWindowInt;

        public RecordWindow()
        {
            InitializeComponent();
        }

        public RecordWindow(String str)
        {
            InitializeComponent();
            Title = str;
            label.Content = str;
            recordWindowInt = Function.JudgeRecordWindow(this);
            Function.GetListBox(this);  //获取listBox控件
            Function.GetComboBox(this); // 获取ComboBox控件


            //检查是否打开高级模式
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.checkBox1.IsChecked==true)
            {
                Function.OpenAdvancedMode(this);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            {  //语句较为复杂  再三考虑
                //String command = "select * from "
                //Function.GetSQLToGrid()
                Function.GetSQLToGrid(Function.GetSQLCommand(this), this);
                Function.ChangeControlsVisibility(this);

            }
        }

        private void Readme_Click(object sender, RoutedEventArgs e)
        {
            String str = "Guest users only provide simple queries. For advanced features, please open advanced mode.";
            MessageBox.Show(str, "Readme");
        }
        #region Canvas中的CLick
        private void Superbutton0_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Function.GetSQLToGrid(Function.GetSuperSQLCommand(this, (String)superbutton0.Content), this);
                


            }
            catch
            {
                MessageBox.Show("Get all information Failed! ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Superbutton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Function.GetSQLToGrid(Function.GetSuperSQLCommand(this, (String)superbutton1.Content), this);
                

            }catch
            {
                MessageBox.Show("Insert Failed! ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Superbutton2_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    Function.GetSQLToGrid(Function.GetSuperSQLCommand(this, (String)superbutton2.Content), this);
                    


                }
                catch
                {
                    MessageBox.Show("Update Failed! ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Superbutton3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Function.GetSQLToGrid(Function.GetSuperSQLCommand(this, (String)superbutton3.Content), this);
               


            }
            catch
            {
                MessageBox.Show("Delete Failed! ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
