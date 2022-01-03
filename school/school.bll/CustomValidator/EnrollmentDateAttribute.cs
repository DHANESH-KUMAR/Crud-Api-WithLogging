using System;
using System.ComponentModel.DataAnnotations;

namespace school.bll
{
    public class EnrollmentDateAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Today.AddYears(-1) && dateValue <= DateTime.Today)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Please provide valid Enrollment Date. Format should be [yyyy-mm-dd]");
        }
    }
}
