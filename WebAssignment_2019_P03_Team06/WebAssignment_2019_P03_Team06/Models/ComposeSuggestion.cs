using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class ComposeSuggestion
    {

        [Display(Name = "Suggestion ID")]
        public int SuggestionId { get; set; }

        [Display(Name ="Lecturer ID")]
        public int LecturerId { get; set; }

        [Display(Name = "Student ID")]
        public int ToStudentId { get; set; }

        
        [Display(Name = "Description")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Please write feedback!")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        
        

    }
}
