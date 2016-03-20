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

namespace Movie
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class home : Window
    {
        public home()
        {
            InitializeComponent();
            IniMyStuff();
        }
        public void IniMyStuff()
        {
            mediaElement.Source = new Uri("D:\\Koulu\\windows-harkka\\Coffeemaker.mp4");
            mediaElement.Play();
        }
    }
}
