using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Morning_wakeup_app
{
    public class NoteGroupModel
    {
        private ObservableCollection<NoteGroup> _noteGroups; // All of the NoteGroups in the app.


        public NoteGroupModel()
        {
            this._noteGroups = new ObservableCollection<NoteGroup>();

        }

        public ObservableCollection<NoteGroup> NoteGroups
        {
            get
            {
                return this._noteGroups;
            }
        }

        public void DeleteNote(NoteGroup noteToDelete) => NoteGroups.Remove(noteToDelete);
    }


}
