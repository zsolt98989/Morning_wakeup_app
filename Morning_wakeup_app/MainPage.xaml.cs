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
using static Morning_wakeup_app.News;

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

        private async void weather_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await Current_weather.GetWeatherInformations();
            string weather_icon = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", Current_weather.weather_reports.weather[0].icon);
            Weather_img.Source = new BitmapImage(new Uri(weather_icon, UriKind.Absolute));

            weather_textblock.Text = Current_weather.weather_reports.name + "\n" + Current_weather.weather_reports.main.temp + " C " + "\n" + Current_weather.weather_reports.main.feels_like + " C" + "\n" + Current_weather.weather_reports.main.humidity + " %" +"\n" + Current_weather.weather_reports.main.pressure + " Pa" + "\n" + Current_weather.weather_reports.weather[0].description;
        }

        private async void news_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await News.GetArticlesMain();
            News.current_news_index = 0;
            news_tb.Text = News.news_articles.First().title + "\n" + News.news_articles.First().author + "\n" + News.news_articles.First().description;
        }

        private void Second_timer_Tick(object sender, object e)
        {
            time_tb.Text = DateTime.Now.ToString("h:mm:ss tt");
        }

        private void next_news_button_Click(object sender, RoutedEventArgs e)
        {
            int length = News.news_articles.Count();
            if (News.news_articles != null)
            {
                if (News.current_news_index+1 != length)
                    News.current_news_index += 1;
                news_tb.Text = News.news_articles[News.current_news_index].title + "\n" + News.news_articles[News.current_news_index].author + "\n" + News.news_articles[News.current_news_index].description;
            }
                


        }

        private void prev_news_button_Click(object sender, RoutedEventArgs e)
        {
            //var flag = await News.GetArticlesMain();
            if (News.news_articles != null)
            {
                if (News.current_news_index > 0)
                    News.current_news_index -= 1;
                news_tb.Text = News.news_articles[News.current_news_index].title + "\n" + News.news_articles[News.current_news_index].author + "\n" + News.news_articles[News.current_news_index].description;
            }
            
        }

        private void news_search_input_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            News.search_by = news_search_input_tb.Text;
        }
    }
}
