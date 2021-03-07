using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using WebAssignment_2019_P03_Team06.Models;

namespace WebAssignment_2019_P03_Team06.DAL
{
    public class StudentProfileDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;
        //Constructor       
        public StudentProfileDAL()
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

        
        /*
        public int Add(Staff staff)
        {
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement 
            //which will return the auto-generated StaffID after insertion, 
            //and the connection object for connecting to the database. 
            SqlCommand cmd = new SqlCommand
            ("INSERT INTO Suggestion (SuggestionID, LecturerID, StudentID, Salary, " +
            "EmailAddr, Nationality, Status) " +
            "OUTPUT INSERTED.StaffID " +
            "VALUES(@name, @gender, @dob, @salary, " +
            "@email, @country, @status)", conn);

            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from respective class's property. 
            cmd.Parameters.AddWithValue("@name", staff.Name);
            cmd.Parameters.AddWithValue("@gender", staff.Gender);
            cmd.Parameters.AddWithValue("@dob", staff.DOB);
            cmd.Parameters.AddWithValue("@salary", staff.Salary);
            cmd.Parameters.AddWithValue("@email", staff.Email);
            cmd.Parameters.AddWithValue("@country", staff.Nationality);
            cmd.Parameters.AddWithValue("@status", staff.IsFullTime);

            //A connection to database must be opened before any operations made. 
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated 
            //StaffID after executing the INSERT SQL statement 
            staff.StaffId = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations. 
            conn.Close();

            //Return id when no error occurs. 
            return staff.StaffId;
        }
        */
    }
}
