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
using Windows.UI.Xaml.Media.Animation;
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
        static public DispatcherTimer Second_timer = new DispatcherTimer();
        static public TextBlock Title = new TextBlock();
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            Second_timer.Tick += Second_timer_Tick;
            Second_timer.Interval = new TimeSpan(0, 0, 1);
            Second_timer.Start();

            Title.FontFamily = new FontFamily("/Assets/Fonts/Highman.ttf#Highman Trial");
            Title.FontSize = 70;
            Title.Margin = new Thickness(120, 0, 0, 0);
            Title.HorizontalAlignment = HorizontalAlignment.Center;
            Title.VerticalAlignment = VerticalAlignment.Bottom;
            Title.Text = "";
            Title.Height = 60;
            Title.Width = 350;
            Title.TextAlignment = TextAlignment.Center;
            maingrid.Children.Add(Title);

            frame.Navigate(typeof(InfoPage), null, new SuppressNavigationTransitionInfo());
        }
        private void Second_timer_Tick(object sender, object e)
        {
            time_tb.Text = DateTime.Now.ToString("HH:mm:ss");
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
                frame.Navigate(typeof(WeatherPage), null, new EntranceNavigationTransitionInfo());
            }
            else if (NewsListBoxItem.IsSelected)
            {
                if (MusicPage.mplayer != null)
                    MusicPage.mplayer.Pause();
                frame.Navigate(typeof(NewsPage), null, new EntranceNavigationTransitionInfo());
            }
            else if (MediaListBoxItem.IsSelected)
            {
                frame.Navigate(typeof(MusicPage), null, new EntranceNavigationTransitionInfo());
            }
            else if (NoteListBoxItem.IsSelected)
            {
                if (MusicPage.mplayer != null)
                    MusicPage.mplayer.Pause();
                frame.Navigate(typeof(NotesPage), null, new EntranceNavigationTransitionInfo());
            }
        }
    }
}
