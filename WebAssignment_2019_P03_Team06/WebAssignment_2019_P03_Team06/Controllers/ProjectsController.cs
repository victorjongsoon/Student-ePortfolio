using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAssignment_2019_P03_Team06.DAL;
using WebAssignment_2019_P03_Team06.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace WebAssignment_2019_P03_Team06.Controllers
{
    public class ProjectsController : Controller
    {
        //Connect to the database 
        private ProjectDAL projectContext = new ProjectDAL();
        private StudentDAL studentContext = new StudentDAL();
        private ProjectMemberDAL projectMemberContext = new ProjectMemberDAL();

        //Retreive all the project done by the sudent and show it in "Personal Project"
        // GET: Project
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in  
            // or account not in the "Staff" role         
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            //To retreive the current student id code --------------------
            string currentLoginId = HttpContext.Session.GetString("LoginID");
            Student student = studentContext.GetDetails(currentLoginId);
            int studentID = student.StudentId;
            HttpContext.Session.SetInt32("StudentID", student.StudentId);
            //--------------------------------------------------------------

            List<ProjectViewModel> projectLeaderList = projectContext.GetAllProject(studentID); //Here to get data 
            return View(projectLeaderList);
        }

        //Get the Create View 
        // GET: Project/Create
        public ActionResult Create()
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //Create the Project 
        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            var studentID = HttpContext.Session.GetInt32("StudentID"); // reteive student id from the session management 

            //Add project record to database 
            project.ProjectID = projectContext.Add(project, (int)studentID);
            return RedirectToAction("Index");

        }

        //Retreive the selected project to the Details.cshtml File 
        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            ProjectViewModel project = projectContext.GetDetails(id); 
            ProjectViewModel projectVM = MaptoProjectVM(project);
            return View(projectVM);
        }


        //Retreive the project data to "public ActionResult Details(int id)", projectVM
        public ProjectViewModel MaptoProjectVM(ProjectViewModel project)
        {

            string studentDetails ="";

            List<Student> studentList = studentContext.GetAllStudent();
            List<ProjectMember> projectMemberList = projectMemberContext.GetAllStudentMembers(project.ProjectID);
            foreach (Student eachstudent in studentList)
            {
                if (eachstudent.StudentId != project.StudentIDs )
                {
                    studentDetails = "Student ID: " + project.StudentIDs.ToString() + ". Name: " + project.StudentLeaderName;
                    break;
                }
            }

            ProjectViewModel projectVM = new ProjectViewModel
            {
                ProjectID = project.ProjectID,
                Title = project.Title,
                Descipition = project.Descipition,
                ProjectPoster = project.ProjectPoster,
                ProjectURL = project.ProjectURL,
                Role = project.Role,
                LeaderStudentID = project.LeaderStudentID,
                StudentLeaderName = project.StudentLeaderName,
                MemberName = studentDetails
            };

            ViewData["ProjectInformation"] = projectVM;

            return projectVM;
        }

        //Edit and add members to the project and projectmember database 
        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["StudentList"] = GetAllStudents(id);
            ProjectViewModel project = projectContext.GetDetails(id);
            return View(project);
        }

        //Edit and add members to the project and projectmember database 
        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel project)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["StudentList"] = GetAllStudents(project.ProjectID);

            projectContext.Update(project);

            //To add member into the ProjectMemberDAL 
            if (project.StudentIDs > 0)
            {
                projectMemberContext.Add(project.ProjectID, project.StudentIDs);
            }
        return RedirectToAction("Index");
        }

        private List<SelectListItem> GetAllStudents(int id)
        {

            string currentLoginId = HttpContext.Session.GetString("LoginID");
            Student student = studentContext.GetDetails(currentLoginId);
            int studentID = student.StudentId;
            HttpContext.Session.SetInt32("StudentID", student.StudentId);

            List<SelectListItem> students = new List<SelectListItem>();
            students.Add(
                new SelectListItem
                {
                    Value = "",
                    Text = "-- Select --"
                });

            List<Student> studentList = studentContext.GetAllStudent();
            List<ProjectMember> projectMembers = projectMemberContext.GetAllStudentMembers(id);
            foreach (Student eachStudent in studentList)
            {
                foreach (ProjectMember projectMember in projectMembers)
                {
                    if (eachStudent.StudentId != projectMember.StudentID)
                    {
                        if (projectMember.StudentID != studentID || eachStudent.StudentId != studentID)
                        {
                            students.Add(
                                new SelectListItem
                                {
                                    Value = eachStudent.StudentId.ToString(),
                                    Text = "Student ID: " + eachStudent.StudentId + ". Name: " + eachStudent.Name.ToString()
                                });
                            break;
                        }
                    }
                }
            }
            return students;
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            ProjectViewModel project = projectContext.GetDetails(id);
            return View (project);
        }

        
        // POST: Project/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProjectViewModel project)
        {
            try
            {
                projectContext.Delete(project);
                return RedirectToAction("Index");  
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UploadPhoto(int id)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            ProjectViewModel project = projectContext.GetDetails(id);
            ProjectViewModel projectVM = MaptoProjectVM(project);
            return View(projectVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPhoto(ProjectViewModel projectVM)
        {
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (projectVM.FileToUpload != null &&
                projectVM.FileToUpload.Length > 0)
            {
                try
                {
                    //Find the filename extension of the file to be uploaded.
                    string fileExt = Path.GetExtension(projectVM.FileToUpload.FileName);

                    //Rename the upload file with the staff's name.
                    string uploadedFile = projectVM.Title + fileExt;

                    //Get the complete path to the images folder in server 
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images", uploadedFile);

                    //Upload the file to server 
                    using (var fileSteam = new FileStream(
                        savePath, FileMode.Create))
                    {
                        await projectVM.FileToUpload.CopyToAsync(fileSteam);
                    }

                    projectContext.UploadPhoto(uploadedFile, projectVM.ProjectID);

                    projectVM.ProjectPoster = uploadedFile;
                    ViewData["Message"] = "File uploaded successfully.";

                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied 
                    ViewData["Message"] = "File uploading fail!";
                }
                catch (Exception ex) //Other type of error
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            return View(projectVM);
        }
    }
}