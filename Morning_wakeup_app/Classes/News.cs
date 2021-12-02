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
        static public string search_by = "Apple"; //Todo: getter settert írni
        public async static Task<bool> GetArticlesMain()
        {
            var http = new HttpClient();
            if (search_by == "")
                search_by = "Apple";
            string url = String.Format("https://newsapi.org/v2/everything?q={0}&from=2021-11-10&sortBy=popularity&apiKey=a00cf881a614443a86effb37430871e7", search_by);
            var response = await http.GetAsync(url);
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
