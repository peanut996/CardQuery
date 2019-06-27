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
        public RecordWindow()
        {
            InitializeComponent();
           

        }

        public RecordWindow(String str)
        {
            InitializeComponent();
            Title = str;
            label.Content = str;
            Function.GetListBox(this);  //获取listBox控件
            Function.GetComboBox(this); // 获取ComboBox控件

            //检查是否打开高级模式
            if (Function.IsAdvancedModeOn)
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
    }
}
