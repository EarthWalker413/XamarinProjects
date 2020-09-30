using System;
using SQLite;
namespace Notatnik.Data
{
    public class Notebook
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Password { get; set; }
    }
    
}
