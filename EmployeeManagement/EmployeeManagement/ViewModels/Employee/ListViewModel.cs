using System;
namespace EmployeeManagement.ViewModels.Employee
{
    public class ListViewModel
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public bool IsDeleted { get; set; }

        public ListViewModel(string employeeCode, string firstName, string lastName, string fatherName, bool isDeleted)
        {
            EmployeeCode = employeeCode;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            IsDeleted = isDeleted;
        }
    }
}