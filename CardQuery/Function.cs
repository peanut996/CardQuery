using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace CardQuery
{
    /// <summary>
    /// 包含各种骚操作的函数库
    /// </summary>
    public class Function
    {
        #region Bool型类变量
        public static bool IsConnectSQL = false;

        public static bool AdminLoginStatus = false;
        #endregion



        #region SQL连接参数
        static String ConStr = "";
        public static String iPAddress = "66.42.63.200";

        //v1.0.0.2 更新目录文件为D:\\
        //static String ConStr1 = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =" + Environment.CurrentDirectory + "\\CardQuery.mdf; Integrated Security = True; Connect Timeout = 30";

        static String ConStr1 = "Data Source=" + iPAddress + ";database=CardQuery;Persist Security Info=True;User ID=sa;Password=Wu849421294";
        static String ConStr2 = "Data Source=.;database=CardQuery;integrated security = true";


        static SqlConnection sqlConnect;
        static MessageBoxResult Chioce;
        static string message = "Connect   to   which   one   you  want ?\n" +
                                 "" + "      'Yes'   for   Remote  Server.      \n" + "      'No'   for   SQL  Server.";

        #endregion

        #region 表和学号的字典
        public static String[] SNoDict = { "ConsumSNo", "ChargeSNo", "LossSNo", "BRecordSNo", "DRecordSNo" };
        public static String[] TableDict = { "ConsumRecord", "ChargeRecord", "LossRecord", "LibraryRecord", "DormRecord" };
        #endregion

        #region 包含判断SQL连接的构造函数
        /// <summary>
        /// 包含判断SQL是否连接成功的构造函数
        /// </summary>
        public Function()
        {
            if (Ping(iPAddress)) //网络联通
            {
                //网络连接
                Chioce = MessageBox.Show(message, "SQL Chioce", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                message = "Do  you  want  to  connect  to  local  SQL Server?";
                Chioce = MessageBox.Show(message, "SQL Chioce", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                if (Chioce == MessageBoxResult.OK)
                {

                    Chioce = MessageBoxResult.No;
                }
                else
                {
                    Chioce = MessageBoxResult.Cancel;
                }
                //本地连接

            }

            if (Chioce == MessageBoxResult.Yes)
            {
                ConStr = ConStr1;

            }
            else if (Chioce == MessageBoxResult.No)
            {
                ConStr = ConStr2;
            }
            else if (Chioce == MessageBoxResult.Cancel)
            {
                return;
            }

            sqlConnect = new SqlConnection(ConStr); //进行数据库连接
            try
            {
                sqlConnect.Open();
                if (sqlConnect.State == ConnectionState.Open)  //测试能否连接
                {
                    IsConnectSQL = true;
                    MessageBox.Show("SQL Connected Succeed!", "Congratulation! ", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }

            }
            catch (SqlException e)
            {
                MessageBox.Show("SQL Connected Failed: " + e.Message, "Bad News", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

            }



        }




        //判断服务器地址能否接通
        public static bool Ping(String iPAddress)
        {
            bool pingResult = false;
            Ping pingSender = new Ping();
            //注意延迟时间
            PingReply pingReply = pingSender.Send(iPAddress, 3000);
            if (pingReply.Status != IPStatus.Success)
            {
                //CloseInitWindow();
                MessageBox.Show("InterNetwork  Not  Connected.  ", "Network Status", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

            }
            else
            {
                //CloseInitWindow();
                MessageBox.Show("InterNetwork  Connected.  ", "Network Status", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                pingResult = true;
            }


            return pingResult;
        }

        #region SQL操作

        /// <summary>
        /// 通过判断ListBox的值给出对应的查询语句
        /// </summary>
        public static String GetSQLCommand(RecordWindow recordWindow)
        {


            String sqlCommand = "select SName, " + recordWindow.Title + ".* from Student, " + recordWindow.Title;  //默认连接学生表
            //"select SName , [title].* from Student, [title]"

            if (recordWindow.textBox.Text != "")
            {
                //"where [Text] = 'text' and Sno = 'Tittle.SNo' and Student,SNo = [Title].SNo"
                sqlCommand = sqlCommand + " where " + recordWindow.comboBox.Text + " = '" + recordWindow.textBox.Text
                    + "' and Student.SNo = " + recordWindow.Title + "." + SNoDict[JudgeRecordWindow(recordWindow)];
            }
            else
            {
                sqlCommand = sqlCommand + " Where  Student.SNo = " + recordWindow.Title + "." + SNoDict[JudgeRecordWindow(recordWindow)];
                // "and Student,SNo = [Title].SNo"

            }
            MessageBox.Show(sqlCommand, "SQL Sentence");
            return sqlCommand;
        }
        public static String GetSuperSQLCommand(RecordWindow recordWindow, String buttonCommand)
        {
            //recordWindowInt参数2 4 只有五个字段 LossRecord 和 DormRecord
            String sqlCommand = "";
            switch (buttonCommand)
            {
                case "GetAll":
                    // "select Sname , * from Student ,[Table] Where SNo = [Table].SNo"
                    sqlCommand = "select SName," + TableDict[recordWindow.recordWindowInt] + ".* from Student, " + TableDict[recordWindow.recordWindowInt] + " Where  Student.SNo = " + SNoDict[JudgeRecordWindow(recordWindow)];
                    break;
                case "Insert":
                    // insert into [Table] values(para,para,para,para,para)
                    if (recordWindow.superTextBox5.Text != "")
                    {

                        //六个字段的情况
                        sqlCommand = "Insert into " + TableDict[recordWindow.recordWindowInt]
                        + " Values('" + recordWindow.superTextBox0.Text + "','" + recordWindow.superTextBox1.Text + "','" + recordWindow.superTextBox2.Text + "','" + recordWindow.superTextBox3.Text + "','" + recordWindow.superTextBox4.Text +
                        "','" + recordWindow.superTextBox5.Text + "')";
                    }
                    else
                    {
                        //五个字段
                        sqlCommand = "Insert into " + TableDict[recordWindow.recordWindowInt]
                        + " Values('" + recordWindow.superTextBox0.Text + "','" + recordWindow.superTextBox1.Text + "','" + recordWindow.superTextBox2.Text + "','" + recordWindow.superTextBox3.Text + "','" + recordWindow.superTextBox4.Text
                        + "')";
                    }
                    break;
                case "Update":
                    //保留记录号所在字段
                    if (recordWindow.superTextBox5.Text != "")
                    {
                        sqlCommand = "Update " + TableDict[recordWindow.recordWindowInt] + " Set "
                            + recordWindow.superLabel1.Content + "= '" + recordWindow.superTextBox1.Text + "' ,"
                            + recordWindow.superLabel2.Content + "= '" + recordWindow.superTextBox2.Text + "' ,"
                            + recordWindow.superLabel3.Content + "= '" + recordWindow.superTextBox3.Text + "' ,"
                            + recordWindow.superLabel4.Content + "= '" + recordWindow.superTextBox4.Text + "' ,"
                            + recordWindow.superLabel5.Content + "= '" + recordWindow.superTextBox5.Text + "' "
                            + "where " + recordWindow.superLabel0.Content + " ='" + recordWindow.superTextBox0.Text + "' ";
                    }
                    else
                    {
                        sqlCommand = "Update " + TableDict[recordWindow.recordWindowInt] + " Set "
                            + recordWindow.superLabel1.Content + "= '" + recordWindow.superTextBox1.Text + "' ,"
                            + recordWindow.superLabel2.Content + "= '" + recordWindow.superTextBox2.Text + "' ,"
                            + recordWindow.superLabel3.Content + "= '" + recordWindow.superTextBox3.Text + "' ,"
                            + recordWindow.superLabel4.Content + "= '" + recordWindow.superTextBox4.Text + "' "
                            + "where " + recordWindow.superLabel0.Content + " ='" + recordWindow.superTextBox0.Text + "' ";

                    }
                    break;
                case "Delete":
                    sqlCommand = "Delete From " + TableDict[recordWindow.recordWindowInt]
                        + " where " + recordWindow.superLabel0.Content + " ='" + recordWindow.superTextBox0.Text + "' ";
                    break;
                default:
                    break;
            }
            MessageBox.Show(sqlCommand, "SQL Sentence");
            return sqlCommand;
        }

        /// <summary>
        /// 把获取到的数据转到表格
        /// </summary>
        /// <param name="sqlconnection">  </param>
        /// <param name="sqlConnection"></param>
        /// <param name="dataGrid"></param>
        public static void GetSQLToGrid(String sqlCommand, RecordWindow recordWindow)
        {

            //SqlCommand sqlcommand = new SqlCommand(commandstr,sqlconnect);//执行数据库操作 
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand, sqlConnect);
            DataSet dataSet = new DataSet();
            dataSet.Clear();
            DataTable dataTable1 = new DataTable();
            try
            {
                sqlDataAdapter.Fill(dataSet, "table");
                MessageBox.Show("SQL Query Succeed! ", "Congratulation！", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("SQL Query Failed: " + e.Message, "Bad News", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            recordWindow.dataGrid.DataContext = dataSet;
            Function.ChangeControlsVisibility(recordWindow);

        }

        /// <summary>
        /// 把获取到的数据转到表格 重载+1
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="superWindow"></param>
        public static void GetSQLToGrid(String sqlCommand, SuperWindow superWindow)
        {

            //SqlCommand sqlcommand = new SqlCommand(commandstr,sqlconnect);//执行数据库操作 
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand, sqlConnect);
            DataSet dataSet = new DataSet();
            dataSet.Clear();
            DataTable dataTable1 = new DataTable();
            try
            {
                sqlDataAdapter.Fill(dataSet, "table");
                MessageBox.Show("SQL Query Succeed! ", "Congratulation！", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("SQL Query Failed: " + e.Message, "Bad News", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            superWindow.dataGrid.DataContext = dataSet;
            Function.ChangeControlsVisibility(superWindow);

        }

        #endregion

        #region 根据表不同产生不同的响应

        //表名字典对应下标0 1 2 3 4 

        /// <summary>
        /// 获取recordWindow的类型并返回int
        /// </summary>
        /// <param name="recordWindow"></param>
        /// <returns></returns>
        public static int JudgeRecordWindow(RecordWindow recordWindow)
        {
            int result = 666;
            for (int i = 0; i < TableDict.Length; i++)
            {
                if (TableDict[i] == recordWindow.Title)
                {
                    result = i;
                }

            }
            return result;
        }

        /// <summary>
        /// 根据不同的表产生不同的CanvasLabelText
        /// </summary>
        /// <param name="recordWindow"></param>
        public static void GetCanvasLabelText(RecordWindow recordWindow)
        {
            //ComboBox.Add(new String[] { "*", "ConsumSNo", "ConsumCardID ", "CRecordNo", "ConsumDate", "ConsumMachSNo" });
            //ComboBox.Add(new String[] { "*", "ChargeSNo", "ChargeCardID ", "ChargeNo", "ChargeMachNo", "ChargeDate" });
            //ComboBox.Add(new String[] { "*", "LossSNo", "LossCardID ", "LossMachNo", "LossDate", });
            //ComboBox.Add(new String[] { "*", "BRecordSNo", "BRecordCardID ", "BRecordDate", "BRecordLibNo" });
            //ComboBox.Add(new String[] { "*", "DRecordSNo", "DRecordCardID", "DRecordDNo", "DRecordDate" });

            switch (recordWindow.recordWindowInt)
            {
                case 0:
                    /*COLUMN_NAME CRecordNo ConsumDate ConsumMachNo ConsumMoney ConsumCardID ConsumSNo
                     * */
                    recordWindow.superLabel0.Content = "CRecordNo"; recordWindow.superLabel1.Content = "ConsumDate"; recordWindow.superLabel2.Content = "ConsumMachNo";
                    recordWindow.superLabel3.Content = "ConsumMoney"; recordWindow.superLabel4.Content = "ConsumCardID"; recordWindow.superLabel5.Content = "ConsumSNo";
                    break;
                case 1:
                    /*ChargeNo ChargeCardID ChargeDate ChargeMoney ChargeSNo ChargeMachNo */
                    recordWindow.superLabel0.Content = "ChargeNo"; recordWindow.superLabel1.Content = "ChargeCardID"; recordWindow.superLabel2.Content = "ChargeDate";
                    recordWindow.superLabel3.Content = "ChargeMoney"; recordWindow.superLabel4.Content = "ChargeSNo"; recordWindow.superLabel5.Content = "ChargeMachNo";
                    break;
                case 2:
                    /*  LossNo LossCardID LossDate LossSNo LossMachNo
                     * */
                    recordWindow.superLabel0.Content = "LossNo"; recordWindow.superLabel1.Content = "LossCardID"; recordWindow.superLabel2.Content = "LossDate";
                    recordWindow.superLabel3.Content = "LossSNo"; recordWindow.superLabel4.Content = "LossMachNo";
                    //隐藏 superLabel5 和 superTextBox5 
                    recordWindow.superLabel5.Visibility = System.Windows.Visibility.Collapsed;
                    recordWindow.superTextBox5.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 3:
                    /*
                     * BRecordNo BRecordCardID BRecordDate BRecordLibNo BRecordList BRecordSNo
                     * */
                    recordWindow.superLabel0.Content = "BRecordNo"; recordWindow.superLabel1.Content = "BRecordCardID"; recordWindow.superLabel2.Content = "BRecordDate";
                    recordWindow.superLabel3.Content = "BRecordLibNo"; recordWindow.superLabel4.Content = "BRecordList"; recordWindow.superLabel5.Content = "BRecordSNo";
                    break;
                case 4:
                    /*
                    DRecordNo DRecordDate DRecordCardID DRecordSNo DRecordDNo
                    */
                    recordWindow.superLabel0.Content = "DRecordNo"; recordWindow.superLabel1.Content = "DRecordDate"; recordWindow.superLabel2.Content = "DRecordCardID";
                    recordWindow.superLabel3.Content = "DRecordSNo"; recordWindow.superLabel4.Content = "DRecordDNo";
                    //隐藏 superlabel5
                    recordWindow.superLabel5.Visibility = System.Windows.Visibility.Collapsed;
                    recordWindow.superTextBox5.Visibility = System.Windows.Visibility.Collapsed;
                    break;

            }

        }

        /// <summary>
        /// 根据不同的表产生不同的ListBox
        /// </summary>
        public static void GetListBox(RecordWindow recordWindow)
        {
            List<String[]> ListBox = new List<String[]>();
            ListBox.Add(new String[] { "ConsumSNo", "ConsumCardID ", "CRecordNo", "ConsumDate", "ConsumMachSNo" });// consum表 下标0
            ListBox.Add(new String[] { "ChargeSNo", "ChargeCardID ", " ChargeNo", "ChargeMachNo", "ChargeDate" });        //Charge 表 下标1
            ListBox.Add(new String[] { "LossSNo", "LossCardID ", "LossMachNo", "LossDate", });                   //Loss表  下标2 
            ListBox.Add(new String[] { "BRecordSNo", "BRecordCardID ", "BRecordDate", "BRecordLibNo" });        // Lib表 下标3
            ListBox.Add(new String[] { "DRecordSNo", "DRecordCardID", "DRecordDNo", "DRecordDate" });         // DormRecord表 下标4

            for (int i = 0; i < ListBox[recordWindow.recordWindowInt].Length; i++)
            {
                recordWindow.listBox.Items.Add(ListBox[recordWindow.recordWindowInt][i]); //listBox添加控件

            }

        }


        /// <summary>
        /// 
        /// 根据不同的表产生不同的ComboBox
        /// </summary>
        /// <param name="recordWindow"></param>
        public static void GetComboBox(RecordWindow recordWindow)
        {
            List<String[]> ComboBox = new List<String[]>();
            ComboBox.Add(new String[] { "*", "ConsumSNo", "ConsumCardID ", "CRecordNo", "ConsumDate", "ConsumMachSNo" });
            ComboBox.Add(new String[] { "*", "ChargeSNo", "ChargeCardID ", "ChargeNo", "ChargeMachNo", "ChargeDate" });
            ComboBox.Add(new String[] { "*", "LossSNo", "LossCardID ", "LossMachNo", "LossDate", });
            ComboBox.Add(new String[] { "*", "BRecordSNo", "BRecordCardID ", "BRecordDate", "BRecordLibNo" });
            ComboBox.Add(new String[] { "*", "DRecordSNo", "DRecordCardID", "DRecordDNo", "DRecordDate" });

            for (int i = 0; i < ComboBox[recordWindow.recordWindowInt].Length; i++)
            {
                recordWindow.comboBox.Items.Add(ComboBox[recordWindow.recordWindowInt][i]); //listBox添加控件

            }

        }

        #endregion


        /// <summary>
        /// 改变控件的可见性
        /// </summary>
        /// <param name="recordWindow"></param>
        public static void ChangeControlsVisibility(RecordWindow recordWindow)
        {
            recordWindow.Image1.Visibility = System.Windows.Visibility.Visible;
            recordWindow.Image2.Visibility = System.Windows.Visibility.Visible;
            recordWindow.dataGrid.Visibility = System.Windows.Visibility.Visible;//设置控件可见
        }
        /// <summary>
        /// 改变控件的可见性 重载+1
        /// </summary>
        /// <param name="superWindow"></param>
        public static void ChangeControlsVisibility(SuperWindow superWindow)
        {

            superWindow.dataGrid.Visibility = System.Windows.Visibility.Visible;//设置控件可见
        }


        /// <summary>
        /// 开启高级模式
        /// </summary>
        /// <param name="recordWindow"></param>
        public static void OpenAdvancedMode(RecordWindow recordWindow)
        {
            recordWindow.superCanvas.Visibility = System.Windows.Visibility.Visible;
            recordWindow.Readme.Visibility = System.Windows.Visibility.Collapsed;
            Function.GetCanvasLabelText(recordWindow);
            //考虑赋值
        }

    }
}
#endregion