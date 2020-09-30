using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notatnik
{
    public partial class MainPage : ContentPage
    {

        private bool isFirstAppearance = true;


        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Zamknąć");
            isFirstAppearance = true;

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
        }

        


        async void OnOpenButtonClicked(object sender, EventArgs e)
        {


            if (CheckAllEntriesFilled())
            {

                var notebookList = await App.Database.GetNotebookAsync(nameEntry.Text);
                if (notebookList.Count < 1)
                    await DisplayAlert("Nie udało się", "Taki notatnik nie istnieje", "Okej");
                else
                {
                    var notebook = notebookList.First();
                    if (notebook.Password != passwordEntry.Text)
                        await DisplayAlert("Nie udało się", " złe hasło", "Okej");
                    else
                    {
                        nameEntry.Text = String.Empty;
                        passwordEntry.Text = String.Empty;

                        await containerStackView.RotateYTo(-90, 220);


                        await Navigation.PushAsync(new NotesListPage
                        {
                            Notebook = notebook
                        });
                    }

                }

            }

        }

        private bool CheckAllEntriesFilled()
        {
            var isAllEntriesFilled = true;

            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                nameRequiredLabel.IsVisible = true;
                isAllEntriesFilled = false;
            }
            else
            {
                nameRequiredLabel.IsVisible = false;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                passwordRequiredLabel.IsVisible = true;
                isAllEntriesFilled = false;
            }
            else
            {
                passwordRequiredLabel.IsVisible = false;
            }

            return isAllEntriesFilled;
        }

        async void OnCreateLabelTapped(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CreateNotebookPage());

        }

        async void OnListNotepadsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotepadsListPage());
        }
    }
}
