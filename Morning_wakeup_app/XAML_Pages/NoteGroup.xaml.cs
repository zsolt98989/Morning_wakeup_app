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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Morning_wakeup_app
{

    public partial class NoteGroup
    {
        //private List<Note> _noteList;
        public NoteModel _noteModel { get; private set; }

        public NoteGroup(String _tempTextBoxText)
        {
            this.InitializeComponent();
            _noteModel = new NoteModel();
            tempTextBox.Text = _tempTextBoxText;

            //_noteList = new List<Note>();
        }

        public void OnDeleteNoteEvent()
        {
            App theApp = Application.Current as App;
            theApp.NoteGroupModel.DeleteNote(this);
        }


        private void deleteNoteGroupButtonClick(object sender, RoutedEventArgs e)
        {
            OnDeleteNoteEvent();

        }

        private void showNoteButtonClick(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note();
            _noteModel.Notes.Add(newNote);

        }
    }
}
