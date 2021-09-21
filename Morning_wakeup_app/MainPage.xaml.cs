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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Root myWeather = await Current_weather.GetWeatherInformations();
            string weather_icon = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", myWeather.weather[0].icon);
            Weather_img.Source = new BitmapImage(new Uri(weather_icon, UriKind.Absolute));

            weather_textblock.Text = myWeather.name + "\n" + myWeather.main.temp + " C " + "\n" +  myWeather.main.feels_like + " C" + "\n" + myWeather.main.humidity + " %" +"\n" + myWeather.main.pressure + " Pa" + "\n" + myWeather.weather[0].description;
        }
    }
}
