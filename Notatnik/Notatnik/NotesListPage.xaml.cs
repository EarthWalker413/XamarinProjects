using System;
using System.Collections.Generic;
using System.Linq;
using Notatnik.Data;
using Xamarin.Forms;

namespace Notatnik
{
    public partial class NotesListPage : ContentPage
    {
        public Notebook Notebook;


        public NotesListPage()
        {
            InitializeComponent();

        }

        public NotesListPage(Notebook notebook)
        {
            InitializeComponent();
            this.Notebook = notebook;

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            var notes = await App.Database.GetNoteForNotebookAsync(this.Notebook);

            listView.ItemsSource = notes.OrderByDescending(n => n.Date).ToList();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new Note
                {
                    Date = DateTime.Now,
                    NotebookName = this.Notebook.Name
                }
            });

        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                var item = e.SelectedItem as Note;
                (sender as ListView).SelectedItem = null;

                await Navigation.PushAsync(new NoteEntryPage
                {
                    BindingContext = e.SelectedItem as Note

                });
               
            }
        }
    }
}
