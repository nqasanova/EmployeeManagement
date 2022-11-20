using System;
using EmployeeManagement.Database.Models;

namespace EmployeeManagement.Contexts
{
    public class EmployeePKGenerator
    {
        static Random _random = new Random();
        private static string _employeeCode;

        public static string EmployeeCode
        {
            get
            {
                DataContext dataContext = new DataContext();
                var employees = dataContext.Employees.ToList();

                bool status = true;
                string _employeeCode = "E" + _random.Next(10000, 100000);

                foreach (var employee in employees)
                {
                    if (employee.EmployeeCode == _employeeCode)
                    {
                        do
                        {
                            _employeeCode = "E" + _random.Next(10000, 100000);

                        } while (employee.EmployeeCode != _employeeCode);
                    }


                }
                return _employeeCode;
            }
        }
    }
}