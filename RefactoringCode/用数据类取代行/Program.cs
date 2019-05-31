using System.Windows.Forms;

namespace DataBaseCmd2Class
{
    public class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AccountView());;
        }
    }
}