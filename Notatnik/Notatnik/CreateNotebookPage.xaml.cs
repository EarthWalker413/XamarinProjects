using System;
using System.Collections.Generic;
using Notatnik.Data;
using Xamarin.Forms;

namespace Notatnik
{
    public partial class CreateNotebookPage : ContentPage
    {
        public CreateNotebookPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Zamknąć");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await containerStackView.RotateYTo(0, 220);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            nameRequiredLabel.IsVisible = false;
            passwordRequiredLabel.IsVisible = false;
            passwordsNotSameLabel.IsVisible = false;
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            if (checkIfDataValid())
            {
                var name = nameEntry.Text;
                var password = passwordEntry.Text;
                var list = await App.Database.GetNotebookAsync(name);
                if (list.Count > 0)
                {
                    await DisplayAlert("Nie udało się", "Taki notatnik już istnieje", "Okej");
                }
                else
                {
                    await App.Database.SaveNotebookAsync(new Notebook
                    {
                        Name = name,
                        Password = password
                    });

                    await containerStackView.RotateYTo(-90, 220);

                    await Navigation.PushAsync(new NotesListPage
                    {
                        Notebook = new Notebook { Name = name, Password = password }
                    });
                }

            }
        }

        private bool checkIfDataValid()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                isValid = false;
                nameRequiredLabel.IsVisible = true;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                isValid = false;
                passwordRequiredLabel.IsVisible = true;
            }

            if (string.IsNullOrWhiteSpace(passwordRequiredLabel.Text))
            {
                isValid = false;
                passwordRequiredLabel.IsVisible = true;
            }

            if (repeatPasswordEntry.Text != passwordEntry.Text || string.IsNullOrWhiteSpace(repeatPasswordEntry.Text))
            {
                isValid = false;
                passwordsNotSameLabel.IsVisible = true;
            }

            return isValid;
        }

    }
}
