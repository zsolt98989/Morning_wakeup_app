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
using Windows.UI.Xaml.Navigation;
using static Morning_wakeup_app.News;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }
        private async void news_button_Click(object sender, RoutedEventArgs e)
        {
            var flag = await News.GetArticlesMain();
            News.current_news_index = 0;
            news_tb.Text = News.news_articles.First().title + "\n" + News.news_articles.First().author + "\n" + News.news_articles.First().description;
        }
        private void next_news_button_Click(object sender, RoutedEventArgs e)
        {
            int length = News.news_articles.Count();
            if (News.news_articles != null)
            {
                if (News.current_news_index + 1 != length)
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
