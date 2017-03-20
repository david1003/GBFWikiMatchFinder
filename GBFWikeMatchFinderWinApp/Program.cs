using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBFWikeMatchFinderWinApp
{
    static class Program
    {
        //最後處理時間(加一小時換成日本時間)
        private static DateTime _lastMatchTime = DateTime.Now.AddHours(1);
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_GBF());
        }




    }
}
