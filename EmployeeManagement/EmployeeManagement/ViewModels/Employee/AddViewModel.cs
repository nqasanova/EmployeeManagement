using System;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Validations;

namespace EmployeeManagement.ViewModels.Employee
{
    public class AddViewModel
    {
        [RegularExpression(@"[A-Za-z]{3,20}", ErrorMessage = "Employee's First Name should be more than 3 and less than 20 characters!")]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"[A-Za-z]{3,20}", ErrorMessage = "Employee's Last Name should be more than 3 and less than 20 characters!")]
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"[A-Za-z]{3,20}", ErrorMessage = "Employee's Father Name should be more than 3 and less than 20 characters!")]
        [Required]
        public string FatherName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [FINValidation(ErrorMessage = "Employee's FIN code must consist of only letters, numbers, and 7 characters!")]
        public string FIN { get; set; }

        public AddViewModel()
        {

        }

        public AddViewModel(string firstName, string lastName, string fatherName, string email, string fin)
        {
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            Email = email;
            FIN = fin;
        }
    }
}