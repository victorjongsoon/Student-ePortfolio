using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAssignment_2019_P03_Team06.DAL;
using WebAssignment_2019_P03_Team06.Models;

namespace WebAssignment_2019_P03_Team06.DAL
{
    public class StudentDAL
    {
        
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;

        //Constructor       
        public StudentDAL()
        {
            //Locate the appsettings.json file             
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            //Read ConnectionString from appsettings.json file             
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("StudentEportfolioConnectionString");

            //Instantiate a SqlConnection object with the              
            //Connection String read.               
            conn = new SqlConnection(strConn);
        }

        public Suggestion GetSuggestion(int suggestionId)
        {
            SqlCommand cmd = new SqlCommand
                ("SELECT * FROM Suggestion WHERE SuggestionID = @selectedSuggestionID", conn);
            //Define the parameter used in SQL statement, value for the    
            //parameter is retrieved from the method parameter “staffId”.    
            cmd.Parameters.AddWithValue("@selectedSuggestionID", suggestionId);
            //Instantiate a DataAdapter object, pass the SqlCommand 
            //object “cmd” as parameter.    
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataSet object “result"    
            DataSet result = new DataSet();
            //Open a database connection.   
            conn.Open();
            //Use DataAdapter to fetch data to a table "StaffDetails" in DataSet. 
            da.Fill(result, "SuggestionDetails");
            //Close the database connection   
            conn.Close();
            Suggestion suggestion = new Suggestion();

            if (result.Tables["SuggestionDetails"].Rows.Count > 0)
            {
                suggestion.SuggestionId = suggestionId;
                // Fill staff object with values from the DataSet  
                DataTable table = result.Tables["SuggestionDetails"];

                //if (!DBNull.Value.Equals(table.Rows[0]["SuggestionID"]))
                //    suggestion.SuggestionId = System.Convert.ToInt32(table.Rows[0]["SuggestionID"]);
                //if (!DBNull.Value.Equals(table.Rows[0]["LecturerID"]))
                //    suggestion.SuggestionId = System.Convert.ToInt32(table.Rows[0]["LecturerID"]);
                //if (!DBNull.Value.Equals(table.Rows[0]["StudentID"]))
                //    suggestion.SuggestionId = System.Convert.ToInt32(table.Rows[0]["StudentID"]);
               

      
                return suggestion;
                // No error occurs   
            }

            else
            {
                return null;
                // Record not found  
            }
        }


        public Student GetDetails(string studentEmail)
        {     //Instantiate a SqlCommand object, supply it with a SELECT SQL 
              //statement which retrieves all attributes of a staff record.  
            SqlCommand cmd = new SqlCommand
                ("SELECT * FROM Student WHERE EmailAddr = @selectedEmailAddr", conn);
            //Define the parameter used in SQL statement, value for the    
            //parameter is retrieved from the method parameter “staffId”.    
            cmd.Parameters.AddWithValue("@selectedEmailAddr", studentEmail);
            //Instantiate a DataAdapter object, pass the SqlCommand 
            //object “cmd” as parameter.    
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataSet object “result"    
            DataSet result = new DataSet();
            //Open a database connection.   
            conn.Open();
            //Use DataAdapter to fetch data to a table "StaffDetails" in DataSet. 
            da.Fill(result, "StudentDetails");
            //Close the database connection   
            conn.Close();
            Student student = new Student();

            if (result.Tables["StudentDetails"].Rows.Count > 0)
            {
                student.Email = studentEmail;
                // Fill staff object with values from the DataSet  
                DataTable table = result.Tables["StudentDetails"];

                if (!DBNull.Value.Equals(table.Rows[0]["StudentID"]))
                    student.StudentId = System.Convert.ToInt32(table.Rows[0]["StudentID"]);

                if (!DBNull.Value.Equals(table.Rows[0]["Photo"]))
                    student.Photo = table.Rows[0]["Photo"].ToString();

                if (!DBNull.Value.Equals(table.Rows[0]["Name"]))
                    student.Name = table.Rows[0]["Name"].ToString();
            
                if (!DBNull.Value.Equals(table.Rows[0]["EmailAddr"]))
                    student.Email = table.Rows[0]["EmailAddr"].ToString();

                if (!DBNull.Value.Equals(table.Rows[0]["Description"]))
                    student.Description = table.Rows[0]["Description"].ToString();

                if (!DBNull.Value.Equals(table.Rows[0]["Achievement"]))
                    student.Achievement = table.Rows[0]["Achievement"].ToString();



                return student;
                // No error occurs   
            }
            else
            {
                return null;
                // Record not found  
            }
        }

        public List<SkillSet> GetAllSkillSet(string emailaddr)
        {
              
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM StudentSkillSet INNER JOIN SkillSet ON StudentSkillSet.SkillSetID = SkillSet.SkillSetID INNER JOIN Student ON Student.StudentID = StudentSkillSet.StudentID WHERE EmailAddr = @selectedEmailAddr ", conn);

            cmd.Parameters.AddWithValue("@selectedEmailAddr", emailaddr);


            //Instantiate a DataAdapter object and pass the              
            //SqlCommand object created as parameter.             
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataSet object to contain records get from database             
            DataSet result = new DataSet();

            //Open a database connection             
            conn.Open();
            //Use DataAdapter, which execute the SELECT SQL through its                                  
            //SqlCommand object to fetch data to a table "Description"             
            //in DataSet "result".           
            da.Fill(result, "SkillSetDetails");
            //Close the database connection                   
            conn.Close();


            //Transferring rows of data in DataSet’s table to “Student” objects   
            List<SkillSet> SkillSetList = new List<SkillSet>();
            foreach (DataRow row in result.Tables["SkillSetDetails"].Rows)
            {

                SkillSetList.Add(new SkillSet
                {                  
                    SkillSetName = row["SkillSetName"].ToString(),             
                    SkillSetId = Convert.ToInt32(row["SkillSetID"]),
                    StudentId = Convert.ToInt32(row["StudentID"])
                });
            }
            return SkillSetList;
        }


        public List<Student> GetAllStudent()
        {
            //Instantiate a SqlCommand object, supply it with a              
            //SELECT SQL statement that operates against the database,             
            //and the connection object for connecting to the database.             
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Student ORDER BY StudentID", conn);

   

            //Instantiate a DataAdapter object and pass the              
            //SqlCommand object created as parameter.             
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataSet object to contain records get from database             
            DataSet result = new DataSet();

            //Open a database connection             
            conn.Open();
            //Use DataAdapter, which execute the SELECT SQL through its                                  
            //SqlCommand object to fetch data to a table "Description"             
            //in DataSet "result".           
            da.Fill(result, "StudentDetails");
            //Close the database connection                   
            conn.Close();


            //Transferring rows of data in DataSet’s table to “Student” objects   
            List<Student> studentList = new List<Student>();
            foreach (DataRow row in result.Tables["StudentDetails"].Rows)
            {

                studentList.Add(new Student {
                    Achievement = row["Achievement"].ToString(),
                    externalLink = row["ExternalLink"].ToString(),
                    Description = row["Description"].ToString(),
                    StudentId = Convert.ToInt32(row["StudentID"]),
                    Photo = row["Photo"].ToString(),
                    Name = row["Name"].ToString(),
                    Email = row["EmailAddr"].ToString(),
                    Password = row["Password"].ToString(),
                    Course = row["Course"].ToString(),
                    MentorId = Convert.ToInt32(row["MentorID"])
                });
            }
            return studentList;
        }


        public int UpdatePhoto(string currentLoginId, string photo)
        {
            //Instantiate a SqlCommand object, supply it with SQL statement UPDATE     
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
                    ("UPDATE Student SET Photo=@Photo " +
                     "WHERE EmailAddr=@selectedStudentID", conn);
            //Define the parameters used in SQL statement, value for each parameter  
            //is retrieved from the respective property of “student” object.  
            cmd.Parameters.AddWithValue("@Photo", photo);
            cmd.Parameters.AddWithValue("@selectedStudentID", currentLoginId);
            //cmd.Parameters.AddWithValue("@selectedEmail", student);

            //Open a database connection.   
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE   
            int count = cmd.ExecuteNonQuery();
            //Close the database connection.    
            conn.Close();
            return count;
        }

        public int Update(Student student, string currentLoginId)
        {
            //Instantiate a SqlCommand object, supply it with SQL statement UPDATE     
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
                    ("UPDATE Student SET Description=@Description, Achievement=@Achievement " +
                     "WHERE EmailAddr=@selectedStudentID", conn);
            //Define the parameters used in SQL statement, value for each parameter  
            //is retrieved from the respective property of “student” object.  
            cmd.Parameters.AddWithValue("@Description", student.Description);
            cmd.Parameters.AddWithValue("@Achievement", student.Achievement);
          
            //cmd.Parameters.AddWithValue("@photo", student.Photo);
            cmd.Parameters.AddWithValue("@selectedStudentID", currentLoginId);
            //cmd.Parameters.AddWithValue("@selectedEmail", student);
            
            //Open a database connection.   
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE   
            int count = cmd.ExecuteNonQuery();
            //Close the database connection.    
            conn.Close();
            return count;
        }

        public int UpdateStatus(Suggestion suggestion)
        {
            //Instantiate a SqlCommand object, supply it with SQL statement UPDATE     
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
                    ("UPDATE   Suggestion SET Status=@Status " +
                     "WHERE SuggestionID=@selectedSuggestionID", conn);
            //Define the parameters used in SQL statement, value for each parameter  
            //is retrieved from the respective property of “student” object.  
            cmd.Parameters.AddWithValue("@Status", suggestion.Status);

            //cmd.Parameters.AddWithValue("@photo", student.Photo);
            cmd.Parameters.AddWithValue("@selectedSuggestionID", suggestion.SuggestionId);
            //cmd.Parameters.AddWithValue("@selectedEmail", student);

            //Open a database connection.   
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE   
            int count = cmd.ExecuteNonQuery();
            //Close the database connection.    
            conn.Close();
            return count;
        }



        public bool IsEmailExist(string email)
        {
            SqlCommand cmd = new SqlCommand("SELECT StudentID FROM Student WHERE EmailAddr=@selectedEmail", conn);
            cmd.Parameters.AddWithValue("@selectedEmail", email);
            SqlDataAdapter daEmail = new SqlDataAdapter(cmd);
            DataSet result = new DataSet();
            conn.Open();
            //Use DataAdapter to fetch data to a table "EmailDetails" in DataSet.    
            daEmail.Fill(result, "EmailDetails");
            conn.Close();
            if (result.Tables["EmailDetails"].Rows.Count > 0)
                return true;
            //The email exists for another staff   
            else
                return false;
            // The email address given does not exist 
        }

        public int Add(Student student)
        {
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement   
            //which will return the auto-generated StaffID after insertion,    
            //and the connection object for connecting to the database.     
            SqlCommand cmd = new SqlCommand
                (
                "INSERT INTO Student ( Name, Course,EmailAddr,Password,MentorID) " +
                "OUTPUT INSERTED.StudentID " +
                "VALUES( @name, @course,@emailAddr,@password,@mentorID) " ,
               conn);
            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from respective class's property.  
            //cmd.Parameters.AddWithValue("@studentid", student.StudentId);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@course", student.Course);
            //cmd.Parameters.AddWithValue("@photo", student.Photo);
       
    
           
            cmd.Parameters.AddWithValue("@emailAddr", student.Email);
            cmd.Parameters.AddWithValue("@password", student.Password);
            cmd.Parameters.AddWithValue("@mentorID", student.MentorId);

            //A connection to database must be opened before any operations made.  
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated   
            //StaffID after executing the INSERT SQL statement  
            student.StudentId = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.  
            conn.Close();
            //Return id when no error occurs.     
            return student.StudentId;
        }

        // get sent suggestion
        public List<Suggestion> GetAllSentSuggestion()
        {
            //Instantiate a SqlCommand object, supply it with a              
            //SELECT SQL statement that operates against the database,             
            //and the connection object for connecting to the database.             
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Suggestion", conn);
            //Instantiate a DataAdapter object and pass the              
            //SqlCommand object created as parameter.             
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataSet object to contain records get from database             
            DataSet result = new DataSet();

            //Open a database connection             
            conn.Open();
            //Use DataAdapter, which execute the SELECT SQL through its                                  
            //SqlCommand object to fetch data to a table "Description"             
            //in DataSet "result".           
            da.Fill(result, "SuggestionDetails");
            //Close the database connection                   
            conn.Close();


            //Transferring rows of data in DataSet’s table to “Staff” objects   
            List<Suggestion> SuggestionList = new List<Suggestion>();
            foreach (DataRow row in result.Tables["SuggestionDetails"].Rows)
            {

                SuggestionList.Add(new Suggestion
                {
                    SuggestionId = Convert.ToInt32(row["SuggestionID"]),

                    LecturerId = Convert.ToInt32(row["LecturerID"]),
                    StudentId = Convert.ToInt32(row["StudentID"]),
                    Description = row["Description"].ToString(),
                    Status = row["Status"].ToString(),
                    Date = Convert.ToDateTime(row["DateCreated"]),
                    //Email = row["EmailAddr"].ToString()
                });
            }

            return SuggestionList;

        }


        

           

        
    }
}
