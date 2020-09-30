using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursy.Models;
using Kursy.Requests;
using Xamarin.Forms;

namespace Kursy
{
    public partial class MainPage : ContentPage
    {
  
        public MainPage()
        {
            InitializeComponent();
           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FillDatabase();
            coursesList.ItemsSource = await GetItemSourceAsync();

        }

        private async Task<List<CourseRequest>> GetItemSourceAsync()
        {
            var courses = await App.Database.GetAllCoursesAsync();
            var cs = await App.Database.GetAllCoursesStudentAsync();
            var requests = new List<CourseRequest>();
            foreach (Course course in courses)
            {
                var organizer = await App.Database.GetOrganizerAsync(course.OrganizerId);
                var courseSrudentList = await App.Database.GetAllCoursesStudentCount(course);
                var location = await App.Database.GetLocationForCourse(course);
                var request = new CourseRequest
                {
                    CourseId = course.CourseId,
                    CourseName = course.Name,
                    CourseImage = course.Image,
                    CoursePrice = course.Price,
                    CourseDescription = course.Description,
                    IsCourseEnrolled = course.IsEnrolled,
                    CourseLocationId = course.LocationId,
                    CourseOrganizerId = course.OrganizerId,
                    OrganizerName = organizer.Firsname + " " + organizer.Surname,
                    OrganizerEmail = organizer.Email,
                    OrganizerImage = organizer.Image,
                    AttendeeNumber = courseSrudentList.Count,
                    LocationName = location.StreetAddress.Trim()
                + " " + location.BuildingNumber.Trim() + " "
                + location.PostalCode.Trim()
                + " " + location.CityName.Trim() + " " + location.Country.Trim()
                };
                requests.Add(request);

            }
            return requests;
        }


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as CourseRequest;


            if (selectedItem != null)
            {
                var item = selectedItem;
                (sender as ListView).SelectedItem = null;
                await Navigation.PushAsync(new CourseDescriptionPage()
                {
                    BindingContext = item
                });
                
            }
      
        }


        private async Task FillDatabase()
        {
          
            var organizer1 = new Organizer {OrganizerId = 1, Firsname = "Anatolii", Surname = "Tomazov", Email = "anatoliy.tomazov@gmail.com", Image = "organizer.png" };
            var organizer2 = new Organizer {OrganizerId = 2, Firsname = "Simon", Surname = "Kozlov", Email = "eartwalker413@gmail.com", Image = "organizer.png" };

            
            var organizers = await App.Database.GetAllOrganizersAsync();
            var organizerOrDefault1 = organizers.FirstOrDefault(i => i.OrganizerId == organizer1.OrganizerId);
            if (organizerOrDefault1 == null)
                await App.Database.SaveOrganizerAsync(organizer1);

            var organizerOrDefault2 = organizers.FirstOrDefault(i => i.OrganizerId == organizer2.OrganizerId);
            if (organizerOrDefault2 == null)
                await App.Database.SaveOrganizerAsync(organizer2);



            var location1 = new CLocation
            {
                LocationId = 1,
                StreetAddress = "Wita Stwosza",
                BuildingNumber = "28A",
                PostalCode = "02-661",
                CityName = "Warszawa",
                Country = "Poland"
            };


            var location2 = new CLocation
            {
                LocationId = 2,
                StreetAddress = "Aleja Wilanowska",
                BuildingNumber = "364A",
                PostalCode = "02-665",
                CityName = "Warszawa",
                Country = "Poland"
            };

            var locationOrDefault1 = await App.Database.GetCLocationAsync(location1.LocationId);

            if (locationOrDefault1 == null)
                await App.Database.SaveCLocationAsync(location1);

            var locationOrDefault2 = await App.Database.GetCLocationAsync(location2.LocationId);

            if (locationOrDefault2 == null)
                await App.Database.SaveCLocationAsync(location2);


            var course1 = new Course
            {
                CourseId =1,
                Name = "Struktury danych",
                LocationId = 1,
                OrganizerId = 1,
                IsEnrolled = false,
                Description = "Opis ma być tutaj",
                Image = "datastructure.png",
                Price = 12
            };

            var course2 = new Course
            {
                CourseId = 2,
                Name = "Logika cyfrowa i projektowanie",
                LocationId = 2,
                OrganizerId = 2,
                IsEnrolled = false,
                Description = "Opis ma być tutaj",
                Image = "design.png",
                Price = 10
            };


            var course3 = new Course
            {
               CourseId = 3,
                Name = "Przetwarzanie danych",
                LocationId = 2,
                OrganizerId = 1,
                IsEnrolled = false,
                Description = "Opis ma być tutaj",
                Image = "laptop.png",
                Price = 12.2
            };

            var course4 = new Course
            {
                CourseId = 4,
                Name = "Multimedia i animacje",
                LocationId = 1,
                OrganizerId = 2,
                IsEnrolled = false,
                Description = "Opis ma być tutaj",
                Image = "animation.png",
                Price = 11.5
            };

            var course5 = new Course
            {
                CourseId = 5,
                Name = "Projektowanie i analiza algorytmów",
                LocationId = 1,
                OrganizerId = 1,
                IsEnrolled = false,
                Description = "Opis ma być tutaj",
                Image = "algorithm.png",
                Price = 5.7
            };

            var courseOrDefault1 = await App.Database.GetCourseAsync(course1.CourseId);
            if(courseOrDefault1 == null)
                await App.Database.SaveCourseAsync(course1);

            var courseOrDefault2 = await App.Database.GetCourseAsync(course2.CourseId);
            if (courseOrDefault2 == null)
                await App.Database.SaveCourseAsync(course2);
            var courseOrDefault3 = await App.Database.GetCourseAsync(course3.CourseId);
            if (courseOrDefault3 == null)
                await App.Database.SaveCourseAsync(course3);
            var courseOrDefault4 = await App.Database.GetCourseAsync(course4.CourseId);
            if (courseOrDefault4 == null)
                await App.Database.SaveCourseAsync(course4);
            var courseOrDefault5 = await App.Database.GetCourseAsync(course5.CourseId);
            if (courseOrDefault5 == null)
                await App.Database.SaveCourseAsync(course5);



            var student1 = new Student { StudentId = 1, Firsname = "Name1", Surname = "Surname1" };
            var student2 = new Student { StudentId = 2, Firsname = "Name2", Surname = "Surname2" };
            var student3 = new Student { StudentId = 3, Firsname = "Name3", Surname = "Surname3" };

            var studentOrDefault1 = App.Database.GetStudentAsync(student1.StudentId);
            if (studentOrDefault1 == null)
                await App.Database.SaveStudentAsync(student1);

            var studentOrDefault2 = App.Database.GetStudentAsync(student2.StudentId);
            if (studentOrDefault2 == null)
                await App.Database.SaveStudentAsync(student2);

            var studentOrDefault3 = App.Database.GetStudentAsync(student3.StudentId);
            if (studentOrDefault3 == null)
                await App.Database.SaveStudentAsync(student3);


            var courseStudent1 = new CourseStudent { CourseStudentId = 1, StudentId = 1, CourseId = 1 };
            var courseStudent2 = new CourseStudent { CourseStudentId = 2, StudentId = 2, CourseId = 2 };
            var courseStudent3 = new CourseStudent { CourseStudentId = 3, StudentId = 3, CourseId = 1 };

            var courseStudentList = await App.Database.GetAllCoursesStudentAsync();
            var courseStudentOrDefault1 = courseStudentList.FirstOrDefault(i => i.CourseStudentId == courseStudent1.CourseStudentId);
            if(courseStudentOrDefault1 == null)
                await App.Database.SaveCourseStudentAsync(courseStudent1);

           
            var courseStudentOrDefault2 = courseStudentList.FirstOrDefault(i => i.CourseStudentId == courseStudent2.CourseStudentId);
            if (courseStudentOrDefault2 == null)
                await App.Database.SaveCourseStudentAsync(courseStudent2);

            var courseStudentOrDefault3 = courseStudentList.FirstOrDefault(i => i.CourseStudentId == courseStudent3.CourseStudentId);
            if (courseStudentOrDefault3 == null)
                await App.Database.SaveCourseStudentAsync(courseStudent3);

        }


    }
}
