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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoPage : Page
    {
        public InfoPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            MainPage.Second_timer.Tick += Second_timer_Tick;

            MainPage.Title.Text = "Main Page";

            get_Tab_Infos(this, new RoutedEventArgs());

            news_tab.Text = "News";
            music_tab.Text = "Music";
            notes_tab.Text = "Notes";
        }

        private async void get_Tab_Infos(object sender, RoutedEventArgs e)
        {
            var flag = await Current_weather.GetWeatherInformations();
            var flag2 = await News.GetArticlesMain();
            try
            {
                weather_tab.Text = Current_weather.weather_reports.name + "\n" + Current_weather.weather_reports.main.temp + "°C\n" + Current_weather.weather_reports.weather[0].description;
                news_tab.Text = News.news_articles[0].title + "\n" + new string (News.news_articles[0].description.Take(40).ToArray()) + "...";
               
            }
            catch (Exception ex)
            {

            }
        }

        private void Second_timer_Tick(object sender, object e)
        {
            secondHand.Angle = DateTime.Now.Second * 6;
            minuteHand.Angle = DateTime.Now.Minute * 6;
            hourHand.Angle = (DateTime.Now.Hour * 30);
        }

        private void weather_tab_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WeatherPage), null, new EntranceNavigationTransitionInfo());
        }

        private void news_tab_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsPage), null, new EntranceNavigationTransitionInfo());
        }

        private void music_tab_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MusicPage), null, new EntranceNavigationTransitionInfo());
        }

        private void notes_tab_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesPage), null, new EntranceNavigationTransitionInfo());
        }
    }
}
