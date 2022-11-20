using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Validations
{
    public class FINValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            Regex regex = new Regex(@"^[A-Z0-9]{7}$");

            if (value != null)
            {
                string FIN = value.ToString();
                if (regex.IsMatch(FIN))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Employee's FIN is incorrect!");
        }
    }
}