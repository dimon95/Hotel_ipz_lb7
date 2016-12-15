using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Hotel.Web.Models
{
    public class AddHallViewModel
    {
        private const string requiredField = "This field can't be empty";
        private const string numberMaxValue = "10000";
        private const string decimalMinVal = "100.00";
        private const string decimalMaxVal = "10000.00";

        [Required(ErrorMessage = requiredField)]
        [Range(typeof(int), "1", numberMaxValue, 
            ErrorMessage = "Number must be greater then null and less then 10000")]
        [System.Web.Mvc.Remote( "CheckHallNumber", "Hall")]
        public int Number { get; set; }

        [Required( ErrorMessage = requiredField )]
        [RegularExpression( @"[A-Za-z0-9]{0,}[A-Za-z0-9\s.,!?]{1,}", 
            ErrorMessage = "In description symbols : A-Z,a-z,0-9,',','.','!','?' and spases are allowed")]
        public string Description { get; set; }

        [Required( ErrorMessage = requiredField )]
        [Range( typeof( decimal ), decimalMinVal, decimalMaxVal,
            ErrorMessage = "Type price in range: " + decimalMinVal + " - " + decimalMaxVal )]
        public decimal Price { get; set; }

        [Required( ErrorMessage = requiredField )]
        [Range(typeof(int), "1", "1000", ErrorMessage = "Room can hold only one or two persons")]
        public int PersonsCount { get; set; }
    }
}