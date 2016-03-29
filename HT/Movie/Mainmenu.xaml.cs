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

namespace Movie.Menu
{
    /// <summary>
    /// Interaction logic for Mainmenu.xaml
    /// </summary>
    public partial class Mainmenu : UserControl, ISwitchable
    {
        public Mainmenu()
        {
            InitializeComponent();
            IniMyStuff();
        }
        public void IniMyStuff()
        {
            mediaElement.Source = new Uri("D:\\Koulu\\windows-harkka\\Coffeemaker.mp4");
            mediaElement.Play();
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
    }
}
