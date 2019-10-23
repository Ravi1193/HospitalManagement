using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Viewmodels;
namespace HospitalManagementSystem.Models
{
    public class AccountRepository
    {
        DataBaseDataContext db = new DataBaseDataContext();
        public List<Accounttype> Accounttypes()
        {
            List<Accounttype> at = db.Accounttypes.ToList();
            return at;
        }

        public bool Authenticate(AccountViewModel viewmodel)
        {
            var auth = db.users.Where(x => x.username == viewmodel.Username && x.password == viewmodel.Password && x.accounttype == Convert.ToInt32(viewmodel.AccountType)).FirstOrDefault();
            if (auth !=null)
            {
                return true;
            }
            return false;
        }

    }
}