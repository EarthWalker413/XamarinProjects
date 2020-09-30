using System;
using SQLite;
namespace Kursy.Models
{
    public class CourseStudent
    {
        [PrimaryKey]
        public int CourseStudentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        
    }
}
