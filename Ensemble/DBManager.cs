using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ensemble
{
    class DBManager
    {
        private static MySqlConnection conn = new MySqlConnection("Server=localhost;Database=ensemble;Uid=root;Pwd=;");
        private static int ReturnCode = 0;

        public static int connect()
        {
            try
            {
                ReturnCode = 0;
                conn.Open();
            }
            catch (Exception e)
            {
                ReturnCode = 1;
                System.Console.WriteLine("Connection goes wrong: " + e.Message);
            }
            return ReturnCode;
        }

        public static void disconnect()
        {
            conn.Close();
        }





    }
}
