using System;
using SQLite;

namespace Kursy.Models
{
    public class CLocation
    {

        [PrimaryKey]
        public int LocationId { get; set; }
        public string StreetAddress { get; set; }
        public string BuildingNumber { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        
    }
}
