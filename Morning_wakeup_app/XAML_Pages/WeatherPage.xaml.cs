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
            forecast_slidertext.Text = "Move the slider to show forecast datas for the next 48 hours";

            MainPage.Second_timer.Tick += Second_timer_Tick;

            weather_button_Click(this, new RoutedEventArgs());
        }
        private void Second_timer_Tick(object sender, object e)
        {
            if (forecast_slider.Value != 48)
                forecast_slider.Value += 1;
            else
                forecast_slider.Value = 1;
            forecast_slidervalue.Text = forecast_slider.Value.ToString() + " hour(s)";
        }
        private async void weather_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await Current_weather.GetWeatherInformations();
            var flag3 = await Weather_forecast.ConvertCityToCoordinates(weather_search_input_tb.Text);
            Weather_forecast.lat = Weather_forecast.citys.lat;
            Weather_forecast.lon = Weather_forecast.citys.lon;
            var flag2 = await Weather_forecast.GetWeatherForecastInformations();
            try
            {
                string weather_icon = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", Current_weather.weather_reports.weather[0].icon);
                Weather_img.Source = new BitmapImage(new Uri(weather_icon, UriKind.Absolute));
                forecast_slider.Value = 1;
                fill_weather_textblock();
                fill_forecast_textblock();
            }
            catch (Exception ex)
            {

            }
        }
        private void weather_search_input_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Current_weather.search_by = weather_search_input_tb.Text;
        }
        private void close_weather_page_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfoPage), null, new EntranceNavigationTransitionInfo());
        }
        private void forecast_slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Weather_forecast.weather_forecasts != null)
            {
                fill_forecast_textblock();
                string weather_icon = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value-1].weather[0].icon);
                if (forecast_image != null)
                    forecast_image.Source = new BitmapImage(new Uri(weather_icon, UriKind.Absolute));
            }
        }
        private void fill_weather_textblock()
        {
            weather_textblock_name.Text = Current_weather.weather_reports.name;
            weather_textblock_temp.Text = Current_weather.weather_reports.main.temp + "°C";
            weather_textblock_tempfeels.Text = "(feels like: " + Current_weather.weather_reports.main.feels_like + " °C)";
            weather_textblock_image.Text = Current_weather.weather_reports.weather[0].description;
            weather_textblock.Text = "Wind: " + Math.Round((3.6 * Current_weather.weather_reports.wind.speed), 2) + " km/h" + "\nHumidity: " + Current_weather.weather_reports.main.humidity + " %" + "\nPressure: " + Current_weather.weather_reports.main.pressure + " Pa";
        }
        private void fill_forecast_textblock()
        {
            forecast_textblock_temp.Text = Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].temp.ToString() + "°C";
            forecast_textblock_tempfeels.Text = "(feels like: " + Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].feels_like.ToString() + " °C)";
            forecast_textblock_image.Text = Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].weather[0].description.ToString();
            forecast_textblock.Text = "Wind: " + Math.Round((3.6 * Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].wind_speed), 2) + " km/h" + "\nHumidity: " + Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].humidity + " %" + "\nProbability of precipitation: " + (Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].pop * 100).ToString() + " %" + "\nUV Index: " + Weather_forecast.weather_forecasts.hourly[(int)forecast_slider.Value - 1].uvi;
        }
    }
}
    