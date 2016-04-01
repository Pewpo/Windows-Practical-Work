using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BL
{
   public  class Movies
    {
        private string name;
        private string genre;
        private int year;
        private string  director;
        private string composer;
        #region CONSTRUCTORS
        public Movies(string name, string genre, int year, string director, string composer)
        {
            this.name = name;
            this.genre = genre;
            this.year = year;
            this.director = director;
            this.composer = composer;
        }
        #endregion CONSTRUCTORS
        #region DESTRUCTORS
        ~Movies()
        {
            this.name = null;
            this.genre = null;
            this.year = 0;
            this.director = null;
            this.composer = null;
        }
        #endregion DESTRUCTORS
        #region GETMETHODS
        public string Name
        {
            get { return this.name; }
        }
        public string Genre
        {
            get { return this.genre; }
        }
        public int Year
        {
            get { return this.year; }
        }
        public string Director
        {
            get { return this.director; }
        }
        public string Composer
        {
            get { return this.composer; }
        }
        #endregion GETMETHODS
    }
    public class MovieReview
    {
        private int reviewerId;
        private int movieId;
        private string reviewText;
        private string link1;
        private string link2;
        #region CONSTRUCTORS
        public MovieReview(string reviewText, string link1)
        {
            this.reviewerId = 1; //MUUUTA NÄMÄ SITTE KU SAAT KUNTOON KIRJAUTUMISEN
            this.movieId = 1;//MUUTA NÄMÄ SITTE 
            this.reviewText = reviewText;
            this.link1 = link1;
            this.link2 = "";
        }
        #endregion CONSTRUCTORS
        #region GETMETHODS
        public int Reviewerid
        {
            get { return this.reviewerId; }
        }
        public int Movieid
        {
            get { return this.movieId; }
        }
        public string Reviewtext
        {
            get { return this.reviewText; }
        }
        public string Link1
        {
            get { return this.link1; }
        }
        public string Link2
        {
            get { return this.link2; }
        }
        #endregion GETMETHODS
    }
}
