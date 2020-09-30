using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kursy.Models;
using Kursy.Requests;
using SQLite;
using Xamarin.Forms;

namespace Kursy.DatabaseServise
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Organizer>().Wait();
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<CLocation>().Wait();
            _database.CreateTableAsync<CourseStudent>().Wait();
            _database.CreateTableAsync<Student>().Wait();
        }

        public Task<List<Course>> GetAllCoursesAsync()
        {
            return _database.Table<Course>().ToListAsync();
        }

        public Task<Course> GetCourseAsync(int id)
        {
            return _database.Table<Course>()
                            .Where(i => i.CourseId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<Organizer> GetOrganizerAsync(int id)
        {
            return _database.Table<Organizer>()
                            .Where(i => i.OrganizerId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<CLocation> GetCLocationAsync(int id)
        {
            return _database.Table<CLocation>()
                            .Where(i => i.LocationId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<CourseStudent> GetCourseStudentAsync(int id)
        {
            return _database.Table<CourseStudent>()
                            .Where(i => i.CourseStudentId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<Student> GetStudentAsync(int id)
        {
            return _database.Table<Student>()
                            .Where(i => i.StudentId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveCourseAsync(Course course)
        {

            return _database.InsertAsync(course);
        }

        public Task<int> updateCourse(Course course)
        {
            return _database.UpdateAsync(course);
        }

       

        public Task<int> SaveCLocationAsync(CLocation location)
        {

            return _database.InsertAsync(location);

        }

        public Task<int> SaveCourseStudentAsync(CourseStudent courseStudent)
        {

            return _database.InsertAsync(courseStudent);


        }

        public Task<int> SaveOrganizerAsync(Organizer organizer)
        {

            return _database.InsertAsync(organizer);

        }

        public Task<int> SaveStudentAsync(Student student)
        {

            return _database.InsertAsync(student);

        }

        public Task<List<Organizer>> GetAllOrganizersAsync()
        {
            return _database.Table<Organizer>().ToListAsync();
        }

        


        public Task<CreateTableResult> CreateOrganizerTable()
        {
            return _database.CreateTableAsync<Organizer>();
        }

        public Task<List<CourseStudent>> GetAllCoursesStudentCount(Course course)
        {
            return _database.Table<CourseStudent>()
                            .Where(i => i.CourseId == course.CourseId)
                            .ToListAsync();
        }

        public Task<List<CourseStudent>> GetAllCoursesStudentAsync()
        {
            return _database.Table<CourseStudent>().ToListAsync();
        }

        public Task<CLocation> GetLocationForCourse(Course course)
        {
            return _database.Table<CLocation>()
                            .Where(i => i.LocationId == course.LocationId)
                            .FirstOrDefaultAsync();
        }

        public Task<List<CLocation>> GetAllLocationsAsync()
        {
            return _database.Table<CLocation>().ToListAsync();
        }

        public Task<List<Course>> GetEnrolledCoursesAsync()
        {
            return _database.Table<Course>()
                            .Where(i => i.IsEnrolled == true)
                            .ToListAsync();
        }

    }
}
