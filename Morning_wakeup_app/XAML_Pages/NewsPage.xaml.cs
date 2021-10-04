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
using static Morning_wakeup_app.News;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            MainPage.Title.Text = "News Page";
        }
        private async void news_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await News.GetArticlesMain();
            News.current_news_index = 0;
            int length = News.news_articles.Count();
            if (News.news_articles != null)
                fill_news_tables();
        }
        private void next_news_button_Click(object sender, RoutedEventArgs e)
        {
            int length = News.news_articles.Count();
            if (News.news_articles != null)
            {
                if (News.current_news_index + 3 < length)
                    News.current_news_index += 3;
                fill_news_tables();
            }
        }
        private void prev_news_button_Click(object sender, RoutedEventArgs e)
        {
            //var flag = await News.GetArticlesMain();
            if (News.news_articles != null)
            {
                if (News.current_news_index - 3 >= 0)
                    News.current_news_index -= 3;
                fill_news_tables();
            }
        }
        private void news_search_input_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            News.search_by = news_search_input_tb.Text;
        }
        private void close_news_page_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfoPage), null, new EntranceNavigationTransitionInfo());
        }
        private void fill_news_tables()
        {
            news_date1.Text = News.news_articles[News.current_news_index].publishedAt.ToString();
            news_title1.Text = News.news_articles[News.current_news_index].title;
            news_author1.Text = News.news_articles[News.current_news_index].author;
            news_description1.Text = News.news_articles[News.current_news_index].description;
            news_imageurl1.Source = new BitmapImage(new Uri(News.news_articles[News.current_news_index].urlToImage, UriKind.Absolute));
            news_date2.Text = News.news_articles[News.current_news_index + 1].publishedAt.ToString();
            news_title2.Text = News.news_articles[News.current_news_index + 1].title;
            news_author2.Text = News.news_articles[News.current_news_index + 1].author;
            news_description2.Text = News.news_articles[News.current_news_index + 1].description;
            news_imageurl2.Source = new BitmapImage(new Uri(News.news_articles[News.current_news_index + 1].urlToImage, UriKind.Absolute));
            news_date3.Text = News.news_articles[News.current_news_index + 2].publishedAt.ToString();
            news_title3.Text = News.news_articles[News.current_news_index + 2].title;
            news_author3.Text = News.news_articles[News.current_news_index + 2].author;
            news_description3.Text = News.news_articles[News.current_news_index + 2].description;
            news_imageurl3.Source = new BitmapImage(new Uri(News.news_articles[News.current_news_index + 2].urlToImage, UriKind.Absolute));
        }
    }
}
