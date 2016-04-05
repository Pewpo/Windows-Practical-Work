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
    /// Interaction logic for RegisterViewer.xaml
    /// </summary>
    public partial class RegisterViewer : UserControl, ISwitchable
    {
        public RegisterViewer()
        {
            InitializeComponent();
        }



        private void btnBackToLogIn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LogIn());
        }
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        // rekisteröidään uusikäyttäjä jos ei jo ole
        private void btnRegisterNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pswbSetPassword1.Password == pswbSetPassword2.Password)
                {
                    if (pswbSetPassword1.Password == txtbSetUsername.Text)
                    {
                        lbMessages.Content = "Username and password cannot be same";
                    }
                    else {
                        bool answer = BLMain.RegisterNewViewer(txtbSetUsername.Text, pswbSetPassword1.Password);
                        if (answer == false)
                        {
                            lbMessages.Content = "New user created";
                        }
                        else
                        {
                            lbMessages.Content = "Username allready exist";
                        }
                    }
                }
                else
                {
                    lbMessages.Content = "your passwords are incorrect";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
