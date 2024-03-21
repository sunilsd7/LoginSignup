﻿
using LoginSignup.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using LoginSignup.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace LoginSignup.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddEmployee()
        {
            var viewModel = new AddEmployeeViewModel();
            Console.WriteLine("ActionResult 1st");
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(AddEmployeeViewModel viewModel)
        {
            Console.WriteLine("ActionResult 2nd");
            if (ModelState.IsValid)
            {
                Console.WriteLine("Isvalid");

                Employee newEmployee = new Employee
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    ImageUrl = viewModel.ImageUrl,
                    PhoneNumber = viewModel.PhoneNumber,
                    Address = viewModel.Address,
                };
                Console.WriteLine(newEmployee);
                _employeeRepository.AddEmployee(newEmployee);
            }
            return RedirectToAction("EmployeeList");
        }
        [Authorize]
        //retrive controller ma
        [HttpGet]
        public IActionResult EmployeeList()
        {

            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel(_employeeRepository.AllEmployee);
            return View(employeeListViewModel);
        }

        //update ko lagi
        [Authorize]
        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            var editEmployee = new UpdateEmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                ImageUrl = employee.ImageUrl,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
            };


            return View(editEmployee);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel model)
        {
            var employee = _employeeRepository.GetEmployeeById(model.EmployeeId);

            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.ImageUrl = model.ImageUrl;
            employee.PhoneNumber = model.PhoneNumber;
            employee.Address = model.Address;

            _employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("EmployeeList");
        }
        [Authorize]
        public IActionResult DeleteEmployee(int id)
        {

            _employeeRepository.DeleteEmployee(id);

            return RedirectToAction("EmployeeList");


        }


    }
}