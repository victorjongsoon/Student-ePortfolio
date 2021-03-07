using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAssignment_2019_P03_Team06.DAL;
using WebAssignment_2019_P03_Team06.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAssignment_2019_P03_Team06.Controllers
{
    public class HomeController : Controller
    {


        private IConfiguration Configuration { get; set; }

        private LecturerDAL lecturerContext = new LecturerDAL();
        private StudentDAL studentContext = new StudentDAL();
        private HomeDAL homeContext = new HomeDAL();

        // GET: /<controller>/
        public IActionResult Index(IFormCollection formData)
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
       
            //in case of the need to return to Create.cshtml view   
         
            if (ModelState.IsValid)
            {
                //Add staff record to database       
                student.StudentId = studentContext.Add(student);
                //Redirect user to Staff/Index view                 
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view       
                //to display error message              
                return View(student);
            }
        }

        // GET: Student/Create         
        public ActionResult Create()
        {       
            return View();
        }

        // GET: LecturerCreate
        public ActionResult LecturerCreate()
        {
            
            return View();
        }

        // POST: Home/LecturerCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LecturerCreate(Lecturer lecturer)
        {
            //Get country list for drop-down list 
            //in case of the need to return to Create.cshtml view 
            //ViewData["CountryList"] = GetCountries();

            

            if (ModelState.IsValid)
            {
                //Add staff record to database 
                lecturer.LecturerId = lecturerContext.Add(lecturer);
                //Redirect user to Staff/Index view 
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view 
                //to display error message 
                return View(lecturer);
            }

        }
        
        public ActionResult Login(IFormCollection formData)
        {
            
            string loginID = formData["txtLoginID"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();
            string username;
            
            List<Home> homeList = homeContext.LogIn(loginID, password);
            List<Lecturer> LecturerList = lecturerContext.GetAllLecturer();
            List<Student> studentList = studentContext.GetAllStudent();

            if (homeList.Count > 0)
            {
                foreach (Home i in homeList)
                {
                    if (i.NameS != null)
                    {
                        username = i.NameS;

                        foreach (Student l in studentList)
                        {
                            if (loginID == l.Email.ToString().ToLower())
                            {
                                HttpContext.Session.SetInt32("studentId", l.StudentId);
                            }
                        }

                        HttpContext.Session.SetString("LoginID", loginID);
                        HttpContext.Session.SetString("Role", "Student");
                        DateTime Date = DateTime.Now;

                         HttpContext.Session.SetString("DateLogin", Date.ToString());
                        return RedirectToAction("StudentMain");
                    }

                    else if (i.NameL != null)
                            
                    {
                        username = i.NameL;
                        
                        
                        foreach (Lecturer l in LecturerList)
                        {
                            if (loginID == l.Email.ToString().ToLower())
                            {
                                HttpContext.Session.SetInt32("LecturerId", l.LecturerId);
                            }
                        }


                        
                        HttpContext.Session.SetString("LoginID", loginID);
                        HttpContext.Session.SetString("Role", "Lecturer");
                        
                        DateTime Date = DateTime.Now;
                        HttpContext.Session.SetString("DateLogin", Date.ToString());
                        return RedirectToAction("LecturerMain");
                    }
                }                                  
                return RedirectToAction();
            }

            else
            {
                // Store an error message in TempData for display at the index view     
                TempData["Message"] = "Invalid Login Credentials!";
                // Redirect user back to the index view through an action             
                return RedirectToAction("Index");
            }
        }

       
     
        //[HttpPost]
        //public ActionResult StudentLogin(IFormCollection formData, string email, SqlConnection conn)
        //{
            
          


        //    // Read inputs from textboxes          
        //    // Email address converted to lowercase          
        //    string loginID = formData["txtLoginID"].ToString().ToLower();
        //    string password = formData["txtPassword"].ToString();





        //    if (loginID == "s1234111@ap.edu.sg" && password == "p@55Student")
        //    {
        //        // Store Login ID in session with the key as “LoginID”     
        //        HttpContext.Session.SetString("LoginID", loginID);

        //        //Store user role “Student” in session with the key as “Role”         
        //        HttpContext.Session.SetString("Role", "Student");

        //        DateTime Date = DateTime.Now;

        //        HttpContext.Session.SetString("DateLogin", Date.ToString());

        //        // Redirect user to the "StudentMain" view through an action            
        //        return RedirectToAction("StudentMain");
        //    }


        //    else
        //    {
        //        // Store an error message in TempData for display at the index view     
        //        TempData["Message"] = "Invalid Login Credentials!";
        //        // Redirect user back to the index view through an action             
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public ActionResult LecturerLogin(IFormCollection formData)
        //{
        //    // Read inputs from textboxes          
        //    // Email address converted to lowercase          
        //    string loginID = formData["txtLoginID"].ToString().ToLower();
        //    string password = formData["txtPassword"].ToString();

        //    if (loginID == "lecturer@gmail.com" && password == "p@55Lecturer")
        //    {
        //        // Store Login ID in session with the key as “LoginID”     
        //        HttpContext.Session.SetString("LoginID", loginID);

        //        // Store user role “Student” in session with the key as “Role”         
        //        HttpContext.Session.SetString("Role", "Lecturer");

        //        DateTime Date = DateTime.Now;

        //        HttpContext.Session.SetString("DateLogin", Date.ToString());

        //        // Redirect user to the "LecturerMain" view through an action            
        //        return RedirectToAction("LecturerMain");
        //    }

        //    else
        //    {
        //        // Store an error message in TempData for display at the index view     
        //        TempData["Message"] = "Invalid Login Credentials!";
        //        // Redirect user back to the index view through an action             
        //        return RedirectToAction("Index");
        //    }
        //}

        public ActionResult StudentMain()
        {
            //Stop accessing the action if not logged in             
             //or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Student"))
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        public ActionResult LecturerMain()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Lecturer"))
            {
                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        


        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state             
            HttpContext.Session.Clear();
            // Call the Index action of Home controller             
            return RedirectToAction("Index");
        }
    }
}
