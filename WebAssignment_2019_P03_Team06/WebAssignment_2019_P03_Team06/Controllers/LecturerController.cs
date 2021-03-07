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

namespace WebAssignment_2019_P03_Team06.Controllers
{
    public class LecturerController : Controller
    {

        private LecturerDAL lecturerContext = new LecturerDAL();
        private StudentProfileDAL studentProfileContext = new StudentProfileDAL();

        public IActionResult Index()
        {
            // Stop accessing the action if not logged in  
            // or account not in the "Staff" role         
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Lecturer"))
            {
                return RedirectToAction("Index", "Home");
            }

            List<Lecturer> lecturerList = lecturerContext.GetAllLecturer();
            return View(lecturerList);
        }

        



        //Get Student
        public IActionResult StudentProfile()
        {
            
            List<StudentProfile> studentList = lecturerContext.GetAllStudent();
            List<StudentProfile> stdList = new List<StudentProfile>();
            foreach (StudentProfile i in studentList)
            {
                if (HttpContext.Session.GetInt32("LecturerId") == i.MentorId)
                {
                    stdList.Add(i);
                }

            }

            return View(stdList);
        }

        //Get Lecturer
        public IActionResult GetAllLecturer()
        {

            List<Lecturer> LecturerList = lecturerContext.GetAllLecturer();

            
            foreach (Lecturer i in LecturerList)
            {
                if ((HttpContext.Session.GetString("LoginID") == i.Email))
                {

                    HttpContext.Session.SetInt32("LecturerId", i.LecturerId);

                }

            }

            return View(LecturerList);
        }

        //Get Sent suggestions
        public IActionResult Sent()
        {
            // Stop accessing the action if not logged in 
            // or account not in the "Staff" role 
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Lecturer"))
            {
                return RedirectToAction("Index", "Home");
            }

            List<SentSuggestion> SentSuggestionList = lecturerContext.GetAllSentSuggestion();
            List<SentSuggestion> ssList = new List<SentSuggestion>();
            foreach (SentSuggestion i in SentSuggestionList)
            {
                if (i.LecturerId == HttpContext.Session.GetInt32("LecturerId"))
                {
                    ssList.Add(i);
                }
            }

           
            { 
            return View(ssList);
            }
        }

        // Change password        
        public ActionResult ChangePassword()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
           (HttpContext.Session.GetString("Role") != "Lecturer"))
            {
                return RedirectToAction("Index", "Home");
            }

            

            return View();
        }

        // GET: Compose stdId
        public ActionResult Compose(int id)
        {
            // Stop accessing the action if not logged in 
            // or account not in the "Staff" role 
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Lecturer"))
            {
                return RedirectToAction("Index", "Home");
            }
            StudentProfile studentId = lecturerContext.GetDetails(id);
            StudentProfile Studentid = lecturerContext.GetDetails(id);
            ComposeSuggestion studentIdVM = MapTostudentIdVM(Studentid);

            int stdId = Convert.ToInt32(Studentid.StudentId);
            HttpContext.Session.SetInt32("stdId", stdId);

            return View(studentIdVM);
        }


        public ComposeSuggestion MapTostudentIdVM(StudentProfile id)
        {
            //Console.WriteLine("LOOK HERE>>>>>>" + id.StudentId);
            
            ComposeSuggestion studentIdVM = new ComposeSuggestion
            {
                ToStudentId = id.StudentId,

                LecturerId = id.MentorId

            };
               
            //ComposeSuggestion lecturerIdVM = new ComposeSuggestion
            //{
            //   
            //};

            //Console.WriteLine("LOOK HERE AGAIN>>>>>>" + id.LecturerId);
            return (studentIdVM);
        }

        //// GET: Compose lecId
        //public ActionResult Compose(int id)
        //{
        //    // Stop accessing the action if not logged in 
        //    // or account not in the "Staff" role 
        //    if ((HttpContext.Session.GetString("Role") == null) ||
        //    (HttpContext.Session.GetString("Role") != "Lecturer"))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    //StudentProfile studentId = lecturerContext.GetDetails(id);
        //    StudentProfile studentid = lecturerContext.GetDetails(id);
        //    ComposeSuggestion studentIdVM = MapTostudentIdVM(studentid);
        //    return View(studentIdVM);
        //}
        //public ComposeSuggestion MaplectureIdVM(Lecturer id)
        //{
        //    Console.WriteLine("LOOK HERE>>>>>>" + id.LecturerId);
        //    ComposeSuggestion lecturerIdVM = new ComposeSuggestion
        //    {

        //        LecturerId = id.LecturerId


        //    };
        //    Console.WriteLine("LOOK HERE AGAIN>>>>>>" + id.LecturerId);

        //    return lecturerIdVM;
        //}

        //send compose
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compose(ComposeSuggestion composeSuggestion)
        {

            composeSuggestion.Date = DateTime.Now;
            List<Lecturer> lecList = lecturerContext.GetAllLecturer();
            List<StudentProfile> stdList = lecturerContext.GetAllStudent();
            foreach (Lecturer i in lecList)
            {

                if (i.LecturerId == HttpContext.Session.GetInt32("LecturerId"))
                {
                    Console.WriteLine("LOOK HERE lec>>>>>>" + "LECID collected");

                    composeSuggestion.LecturerId = i.LecturerId;

                }

            }

            foreach (StudentProfile i in stdList)
            {
                //Console.WriteLine("LOOK HERE std>>>>>>" + i.StudentId);

                if (i.StudentId == HttpContext.Session.GetInt32("stdId"))
                {
                    Console.WriteLine("LOOK HERE std>>>>>>" + "STDID collected");

                    composeSuggestion.ToStudentId = i.StudentId;
                }
            }

            if (composeSuggestion.Description != null)
            { 
            //Console.WriteLine("LOOK HERE lec>>>>>>" + composeSuggestion.LecturerId);
            //Console.WriteLine("LOOK HERE std>>>>>>" + composeSuggestion.ToStudentId);

            composeSuggestion.SuggestionId = lecturerContext.Send(composeSuggestion);
            //Redirect user 
            return RedirectToAction("StudentProfile");
            }
            else
            {
                ViewData["Message"] = "Please write your feedback in the description box";

                StudentProfile studentId = lecturerContext.GetDetails(composeSuggestion.LecturerId);
                StudentProfile Studentid = lecturerContext.GetDetails(composeSuggestion.ToStudentId);
                ComposeSuggestion studentIdVM = MapTostudentIdVM(Studentid);

                return View(studentIdVM);
            }

            
        
         }

        //Change password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Lecturer lecturer)
        {




            List<Lecturer> lecList = lecturerContext.GetAllLecturer();
            foreach (Lecturer i in lecList)
            {
                if (i.LecturerId == HttpContext.Session.GetInt32("LecturerId"))
                {
                    lecturer.LecturerId = i.LecturerId;
                    lecturer.LecturerPassword = i.LecturerPassword;
                }
            }

            if (lecturer.reNewLecturerPassword != null &&
               lecturer.NewLecturerPassword != null &&
               lecturer.oldLecturerPassword != null)
            {
                if (lecturer.oldLecturerPassword == lecturer.LecturerPassword)
                {
                    if (lecturer.reNewLecturerPassword == lecturer.NewLecturerPassword)
                    {
                        if (lecturer.NewLecturerPassword != lecturer.oldLecturerPassword)
                        {

                            //bool havedigit = false;
                            bool havestringlength = false;
                            bool digitpresent = false;


                            int stringlength = 0;

                            char[] charNewLecturerPassword = lecturer.NewLecturerPassword.ToCharArray();

                            digitpresent = lecturer.NewLecturerPassword.Any(char.IsDigit);

                            foreach (char c in charNewLecturerPassword)
                            {
                                //bool checkresult = false;
                                bool checkdigit = false;


                                checkdigit = char.IsDigit(c);
                                stringlength += 1;
                                //if (checkresult == true)
                                //{
                                //    havedigit = true;
                                //}
                                if (stringlength >= 8)
                                {
                                    havestringlength = true;
                                }
                            }



                            if (digitpresent == true && havestringlength == true)
                            {
                                lecturerContext.ChangePass(lecturer);
                                ViewData["reply"] = "Password changed succussfully ";
                            }
                            else
                            {
                                ViewData["reply"] = "New Passwords should have at least 8 characters including at least a digit ";
                            }


                        }
                        else
                        {
                            ViewData["reply"] = "New password is the same as current password";
                        }
                    }
                    else
                    {
                        ViewData["reply"] = "New Passwords does not match each other ";
                    }
                }
                else
                {
                    ViewData["reply"] = "old password does not match Current password  ";
                }
            }
            else
            {
                ViewData["reply"] = "Fill in empty fields";
            }

            return View();
        }



    }
} 