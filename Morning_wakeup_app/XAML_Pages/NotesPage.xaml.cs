using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using System.Text;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;
using System;
using Windows.Globalization;
using System.Threading.Tasks;
using Windows.Media.Capture;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app.XAML_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesPage : Page
    {
        private NoteGroupModel _noteGroupModel { get; set; }
        public NoteGroup FocusedGroup;

        // Speech Recognition elements
        private SpeechRecognizer speechRecognizer;
        private bool isListening;
        private StringBuilder dictatedTextBuilder;

        public NotesPage()
        {
            this.InitializeComponent();

            //ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            MainPage.Title.Text = "Notes Page";

            App app = (App)Application.Current;
            _noteGroupModel = app.NoteGroupModel;

            isListening = false;
            dictateButtonTextbox.Text = " Dictate";
            dictatedTextBuilder = new StringBuilder();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Prompt the user for permission to access the microphone. This request will only happen
            // once, it will not re-prompt if the user rejects the permission.           
            bool permissionGained = await AudioCapturePermissions.RequestMicrophonePermission();
            if (permissionGained)
            {
                dictateButton.IsEnabled = true;
                var language = new Windows.Globalization.Language("en-US");
                await InitializeRecognizer(language);
            }
            else
            {
                this.noteBox.Text = "Permission to access capture resources was not given by the user, reset the application setting in Settings->Privacy->Microphone.";
                dictateButton.IsEnabled = false;
            }

        }

        private async Task InitializeRecognizer(Language recognizerLanguage)
        {
            if (speechRecognizer != null)
            {
                // cleanup prior to re-initializing this scenario.
                speechRecognizer.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
                speechRecognizer.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;

                this.speechRecognizer.Dispose();
                this.speechRecognizer = null;
            }

            this.speechRecognizer = new SpeechRecognizer(recognizerLanguage);

         
            // Apply the dictation topic constraint to optimize for dictated freeform speech.
            var dictationConstraint = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
            speechRecognizer.Constraints.Add(dictationConstraint);
            SpeechRecognitionCompilationResult result = await speechRecognizer.CompileConstraintsAsync();
            if (result.Status != SpeechRecognitionResultStatus.Success)
            {
                dictateButton.IsEnabled = false;
            }

            // Handle continuous recognition events. Completed fires when various error states occur. ResultGenerated fires when
            // some recognized phrases occur, or the garbage rule is hit. HypothesisGenerated fires during recognition, and
            // allows us to provide incremental feedback based on what the user's currently saying.
            
            speechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
        }

        /// <summary>
        /// Upon leaving, clean up the speech recognizer. Ensure we aren't still listening, and disable the event 
        /// handlers to prevent leaks.
        /// </summary>
        /// <param name="e">Unused navigation parameters.</param>
        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (this.speechRecognizer != null)
            {
                if (isListening)
                {
                    await this.speechRecognizer.ContinuousRecognitionSession.CancelAsync();
                    isListening = false;
                    dictateButtonTextbox.Text = " Dictate";
                }

                noteBox.Text = "";

                speechRecognizer.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
                speechRecognizer.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;

                this.speechRecognizer.Dispose();
                this.speechRecognizer = null;
            }
        }

        /// <summary>
        /// Handle events fired when error conditions occur, such as the microphone becoming unavailable, or if
        /// some transient issues occur.
        /// </summary>
        /// <param name="sender">The continuous recognition session</param>
        /// <param name="args">The state of the recognizer</param>
        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (args.Status != SpeechRecognitionResultStatus.Success)
            {
                // If TimeoutExceeded occurs, the user has been silent for too long. We can use this to 
                // cancel recognition if the user in dictation mode and walks away from their device, etc.
                // In a global-command type scenario, this timeout won't apply automatically.
                // With dictation (no grammar in place) modes, the default timeout is 20 seconds.
                if (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        dictateButtonTextbox.Text = " Dictate";
                        noteBox.Text = dictatedTextBuilder.ToString();
                        isListening = false;
                    });
                }
                else
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        dictateButtonTextbox.Text = " Dictate";
                        isListening = false;
                    });
                }
            }
        }

        /// <summary>
        /// Handle events fired when a result is generated. Check for high to medium confidence, and then append the
        /// string to the end of the stringbuffer, and replace the content of the textbox with the string buffer, to
        /// remove any hypothesis text that may be present.
        /// </summary>
        /// <param name="sender">The Recognition session that generated this result</param>
        /// <param name="args">Details about the recognized speech</param>
        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            // We may choose to discard content that has low confidence, as that could indicate that we're picking up
            // noise via the microphone, or someone could be talking out of earshot.
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
                args.Result.Confidence == SpeechRecognitionConfidence.High)
            {
                dictatedTextBuilder.Append(args.Result.Text + " ");

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    noteBox.Text = dictatedTextBuilder.ToString();
                });
            }
            else
            {
                // In some scenarios, a developer may choose to ignore giving the user feedback in this case, if speech
                // is not the primary input mechanism for the application.
                // Here, just remove any hypothesis text by resetting it to the last known good.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    noteBox.Text = dictatedTextBuilder.ToString();
                    string discardedText = args.Result.Text;
                    if (!string.IsNullOrEmpty(discardedText))
                    {
                        discardedText = discardedText.Length <= 25 ? discardedText : (discardedText.Substring(0, 25) + "...");

                        noteBox.Text = "Discarded due to low/rejected Confidence: " + discardedText;
                    }
                });
            }
        }


        public class AudioCapturePermissions
        {
            // If no microphone is present, an exception is thrown with the following HResult value.
            private static int NoCaptureDevicesHResult = -1072845856;

            /// <summary>
            /// Note that this method only checks the Settings->Privacy->Microphone setting, it does not handle
            /// the Cortana/Dictation privacy check.
            ///
            /// You should perform this check every time the app gets focus, in case the user has changed
            /// the setting while the app was suspended or not in focus.
            /// </summary>
            /// <returns>True, if the microphone is available.</returns>
            public async static Task<bool> RequestMicrophonePermission()
            {
                try
                {
                    // Request access to the audio capture device.
                    MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
                    settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
                    settings.MediaCategory = MediaCategory.Speech;
                    MediaCapture capture = new MediaCapture();

                    await capture.InitializeAsync(settings);
                }
                catch (TypeLoadException)
                {
                    // Thrown when a media player is not available.
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Media player components are unavailable.");
                    await messageDialog.ShowAsync();
                    return false;
                }
                catch (UnauthorizedAccessException)
                {
                    // Thrown when permission to use the audio capture device is denied.
                    // If this occurs, show an error or disable recognition functionality.
                    return false;
                }
                catch (Exception exception)
                {
                    // Thrown when an audio capture device is not present.
                    if (exception.HResult == NoCaptureDevicesHResult)
                    {
                        var messageDialog = new Windows.UI.Popups.MessageDialog("No Audio Capture devices are present on this system.");
                        await messageDialog.ShowAsync();
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                return true;
            }
        }


        private async void dictateButtonClick(object sender, RoutedEventArgs e)
        {
            dictateButton.IsEnabled = false;
            if (isListening == false)
            {
                // The recognizer can only start listening in a continuous fashion if the recognizer is currently idle.
                // This prevents an exception from occurring.
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    noteBox.Text = "Dictate your note!";
                    dictateButtonTextbox.Text = " Stop Dictation";       

                    try
                    {
                        isListening = true;
                        await speechRecognizer.ContinuousRecognitionSession.StartAsync();
                    }
                    catch (Exception ex)
                    {
                        isListening = false;
                        dictateButtonTextbox.Text = " Dictate";
                    }
                }
            }
            else
            {
                isListening = false;
                dictateButtonTextbox.Text = " Dictate";

                if (speechRecognizer.State != SpeechRecognizerState.Idle)
                {
                    // Cancelling recognition prevents any currently recognized speech from
                    // generating a ResultGenerated event. StopAsync() will allow the final session to 
                    // complete.
                    try
                    {
                        await speechRecognizer.ContinuousRecognitionSession.StopAsync();

                        // Ensure we don't leave any hypothesis text behind
                        noteBox.Text = dictatedTextBuilder.ToString();
                    }
                    catch (Exception exception)
                    {
                        var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                        await messageDialog.ShowAsync();
                    }
                }
            }
            dictateButton.IsEnabled = true;

        }

        private void deleteTextButtonClick(object sender, RoutedEventArgs e)
        {
            dictatedTextBuilder.Clear();
            noteBox.Text = "";
        }

        private void close_notes_page_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfoPage), null, new EntranceNavigationTransitionInfo());
        }

        public NoteGroup CreateNoteGroup()
        {
            // Create the new note
            NoteGroup newNoteGroup = new NoteGroup(noteBox.Text);
            _noteGroupModel.NoteGroups.Add(newNoteGroup);
            dictatedTextBuilder.Clear();
            noteBox.Text = "";
            return newNoteGroup;
        }

        private void createNewNoteGroupButtonClick(object sender, RoutedEventArgs e)
        {
            FocusedGroup = CreateNoteGroup();

        }
    }
}
