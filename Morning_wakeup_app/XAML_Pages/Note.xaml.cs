using Windows.UI.Xaml;

using Windows.Media.SpeechRecognition;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media;
using Windows.Globalization;
using Windows.UI.Xaml.Documents;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app
{
    //public override NoteModel _noteModel { get; private set; }
    public partial class Note
    {
        private SpeechRecognizer speechRecognizer;
        private bool isListening;
        private StringBuilder dictatedTextBuilder;

        public Note()
        {
            this.InitializeComponent();
            isListening = false;
            dictatedTextBuilder = new StringBuilder();           
        }

        private void buttonCloseClick(object sender, RoutedEventArgs e)
        {

        }


        private void dictateButtonClick(object sender, RoutedEventArgs e)
        {
        }

           
    }
}
