using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using WebAssignment_2019_P03_Team06.Models;

namespace WebAssignment_2019_P03_Team06.DAL
{
    public class LecturerDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;
        //Constructor       
        public LecturerDAL()
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

        public List<Lecturer> GetAllLecturer()
        {
            //Instantiate a SqlCommand object, supply it with a              
            //SELECT SQL statement that operates against the database,             
            //and the connection object for connecting to the database.             
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Lecturer ORDER BY LecturerID", conn);
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
            da.Fill(result, "LecturerDetails");
            //Close the database connection                   
            conn.Close();


            //Transferring rows of data in DataSet’s table to “Staff” objects   
            List<Lecturer> lecturerList = new List<Lecturer>();
            foreach (DataRow row in result.Tables["LecturerDetails"].Rows)
            {

                lecturerList.Add(new Lecturer {
                    LecturerId = Convert.ToInt32(row["LecturerID"]),
                    LecturerName = row["Name"].ToString(),
                    Email = row["EmailAddr"].ToString(),
                    LecturerPassword = row["Password"].ToString(),
                    Description = row["Description"].ToString() });
            }

            return lecturerList;

        }

        // get sent suggestion
        public List<SentSuggestion> GetAllSentSuggestion()
        {
            //Instantiate a SqlCommand object, supply it with a              
            //SELECT SQL statement that operates against the database,             
            //and the connection object for connecting to the database.             
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Suggestion ORDER BY DateCreated", conn);
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
            da.Fill(result, "SentSuggestionDetails");
            //Close the database connection                   
            conn.Close();


            //Transferring rows of data in DataSet’s table to “Staff” objects   
            List<SentSuggestion> SentSuggestionList = new List<SentSuggestion>();
            foreach (DataRow row in result.Tables["SentSuggestionDetails"].Rows)
            {

                SentSuggestionList.Add(new SentSuggestion
                {
                    SuggestionId = Convert.ToInt32(row["SuggestionID"]),
                    LecturerId = Convert.ToInt32(row["LecturerID"]),
                    StudentId = Convert.ToInt32(row["StudentID"]),
                    Description = row["Description"].ToString(),
                    Status = row["Status"].ToString(),
                    DateCreated = Convert.ToDateTime(row["DateCreated"])
                });
            }


            return SentSuggestionList;


        }

        public List<StudentProfile> GetAllStudent()
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


            //Transferring rows of data in DataSet’s table to “Staff” objects   
            List<StudentProfile> studentList = new List<StudentProfile>();
            foreach (DataRow row in result.Tables["StudentDetails"].Rows)
            {

                studentList.Add(new StudentProfile
                {

                    StudentId = Convert.ToInt32(row["StudentID"]),
                    Photo = row["Photo"].ToString(),
                    Name = row["Name"].ToString(),
                    Email = row["EmailAddr"].ToString(),
                    Password = row["Password"].ToString(),
                    Link = row["ExternalLink"].ToString(),
                    MentorId = Convert.ToInt32(row["MentorID"])
                });
            }
            return studentList;
        }

        public int Add(Lecturer lecturer)
        {
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement 
            //which will return the auto-generated StaffID after insertion, 
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
            ("INSERT INTO Lecturer (Name, EmailAddr, Password, Description)" +
            "OUTPUT INSERTED.LecturerID " +
            "VALUES(@name, @emailAddr, @password, @description)", conn);

            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from respective class's property.
            
            cmd.Parameters.AddWithValue("@name", lecturer.LecturerName);
            cmd.Parameters.AddWithValue("@emailAddr", lecturer.Email);
            cmd.Parameters.AddWithValue("@password", "p@55Mentor");
            cmd.Parameters.AddWithValue("@description", lecturer.Description);
            

            //A connection to database must be opened before any operations made. 
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated 
            //StaffID after executing the INSERT SQL statement 
            lecturer.LecturerId = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations. 
            conn.Close();

            //Return id when no error occurs. 
            return lecturer.LecturerId;
        }

        //For compose get student name and ID
        public StudentProfile GetDetails(int studentid)
        {
            //Instantiate a SqlCommand object, supply it with a SELECT SQL 
            //statement which retrieves all attributes of a staff record. 
            SqlCommand cmd = new SqlCommand
            ("SELECT * FROM Student WHERE StudentID = @selectedStudentID", conn);
            //Define the parameter used in SQL statement, value for the 
            //parameter is retrieved from the method parameter “staffId”. 
            cmd.Parameters.AddWithValue("@selectedStudentID", studentid);
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


            //Transferring rows of data in DataSet’s table to “Staff” objects   
            StudentProfile studentId = new StudentProfile();
            if (result.Tables["StudentDetails"].Rows.Count > 0)
            {
                studentId.StudentId = studentid;
                // Fill staff object with values from the DataSet 
                DataTable table = result.Tables["StudentDetails"];
                if (!DBNull.Value.Equals(table.Rows[0]["MentorID"]))
                    studentId.MentorId = Convert.ToChar(table.Rows[0]["MentorID"]);
                if (!DBNull.Value.Equals(table.Rows[0]["StudentID"]))
                    studentId.StudentId = Convert.ToChar(table.Rows[0]["StudentID"]);

                return studentId; // No error occurs 
            }
            else
            {
                return null; // Record not found 
            }

        }

        //public ComposeSuggestion GetDetails(int std)
        //{
        //    //Instantiate a SqlCommand object, supply it with a SELECT SQL 
        //    //statement which retrieves all attributes of a staff record. 
        //    SqlCommand cmd = new SqlCommand
        //    ("SELECT * FROM Student WHERE StudentID = @selectedStudentID", conn);
        //    //Define the parameter used in SQL statement, value for the 
        //    //parameter is retrieved from the method parameter “staffId”. 
        //    cmd.Parameters.AddWithValue("@selectedStudentID", std.);
        //    //Instantiate a DataAdapter object, pass the SqlCommand 
        //    //object “cmd” as parameter. 
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //Create a DataSet object “result" 
        //    DataSet result = new DataSet();
        //    //Open a database connection. 
        //    conn.Open();
        //    //Use DataAdapter to fetch data to a table "StaffDetails" in DataSet. 
        //    da.Fill(result, "StudentDetails");
        //    //Close the database connection 
        //    conn.Close();


        //    //Transferring rows of data in DataSet’s table to “Staff” objects   
        //    List<ComposeSuggestion> studentId = new List<ComposeSuggestion>();
        //    foreach (DataRow row in result.Tables["StudentDetails"].Rows)
        //    {

        //        studentId.Add(new ComposeSuggestion
        //        {
                    
        //            LecturerId = Convert.ToInt32(row["LecturerID"]),
        //            ToStudentId = Convert.ToInt32(row["StudentID"])
                   
        //        });
        //    }
        //    return studentId; // No error occurs 
            
           

        //}



        //sending feedback
        public int Send(ComposeSuggestion composeSuggestion)
        {
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement 
            //which will return the auto-generated StaffID after insertion, 
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
            ("INSERT INTO Suggestion (LecturerID, StudentID, Description, Status, DateCreated)" +
            "OUTPUT INSERTED.SuggestionID " +
            "VALUES((SELECT LecturerID FROM Lecturer WHERE LecturerID = @lecturerId), @studentId, @description, @status, @dateCreated)", conn);

            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from respective class's property.

            cmd.Parameters.AddWithValue("@lecturerId", composeSuggestion.LecturerId);
            cmd.Parameters.AddWithValue("@studentId", composeSuggestion.ToStudentId);
            cmd.Parameters.AddWithValue("@description", composeSuggestion.Description);
            cmd.Parameters.AddWithValue("@status", "N");
            cmd.Parameters.AddWithValue("@dateCreated", composeSuggestion.Date);


            //A connection to database must be opened before any operations made. 
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated 
            //StaffID after executing the INSERT SQL statement 
            composeSuggestion.SuggestionId = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations. 
            conn.Close();

            //Return id when no error occurs. 
            return composeSuggestion.SuggestionId;
        }

        public int ChangePass(Lecturer lecturer)
        {
            //Instantiate a SqlCommand object, supply it with SQL statement UPDATE 
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
            ("UPDATE Lecturer SET Password=@password WHERE LecturerID = @LecturerID", conn);
            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from the respective property of “staff” object. 
            cmd.Parameters.AddWithValue("@password", lecturer.NewLecturerPassword);
            cmd.Parameters.AddWithValue("@LecturerID", lecturer.LecturerId);


            //if (staff.BranchNo != null) // A branch is assigned 
            //    cmd.Parameters.AddWithValue("@branchNo", staff.BranchNo.Value);
            //else // No branch is assigned 
            //    cmd.Parameters.AddWithValue("@branchNo", DBNull.Value);
            //cmd.Parameters.AddWithValue("@selectedStaffID", staff);

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
            SqlCommand cmd = new SqlCommand
            ("SELECT LecturerID FROM Lecturer WHERE EmailAddr=@selectedEmail", conn);
            cmd.Parameters.AddWithValue("@selectedEmail", email);
            SqlDataAdapter daEmail = new SqlDataAdapter(cmd);
            DataSet result = new DataSet();
            conn.Open();
            //Use DataAdapter to fetch data to a table "EmailDetails" in DataSet. 
            daEmail.Fill(result, "EmailDetails");
            conn.Close();
            if (result.Tables["EmailDetails"].Rows.Count > 0)
                return true; //The email exists for another staff 
            else
                return false; // The email address given does not exist 
        }
    }
}