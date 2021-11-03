using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Morning_wakeup_app
{
    public class NoteModel
    {
        private ObservableCollection<Note> _notes; // All of the notes in one NoteGroup.


        public NoteModel()
        {
            this._notes = new ObservableCollection<Note>();

        }

        public ObservableCollection<Note> Notes
        {
            get
            {
                return this._notes;
            }
        }

        public void DeleteNote(Note noteToDelete) => Notes.Remove(noteToDelete);
    }


}
