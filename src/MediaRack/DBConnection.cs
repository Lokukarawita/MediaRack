using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace MediaRack
{
    public class DBConnection
    {
        private static OleDbConnection con;

        static DBConnection()
        {
            string constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=False;";
            string dbp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mcdb.accdb");
            constr = string.Format(constr, dbp);
            con = new OleDbConnection(constr);
        }

        public static OleDbConnection Open() 
        {
            switch (con.State)
            {
                case ConnectionState.Broken:
                    con.Open();
                    break;
                case ConnectionState.Closed:
                    con.Open();
                    break;
                default:
                    break;
            }

            return con;
        }
        public static void Close()
        {
            switch (con.State)
            {
                case ConnectionState.Connecting:
                    con.Close();
                    break;
                case ConnectionState.Executing:
                    con.Close();
                    break;
                case ConnectionState.Fetching:
                    con.Close();
                    break;
                case ConnectionState.Open:
                    con.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
