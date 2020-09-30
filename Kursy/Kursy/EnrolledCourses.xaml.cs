using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kursy.Models;
using Kursy.Requests;
using Xamarin.Forms;

namespace Kursy
{
    public partial class EnrolledCourses : ContentPage
    {
        public EnrolledCourses()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            enrolledCoursesList.ItemsSource = await GetItemSourceAsync();

        }

        private async Task<List<CourseRequest>> GetItemSourceAsync()
        {
            var courses = await App.Database.GetAllCoursesAsync();
            var organizers = await App.Database.GetAllOrganizersAsync();

            var enrolledCourses = courses.FindAll(e => e.IsEnrolled == true);
            var requests = new List<CourseRequest>();
            foreach (Course course in enrolledCourses)
            {
                var organizer = await App.Database.GetOrganizerAsync(course.OrganizerId);

                var location = await App.Database.GetLocationForCourse(course);
                var request = new CourseRequest
                {
                    CourseName = course.Name,

                    CoursePrice = course.Price,
                    CourseDescription = course.Description,
                    IsCourseEnrolled = course.IsEnrolled,
                    CourseLocationId = course.LocationId,
                    CourseOrganizerId = course.OrganizerId,
                    CourseImage = course.Image,


                    OrganizerName = organizer.Firsname + " " + organizer.Surname,
                    OrganizerEmail = organizer.Email,
                    OrganizerImage = organizer.Image,

                    LocationName = location.StreetAddress.Trim()
                    + " " + location.BuildingNumber.Trim() + " "
                    + location.PostalCode.Trim()
                    + " " + location.CityName.Trim() + " " + location.Country.Trim()
                };
                requests.Add(request);

            }

            return requests;
        }

        async void OnEnrolledItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as CourseRequest;

            if (selectedItem != null)
            {
                var item = selectedItem;
                (sender as ListView).SelectedItem = null;

               await Navigation.PushAsync(new EnrolledCourseDescriptionPage()
                {
                    BindingContext = selectedItem
                });
                (sender as ListView).SelectedItem = null;
            }


        }
    }
}
