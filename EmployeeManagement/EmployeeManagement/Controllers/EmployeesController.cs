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
        [HttpGet("Add", Name = "Employee-add")]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost("Add", Name = "Employee-add")]
        public ActionResult Add(AddViewModel model)
        {
            using DataContext dbcontext = new DataContext();

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            dbcontext.Employees.Add(new Database.Models.Employee
            {
                EmployeeCode = EmployeePKGenerator.EmployeeCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                Email = model.Email,
                FIN = model.FIN,
                IsDeleted = default
            });

            dbcontext.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region List

        [HttpGet("List", Name = "Employee-list")]
        public IActionResult List()
        {
            using DataContext dbcontext = new DataContext();
            var employees = dbcontext.Employees.ToList();

            var model = employees.Select(e => new ListViewModel(e.EmployeeCode, e.FirstName, e.LastName, e.FatherName, e.IsDeleted)).ToList();
            return View("~/Views/Employees/List.cshtml", model);
        }
        #endregion

        #region Edit

        [HttpGet("Edit/{employeeCode}", Name = "Emloyee-edit-employeeCode")]
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

        [HttpPost("Edit", Name = "Employee-edit")]
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

        [HttpGet("Delete/{employeeCode}", Name = "Employee-delete")]
        public ActionResult Delete(string employeeCode)
        {
            using DataContext dbcontext = new DataContext();
            var employees = dbcontext.Employees.FirstOrDefault(e => e.EmployeeCode == employeeCode);


            if (employees is null)
            {
                return NotFound();
            }

            employees.IsDeleted = true;
            dbcontext.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}