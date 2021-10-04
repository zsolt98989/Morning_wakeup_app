using Morning_wakeup_app.Classes;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        public WeatherPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            MainPage.Title.Text = "Weather Page";
        }
        private async void weather_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await Current_weather.GetWeatherInformations();
            string weather_icon = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", Current_weather.weather_reports.weather[0].icon);
            Weather_img.Source = new BitmapImage(new Uri(weather_icon, UriKind.Absolute));

            weather_textblock.Text = Current_weather.weather_reports.name + "\n" + Current_weather.weather_reports.main.temp + " C " + "\n" + Current_weather.weather_reports.main.feels_like + " C" + "\n" + Current_weather.weather_reports.main.humidity + " %" + "\n" + Current_weather.weather_reports.main.pressure + " Pa" + "\n" + Current_weather.weather_reports.weather[0].description;
        }
        private void weather_search_input_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Current_weather.search_by = weather_search_input_tb.Text;
            //Weather_forecast.Convert_city_to_coord(weather_search_input_tb.Text);
        }
        private void close_weather_page_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfoPage), null, new EntranceNavigationTransitionInfo());
        }
        private async void forecast_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await Weather_forecast.GetWeatherForecastInformations();
            forecast_textblock.Text = "Please move the slider to show values";
        }
        private void forecast_slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Weather_forecast.weather_forecasts != null)
            {
                forecast_textblock.Text = "lat: " + Weather_forecast.weather_forecasts.lat.ToString() + "\nlon: " + Weather_forecast.weather_forecasts.lon.ToString() + "\nCurrent: " + Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value-1].temp.ToString() + " C" + "\nFeels like: " + Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].feels_like.ToString() + " C" + "\n" + Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value-1].humidity + " %" + "humidity" + "\n" + (Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].pop*100).ToString() + " % probability of precipitation";
                string weather_icon = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value-1].weather[0].icon);
                if (forecast_image != null)
                    forecast_image.Source = new BitmapImage(new Uri(weather_icon, UriKind.Absolute));
            }
        }
    }
}
