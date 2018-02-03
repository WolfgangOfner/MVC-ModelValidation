using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ModelValidation.CustomValidation;

namespace ModelValidation.Models
{
    public class Customer
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Remote("ValidateName", "Home")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DateIsInPast(ErrorMessage = "Please enter a date in the past")]
        public DateTime Birthday { get; set; }

        [MustBeTrue(ErrorMessage = "You must accept the terms")]
        public bool TermsAndConditionAccepted { get; set; }
    }
}