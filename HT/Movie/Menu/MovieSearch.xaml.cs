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
                FillMyCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillMyCombo()
        {
            try
            {
                var result = (from c in movies.AsEnumerable()
                              select c.Genre).Distinct().ToList();
                cbGenre.DataContext = result;              
                var result2 = (from c in movies.AsEnumerable()
                              select c.Year).Distinct().ToList();
                cbYear.DataContext = result2;
                var result3 = (from c in movies.AsEnumerable()
                               select c.Director).Distinct().ToList();
                cbDirector.DataContext = result3;
                var result4 = (from c in movies.AsEnumerable()
                               select c.Name).Distinct().ToList();
                cbName.DataContext = result4;
            }
            catch (Exception ex)
            {

                throw ex;
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
                if (mylistmovies.Count != 0)
                {
                    string filename = BLMain.SaveToTextfile(mylistmovies);
                    lbMessages.Content = "My movie list saved to : " + filename;
                }
                else
                {
                    lbMessages.Content = "Select first some movies into the list that you want to save";
                }
           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSettings.SelectedIndex == 1)
            {
                LogOut.LogOutNow();
            }
        }

        private void cbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {            
                movies = BLMain.GetWantedData("genre", cbGenre.SelectedValue.ToString());
                lboxAllMovies2.DataContext = movies;          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                movies = BLMain.GetWantedData("julkaisuvuosi", cbYear.SelectedValue.ToString());
                lboxAllMovies2.DataContext = movies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbDirector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                movies = BLMain.GetWantedData("ohjaaja", cbDirector.SelectedValue.ToString());
                lboxAllMovies2.DataContext = movies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                movies = BLMain.GetWantedData("nimi", cbName.SelectedValue.ToString());
                lboxAllMovies2.DataContext = movies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
