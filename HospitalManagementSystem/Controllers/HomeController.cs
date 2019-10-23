using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Viewmodels;
namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        HomeRepository hr = new HomeRepository();
        AccountRepository ac = new AccountRepository();
        public ActionResult Index()
        {
            try
            {

                ViewBag.countOfDoctor = hr.getAllDoctors().Count();
                ViewBag.countOfPatient = hr.getAllPatients().Count();
                ViewBag.countOfNurse = hr.getAllNurses().Count();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        public ActionResult doctors()
        {
            ViewBag.getAllDoctors = hr.getAllDoctors().ToList();
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult addDoctor()
        {
            ViewBag.AccountType = new SelectList(ac.Accounttypes(), "id", "accounttype");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult addDoctor(HomeViewModel viewModel)
         {
            try
            {
                ViewBag.AccountType = new SelectList(ac.Accounttypes(), "id", "accounttype");
                if (ModelState.IsValid)
                {
                    viewModel.Status = "N";
                    var saveUser = hr.saveUsers(viewModel);
                    if(saveUser)
                    {
                        TempData["addUser"] = "User Added Succesfully";
                        return PartialView("addDoctor");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Somthing went Wrong");
                       
                    }
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            

        }
        public ActionResult Department()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                ViewBag.AccountType = new SelectList(ac.Accounttypes(), "id", "accounttype");
                HomeRepository hr = new HomeRepository();
                var model = hr.getUserById(id);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public ActionResult Edit(HomeViewModel hv)
        {
            try
            {
                ViewBag.AccountType = new SelectList(ac.Accounttypes(), "Id", "accounttype");
                if (ModelState.IsValid)
                {
                    HomeRepository hr = new HomeRepository();
                    var eUser = hr.editUser(hv);
                    if(eUser)
                    {
                        TempData["addUser"] = "User Updated Succesfully";
                        return PartialView("Edit");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something Went Wrong");
                    }
                }
                return View(hv);
            }catch(Exception ex)
            {
                return View("Error", ex);
            }
        }
        public ActionResult notFound()
        {
            return View("_CustomError");
        }
    }
}