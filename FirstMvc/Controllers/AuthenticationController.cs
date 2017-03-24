using FirstMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FirstMvc.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if(!ModelState.IsValid)
            {
                return View("Login");
            }
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            UserStatus state = bal.GetUserValidity(u);
            bool IsAdmin = false;

            switch (state)
            {
                case UserStatus.AuthenticatedAdmin:
                    IsAdmin = true;
                    break;
                case UserStatus.AuthentucatedUser:
                    IsAdmin = false;
                    break;
                default:
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
            }
            FormsAuthentication.SetAuthCookie(u.UserName, false);
            Session["IsAdmin"] = IsAdmin;
            return RedirectToAction("Index", "Employee");

            //if (bal.IsValidUser(u))
            //{
            //    FormsAuthentication.SetAuthCookie(u.UserName, false);
            //    return RedirectToAction("Index", "Employee");
            //}
            //else
            //{
            //    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
            //    return View("Login");
            //}
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
	}
}