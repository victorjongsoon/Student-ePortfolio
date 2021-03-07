using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAssignment_2019_P03_Team06.Models;

namespace WebAssignment_2019_P03_Team06.DAL
{
    public class HomeDAL
    {
        public HomeDAL()
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

        private SqlConnection conn;
        private IConfiguration Configuration { get; set; }


        public List<Home> LogIn(string email, string password)
        {
            
            SqlCommand cmds = new SqlCommand("SELECT Name FROM Student WHERE EmailAddr=@email AND Password = @password  " , conn);
            SqlCommand cmdl = new SqlCommand("SELECT Name FROM Lecturer WHERE EmailAddr=@email AND Password = @password ", conn);

            cmds.Parameters.AddWithValue("@email", email);
            cmds.Parameters.AddWithValue("@password",password);
            //cmds.Parameters.AddWithValue("@id", id);


            cmdl.Parameters.AddWithValue("@email", email);
            cmdl.Parameters.AddWithValue("@password", password);
            //cmdl.Parameters.AddWithValue("@id", id);


            SqlDataAdapter das = new SqlDataAdapter(cmds);
            SqlDataAdapter dal = new SqlDataAdapter(cmdl);

            DataSet result = new DataSet();

            conn.Open();

            das.Fill(result, "StudentName");
            dal.Fill(result, "LecturerName");

            conn.Close();

            List<Home> homeList = new List<Home>();

            foreach (DataRow row in result.Tables["StudentName"].Rows)
            {
                homeList.Add(
                    new Home
                    {
                        NameS = row["Name"].ToString()


                    });
            }

            foreach (DataRow row in result.Tables["LecturerName"].Rows)
            {
                homeList.Add(
                    new Home
                    {
                        NameL = row["Name"].ToString(),
                        //IdL = Convert.ToInt32(row["LecturerID"])

                    });
            }

            return homeList;






        }



    }
}
