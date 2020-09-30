using System;
using System.IO;
using Kursy.DatabaseServise;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kursy
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Courses.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new CoursesTabbedPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
