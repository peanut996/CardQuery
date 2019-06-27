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
        static String ConStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =" + Environment.CurrentDirectory + "\\CardQuerysqlserver2012.mdf; Integrated Security = True; Connect Timeout = 30";
        static SqlConnection  sqlconnect = new SqlConnection(ConStr); //进行数据库连接
        public Function()
        {
                //String ConStr = "Server=.; database = CardQuery;Trusted_Connection=SSPI"; //link the local DB with windows auth
            try
            {
                sqlconnect.Open();
                if (sqlconnect.State == ConnectionState.Open)  //测试能否连接
                {
                    IsConnectSQL = true;
                }
                else
                {
                    IsConnectSQL = false;
                }
                if (IsConnectSQL == true)
                {
                    MessageBox.Show("SQL Link Succeed!","Succeed");
                }
            }
            catch
            {
                MessageBox.Show("SQL Connected Error. ","Connected Failed");
            }
        }



        #region 测试注释 
        /*code*/

        #endregion


        /// <summary>
        /// 把获取到的数据转到表格
        /// </summary>
        /// <param name="sqlconnection">  </param>
        /// <param name="sqlConnection"></param>
        /// <param name="dataGrid"></param>
        public static void GetSQLToGrid(String sqlcommand,DataGrid dataGrid)
        {

            //SqlCommand sqlcommand = new SqlCommand(commandstr,sqlconnect);//执行数据库操作 
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcommand, sqlconnect);
            DataSet dataSet = new DataSet();
            dataSet.Clear();
            DataTable dataTable1 = new DataTable();
            sqlDataAdapter.Fill(dataSet, "table");
            dataGrid.DataContext = dataSet;
            dataGrid.Visibility = System.Windows.Visibility.Visible;//设置控件可见

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
            int result = 996;        //随机取值
            switch (recordWindow.Title)
            {
                case "ConsumRecord": result = 0;break;
                case "ChargeRecord": result = 1;break;
                case "LossRecord": result = 2; break;
                case "LibraryRecord": result=3; break;
                case "DormRecord":  result = 4; break;
                default :
                    MessageBox.Show("ListBox获取失败","Error");
                    break;
            }
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
            ComboBox.Add(new String[] { "*", "ConsumSNo", "ConsumCardID ", "CRecordNo", "ConsumDate", "ConsumMachSNo" });// consum表 下标0
            ComboBox.Add(new String[] { "*", "ChargeSNo", "ChargeCardID ", " ChargeNo", "ChargeMachNo", "ChargeDate" });        //Charge 表 下标1
            ComboBox.Add(new String[] { "*", "LossSNo", "LossCardID ", "LossMachNo", "LossDate", });                   //Loss表  下标2 
            ComboBox.Add(new String[] { "*", "BRecordSNo", "BRecordCardID ", "BRecordDate", "BRecordLibNo" });        // Lib表 下标3
            ComboBox.Add(new String[] { "*", "DRecordSNo", "DRecordCardID", "DRecordDNo", "DRecordDate" });         // DormRecord表 下标4
            int result = 996;    //随机取值
            switch (recordWindow.Title)
            {
                case "ConsumRecord": result = 0; break;
                case "ChargeRecord": result = 1; break;
                case "LossRecord": result = 2; break;
                case "LibraryRecord": result = 3; break;
                case "DormRecord": result = 4; break;
                default:
                    MessageBox.Show("ListBox获取失败", "Error");
                    break;
            }
            for (int i = 0; i < ComboBox[result].Length; i++)
            {
                recordWindow.comboBox.Items.Add(ComboBox[result][i]); //listBox添加控件

            }

        }




        /// <summary>
        /// 通过判断ListBox的值给出对应的查询语句
        /// </summary>
        public static String  GetSQCommand(RecordWindow recordWindow) {
            String str=null;
            str = "select "+ recordWindow.comboBox.Text+ " from " +recordWindow.Title ;
            if (recordWindow.comboBox.Text == null)
            {
                str = str + " where " + recordWindow.comboBox.Text + " = " + recordWindow.label.Content;
            }
            else
            {
                str = "select " + recordWindow.comboBox.Text + " from " + recordWindow.Title;

            }
            return str;
        }

    }
}
