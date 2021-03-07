using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class SuggestionViewModel
    {
        [Display(Name = "Suggestion ID")]
        public int SuggestionId { get; set; }


        [Display(Name = "Lecturer ID")]
        public int LecturerId { get; set; }

        [Display(Name = "Student ID")]
        public int StudentId { get; set; }


        [Display(Name = "Description")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Please write feedback!")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        // Validation Annotation for email address format 
        // Custom Validation Attribute for checking email address exists 
        [ValidateEmailExists(ErrorMessage = "Email address already exists!")]
        public string Email { get; set; }
    }
}
