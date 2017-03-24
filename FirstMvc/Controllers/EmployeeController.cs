using FirstMvc.Filters;
using FirstMvc.Models;
using FirstMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMvc.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Test/

        public string GetString()
        {
            return "Hello MVC!"; 
        }

        [AdminFilter]
        public ActionResult Index()
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
            //employeeListViewModel.UserName = "Admin";
            employeeListViewModel.UserName = User.Identity.Name;

            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.CompanyName = "comc";
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", employeeListViewModel);
        }

        [Authorize]
        public ActionResult AddNew()
        {
            return View("CreateEmployee", new CreateEmployeeViewModel());
        }
        [AdminFilter]
        public ActionResult  SaveEmployee(Employee e, string BtnSubmit)
        {
            switch(BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer eb = new EmployeeBusinessLayer();
                        eb.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if (e.Salary > 0)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
                        return View("CreateEmployee",vm);
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
	}
}