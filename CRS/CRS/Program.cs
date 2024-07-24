using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm());
        }
    }

    public static class Globals
    {
        private static string connectionStringForWaleed = "Data Source=DESKTOP-B30LDQ6\\SQLEXPRESS;Initial Catalog=crs;Integrated Security=True";
        private static string connectionStringForAliAhmed = "Data Source=DESKTOP-15SJL0B;Initial Catalog=crs;Integrated Security=True";

        public static SqlConnection createSQLConnection()
        {
            return new SqlConnection(connectionStringForWaleed);
        }
    }
}
