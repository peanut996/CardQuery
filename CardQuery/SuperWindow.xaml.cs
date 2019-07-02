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
    /// SuperWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SuperWindow : Window
    {
        /// <summary>
        /// 高级权限 包括写入、删除等等
        /// </summary>
        /// 



        public SuperWindow()
        {
            InitializeComponent();
        }

        private void Excute_Click(object sender, RoutedEventArgs e)
        {
            Function.GetSQLToGrid(textBox.Text,this);
            Function.ChangeControlsVisibility(this);
        }
    }
}
