using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursy.Models;
using Kursy.Requests;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kursy
{
    public partial class CourseDescriptionPage : ContentPage
    {

        private CourseRequest Request;
        public CourseDescriptionPage()
        {
            InitializeComponent();
            noteDescriptionEntry.Focused += async (s, e) => { await scrollView.ScrollToAsync(0, 0, false); };
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Request = (CourseRequest)BindingContext;
            if (Request.IsCourseEnrolled)
                enrollBarButton.Text = "Wypisać się";
            else
                enrollBarButton.Text = "Zapisać się";
        }


        async void OnShowDirectionClicked(object sender, EventArgs e)
        {
            try
            {

                var locations = await Geocoding.GetLocationsAsync(Request.LocationName);
                var location = locations?.FirstOrDefault();

                await Map.OpenAsync(location.Latitude, location.Longitude, new MapLaunchOptions
                {
                    NavigationMode = NavigationMode.None
                });
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Nie udało się", "Mapy nie są obsługiwane w tym telefonie", "Okej");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Nie udało się", "Mapy nie są włączone w tym telefonie", "Okej");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Nie udało się", "Przyznaj uprawnienia usługom lokalizacyjnym dla tej aplikacji", "Okej");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Nie udało się", "Coś jest złe", "Okej");
            }
        }

        async void OnEnrollClicked(object sender, EventArgs e)
        {
     
            Request.IsCourseEnrolled = !Request.IsCourseEnrolled;
            if (Request.IsCourseEnrolled)
                enrollBarButton.Text = "Wypisać się";
            else
                enrollBarButton.Text = "Zapisać się";

            var course = new Course
            {
                CourseId = Request.CourseId,
                Name = Request.CourseName,
                Price = Request.CoursePrice,
                Description = Request.CourseDescription,
                Image = Request.CourseImage,
                IsEnrolled = Request.IsCourseEnrolled,
                LocationId = Request.CourseLocationId,
                OrganizerId = Request.CourseOrganizerId
                
            };

            await App.Database.updateCourse(course);
        }

        async void OnSendEmailClick(object sender, EventArgs e)
        {
            try
            {
                await Email.ComposeAsync(String.Empty, String.Empty, organizerEmailLabel.Text);

            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Nie udało się", "usługi e - mail nie są obsługiwane w tym telefonie", "Okej");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Nie udało się", "Coś jest złe", "Okej");
            }
        }

    }
}
