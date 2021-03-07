using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class Lecturer
    {

        [Display(Name = "Lecturer ID")]
        public int LecturerId { get; set; }

        //Lecturer Name
        [Required]
        [Display(Name = "Lecturer Name")]
        public string LecturerName { get; set; }

        //Email
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress] // Validation Annotation for email address format
        // Custom Validation Attribute for checking email address exists
        [LecturerValidEmail(ErrorMessage = "Email address already exists!")]
        public string Email { get; set; }

        [Display(Name = "Lecturer Password")]
        public string LecturerPassword { get; set; }

        [Display(Name = "Old Lecturer Password")]
        public string oldLecturerPassword { get; set; }

        [Display(Name = "New Password")]
        public string NewLecturerPassword { get; set; }

        [Display(Name = "Re-enter Password")]
        public string reNewLecturerPassword { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }


    }
}
