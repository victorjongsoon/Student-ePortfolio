using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class Project
    {
        [Required]
        [Display(Name = "ID")]
        public int ProjectId { get; set; }

        //[Required(ErrorMessage = "Please give your project a title!")]
        [ValidateTitle(ErrorMessage = "Enter a title")]     
        [StringLength(255, MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(3000)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [StringLength(255)]
        [DataType(DataType.Upload)]
        [Display(Name = "Project Poster")]
        public string ProjectPoster { get; set; }

        [StringLength(255)]
        [DataType(DataType.Url)]
        [Display(Name = "Project Link (URL)")]
        public string ProjectURL { get; set; }
    }
}
