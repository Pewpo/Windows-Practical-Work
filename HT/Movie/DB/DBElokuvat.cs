﻿using System;
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
        #region REGISTER/LOGIN
        //Kirjautumisen tarkistus
        public static bool CheckLogIn(string username, string password, string connStr)
        {
            try
            {/*
                // TÄTÄ KÄYTETÄÄN SIIHEN PAREMPAAN SALAUKSEEN
                bool PswExists = false;
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = string.Format("SELECT salasana FROM arvostelija WHERE tunnus = '{0}'", username);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Console.WriteLine("Taalla");
                        if (dr.HasRows == true)
                        {
                            Console.WriteLine(password);
                            Console.WriteLine(dr.GetString(0).ToString());

                            PswExists = BLMain.VerifyCryptedPassword(dr.GetString(0).ToString(), password);
                        }
                        Console.WriteLine("nyt taalla");
                    }
                    dr.Close();
                    conn.Close();
                }
                //Jos salasanat vastaavat toisiaan voidaan jatkaa eteenpäin
                if (PswExists == true)
                {*/
                bool help = false;
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        string sql = string.Format("SELECT id,tunnus,salasana FROM arvostelija WHERE tunnus = '{0}' AND salasana ='{1}'", username, password);
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
                        dr.Close();
                        conn.Close();
                    }
                    return help;
                // TÄTÄ KÄYTETÄÄN SIIHEN PAREMPAAN SALAUKSEEN    
                /*    } 
                    else
                    {
                        return false;
                    }*/
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //uuden käyttäjän rekisteröinti
        public static bool RegisterNewViewer(string connStr, string username, string password)
        {
            try
            {
                bool AllreadyExists = false;
                bool help = false;
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = string.Format("SELECT tunnus FROM arvostelija WHERE tunnus = '{0}'", username);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    
                    //tarkistetaan löytyykö kannasta
                    while (dr.Read())
                    {
                        if (dr.HasRows == true)
                        {
                            help = true;
                        }
                        else
                        {
                            help = false;
                        }
                    }
                    dr.Close();
                    if (help == true)
                    {
                        AllreadyExists = true;
                    }
                    else
                    {   //Käyttäjää ei vielä ole --> lisätään se
                        AllreadyExists = false;
                        string sql2 = string.Format("INSERT INTO arvostelija(tunnus, salasana) VALUES (@TunnusAtr, @SalasanaAtr)");
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        cmd2.Parameters.AddWithValue("@TunnusAtr", username);
                        cmd2.Parameters.AddWithValue("@SalasanaAtr", password);
                        cmd2.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return AllreadyExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion REGISTER/LOGIN
        public static DataTable GetWantedData(string what, string wanted, string connStr)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string sql = string.Format( "SELECT* FROM elokuva WHERE {0} = '{1}'", what, wanted);
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
    }
}
