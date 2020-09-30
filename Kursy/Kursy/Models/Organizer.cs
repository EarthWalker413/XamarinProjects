using System;
using SQLite;

namespace Kursy.Models
{
    public class Organizer: Person
    {
        [PrimaryKey]
        public int OrganizerId { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        
    }
}
