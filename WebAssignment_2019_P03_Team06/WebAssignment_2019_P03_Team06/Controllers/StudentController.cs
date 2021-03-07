using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAssignment_2019_P03_Team06.DAL; 
using WebAssignment_2019_P03_Team06.Models;
using WebAssignment_2019_P03_Team06.Models.WebAssignment_2019_P03_Team06.Models;
using System.Drawing;


namespace WebAssignment_2019_P03_Team06.Controllers
{
    public class StudentController : Controller
    {
        private StudentDAL studentContext = new StudentDAL();
        

        public IActionResult Index()
        {
            // Stop accessing the action if not logged in            
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            List<Student> studentList = studentContext.GetAllStudent();

            

            return View(studentList);
        }

        public StudentViewModel MapToStudentVM(Student student)
        {
            string currentLoginId = HttpContext.Session.GetString("LoginID");
            
            
            StudentViewModel studentVM = new StudentViewModel
            {
                
                StudentId = student.StudentId,
                Name = student.Name,
                Email = currentLoginId,
                Photo = student.Photo
            };
            return studentVM;
        }


        public ActionResult UploadPhoto(string currentLoginId)
        {
            currentLoginId = HttpContext.Session.GetString("LoginID");
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }
            Student student = studentContext.GetDetails(currentLoginId);
            StudentViewModel studentVM = MapToStudentVM(student);
            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPhoto(StudentViewModel studentVM)
        {


            string currentLoginId = HttpContext.Session.GetString("LoginID");

            if (studentVM.FileToUpload != null &&
                studentVM.FileToUpload.Length > 0)
            {
               
            try
                {
                    // Find the filename extension of the file to be uploaded.      
                    //string fileExt = Path.GetExtension(
                    //    studentVM.FileToUpload.FileName);

                    string uploadedFile = studentVM.FileToUpload.FileName;
            
                    // Get the complete path to the images folder in server  
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images", uploadedFile);

                    // Upload the file to server          
                    using (var fileSteam = new FileStream(
                        savePath, FileMode.Create))
                    { await studentVM.FileToUpload.CopyToAsync(fileSteam); }

                    studentVM.Photo = uploadedFile;

                    studentContext.UpdatePhoto(currentLoginId,uploadedFile);

                    ViewData["Message"] = "File uploaded successfully.";
                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied   
                    ViewData["Message"] = "File uploading fail!";
                }
                catch (Exception ex)
                //Other type of error       
                {
        
                ViewData["Message"] = ex.Message;
                }


        }

            else
            {
            
                ViewData["Message"] = "File uploading fail! Try Again!";
            }
            return View(studentVM);
        }



        public ActionResult Edit(int? id)
        {
            //// Stop accessing the action if not logged in     
            //// or account not in the "Student" role  
            //if ((HttpContext.Session.GetString("Role") == null) ||    
            //    (HttpContext.Session.GetString("Role") != "Student"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //if (id == null)
            ////Query string parameter not provided  
            //{
            //    //Return to listing page, not allowed to edit     
            //    return RedirectToAction("Index");
            //}

            //Student student = studentContext.GetDetails(id.Value);

            //if (student == null)
            //{
            //    //Return to listing page, not allowed to edit       
            //    return RedirectToAction("Index");
            //} 
            //    return View(student);

            return View();

        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {


            string currentLoginId = HttpContext.Session.GetString("LoginID");
            //Get branch list for drop-down list          
            //in case of the need to return to Edit.cshtml view  

            if (ModelState.IsValid)
            {
                //Update staff record to database    
                studentContext.Update(student, currentLoginId);
                return RedirectToAction("Index");
            }
            //Input validation fails, return to the view    
            //to display error message    
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(Suggestion suggestion)
        {

  
            
            //Get branch list for drop-down list          
            //in case of the need to return to Edit.cshtml view  

            if (ModelState.IsValid)
            {
                //Update staff record to database    
                studentContext.UpdateStatus(suggestion);
                return RedirectToAction("Index");
            }
            //Input validation fails, return to the view    
            //to display error message    
            return View(suggestion);
        }




        public IActionResult Feedback()
        {
            List<Suggestion> suggestionList = studentContext.GetAllSentSuggestion();
            return View(suggestionList);
        }



        public IActionResult UpdateStatus(int? id)
        {
            
          
            //currentLoginId = HttpContext.Session.GetString("LoginID");
            // Stop accessing the action if not logged in      
            // or account not in the "Student" role      
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");
            }

            Suggestion suggestion = studentContext.GetSuggestion(id.Value);

            return View(suggestion);


        }

        public SuggestionViewModel MapToSuggestionVM(Suggestion suggestion)
        {
            string currentLoginId = HttpContext.Session.GetString("LoginID");


            SuggestionViewModel suggestionVM = new SuggestionViewModel
            {

                StudentId = suggestion.StudentId,
                SuggestionId = suggestion.SuggestionId,
                Email = currentLoginId,

            };
            return suggestionVM;
        }


        public IActionResult SkillSet()
        {
            string currentLoginId = HttpContext.Session.GetString("LoginID");

            List<SkillSet> SkillSetList = studentContext.GetAllSkillSet(currentLoginId);
           
            return View(SkillSetList);
        }




    }
}