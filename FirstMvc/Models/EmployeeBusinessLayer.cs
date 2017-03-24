using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstMvc.Data_Access_Layer;

namespace FirstMvc.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            //List<Employee> employees = new List<Employee>();
            //Employee emp = new Employee();
            //emp.FirstName = "johnson";
            //emp.LastName = " fernandes";
            //emp.Salary = 14000;
            //employees.Add(emp);
     
            //emp = new Employee();
            //emp.FirstName = "michael";
            //emp.LastName = "jackson";
            //emp.Salary = 16000;
            //employees.Add(emp);
     
            //emp = new Employee();
            //emp.FirstName = "robert";
            //emp.LastName = " pattinson";
            //emp.Salary = 20000;
            //employees.Add(emp);
     
            //return employees;
            SalesERPDAL saleDal = new SalesERPDAL();
            return saleDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;

        }

        public bool IsValidUser(UserDetails u)
        {
            return (u.UserName == "admin" && u.Password == "admin");
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "admin" && u.Password == "admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "user" && u.Password == "user")
            {
                return UserStatus.AuthentucatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
    }

}