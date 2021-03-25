using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfAppCore.model
{
    [Table("Apartment")]
    class Apartment : IRealty
    {
        public int id { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string apartmentNumber { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }

        public int roomsAmount { get; set; }
        public int square { get; set; }
    }
}
