using System;
using SQLite;

namespace Kursy.Models
{
    public class Student: Person
    {
        [PrimaryKey]
        public int StudentId { get; set; }
    }
}
