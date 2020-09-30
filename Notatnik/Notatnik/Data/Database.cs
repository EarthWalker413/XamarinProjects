using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Notatnik.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Notebook>().Wait();
            _database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Notebook>> GetNotebooksListAsync()
        {
            return _database.Table<Notebook>().ToListAsync();
        }


        public Task<int> deleteNotebookAsync(Notebook notebook)
        {

            return _database.DeleteAsync(notebook);
        }

        public Task<List<Notebook>> GetNotebookAsync(string name)
        {
            string queryString = string.Format("SELECT * FROM NOTEBOOK WHERE Name = '{0}'", name);
            return _database.QueryAsync<Notebook>(queryString);
        }

        public Task<List<Notebook>> GetNotebookAsync(string name, string password)
        {
            string queryString = string.Format("SELECT * FROM NOTEBOOK WHERE Name = '{0}' AND Password = '{1}'",name, password);
            return _database.QueryAsync<Notebook>(queryString);
        }

        public Task<int> SaveNotebookAsync(Notebook notebook)
        {
            return _database.InsertAsync(notebook);
        }

        public Task<Note> GetNoteAsync(int id)
        {
            return _database.Table<Note>()
                            .Where(i => i.NoteID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Note>> GetNoteForNotebookAsync(Notebook notebook)
        {
            
            string queryString = string.Format("SELECT * FROM Note WHERE NotebookName = '{0}'", notebook.Name);
            return _database.QueryAsync<Note>(queryString);
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            return _database.InsertAsync(note);
        }

        public Task<int> UpdateNoteAsync(Note note)
        {
            return _database.UpdateAsync(note);
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            return _database.DeleteAsync(note);
        }

        public Task<List<Note>> DeleteNotesForNotepadAsync(string notebookName)
        {
            string queryString = string.Format("DELETE FROM Note WHERE NotebookName = '{0}'", notebookName);
            return _database.QueryAsync<Note>(queryString);
        }


    }

}
