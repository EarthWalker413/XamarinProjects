using System;
using System.Collections.Generic;
using System.Linq;
using Notatnik.Data;
using Xamarin.Forms;

namespace Notatnik
{
    public partial class NotepadsListPage : ContentPage
    {
        private Notebook Notebook = null;

        public NotepadsListPage()
        {
            InitializeComponent();
            HideDeleteButton();


        }

        private void HideDeleteButton()
        {
            DeleteNotebookButton.IsEnabled = false;
            DeleteNotebookButton.Text = String.Empty;
        }

        private void ShowDeleteButton()
        {
            DeleteNotebookButton.IsEnabled = true;
            DeleteNotebookButton.Text = "Usunąć";
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            UpdateListData();
            
        }

        private async void UpdateListData()
        {
            var notepadsList = await App.Database.GetNotebooksListAsync();

            notepadsListView.ItemsSource = notepadsList.OrderByDescending(n => n.Name).ToList();
        }

        

        async void OnNotebookDeletedClicked(object sender, EventArgs e)
        {
            if (this.Notebook != null) {

                

                string password = await DisplayPromptAsync("Potwierdzenie usunięcia notatnika", "Wpisz hasło");

                //returns if cancel button clicked
                if (string.IsNullOrWhiteSpace(password))
                    return;

                var notebookList = await App.Database.GetNotebookAsync(this.Notebook.Name, password);
                if (notebookList.Count < 1)
                    await DisplayAlert("Nie udało się", "Złe hasło", "Okej");
                else
                {
                    await App.Database.deleteNotebookAsync(this.Notebook);
                    await App.Database.DeleteNotesForNotepadAsync(this.Notebook.Name);
                    this.Notebook = null;
                    UpdateListData();
                    HideDeleteButton();
                }
            } 
        }

        
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ShowDeleteButton();
                this.Notebook = e.SelectedItem as Notebook;
            }
        }
    }
}
