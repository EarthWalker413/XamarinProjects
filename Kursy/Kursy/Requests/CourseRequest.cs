using System;
namespace Kursy.Requests
{
    public class CourseRequest
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public double CoursePrice { get; set; }
        public bool IsCourseEnrolled { get; set; }

        public string CourseImage { get; set; }
        public int CourseLocationId { get; set; }
        public int CourseOrganizerId { get; set; }

        public int AttendeeNumber { get; set; }
        
        public string OrganizerName { get; set; }
        public string OrganizerImage { get; set; }
        public string OrganizerEmail { get; set; }
        public string LocationName { get; set; }

        
      
    }
}
