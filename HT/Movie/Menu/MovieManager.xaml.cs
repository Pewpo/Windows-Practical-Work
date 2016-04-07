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
    /// Interaction logic for MovieManager.xaml
    /// </summary>
 
    public partial class MovieManager : UserControl, ISwitchable
    {
        List<Movies> movies;
        List<MovieReview> moviesrv;
        public MovieManager()
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
                lboxAllMovies.DataContext = movies;
                lbMessages.Content = "Data taken from mysql server";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }
        void ISwitchable.UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AddMovie());
        }
        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Mainmenu());
        }
        //Poistetaan elokuva ja arvostelu
        private void btnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Movies current = (Movies)lboxAllMovies.SelectedItem;
          
                var retval = MessageBox.Show("Do you realy want to delete movie : " + current.ToString(), "Movie software ask", MessageBoxButton.YesNo);
                if (retval == MessageBoxResult.Yes)
                {
                        foreach(MovieReview help in moviesrv)
                        {
                            if(current.MovieId == help.Movieid)
                            {
                                //tarkistetaan vielä että muokattava arvostelu on henkilön oma tekemä
                                if (help.Reviewerid == BLMain.current.Id)
                                {
                                    bool answer = BLMain.DeleteData(current.MovieId);
                                    if (answer == true)
                                    {
                                        lbMessages.Content = string.Format("Movie review : " + current.ToString() + "deleted");
                                    }
                                }
                                else
                                {
                                     lbMessages.Content = "That's not your movie review. because of that you can't delete it.";
                                }
                            }
                        }               
                }
                else
                {
                    lbMessages.Content = "Something went wrong"; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnModifyMovie_Click(object sender, RoutedEventArgs e)
        {
            if (lboxAllMovies.SelectedItem != null)
            {                       
                // vaihdetaan sivua ja viedään samalla halutun olion tiedot toiselle leiskalle
                Movies current = (Movies)lboxAllMovies.SelectedItem;
                ModifyMovie ModMov = new ModifyMovie();

                foreach (MovieReview help in moviesrv)
                {
                    if (current.MovieId == help.Movieid)
                    {
                        //tarkistetaan vielä että muokattava arvostelu on henkilön oma tekemä
                        if (help.Reviewerid == BLMain.current.Id)
                        {
                            ModMov.SetMovieInfo(current, help);
                            Switcher.Switch(ModMov);
                        }
                        else
                        {
                            lbMessages.Content = "That's not your movie review. because of that you can't modify it.";
                        }
                    }
                }

            }
            else
            {
                lbMessages.Content = "Select first movie for editing ";
            }
        }
        private void cmbSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSettings.SelectedIndex == 1)
            {
                LogOut.LogOutNow();
            }
        }
    }
}
