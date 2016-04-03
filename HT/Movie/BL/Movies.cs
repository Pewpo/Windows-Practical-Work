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
        private int movieid;
        
        #region CONSTRUCTORS
        public Movies(string name, string genre, int year, string director, string composer)
        {
            this.name = name;
            this.genre = genre;
            this.year = year;
            this.director = director;
            this.composer = composer;
        }
        public Movies()
        {
            this.movieid = 0;
            this.name = "";
            this.genre = "";
            this.year = 0;
            this.director = "";
            this.composer = "";
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
        #region GET/SET-METHODS
        public int MovieId
        {
            get { return this.movieid; }
            set { this.movieid= value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Genre
        {
            get { return this.genre; }
            set { this.genre = value; }
        }
        public int Year
        {
            get { return this.year; }
            set { this.year = value; }
        }
        public string Director
        {
            get { return this.director; }
            set { this.director = value; }
        }
        public string Composer
        {
            get { return this.composer; }
            set { this.composer = value; }
        }
        #endregion GET/SET-METHODS
        public override string ToString()
        {
            return this.name + " | " + this.genre + " | " + this.composer;
        }
    }
    public class MovieReview
    {
        private int reviewerId;
        private int movieId;
        private string reviewText;
        private string link1;
        private string link2;
        #region CONSTRUCTORS
    /*    public MovieReview(int reviewerid,  string reviewText, string link1)
        {
            this.reviewerId = reviewerid;
            this.movieId = 0;
            this.reviewText = reviewText;
            this.link1 = link1;
            this.link2 = "";
        }*/
        public MovieReview(int reviewerid, string reviewText, string link1, string link2)
        {
            this.reviewerId = reviewerid;
            this.movieId = 0;
            this.reviewText = reviewText;
            this.link1 = link1;
            this.link2 = link2;
        }
        public MovieReview()
        {
            this.reviewerId = 0; 
            this.movieId = 0;
            this.reviewText = "";
            this.link1 = "";
            this.link2 = "";
        }
        #endregion CONSTRUCTORS
        #region GET/SET-METHODS
        public int Reviewerid
        {
            get { return this.reviewerId; }
            set { this.reviewerId = value; }
        }
        public int Movieid
        {
            get { return this.movieId; }
            set { this.movieId = value; }
        }
        public string Reviewtext
        {
            get { return this.reviewText; }
            set { this.reviewText = value; }
        }
        public string Link1
        {
            get { return this.link1; }
            set { this.link1 = value; }
        }
        public string Link2
        {
            get { return this.link2; }
            set { this.link2 = value; }
        }
        #endregion GET/SET-METHODS
    }
}
