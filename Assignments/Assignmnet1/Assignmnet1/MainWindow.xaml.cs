//Josh Gauthier
using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Assignment_1
{
	/// <summary>
	/// Main Window code
	/// </summary>
	
	public partial class Mp3Player : Window
	{
		
		private bool slider = false;
		private bool songIsPlayering = false;
		//File Path Varible
		public string fileName;
		

		public Mp3Player()
		{

            InitializeComponent();
			
			
			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;		
			//Timer
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
		}

		MediaPlayer Player = new MediaPlayer();

		private void timer_Tick(object sender, EventArgs e)
		{
			// use the mp3 example on how to set up a slider with music
			// http://www.wpf-tutorial.com/audio-video/how-to-creating-a-complete-audio-video-player/
			if ((Player.Source != null) && 
				(Player.NaturalDuration.HasTimeSpan) && 
				(!slider))
			{
				songProgress.Minimum = 0;
				
				songProgress.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;
				songProgress.Value = Player.Position.TotalSeconds;
			}
		}
		// Opening the file dialong for some reason
		
		private void Can_Edit(object sender, CanExecuteRoutedEventArgs e)
		{
			
			//can execute
			e.CanExecute = true;
			
		}
		//Opens Edit screen
		private void Can_Edit1(object sender, ExecutedRoutedEventArgs e)
		{

			EditScreen editScreen = new EditScreen();
			var filesend = TagLib.File.Create(fileName);
			editScreen.Show();
			
			// Testing 
			//Debug.WriteLine(Song.Text);
			//Debug.WriteLine(Album.Text);
			//Debug.WriteLine(Year.Text);
			//Debug.WriteLine(Artist.Text);
		}

		

		private void Open_FileDialog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void Open_FileDialog(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				OpenFileDialog openDialog = new OpenFileDialog();
				// filter for mp3s Credit 
				openDialog.Filter = "Media files (*.mp3;)|*.mp3;";
				if (openDialog.ShowDialog() == true)
				{
					
					
					Player.Open(new Uri(openDialog.FileName));
					fileName = openDialog.FileName;									
				    var file = TagLib.File.Create(fileName);					
					// display info on opening of file
					Song.Text = file.Tag.Title;
					Album.Text = file.Tag.Album;
					string year = file.Tag.Year.ToString();
					Year.Text = year;
					Artist.Text = file.Tag.FirstAlbumArtist;
					
					//Testing values were set 
					//Debug.WriteLine(year);
				}	
				
			}
            catch (Exception)
            {
				MessageBox.Show("Error Opening File Browser");
            }
		}
		
		// Stop Exucted
		private void Stop_Execute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = songIsPlayering;
		}

		// Stop button
		private void Stop_Music(object sender, ExecutedRoutedEventArgs e)
		{
			Player.Stop();
			songIsPlayering = false;
		}

		private void Play_Execute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Player != null) && (Player.Source != null);
		}
		//Play buttom
		private void Play_Music(object sender, ExecutedRoutedEventArgs e)
		{

			Player.Play();

			songIsPlayering = true;
		}
		//Pause_Executed
		private void Pause_Executed(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = songIsPlayering;
		}

		// Pause Button
		private void Pause_Music(object sender, ExecutedRoutedEventArgs e)
		{
			Player.Pause();
		}

		
		//Slider moved
		private void Slider_Draged(object sender, DragStartedEventArgs e)
		{
			slider = true;
		}

		//Slider Let go by user
		private void Slider_Draged_Stopped(object sender, DragCompletedEventArgs e)
		{
			slider = false;
			Player.Position = TimeSpan.FromSeconds(songProgress.Value);
		}

		// New slider time value
		private void Slider_Val_CHanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			songTimer.Text = TimeSpan.FromSeconds(songProgress.Value).
				ToString(@"hh\:mm\:ss");
			
		}
      
    }
}