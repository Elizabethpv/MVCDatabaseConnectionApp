using MVCDatabaseConnectionApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDatabaseConnectionApp.Controllers
{
    public class PersonController : Controller
    {
        String connections = "Data Source=DESKTOP-NDM7TFA\\SQLEXPRESS;Initial Catalog=PersonManagementSystem;Integrated Security=True;";
        // GET: Person
        public ActionResult PersonView()
        {
            List<Person> persons = new List<Person>();
            Person p1 = new Person();
            //string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connections);
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());
            con.Open();
            //SqlCommand command = new SqlCommand("SelectPerson", con);
            String sql = @"select * from Person";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, con);

            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var son = new Models.Person();
                son.ID = Convert.ToInt32(row["ID"]);
                son.Name = row["Name"].ToString();
                son.Email = row["Email"].ToString();
                son.PhoneNumber = row["PhoneNumber"].ToString();

                persons.Add(son);
            }

            con.Close();
            return View(persons);



        }

        public ActionResult PersonList()
        {
            Person p1 = new Person();
            SqlConnection con = new SqlConnection(connections);

            con.Open();
            SqlCommand cmd = new SqlCommand("PersonTopView", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                p1.ID = Convert.ToInt32(reader["ID"]);
                p1.Name = reader["Name"].ToString();
                p1.Email = reader["Email"].ToString();
                p1.PhoneNumber = reader["PhoneNumber"].ToString();
            }
            con.Close();
            
            return View(p1);
        }
      
    } 
}