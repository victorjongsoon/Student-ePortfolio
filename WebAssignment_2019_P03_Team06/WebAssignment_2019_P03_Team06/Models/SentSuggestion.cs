using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WebAssignment_2019_P03_Team06.Models
{
    public class SentSuggestion
    {
        [Display(Name = "Suggestion ID")]
        public int SuggestionId { get; set; }

        [Display(Name = "Lecturer ID")]
        public int LecturerId { get; set; }

        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateCreated { get; set; }


    }
}
