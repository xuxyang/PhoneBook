using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookIdeal.Models
{
    public class Phone
    {
        public int ID { get; set; }
        [RegularExpression("[a-zA-Z]{3,30}")]
        //[Required(ErrorMessage = "wrong first name")]
        public string FirstName { get; set; }
        [RegularExpression("[a-zA-Z]{3,30}")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:\([2-9]\d{2}\)\ ?|[2-9]\d{2}(?:\-?|\ ?))[2-9]\d{2}[- ]?\d{4}$")]
        public string PhoneNumber { get; set; }
    }
}