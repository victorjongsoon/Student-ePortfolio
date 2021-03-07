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
    public class ProjectDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;

        //Constructor 
        public ProjectDAL()
        {
            //Locate the appsettings.json file 
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            //Read ConnectionString from appsettings.jason file 
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("StudentEportfolioConnectionString");

            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn); 
        }

        public List<ProjectViewModel> GetAllProject(int studentID)
        {
            //Instantiate a SqlCommand object, supply it with a
            //SELECT SQL statement that operates against the database,
            //and the connection object for connecting to the database.
            SqlCommand cmd = new SqlCommand(
                "select p.*, pm.*, s.Name from Project p Inner join ProjectMember pm ON p.ProjectID = pm.ProjectID INNER JOIN  Student s ON pm.StudentID = s.StudentID Where pm.StudentID = @selectedStudentID", conn);

            //System.Diagnostics.Debug.WriteLine(studentID.ToString());
            cmd.Parameters.AddWithValue("@selectedStudentID", studentID); //studentID

            //Instantiate a DataAdapter object and pass the
            //SqlCommand object created as parameter.
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create a DataSet object to contain records get from database 
            DataSet result = new DataSet();

            //Open a database connection
            conn.Open();

            //Use DataAdapter, which execute the SELECT SQL through its 
            //SqlCommand object to fetch data to a table "ProjectDetails"
            //in DataSet "result".
            da.Fill(result, "ProjectDetails");

            //Close the database connection
            conn.Close();

            //Transferring rows of data in DataSet's table to "Project" objects
            List<ProjectViewModel> projectList = new List<ProjectViewModel>();

            if(result.Tables["ProjectDetails"].Rows.Count > 0)
            {
                foreach (DataRow row in result.Tables["ProjectDetails"].Rows)
                {
                    projectList.Add(
                        new ProjectViewModel
                        {
                            ProjectID = Convert.ToInt32(row["ProjectID"]),
                            Title = row["Title"].ToString(),
                            ProjectPoster = row["ProjectPoster"].ToString(),
                            Descipition = row["Description"].ToString(),
                            ProjectURL = row["ProjectURL"].ToString(),
                            Role = row["Role"].ToString(),
                            StudentLeaderName = row["Name"].ToString()
                        }
                    );

                }
                return projectList;
            }
            else
            {
                //No project records under selected student 
                return null;
            }  
        }

        public int Add(ProjectViewModel project, int studentID)
        {
            //Insert to project
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement 
            //which will return the auto-generated StaffID after insertion, 
            //and the connection object for connecting to the database. 
            //SqlCommand cmd = new SqlCommand ("INSERT INTO Project (Title, Description, ProjectId VALUES(@title, @descipition, @projectId)", conn);
            SqlCommand cmd = new SqlCommand("INSERT INTO Project(Title, Description) OUTPUT INSERTED.ProjectID VALUES(@title, @descipition)", conn);

            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from respective class's property. 

            cmd.Parameters.AddWithValue("@title", project.Title);
            cmd.Parameters.AddWithValue("@descipition", project.Descipition);

            //A connection to database must be opened before any operations made. 
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated 
            //ProjectID after executing the INSERT SQL statement 
            project.ProjectID = (int)cmd.ExecuteScalar();
            //projectCount = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations. 
            conn.Close();


            //Insert into ProjectMember 
            //Instantiate a SqlCommand object,supply it with an INSERT SQL statement 
            //which will return the auto-generated StaffID after insertion, 
            //and the connection object for connecting to the database. 
            SqlCommand cmd1 = new SqlCommand("INSERT INTO ProjectMember (ProjectID, StudentID, Role) VALUES(@projectID, @studentID, @role)", conn);


            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from respective class's property. 

            cmd1.Parameters.AddWithValue("@projectID", project.ProjectID);
            cmd1.Parameters.AddWithValue("@studentID", studentID);
            cmd1.Parameters.AddWithValue("@role", "Leader");

            //A connection to database must be opened before any operations made. 
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated 
            //ProjectID after executing the INSERT SQL statement
            cmd1.ExecuteScalar();

            //A connection should be closed after operations. 
            conn.Close();

            //Return id when no error occurs. 
            return project.ProjectID;
        }

        public ProjectViewModel GetDetails(int id)
        {
            //Instantiate a SqlCommand object, supply it with a SELECT SQL 
            //statement which retrieves all attributes of a project record.
            SqlCommand cmd = new SqlCommand("select p.*, pm.*, s.StudentID, s.Name from Project p Inner join ProjectMember pm ON p.ProjectID = pm.ProjectID INNER JOIN  Student s ON pm.StudentID = s.StudentID Where p.ProjectID = @selectedprojectID", conn);

            //Defind the parameter used in SQL statement, value for the 
            //parmeter is retrieved from the method parameter 'projectId' 
            cmd.Parameters.AddWithValue("@selectedprojectID", id);

            //Instantiate a DataAdapter object, pass the SQLCommand 
            //object "cmd" as parameter
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create a DataSet object "result" 
            DataSet result = new DataSet();

            //Use DataAdapter to fetch data to a table "ProjectDetails" in DataSet 
            da.Fill(result, "ProjectDetails");

            ProjectViewModel project = new ProjectViewModel();
            if (result.Tables["ProjectDetails"].Rows.Count > 0)
            {
                project.ProjectID = id;
                //Fill project object with values from the DataSet 
                DataTable table = result.Tables["ProjectDetails"];
                if (!DBNull.Value.Equals(table.Rows[0]["Title"]))
                    project.Title = table.Rows[0]["Title"].ToString();
                if (!DBNull.Value.Equals(table.Rows[0]["Description"]))
                    project.Descipition = table.Rows[0]["Description"].ToString();
                if (!DBNull.Value.Equals(table.Rows[0]["ProjectPoster"]))
                    project.ProjectPoster = table.Rows[0]["ProjectPoster"].ToString();
                if (!DBNull.Value.Equals(table.Rows[0]["ProjectURL"]))
                    project.ProjectURL = table.Rows[0]["ProjectURL"].ToString();
                if (!DBNull.Value.Equals(table.Rows[0]["Role"]))
                    project.Role = table.Rows[0]["Role"].ToString();
                if (!DBNull.Value.Equals(table.Rows[0]["StudentID"]))
                    project.LeaderStudentID = Convert.ToInt32(table.Rows[0]["StudentID"]);
                if (!DBNull.Value.Equals(table.Rows[0]["Name"]))
                    project.StudentLeaderName = table.Rows[0]["Name"].ToString();

                return project;
            }
            else
            {
                return null;
            }
        }

        //Return number of row updated 
        public int Update(ProjectViewModel project)
        {
            //Instantiate a SqlCommand object, supply it with SQL statement
            //and the connection object for connecting to the database. 
            SqlCommand cmd1 = new SqlCommand
                ("UPDATE Project SET Description=@Description, ProjectURL=@ProjectURL WHERE ProjectID=@selectedProjectID", conn);

            SqlCommand cmd2 = new SqlCommand
                ("INSERT INTO ProjectMember (ProjectID, StudentID,Role) VALUES (@selectedprojectID, @StudentID, @Role)", conn);

            //Define the parameters used in SQL statement, value for each parameter 
            //is retrieved from the respective property of “Project” object. 
            if (project.Descipition == null)
                project.Descipition = "";

            if (project.ProjectURL == null)
                project.ProjectURL = "";

            //Parameters for updating Project
            cmd1.Parameters.AddWithValue("@selectedprojectID", project.ProjectID);
            cmd1.Parameters.AddWithValue("@Description", project.Descipition);
            cmd1.Parameters.AddWithValue("@ProjectURL", project.ProjectURL);

            //Parameters for updating ProjectMember 
            cmd2.Parameters.AddWithValue("@selectedprojectID", project.ProjectID);
            cmd2.Parameters.AddWithValue("@StudentID", project.StudentIDs);
            cmd2.Parameters.AddWithValue("@Role", "Member");

            //Open a database connection.
            conn.Open();

            //ExecturNonQuery is used for UPDATE and DELETE
            int count = cmd1.ExecuteNonQuery();

            //Insert ProjectMember after executing the INSERT SQL statement
            if(project.StudentIDs > 0)
                cmd2.ExecuteScalar();

            //Close the database connection.
            conn.Close();

            return count;
        }

        public int Delete(ProjectViewModel project)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Project ID.
            SqlCommand cmd1, cmd2;
            cmd1 = new SqlCommand("Delete FROM ProjectMember WHERE  ProjectID = @selectProjectID", conn);
            cmd1.Parameters.AddWithValue("@selectProjectID", project.ProjectID);
            cmd2 = new SqlCommand("Delete FROM Project WHERE  ProjectID = @selectProjectID", conn);
            cmd2.Parameters.AddWithValue("@selectProjectID", project.ProjectID);

            //Open a database connection.
            conn.Open();

            //Execute the DELETE SQL to remove the staff record.
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            //Close database connection.
            conn.Close();

            //Return number of row of staff record deleted.
            return project.ProjectID;
        }

        public int UploadPhoto(string uploadFile, int projectID)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Project SET ProjectPoster = @projectPoster WHERE ProjectID = @selectedProjectID", conn);

            cmd.Parameters.AddWithValue("@selectedProjectID", projectID);
            cmd.Parameters.AddWithValue("@projectPoster", uploadFile);

            conn.Open();

            int count = cmd.ExecuteNonQuery();

            conn.Close();

            return count;
        }

        //Check if title exist in Project
        public bool IsTitleExist(string title)
        {
            SqlCommand cmd = new SqlCommand
            ("SELECT * FROM Project WHERE Title = @selectedTitle", conn);
            cmd.Parameters.AddWithValue("@selectedTitle", title);
            SqlDataAdapter daEmail = new SqlDataAdapter(cmd);
            DataSet result = new DataSet();
            conn.Open();
            //Use DataAdapter to fetch data to a table "EmailDetails" in DataSet.
            daEmail.Fill(result, "projectDetails");
            conn.Close();

            if (result.Tables["projectDetails"].Rows.Count < 0)
                return false; //The title exists in database
            else
                return true; // TThe title does not exists in database
        }
    }
}

