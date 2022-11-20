using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels.Employee
{
    public class EditViewModel
    {
        public string EmployeeCode { get; set; }

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

        public EditViewModel()
        {

        }

        public EditViewModel(string employeeCode, string firstName, string lastName, string fatherName, string email)
        {
            EmployeeCode = employeeCode;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            Email = email;
        }
    }
}