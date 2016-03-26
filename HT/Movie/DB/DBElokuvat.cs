using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DB
{
    class DBElokuvat
    {
        public static DataTable GetData(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {          
                    string sql = "SELECT* FROM elokuva";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Elokuva");
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
