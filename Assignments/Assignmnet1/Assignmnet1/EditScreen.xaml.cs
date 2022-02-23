using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TagLib;

namespace Assignment_1
{
    /// <summary>
    /// Interaction logic for EditScreen.xaml
    /// Visual studio is cursed couldnt get editing to save assignement wont even open for me 
    /// even though it shows it in task manager
    /// </summary>
    public partial class EditScreen : Window
    {
		public string fileNameEdit;
        MediaPlayer Player = new MediaPlayer();       
		public string editFileName { get; set;}
		public string a;
        
        

		public EditScreen()
        {


            InitializeComponent();
            var editscreen = this.DataContext;
            

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

          


        }

        private void Save_file(object sender, RoutedEventArgs e)
        {



        }
    }
}
