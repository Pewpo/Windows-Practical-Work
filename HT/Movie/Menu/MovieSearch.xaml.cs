using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Movie.BL;
using System.Data;

namespace Movie.Menu
{
    /// <summary>
    /// Interaction logic for ShowMovie.xaml
    /// </summary>
    public partial class MovieSearch : UserControl, ISwitchable
    {
        List<Movies> movies;
        List<MovieReview> moviesrv;
        List<Movies> mylistmovies = new List<Movies>();
        public MovieSearch()
        {
            InitializeComponent();
            IniMyStuff();
        }

        private void IniMyStuff()
        {
            try
            {
                movies = BLMain.GetMovieData();
                moviesrv = BLMain.GetReviewData();
                lboxAllMovies2.DataContext = movies;
                lbMessages.Content = "Data taken from mysql server";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (lboxAllMovies2.SelectedItem != null)
            {
                ShowMovie showmovie = new ShowMovie();
                Movies current = (Movies)lboxAllMovies2.SelectedItem;
                foreach (MovieReview help in moviesrv)
                {
                    if (current.MovieId == help.Movieid)
                    {
                        showmovie.SetMovieInfo(current, help);
                        Switcher.Switch(showmovie);
                    }
                }
                Switcher.Switch(showmovie);
            }
            else
            {
                lbMessages.Content = "Select first movie you want to view";
            }
        }

        private void btnBackTo_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Mainmenu());
        }
        //lisäään omaan listaan
        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            try
            {             
                if (lboxAllMovies2.SelectedItem != null)
                {
                    Movies current = (Movies)lboxAllMovies2.SelectedItem;
                    mylistmovies.Add(current);
                    lboxMyList.Items.Add(current);
                    lbMessages.Content = "New movie added to Mylist";
                }
                else
                {
                    lbMessages.Content = "Select first movie you want to add";
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRandomGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rand = new Random();
                int selected = rand.Next(0, movies.Count());
                lboxAllMovies2.SelectedIndex = selected;
                if (lboxAllMovies2.SelectedItem != null)
                {
                    ShowMovie showmovie = new ShowMovie();
                    Movies current = (Movies)lboxAllMovies2.SelectedItem;
                    foreach (MovieReview help in moviesrv)
                    {
                        if (current.MovieId == help.Movieid)
                        {
                            showmovie.SetMovieInfo(current, help);
                            Switcher.Switch(showmovie);
                        }
                    }
                    Switcher.Switch(showmovie);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        // Tulostetaan oma lista
        private void btnPrintMyList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               bool answer = BLMain.SaveToTextfile(mylistmovies);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
