using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Hotel.Web.Models
{
    public class UpdateRoomViewModel 
    {
        private const string requiredField = "This field can't be empty";
        private const string decimalMinVal = "100.00";
        private const string decimalMaxVal = "10000.00";

        public Guid Id { get; set; }

        public int Number { get; set; }

        [Required( ErrorMessage = requiredField )]
        [Range( typeof( decimal ), decimalMinVal, decimalMaxVal,
            ErrorMessage = "Type price in range: " + decimalMinVal + " - " + decimalMaxVal )]
        public decimal Price { get; set; }

        [Required( ErrorMessage = requiredField )]
        [RegularExpression( @"[A-Za-z0-9]{0,}[A-Za-z0-9\s.,!?]{1,}",
           ErrorMessage = "In description symbols : A-Z,a-z,0-9,',','.','!','?' and spases are allowed" )]
        public string Description { get; set; }

        public int PersonsCount { get; set; }

        public int BedCount { get; set; }

        public bool OnRestavration { get; set; }

        //public int Criterias { get; set; }

        public IDictionary<string, bool> Criterias { get; set; }
    }
}