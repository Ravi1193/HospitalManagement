using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Viewmodels
{
    public class AccountViewModel
    {
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public bool Checkbox { get; set; }
    }
}