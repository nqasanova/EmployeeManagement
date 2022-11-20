using System;
using EmployeeManagement.Contexts;
using EmployeeManagement.Database.Models;
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

            var model = employees.Select(e => new ListViewModel(e.EmployeeCode, e.FirstName, e.LastName, e.FatherName, e.IsDeleted))
            .ToList();
            return View(model);
        }
        #endregion

        #region Edit

        [HttpGet("Edit/{EmployeeCode}", Name = "Employee-edit-info")]
        public ActionResult Edit(string EmployeeCode)
        {
            using DataContext dbcontext = new DataContext();
            var employees = dbcontext.Employees.FirstOrDefault(e => e.EmployeeCode == EmployeeCode);


            return View(
                new EditViewModel
                (employees.EmployeeCode, employees.FirstName, employees.LastName, employees.FatherName, employees.Email));
        }

        [HttpPost("Edit", Name = "Employee-edit")]
        public ActionResult Edit(EditViewModel model)
        {
            using DataContext dbcontext = new DataContext();
            var employees = dbcontext.Employees.FirstOrDefault(e => e.EmployeeCode == model.EmployeeCode);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (employees is null)
            {
                return NotFound();
            }

            employees.FirstName = model.FirstName;
            employees.LastName = model.LastName;
            employees.FatherName = model.FatherName;
            employees.Email = model.Email;
            dbcontext.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete

        [HttpGet("Delete/{EmployeeCode}", Name = "Employee-delete")]
        public ActionResult Delete(string EmployeeCode)
        {
            using DataContext dbcontext = new DataContext();
            var employees = dbcontext.Employees.FirstOrDefault(e => e.EmployeeCode == EmployeeCode);

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