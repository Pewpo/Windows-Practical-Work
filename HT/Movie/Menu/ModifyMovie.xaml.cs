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
            spMovieInfo.DataContext = cur;
            spReview.DataContext = curretmr;
            txtbLink1.DataContext = curretmr;
            txtbLink2.DataContext = curretmr;      
        }

        private void btnSaveModified_Click(object sender, RoutedEventArgs e)
        {
           int number = BLMain.UpdateData(current, curretmr);
            lbMessages.Content = string.Format("{0} movie is updated", number);
            MessageBox.Show("Update managed");
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
