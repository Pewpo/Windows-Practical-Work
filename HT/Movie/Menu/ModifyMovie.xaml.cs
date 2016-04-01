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

namespace Movie.Menu
{
    /// <summary>
    /// Interaction logic for ModifyMovie.xaml
    /// </summary>
    public partial class ModifyMovie : UserControl, ISwitchable
    {
        Movies current;
        MovieReview curretmr;
        public ModifyMovie()
        {
            InitializeComponent();
            IniMyStuff();
        }
        
        private  void IniMyStuff()
        {
            txtbName.Text = current.Name;
            txtbGenre.Text = current.Genre;
            txtbYear.Text = current.Year.ToString();
            txtbDirector.Text = current.Director;
            txtbComposer.Text = current.Composer;
            

        }
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void btnBackToManager_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MovieManager());
        }
        public  void SetMovieInfo(Movies cur, MovieReview mod)
        {
            this.current = cur;
            this.curretmr = mod;
        }

    }

}
