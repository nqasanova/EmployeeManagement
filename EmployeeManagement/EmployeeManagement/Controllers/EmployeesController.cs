using System;
using EmployeeManagement.Contexts;
using EmployeeManagement.Database.Models;
using EmployeeManagement.Migrations;
using EmployeeManagement.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("Employee")]
    public class EmployeesController : Controller
    {
        #region Add
        [HttpGet("Add", Name = "employee-add")]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost("Add", Name = "employee-add")]
        public ActionResult Add(AddViewModel addedModel)
        {

            if (!ModelState.IsValid)
            {
                return View("~/Views/Employees/Add.cshtml");
            }

            using DataContext dbcontext = new DataContext();

            dbcontext.Employees.Add(new Employee
            {
                EmployeeCode = EmployeePKGenerator.randomEmployeeCode,
                FirstName = addedModel.FirstName,
                LastName = addedModel.LastName,
                FatherName = addedModel.FatherName,
                Email = addedModel.Email,
                FIN = addedModel.FIN,
                IsDeleted = default
            });

            dbcontext.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region List

        [HttpGet("List", Name = "employee-list")]
        public IActionResult List()
        {
            using DataContext dbcontext = new DataContext();

            var model = dbcontext.Employees.Select(e => new ListViewModel(e.EmployeeCode, e.FirstName, e.LastName, e.FatherName, e.IsDeleted)).ToList();
            return View("~/Views/Employees/List.cshtml", model);
        }
        #endregion

        #region Edit

        [HttpGet("Edit/{employeeCode}", Name = "employee-edit-employeeCode")]
        public ActionResult Edit(string employeeCode)
        {
            using DataContext dbContext = new DataContext();
            var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeCode == employeeCode);

            if (employee == null && employee.IsDeleted == true)
            {
                return NotFound();
            }

            var pastEmployee = new EditViewModel(employee.EmployeeCode, employee.FirstName, employee.LastName, employee.FatherName, employee.Email, employee.FIN);
            return View("~/Views/Employees/Edit.cshtml", pastEmployee);
        }

        [HttpPost("Edit", Name = "employee-edit")]
        public ActionResult Edit(EditViewModel newModel)
        {
            using DataContext dbContext = new DataContext();
            var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeCode == newModel.EmployeeCode);

            if (employee == null && employee.IsDeleted == true)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Employees/Edit.cshtml", newModel);
            }
            employee.FirstName = newModel.FirstName;
            employee.LastName = newModel.LastName;
            employee.FatherName = newModel.FatherName;
            employee.Email = newModel.Email;
            employee.FIN = newModel.FIN;

            dbContext.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete

        [HttpGet("Delete/{employeeCode}", Name = "employee-delete")]
        public ActionResult Delete(string employeeCode)
        {
            using DataContext dbcontext = new DataContext();
            var employee = dbcontext.Employees.FirstOrDefault(e => e.EmployeeCode == employeeCode);


            if (employee is null)
            {
                return NotFound();
            }

            employee.IsDeleted = true;
            dbcontext.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}