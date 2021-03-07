using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class ProjectViewModel
    {

        /*Project Table*/

        [Required]
        [Display(Name = "ID")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Please give your project a title!")]
        [StringLength(255, MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(3000)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Descipition { get; set; }

        [StringLength(255)]
        [DataType(DataType.Upload)]
        [Display(Name = "Project Poster")]
        public string ProjectPoster { get; set; }

        [StringLength(255)]
        [DataType(DataType.Url)]
        [Display(Name = "Project Link (URL)")]
        public string ProjectURL { get; set; }

        /*ProjectMember Table*/

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Project leader")]
        public string StudentLeaderName { get; set; }

        /*Student Table*/

        [Required(ErrorMessage = "Please give your Student!")]
        public int LeaderStudentID { get; set; }

        [Display(Name = "Add project member")]
        public int StudentIDs { get; set; }

        public int MemberStudentID { get; set; }

        public string MemberName { get; set; }

        public IFormFile FileToUpload { get; set; }

        public List<Student> StudentList { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<ProjectMember> ProjectMemberList { get; set; }
    }   
}
