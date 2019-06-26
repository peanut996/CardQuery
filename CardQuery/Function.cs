using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CardQuery
{
    /// <summary>
    /// 包含各种骚操作的函数库
    /// </summary>
    class Function
    {
          Boolean IsConnectSQL = false;
        static String ConStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =" + Environment.CurrentDirectory + "\\CardQuerysqlserver2012.mdf; Integrated Security = True; Connect Timeout = 30";
        static SqlConnection  sqlconnect = new SqlConnection(ConStr); //进行数据库连接
        Function()
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
        public static void GetListBox() { } 



        /// <summary>
        /// 通过判断ListBox的值给出对应的查询语句
        /// </summary>
        public static String  GetSQCommand() {
            return " ";
        }

    }
}
