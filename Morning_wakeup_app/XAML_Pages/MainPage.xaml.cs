using Morning_wakeup_app.XAML_Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Morning_wakeup_app
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer Second_timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            Second_timer.Tick += Second_timer_Tick;
            Second_timer.Interval = new TimeSpan(0, 0, 1);
            Second_timer.Start();
        }

        private void Second_timer_Tick(object sender, object e)
        {
            time_tb.Text = DateTime.Now.ToString("HH:mm:ss");
            secondHand.Angle = DateTime.Now.Second * 6;
            minuteHand.Angle = DateTime.Now.Minute * 6;
            hourHand.Angle = (DateTime.Now.Hour * 30);
        }
        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeatherListBoxItem.IsSelected)
            {
                if (MusicPage.mplayer != null)
                    MusicPage.mplayer.Pause();
                frame.Content = null;
                frame.Navigate(typeof(WeatherPage));
            }
            else if (NewsListBoxItem.IsSelected)
            {
                if (MusicPage.mplayer != null)
                    MusicPage.mplayer.Pause();
                frame.Content = null;
                frame.Navigate(typeof(NewsPage));
            }
            else if (MediaListBoxItem.IsSelected)
            {
                frame.Content = null;
                frame.Navigate(typeof(MusicPage));
            }
            else if (NoteListBoxItem.IsSelected)
            {
                if (MusicPage.mplayer != null)
                    MusicPage.mplayer.Pause();
                frame.Content = null;
                frame.Navigate(typeof(NotesPage));
            }
        }
    }
}
