using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DB;
using System.IO;
using System.Security.Cryptography;

namespace Movie.BL
{   
    class BLMain
    {

        public static Viewer current;
        private static string cs = Movie.Properties.Settings.Default.Elokuva;
        //haetaan elokuva tualun tiedot movie olioon
        public static List<Movies> GetMovieData()
        {
            try
            {
               DataTable dt = DBElokuvat.GetMovieData(cs);
               List<Movies> movies = PutToMovie(dt);
                return movies;
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Movies> GetWantedData(string what, string wanted)
        {
            try
            {
                DataTable dt = DBElokuvat.GetWantedData(what, wanted, cs);
                List<Movies> movies = PutToMovie(dt);
                return movies;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static List<Movies> PutToMovie(DataTable dt)
        {
            List<Movies> movie = new List<Movies>();

            foreach (DataRow row in dt.Rows)
            {
                Movies oneMovie = new Movies();
                oneMovie.MovieId = (int)row["id"];
                oneMovie.Name = row["nimi"].ToString();
                oneMovie.Genre = row["genre"].ToString();
                oneMovie.Year = (int)row["julkaisuvuosi"];
                oneMovie.Director = row["ohjaaja"].ToString();
                oneMovie.Composer = row["saveltaja"].ToString();
                movie.Add(oneMovie);
            }
            return movie;
        }
        //Haetaan henkilön tiedot
        public static bool CheckLogIn(string username, string password)
        {
            try
            {
              bool help =  DBElokuvat.CheckLogIn(username, password, cs);
                return help;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool RegisterNewViewer(string username, string password)
        {
            try
            {
                bool answer = DBElokuvat.RegisterNewViewer(cs, username, password);
                return answer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        //     private static byte[] CryptPassword(string password byte[] salt)
        //   {
        //     try
        //   { // TÄTÄ KÄYTETÄÄN SIIHEN PAREMPAAN SALAUKSEEN
        /*byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        string savedPasswordHash = Convert.ToBase64String(hashBytes);
        return savedPasswordHash;*/


        /*var md5 = new MD5CryptoServiceProvider();
        byte[] byteArray = Encoding.ASCII.GetBytes(password);
        var md5data = md5.ComputeHash(byteArray);               
        return md5data;*/


        //            }
        //          catch (Exception ex)
        //        {

        //          throw ex;
        //    }
        //}
        //salasanojen vastaavuuden tarkistus
        // TÄTÄ KÄYTETÄÄN SIIHEN PAREMPAAN SALAUKSEEN
        /*  public static bool VerifyCryptedPassword(string cryptedPassword, string clearPassword)
          {
              try
              {
                        bool answer = true;
                         //Fetch the stored value 
                         string savedPasswordHash = cryptedPassword;
                         // Extract the bytes 
                         byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                         // Get the salt 
                         byte[] salt = new byte[16];
                         Array.Copy(hashBytes, 0, salt, 0, 16);
                         // Compute the hash on the password the user entered 
                         var pbkdf2 = new Rfc2898DeriveBytes(clearPassword, salt, 10000);
                         byte[] hash = pbkdf2.GetBytes(20);
                         // Compare the results 
                         for (int i = 0; i < 20; i++)
                         {
                             Console.WriteLine(hashBytes[i + 16]);
                             if (hashBytes[i + 16] != hash[i]) answer = false;
                         }
                         return answer;
                  return false;
              }

              catch (Exception ex)
              {
                  throw ex;
              }
          }*/
        //haetaan arvostelu tiedot movierv olioon
        public static List<MovieReview> GetReviewData()
        {
            try
            {
                List<MovieReview> movierv = new List<MovieReview>();
                DataTable dt = DBElokuvat.GetReviewData(cs);
                foreach (DataRow row in dt.Rows)
                {
                    MovieReview oneMovieRv = new MovieReview();
                    oneMovieRv.Movieid = (int)row["elokuva_id"];
                    oneMovieRv.Reviewerid = (int)row["arvostelija_id"];
                    oneMovieRv.Reviewtext = row["arvosteluteksti"].ToString();
                    oneMovieRv.Link1 = row["linkki1"].ToString();
                    oneMovieRv.Link2 = row["linkki2"].ToString();
                    movierv.Add(oneMovieRv);
                }
                return movierv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int UpdateData(Movies movie, MovieReview movierv)
        {
            try
            {
               int number = DBElokuvat.UpdateData(cs, movie, movierv);
               return number;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #region ADD DATA
        public static int AddData( string name, string genre, int year, string director, string composer, string link1, string link2, string review )
        {
            try
            {
                Movies newMovie = new Movies(name, genre, year, director, composer);
                MovieReview newMovieReview = new MovieReview(current.Id, review, link1, link2);
                int number = DBElokuvat.AddData(cs, newMovie, newMovieReview);
                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion ADD DATA
        #region DELETE DATA
        public static bool DeleteData(int id)
        {
            try
            {
                int number = DBElokuvat.DeleteData(cs, id);
                if(number > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion DELETE DATA
        public static void SetViewer(int id, string username, string password)
        {
            current = new Viewer(id, username, password);        
        }
        public static string SaveToTextfile(List<Movies> mylistmovies)
        {
            try
            {
              string path =  Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                string filename = string.Format("{0}\\desktop\\MyMovies.txt", path);
                using (StreamWriter sw = File.CreateText(filename))
                {
                    int help = mylistmovies.Count;
                    for (int i = 0; i < help; i++)
                    {
                        sw.WriteLine("Number " + i + ":");
                        sw.WriteLine("=================");
                        sw.WriteLine("Movie :" + mylistmovies[i].Name);
                        sw.WriteLine("Genre :" + mylistmovies[i].Genre);
                        sw.WriteLine("Director :" + mylistmovies[i].Director);
                        sw.WriteLine("Composer :" + mylistmovies[i].Composer);
                        sw.WriteLine("Release year :" + mylistmovies[i].Year);
                        sw.WriteLine("-----------------");
                    }                  
                }
                return filename;    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
