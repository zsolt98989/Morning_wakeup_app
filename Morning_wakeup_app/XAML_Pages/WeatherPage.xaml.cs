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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();
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
        }
    }
}
