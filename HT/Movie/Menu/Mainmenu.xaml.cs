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
using System.Windows.Shapes;
using Movie.BL;
using Path = System.IO.Path;

namespace Movie.Menu
{
    /// <summary>
    /// Interaction logic for Mainmenu.xaml
    /// </summary>
    public partial class Mainmenu : UserControl, ISwitchable
    {
        List<MovieReview> moviesrv;
        public Mainmenu()
        {
            InitializeComponent();
            IniMyStuff();
        }
        public void IniMyStuff()
        {
            string randmovie = GetRandomMovie();
            mediaElement.Source = new Uri(randmovie);
            mediaElement.Play();
           int id= BLMain.current.Id;
           string name = BLMain.current.Username;
            string p = BLMain.current.Password;
            txtbWelcome.Text = "Welcome " + BLMain.current.Username + "!";
          
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void btnSearchMovies_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MovieSearch());
        }

        private void btnMovieManager_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MovieManager());
        }

        private void cmbSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbSettings.SelectedIndex == 1)
            {
                LogOut.LogOutNow();
            }
        }
        //VALITAAN RANDOM MOVIE TOISTETTAVAKSI
        private string GetRandomMovie()
        {
            moviesrv = BLMain.GetReviewData();
            int number = moviesrv.Count;
            Random rand = new Random();
            string extension = "";
            int randnumber = 0;
            while (extension != ".mp4")
            {
                randnumber = rand.Next(1, number);
                extension = Path.GetExtension(moviesrv[randnumber].Link1); // tarkistetaan että video on .mp4
            }
            return moviesrv[randnumber].Link1;
            

        }
    }
}
