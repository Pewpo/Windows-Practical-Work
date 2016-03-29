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
namespace Movie.Menu
{
    /// <summary>
    /// Interaction logic for ShowMovie.xaml
    /// </summary>
public partial class ShowMovie : UserControl, ISwitchable
    {
        public ShowMovie()
        {
            InitializeComponent();
            mediaElement.Source = new Uri("D:\\Koulu\\windows-harkka\\Coffeemaker.mp4");
            mediaElement.Play();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }


        private void btnBackToSearch_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MovieSearch());

        }
    }
}
