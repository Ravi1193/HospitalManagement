using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Viewmodels;
namespace HospitalManagementSystem.Models
{
    public class HomeRepository
    {
        DataBaseDataContext db = new DataBaseDataContext();
        public List<sp_getAllDoctorsResult> getAllDoctors()
        {
            List<sp_getAllDoctorsResult> doctor= db.sp_getAllDoctors().ToList();
            return doctor;
        }

        public List<sp_getAllPatientResult> getAllPatients()
        {
            List<sp_getAllPatientResult> patient = db.sp_getAllPatient().ToList();
            return patient;
        }

        public List<sp_getAllNurseResult> getAllNurses()
        {
            List<sp_getAllNurseResult> Nurse = db.sp_getAllNurse().ToList();
            return Nurse;
        }

        public bool saveUsers(HomeViewModel hr)
        {
            try
            {
                var saveUser = db.sp_saveUsers(hr.Firstname, hr.Lastname, hr.AcocuntType, hr.Username, hr.Password, hr.Email, hr.Status);
                //user us = new user();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<sp_getUserRolesResult> userRoles(string userName)
        {
                List<sp_getUserRolesResult> userRole = db.sp_getUserRoles(userName).ToList();
                return userRole;
         }

        public HomeViewModel getUserById(int? id)
        {
            return db.users.Where(x => x.id == id).Select(x => new HomeViewModel
            {
                Id =x.id,
                Firstname = x.firstname,
                Lastname = x.lastname,
                Username=x.username,
                Password=x.password,
                Email=x.email,
                AcocuntType=x.accounttype

            }).SingleOrDefault();
        }

        public bool editUser(HomeViewModel model)
        {
            var editUser = db.users.Where(x => x.id == model.Id).FirstOrDefault();
            editUser.id = model.Id;
            editUser.firstname = model.Firstname;
            editUser.lastname = model.Lastname;
            editUser.username = model.Username;
            editUser.password = model.Password;
            editUser.email = model.Email;
            editUser.accounttype = model.AcocuntType;
            db.SubmitChanges();
            return true;
        }
    }
}