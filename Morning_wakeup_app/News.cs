using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Morning_wakeup_app
{
    class News
    {
        static public List<Article> news_articles; //Todo: getter settert írni
        static public int current_news_index = 0; //Todo: getter settert írni
        public async static Task<bool> GetArticlesMain()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://newsapi.org/v2/everything?q=Apple&from=2021-09-21&sortBy=popularity&apiKey=98b035024d8b4c728e6433fdcf1b5ada");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NewsResponse_root>(result);

            news_articles = data.articles;
            return true; //Todo: hibavizsgálat visszatérésnél, nem csak simán true-t visszaadni
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Source
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Article
    {
        public Source source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
    }

    public class NewsResponse_root
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> articles { get; set; }
    }


}
