using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Viewmodels
{
    public class HomeViewModel
    {
        public int Id  { get; set; }
        [Required]
        public string  Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string  Username { get; set; }

        [Required]
        public string  Password { get; set; }

        [Required]
        public int? AcocuntType { get; set; }

        public string  Status{ get; set; }

    }
}