using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Morning_wakeup_app.Classes
{
    class Weather_forecast
    {
        public static Root weather_forecasts;
        static public double lon = 19.040236;
        static public double lat = 47.497913;

        public async static Task<bool> GetWeatherForecastInformations()
        {
            var http = new HttpClient();
            string url = String.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=current,minutely,daily,alerts&appid=fddc3f024e2f2d470a0582adfc97d810&units=metric", lat, lon);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Root));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = JsonConvert.DeserializeObject<Root>(result);

            weather_forecasts = (Root)serializer.ReadObject(ms);
            return true; //Todo: hibavizsgálat visszatérésnél, nem csak simán true-t visszaadni
        }
     


    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }
    [DataContract]
    public class Rain
    {
        [DataMember]
        public double _1h { get; set; }
    }
    [DataContract]
    public class Hourly
    {
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public double feels_like { get; set; }
        [DataMember]
        public int pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double dew_point { get; set; }
        [DataMember]
        public double uvi { get; set; }
        [DataMember]
        public int clouds { get; set; }
        [DataMember]
        public int visibility { get; set; }
        [DataMember]
        public double wind_speed { get; set; }
        [DataMember]
        public int wind_deg { get; set; }
        [DataMember]
        public double wind_gust { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public double pop { get; set; }
        [DataMember]
        public Rain rain { get; set; }
    }
    [DataContract]
    public class Root
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public string timezone { get; set; }
        [DataMember]
        public int timezone_offset { get; set; }
        [DataMember]
        public List<Hourly> hourly { get; set; }
    }


}
