using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RentAWheel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmFleetView());

            //.Net所有未处理的异常都可以通过，AppDomain类的UnhandledException事件来捕获
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleUnhandledException);
        }
        //所有未处理异常事件的，事件处理程序
         static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("A problem occurred and the application cannot recover!Please contact the technical support.");
        }


    }
}
