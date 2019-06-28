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

namespace CardQuery
{
    /// <summary>
    /// 包含各种骚操作的函数库
    /// </summary>
    public class Function
    {
        private bool IsConnectSQL = false;
        public static bool IsAdvancedModeOn = false;
        public static bool AdminLoginStatus = false;
        
        static String ConStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =" + Environment.CurrentDirectory + "\\CardQuery.mdf; Integrated Security = True; Connect Timeout = 30";
        // Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\84942\Desktop\SQl Server\GUI\CardQuery\CardQuery\bin\Debug\CardQuery.mdf";Integrated Security = True; Connect Timeout = 30
        static SqlConnection  sqlConnect = new SqlConnection(ConStr); //进行数据库连接

        static String[] TableDict = { "ConsumRecord", "ChargeRecord","LossRecord","LibraryRecord","DormRecord" };
        //表名字典对应下标0 1 2 3 4 


        /// <summary>
        /// 包含判断SQL是否连接成功的构造函数
        /// </summary>
        public Function()
        {
            try
            {
                sqlConnect.Open();
                if (sqlConnect.State == ConnectionState.Open)  //测试能否连接
                {
                    IsConnectSQL = true;
                }
                else
                {
                    IsConnectSQL = false;
                }
                if (IsConnectSQL == true)
                {
                    MessageBox.Show("SQL Connected Succeed!","Succeed");
                }
 
            }
            catch
            {
                MessageBox.Show("SQL Connected Error. ","Connected Failed");
            }
        }

        /// <summary>
        /// 把获取到的数据转到表格
        /// </summary>
        /// <param name="sqlconnection">  </param>
        /// <param name="sqlConnection"></param>
        /// <param name="dataGrid"></param>
        public static void GetSQLToGrid(String sqlCommand,RecordWindow recordWindow)
        {

            //SqlCommand sqlcommand = new SqlCommand(commandstr,sqlconnect);//执行数据库操作 
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand, sqlConnect);
            DataSet dataSet = new DataSet();
            dataSet.Clear();
            DataTable dataTable1 = new DataTable();
            try { sqlDataAdapter.Fill(dataSet, "table"); }
            catch {
                MessageBox.Show("SQL Query Failed: "+sqlCommand,"ERROR");
            }
            recordWindow.dataGrid.DataContext = dataSet;
            recordWindow.dataGrid.Visibility = System.Windows.Visibility.Visible;//设置控件可见

        }
        /// <summary>
        /// 重载GetSQLtoGrid()方法
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
            try { sqlDataAdapter.Fill(dataSet, "table"); }
            catch
            {
                MessageBox.Show("SQL statement error: " + sqlCommand, "ERROR");
            }
            superWindow.dataGrid.DataContext = dataSet;
            superWindow.dataGrid.Visibility = System.Windows.Visibility.Visible;//设置控件可见

        }

        /// <summary>
        /// 根据不同的表产生不同的ListBox
        /// </summary>
        public static void GetListBox(RecordWindow recordWindow) {
            List<String[]> ListBox = new List<String[]>();
            ListBox.Add(new String[] { "ConsumSNo","ConsumCardID " ,"CRecordNo","ConsumDate","ConsumMachSNo"   });// consum表 下标0
            ListBox.Add(new String[] { "ChargeSNo", "ChargeCardID ", " ChargeNo","ChargeMachNo" ,"ChargeDate"});        //Charge 表 下标1
            ListBox.Add(new String[] { "LossSNo", "LossCardID ", "LossMachNo","LossDate", });                   //Loss表  下标2 
            ListBox.Add(new String[] { "BRecordSNo", "BRecordCardID ","BRecordDate", "BRecordLibNo"});        // Lib表 下标3
            ListBox.Add(new String[] { "DRecordSNo","DRecordCardID", "DRecordDNo","DRecordDate" });         // DormRecord表 下标4
            int result = JudgeRecordWindow(recordWindow);        //随机取值
            for (int i = 0; i < ListBox[result].Length; i++)
            {
                recordWindow.listBox.Items.Add(ListBox[result][i]); //listBox添加控件

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
            int result = JudgeRecordWindow(recordWindow);    //判断窗口
            for (int i = 0; i < ComboBox[result].Length; i++)
            {
                recordWindow.comboBox.Items.Add(ComboBox[result][i]); //listBox添加控件

            }

        }


        /// <summary>
        /// 获取recordWindow的类型并返回int
        /// </summary>
        /// <param name="recordWindow"></param>
        /// <returns></returns>
        public static int JudgeRecordWindow(RecordWindow recordWindow)
        {
            int result =666;
            for(int i = 0; i < TableDict.Length; i++)
            {
                if (TableDict[i] == recordWindow.Title)
                {
                    result = i;
                }

            }
            return result;
        }

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
        /// 通过判断ListBox的值给出对应的查询语句
        /// </summary>
        public static String  GetSQLCommand(RecordWindow recordWindow) {
            
            String[] SNoDict = { "ConsumSNo","ChargeSNo","LossSNo","BRecordSNo","DRecordSNo"};
            String str  = "select SName, "+ recordWindow.Title+ ".* from Student, " +recordWindow.Title ;  //默认连接学生表
            //"select SName , [title].* from Student, [title]"

            if (recordWindow.textBox.Text != "")
            {
                //"where [Text] = 'text' and Sno = 'Tittle.SNo' and Student,SNo = [Title].SNo"
                str = str + " where " + recordWindow.comboBox.Text + " = '" + recordWindow.textBox.Text
                    +"' and Student.SNo = "+recordWindow.Title+"."+SNoDict[JudgeRecordWindow(recordWindow)]; 
            }
            else
            {
                str = str+ " Where  Student.SNo = "+recordWindow.Title+"."+SNoDict[JudgeRecordWindow(recordWindow)];
                // "and Student,SNo = [Title].SNo"

            }
            MessageBox.Show(str,"SQL Sentence");
            return str;
        }

        /// <summary>
        /// 开启高级模式
        /// </summary>
        /// <param name="recordWindow"></param>
        public static void OpenAdvancedMode(RecordWindow recordWindow) {
            recordWindow.superCanvas.Visibility = System.Windows.Visibility.Visible;
            //考虑赋值
        }

    }
}
