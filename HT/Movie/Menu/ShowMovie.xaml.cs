﻿using System;
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
using Path = System.IO.Path;

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
            try
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
                    imgMovie.Source = null;
                }
                StartVideo(mod.Link1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    
        }
        public void StartVideo( string videoUrl)
        {
            string extension;
            try
            {
                if (videoUrl != null && videoUrl != "")
                {
                   // tarkastetaan että video on .mp4
                    extension = Path.GetExtension(videoUrl);
                    if(extension == ".mp4")
                    {

                        wbSite.Visibility = Visibility.Collapsed; 
                        mediaElement.Visibility = Visibility.Visible;
                        mediaElement.Source = new Uri(@"" + videoUrl + "");
                        mediaElement.Play();
                        lbMessages.Content = "Videos link : " + videoUrl;                 
                    }
                    else
                    {
                        mediaElement.Visibility = Visibility.Collapsed;
                        wbSite.Visibility = Visibility.Visible;
                        string lastPart = videoUrl.Split('=').Last();
                        wbSite.Navigate(new Uri("http://student.labranet.jamk.fi/~H9577/WWW-palvelinohjelmointi/YoutubePlayer.php?param="+lastPart));                                 
                        lbMessages.Content = "Videos link : " + videoUrl+".";
                    }         
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
