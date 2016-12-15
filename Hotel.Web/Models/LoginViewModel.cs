using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Hotel.Web.Models
{
    public class LoginViewModel
    {
        [RegularExpression("[A-Za-z0-9!()_#$]{4,30}")]
        public string Password { get; set; }

        [EmailAddress]
        //[RegularExpression("[A-Za-z0-9]{1,}@[a-z]{2,10}.[a-z]{2,5}")]
        public string Email { get; set; }
    }
}