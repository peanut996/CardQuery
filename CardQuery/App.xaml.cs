using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CardQuery
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Dictionary<string, object> Dic = new Dictionary<string, object>();
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                InitWindow initWindow = new InitWindow();
                Dic["InitWindow"] = initWindow;//储存
                initWindow.ShowDialog();//不能用Show
            });
            t.SetApartmentState(ApartmentState.STA);//设置单线程
            t.Start();

            base.OnStartup(e);

        }


    }
}
