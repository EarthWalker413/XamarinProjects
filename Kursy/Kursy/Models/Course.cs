using System;
using SQLite;

namespace Kursy.Models
{
    public class Course
    {
        [PrimaryKey]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsEnrolled { get; set; }
        public int LocationId { get; set; }
        public int OrganizerId { get; set; }

    }
}
