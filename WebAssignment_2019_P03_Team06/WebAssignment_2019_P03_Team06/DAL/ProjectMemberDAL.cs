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
    public class ProjectMemberDAL
    {
        private IConfiguration Configuartion { get; set; }
        private SqlConnection conn;

        //Constructor 
        public ProjectMemberDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuartion = builder.Build();
            string strConn = Configuartion.GetConnectionString("StudentEportfolioConnectionString");

            //Instantiate a SqlConnection object with the 
            //Connection String read.
            conn = new SqlConnection(strConn);
        }


        public List<ProjectMember> GetAllStudentMembers(int projectID)
        {
            //Instantiate a SqlCommand object, supply it with a SELECT SQL 
            //statement which retrieves all attributes of the selected student record.
            SqlCommand cmd = new SqlCommand
                ("SELECT * From ProjectMember WHERE ProjectID =@selectedProjectID",conn);

            //System.Diagnostics.Debug.WriteLine(studentID.ToString());
            cmd.Parameters.AddWithValue("@selectedProjectID", projectID);

            //Instantiate a DataAdapter object, pass the SqlCommand 
            //object created as parameter. 
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            // Create a DataSet object result 
            DataSet result = new DataSet();

            //Open a database connection. 
            conn.Open();

            //Use a DataAdapter "da" to fill up a DataTable "Projects" that the student did 
            //in DataSet "result".
            da.Fill(result, "ProjectMDetails");

            //Close the database connection
            conn.Close();

            if (result.Tables["ProjectMDetails"].Rows.Count > 0)
            {
                List<ProjectMember> projectMemberList = new List<ProjectMember>();
                foreach (DataRow row in result.Tables["ProjectMDetails"].Rows)
                {
                    projectMemberList.Add(
                        new ProjectMember
                        {
                            ProjectID = Convert.ToInt32(row["ProjectID"]),
                            StudentID = Convert.ToInt32(row["ProjectID"]),
                            Role = row["Role"].ToString()
                        }
                    );
                }
                return projectMemberList;
            }
            else
            {
                //No project records in ProjectMember
                return null;
            }
        }

        
        public int Add(int projectid, int studentID)
        {
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement
            //which will return the auto-generated StaffID after insertion,
            //and the connection object for connecting to the database.
            SqlCommand cmd = new SqlCommand("INSERT INTO ProjectMember (ProjectID, StudentID, Role) VALUES (@selectedProjectID,@selectStudentID,@selectRole)", conn);

            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@selectedProjectID", projectid);
            cmd.Parameters.AddWithValue("@selectStudentID", studentID);
            cmd.Parameters.AddWithValue("@selectRole", "Member");


            //A connection to database must be opened before any operations made. 
            conn.Open();

            
            cmd.ExecuteScalar();

            //ExecuteScalar is used to retrieve the auto-generated 
            //StaffID after executing the INSERT SQL statement
            //projectMember.ProjectID = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations.
            conn.Close();

            //Return id when no error occurs.
            return projectid;

        }

        public int Delete(ProjectViewModel project)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Project ID.
            SqlCommand cmd1;
            cmd1 = new SqlCommand("Delete FROM ProjectMember WHERE  ProjectID = @selectProjectID and StudentID = @selectStudentID", conn);

            cmd1.Parameters.AddWithValue("@selectProjectID", project.ProjectID);
            cmd1.Parameters.AddWithValue("@selectStudentID", project.MemberStudentID);

            //Open a database connection.
            conn.Open();

            //Execute the DELETE SQL to remove the staff record.
            cmd1.ExecuteNonQuery();

            //Close database connection.
            conn.Close();

            //Return number of row of staff record deleted.
            return project.ProjectID;
        }
    }
}
