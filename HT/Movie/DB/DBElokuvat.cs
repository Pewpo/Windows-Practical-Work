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
        //haetaan elokuva taulun tiedot
        public static DataTable GetMovieData(string connStr)
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
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //haetaan arvostelu taulun tiedot
        public static DataTable GetReviewData(string connStr)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = "SELECT* FROM arvostelu";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Arvostelu");
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
        public static int UpdateData(string connStr, Movies movie, MovieReview movierv)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = string.Format("UPDATE elokuva SET nimi = @NimiAtr, genre = @GengeAtr , julkaisuvuosi =  @JulkaisuvuosiAtr, ohjaaja = @OhjaajaAtr , saveltaja = @SaveltajaAtr WHERE id ={0}", movie.MovieId);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NimiAtr", movie.Name);
                    cmd.Parameters.AddWithValue("@GengeAtr", movie.Genre);
                    cmd.Parameters.AddWithValue("@JulkaisuvuosiAtr", movie.Year);
                    cmd.Parameters.AddWithValue("@OhjaajaAtr", movie.Director);
                    cmd.Parameters.AddWithValue("@SaveltajaAtr", movie.Composer);
                    int number = cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    sql = string.Format("UPDATE arvostelu SET arvosteluteksti = @ArvostelutekstiAtr, linkki1 =  @Linkki1Atr, linkki2 =  @linkki2Atr WHERE elokuva_id ={0}", movie.MovieId);
                    MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@ArvostelutekstiAtr", movierv.Reviewtext);
                    cmd2.Parameters.AddWithValue("@Linkki1Atr", movierv.Link1);
                    cmd2.Parameters.AddWithValue("@linkki2Atr", movierv.Link2);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    return number;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region ADD DATA
        //lisätään elokuva sekä arvostelu tauluihin halutut tiedot
        public static int AddData(string connStr, Movies movie, MovieReview review)  
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    //Elokuvan tiedot kantaan
                    string sql = string.Format("INSERT INTO elokuva (nimi, genre, julkaisuvuosi, ohjaaja, saveltaja) VALUES (@NimiAtr, @GengeAtr, @JulkaisuvuosiAtr, @OhjaajaAtr, @SaveltajaAtr);");
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NimiAtr", movie.Name);
                    cmd.Parameters.AddWithValue("@GengeAtr", movie.Genre);
                    cmd.Parameters.AddWithValue("@JulkaisuvuosiAtr", movie.Year);
                    cmd.Parameters.AddWithValue("@OhjaajaAtr", movie.Director);
                    cmd.Parameters.AddWithValue("@SaveltajaAtr", movie.Composer);
                    int number = cmd.ExecuteNonQuery();

                    // otetaan viimeisin lisätty id  jotta saadaan Fkey  arvosteluun                 
                    sql = string.Format("SELECT LAST_INSERT_ID();");
                    cmd = new MySqlCommand(sql, conn);
                    int lastInsertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    conn.Open();
                    //Arvostelu kantaan   
                    string sql2 = string.Format("INSERT INTO arvostelu(arvostelija_id, elokuva_id, arvosteluteksti, linkki1, linkki2) VALUES (@ArvostelijaIdAtr, @ElokuvaIdAtr, @ArvostelutekstiAtr, @Linkki1Atr, @linkki2Atr)");
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@ArvostelijaIdAtr", review.Reviewerid);
                    cmd2.Parameters.AddWithValue("@ElokuvaIdAtr", lastInsertedID);
                    cmd2.Parameters.AddWithValue("@ArvostelutekstiAtr", review.Reviewtext);
                    cmd2.Parameters.AddWithValue("@Linkki1Atr", review.Link1);
                    cmd2.Parameters.AddWithValue("@linkki2Atr", review.Link2);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    return number;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion ADD DATA
        #region DELETE FUNCTIONS
        public static int DeleteData(string connStr, int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = string.Format("DELETE FROM elokuva WHERE id = {0} ", id);
                    string sql2 = string.Format("DELETE FROM arvostelu WHERE elokuva_id = {0} ", id);
                    conn.Open();
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);                                     
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
        #endregion DELETE FUNCTIONS
        public static bool CheckLogIn(string username, string password, string connStr)
        {
            try
            {
                bool help = false;
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = string.Format("SELECT id,tunnus,salasana FROM arvostelija WHERE tunnus = '{0}' AND salasana ='{1}'",username ,password);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    //tarkistetaan löytyykö kannasta
                    while (dr.Read())
                    {
                        if (dr.HasRows == true)
                        {
                            help = true;
                            BLMain.SetViewer(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                        }
                        else
                        {
                            help = false;
                        }
                    }
                    conn.Close();
                }
                return help;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
