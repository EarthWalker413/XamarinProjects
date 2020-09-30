using System;
using SQLite;
namespace Notatnik.Data
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        //ForeignKey
        public string NotebookName { get; set; }
    }
}
