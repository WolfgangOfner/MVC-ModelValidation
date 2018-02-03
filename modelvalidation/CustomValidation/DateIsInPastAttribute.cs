using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.CustomValidation
{
    public class DateIsInPastAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value) && (DateTime) value < DateTime.Now;
        }
    }
}