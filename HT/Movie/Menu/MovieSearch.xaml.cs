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
                dgAllMovies.DataContext = movies;
                lbMessages.Content = "Tiedostojen haku onnistui";
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
            Switcher.Switch(new ShowMovie());
        }

        private void btnBackTo_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Mainmenu());
        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
