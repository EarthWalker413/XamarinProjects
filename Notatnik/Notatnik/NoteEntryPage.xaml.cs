using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notatnik.Data;
using Xamarin.Forms;

namespace Notatnik
{
    public partial class NoteEntryPage : ContentPage
    {


        public NoteEntryPage()
        {
            InitializeComponent();
            noteDescriptionEntry.Focused += async (s, e) => { await scrollView.ScrollToAsync(0, 0, false); };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var note = (Note)BindingContext;

            if (note.NoteID == 0)
            {
                DeleteToolBarItem.IsEnabled = false;
            }
            else
            {
                DeleteToolBarItem.IconImageSource = "trashcan.png";
                DeleteToolBarItem.IsEnabled = true;
                
            }
            dateLabel.IsVisible = true;
            noteDescriptionEntry.IsVisible = true;
            dateLabel.Opacity = 0;
            scrollView.Opacity = 0;
            await Task.WhenAll(
                dateLabel.FadeTo(1, 220),
                dateLabel.RotateTo(360, 220),
                scrollView.FadeTo(1, 220));
            dateLabel.Rotation = 0;

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await Task.WhenAll(
                    dateLabel.FadeTo(0, 220),
                    dateLabel.RotateTo(-360, 220),
                    scrollView.FadeTo(0, 220));
            dateLabel.Rotation = 0;
        }

        async void OnDoneClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (note.NoteID != 0)
            {
                note.Date = DateTime.Now;
                await App.Database.UpdateNoteAsync(note);
            }
            else
                if (!string.IsNullOrWhiteSpace(note.Description))
                    await App.Database.SaveNoteAsync(note);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {

            var note = (Note)BindingContext;

            if (note.NoteID != 0)
            {
                
                bool response = await DisplayAlert(String.Empty, " Jesteś pewien, że chcesz usunąć tę notatkę?", "Tak", "Nie");
                if (response)
                    await App.Database.DeleteNoteAsync(note);
                else
                    return;
            }
            await Navigation.PopAsync();
        }

    }
}
