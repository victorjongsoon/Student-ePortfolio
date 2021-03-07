using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class ProjectMember
    {
        //public List<Project> projectList { get; set; }
        //public List<Student> studentList { get; set; }
        //public List<Student> studentList { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
