using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Viewmodels;
namespace HospitalManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {

            AccountRepository ac = new AccountRepository();
            AccountViewModel acc = new AccountViewModel();
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.AccountType = new SelectList(ac.Accounttypes(), "id", "accounttype");
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel viewmodel,string returnUrl)
        {
  
            AccountRepository repository = new AccountRepository();
            if (ModelState.IsValid)
            {
                Session["Username"] = viewmodel.Username;
                var obj = repository.Authenticate(viewmodel);
                if (obj ==true)
                {

                    HttpContext.Session.Add("SessionUser", obj);
                    FormsAuthentication.SetAuthCookie(viewmodel.Username, false);
                    if (viewmodel.Checkbox)
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                    }
                   if(returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                   
                    ModelState.AddModelError("username", "Please enter valid credentials.");
                }
            }
            ViewBag.AccountType = new SelectList(repository.Accounttypes(), "id", "accounttype");
                return View(viewmodel);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            cookie.HttpOnly = true;
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Login");

        }
    }
}