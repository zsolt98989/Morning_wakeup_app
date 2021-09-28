using Morning_wakeup_app.XAML_Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

            Second_timer.Tick += Second_timer_Tick;
            Second_timer.Interval = new TimeSpan(0, 0, 1);
            Second_timer.Start();
        }

        private void Second_timer_Tick(object sender, object e)
        {
            time_tb.Text = DateTime.Now.ToString("h:mm:ss tt");
        }
        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeatherListBoxItem.IsSelected) { frame.Navigate(typeof(WeatherPage)); }
            else if (NewsListBoxItem.IsSelected) { frame.Navigate(typeof(NewsPage)); }
            else if (MediaListBoxItem.IsSelected) { frame.Navigate(typeof(MusicPage)); }
            else if (NoteListBoxItem.IsSelected) { frame.Navigate(typeof(NotesPage)); }
        }
    }
}
