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
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : UserControl, ISwitchable
    {
        public AddMovie()
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

        private void btnAddNewMovie_Click(object sender, RoutedEventArgs e)
        {
            //Elokuvien lisäys
            try
            {
                if (txtbName.Text != "" && txtbGenre.Text != "" && txtbYear.Text != null && txtbDirector.Text != "" && txtbReview.Text != "") {
                int number =   BLMain.AddData(txtbName.Text, txtbGenre.Text, (Int32.Parse(txtbYear.Text)), txtbDirector.Text, txtbComposer.Text,
                    txtbLink1.Text, txtbLink2.Text, txtbReview.Text);
                    lbMessages.Content = string.Format("{0} Movie was added correctly!", number);
                    CleanFields();
                }
                else
                {
                    MessageBox.Show("Fill all the cells!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CleanFields()
        {
            txtbName.Text = "";
            txtbGenre.Text = "";
            txtbYear.Text = "";
            txtbDirector.Text = "";
            txtbComposer.Text = "";
            txtbLink1.Text = "";
            txtbLink2.Text = "";
            txtbReview.Text = "";
        }
        private void cmbSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbSettings.SelectedIndex == 1)
            {
                LogOut.LogOutNow();
            }
}
    }
}
