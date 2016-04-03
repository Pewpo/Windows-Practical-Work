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
    /// Interaction logic for ShowMovie.xaml
    /// </summary>
public partial class ShowMovie : UserControl, ISwitchable
    {
        Movies current;
        MovieReview curretmr;
        public ShowMovie()
        { 
            InitializeComponent();
           // mediaElement.Source = new Uri("D:\\Koulu\\windows-harkka\\Coffeemaker.mp4");
           // mediaElement.Play();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }


        private void btnBackToSearch_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MovieSearch());

        }
        public void SetMovieInfo(Movies cur, MovieReview mod)
        {
            this.current = cur;
            this.curretmr = mod;

            txtbMovieName.DataContext = cur;
            txtbMovieText.DataContext = curretmr;
            mediaElement.DataContext = curretmr;
            //Lisätään image
            if (curretmr.Link2 != "")
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(curretmr.Link2);
                bitmapImage.DecodePixelHeight = 300;
                bitmapImage.DecodePixelWidth = 250;
                bitmapImage.EndInit();
                imgMovie.Source = bitmapImage;
            }
            else
            {
               imgMovie.Source =  null;
            }

      /* NÄILLÄ SAA KYLLÄ MP4 VIDEOT URLIN KAUTTA NÄKYVIIN    
      mediaElement.MediaFailed += MyMediaElement_MediaFailed;
            mediaElement.LoadedBehavior = MediaState.Play;
            mediaElement.Source =   new Uri(@"https://www.youtube.com/watch?v=DPEJB-FCItk.mp4");
            */

        }
   /*     void MyMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }**/
    }
}
