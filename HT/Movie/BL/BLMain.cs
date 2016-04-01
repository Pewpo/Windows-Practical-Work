using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DB

namespace Movie.BL
{
    class BLMain
    {
        private static string cs = Movie.Properties.Settings.Default.Elokuva;
        
        public static DataTable GetData()
        {
            try
            {
                DataTable dt = DBElokuvat.GetData(cs);              
                return dt;
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int AddData( string name, string genre, int year, string director, string composer, string link1, string link2, string review )
        {
            try
            {
                Movies newMovie = new Movies(name, genre, year, director, composer);
                MovieReview newMovieReview = new MovieReview(review, link1);
                int number = DBElokuvat.AddData(cs, newMovie); // lisää, newMovieReview
                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
