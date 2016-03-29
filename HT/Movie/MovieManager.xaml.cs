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
        public MovieManager()
        {
            InitializeComponent();
            //IniMyStuff();
        }
        private void IniMyStuff()
        {
            try
            {
               DataTable dt = BLMain.GetData();
                dgAllMovies.ItemsSource = dt.DefaultView;
                lbMessages.Content = "Tiedostojen haku onnistui";
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ModifyMovie());
        }
    }
}
