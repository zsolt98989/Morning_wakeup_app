using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicPage : Page
    {
        MediaPlayer mplayer;
        bool is_playing;
        string[] fileEntries;
        int currently_playing_index = 0;
        public MusicPage()
        {
            this.InitializeComponent();
            mplayer = new MediaPlayer();
            is_playing = false;
            Get_File_Names();
        }

        private async void Get_File_Names()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Music");
            fileEntries = Directory.GetFiles(folder.Path);
            int length_dir = folder.Path.Length + 1;
            
            for (int i = 0; i < fileEntries.Length; i++)
            {
                fileEntries[i] = fileEntries[i].Remove(0, length_dir);
            }
                
        }


        private async void play_button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Get_File_Names();
            currently_playing_tb.Text = fileEntries[currently_playing_index];

            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Music");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(fileEntries[currently_playing_index]);

            mplayer.AutoPlay = false;
            mplayer.Source = MediaSource.CreateFromStorageFile(file);

            if (is_playing)
            {
                mplayer.Source = null;
                is_playing = false;
            }
            else
            {
                mplayer.Play();
                is_playing = true;
            }

        }

        private void next_music_button_Click(object sender, RoutedEventArgs e)
        {
            if (currently_playing_index != fileEntries.Length - 1)
                currently_playing_index += 1;
        }

        private void prev_music_button_Click(object sender, RoutedEventArgs e)
        {
            if (currently_playing_index != 0)
                currently_playing_index -= 1;
        }
    }
}
