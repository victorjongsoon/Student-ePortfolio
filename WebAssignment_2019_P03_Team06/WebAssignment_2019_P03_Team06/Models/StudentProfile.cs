using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class StudentProfile
    {
        [Display(Name = "ID")]
        public int StudentId { get; set; }

        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        // Validation Annotation for email address format 
        // Custom Validation Attribute for checking email address exists 
        public string Email { get; set; }

        public string Password { get; set; }

        
        public string Photo { get; set; }

        [Display(Name = "External Link")]
        public string Link { get; set; }


        [Display(Name = "Mentor ID")]
        public int MentorId { get; set; }

        //Lecturer

        public int LecturerId { get; set; }
    }
}
