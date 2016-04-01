using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.BL;
namespace Movie.DB
{
    class DBElokuvat
    {
        public static DataTable GetData(string connStr)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {          
                    string sql = "SELECT* FROM elokuva";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Elokuva");
                    da.Fill(dt);
                    conn.Close();
                    Console.WriteLine(dt.DefaultView);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int AddData(string connStr, Movies movie) //lisää , MovieReview review
        {
            try
            {               
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {                   
                    conn.Open();
                    string sql = string.Format("INSERT INTO elokuva (nimi, genre, julkaisuvuosi, ohjaaja, saveltaja) VALUES (@NimiAtr, @GengeAtr, @JulkaisuvuosiAtr, @OhjaajaAtr, @SaveltajaAtr)");
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NimiAtr", movie.Name);
                    cmd.Parameters.AddWithValue("@GengeAtr", movie.Genre);
                    cmd.Parameters.AddWithValue("@JulkaisuvuosiAtr", movie.Year);
                    cmd.Parameters.AddWithValue("@OhjaajaAtr", movie.Director);
                    cmd.Parameters.AddWithValue("@SaveltajaAtr", movie.Composer);
                    int number = cmd.ExecuteNonQuery();
                    conn.Close();                 
                    return number;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
