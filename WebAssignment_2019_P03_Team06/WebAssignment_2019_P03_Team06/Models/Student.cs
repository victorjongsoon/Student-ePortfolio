using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class Student
    {
        [Display(Name = "ID")]
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }



        
        public string Name { get; set; }

        public char Gender { get; set; }


        [Display(Name = "Email")]
        [EmailAddress]
        // Validation Annotation for email address format 
        // Custom Validation Attribute for checking email address exists 
        [ValidateEmailExists(ErrorMessage = "Email address already exists!")] 
        public string Email { get; set; }

        public string Password { get; set; }

        public string Photo { get; set; }

        [Display(Name = "Mentor ID")]
        public int MentorId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        
        [Display(Name = "Achievement")]
        public string Achievement { get; set; }

        [Display(Name = "External Link")]
        public string externalLink { get; set; }


        [Display(Name ="Course")]
        public string Course { get; set; }

        public IFormFile FileToUpload { get; set; }

    }
}
