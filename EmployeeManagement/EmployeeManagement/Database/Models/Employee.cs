using System;
namespace EmployeeManagement.Database.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string FIN { get; set; }
        public bool IsDeleted { get; set; }
    }
}