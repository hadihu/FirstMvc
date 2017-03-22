using FirstMvc.Models;
using FirstMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMvc.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public string GetString()
        {
            return "Hello MVC!"; 
        }

        public ActionResult GetView()
        {
            //Employee emp = new Employee();
            //emp.FirstName = "zhang";
            //emp.LastName = "san";
            //emp.Salary = 10000;
            //ViewData["Employee"] = emp;
            ////ViewBag.Employee = emp;
            //EmployeeViewModel evm = new EmployeeViewModel();
            //evm.EmployeeName = emp.FirstName + " " + emp.LastName;
            //evm.Salary = emp.Salary.ToString("C");
            //evm.SalaryColor = emp.Salary > 1000 ? "yellow" : "green";
            //return View("GetView", evm);

            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
      
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();
      
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
      
            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            employeeListViewModel.UserName = "Admin";
            return View("GetView", employeeListViewModel);
        }
      
	}
}