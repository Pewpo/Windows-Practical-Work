using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DB;

namespace Movie.BL
{
    class BLMain
    {
        private static string cs = Movie.Properties.Settings.Default.Elokuva;
        //haetaan elokuva tualun tiedot movie olioon
        public static List<Movies> GetMovieData()
        {
            try
            {
                List<Movies> movie = new List<Movies>();
                DataTable dt = DBElokuvat.GetMovieData(cs);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        #region ADD DATA
        public static int AddData( string name, string genre, int year, string director, string composer, string link1, string link2, string review )
        {
            try
            {
                Movies newMovie = new Movies(name, genre, year, director, composer);
                MovieReview newMovieReview = new MovieReview(review, link1);
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
    }
}
