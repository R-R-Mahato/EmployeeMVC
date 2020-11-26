using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using EmpFormMVC.Models;
namespace EmpFormMVC.Controllers
{
    public class HomeController : Controller
    {
        // Default Start Page . Displays list of employees

        public ActionResult Index()
        {
            //list to collect all employee details
            var emp_list = new List<Employee>();
            string connectionstring = ConfigurationManager.ConnectionStrings["empDbConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "select * from dbo.employee_details";
            SqlCommand cmd = new SqlCommand(query, conn);            
            conn.Open();
            //Continue reading rows from database and add to list
            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                var employee = new Employee();
                employee.id = (int)rdr["ID"];
                employee.fname = rdr["fname"].ToString();
                employee.lname = rdr["lname"].ToString();
                employee.gender = rdr["gender"].ToString();
                employee.address = rdr["address"].ToString();
                employee.phone = rdr["phone"].ToString();
                employee.email = rdr["email"].ToString();
                emp_list.Add(employee);
            }
            conn.Close();
            //Pass employee list to view to display
            return View(emp_list);
            
        }
       
        //Show form to add employee data
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        //Add form data posted by user
        [HttpPost]
        public ActionResult AddEmployee(Employee emp,HttpPostedFileBase pic)
        {            
            string connectionstring = ConfigurationManager.ConnectionStrings["empDbConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "insert into dbo.employee_details (fname,lname,gender,address,phone,email) values(@fname,@lname,@gender,@address,@phone,@email)";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@fname", emp.fname);
            cmd.Parameters.AddWithValue("@lname", emp.lname);
            cmd.Parameters.AddWithValue("@gender", emp.gender);
            cmd.Parameters.AddWithValue("@address", emp.address);
            cmd.Parameters.AddWithValue("@phone", emp.phone);
            cmd.Parameters.AddWithValue("@email", emp.email);
            //Tried many ways but pic parameter doesn't read uploaded file's data.Sorry about this.

     /*     if(pic!=null && pic.ContentLength>0)
            {
                string filename = Path.GetFileName(pic.FileName);
                string path = Path.Combine(Server.MapPath("~/Employee_img/"),filename);
                pic.SaveAs(path);
            }
            cmd.Parameters.AddWithValue("@img", "~/Employee_img/" + pic.FileName);  */          
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }

        //Action to find employee data that is to be updated and pass it to view employee action
        public ActionResult UpdateEmployee(int Id)
        {
            var employee = new Employee();
            string connectionstring = ConfigurationManager.ConnectionStrings["empDbConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "select * from dbo.employee_details where ID="+Id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
                employee.id = (int)rdr["ID"];
                employee.fname = rdr["fname"].ToString();
                employee.lname = rdr["lname"].ToString();
                employee.gender = rdr["gender"].ToString();
                employee.address = rdr["address"].ToString();
                employee.phone = rdr["phone"].ToString();
                employee.email = rdr["email"].ToString();                
            }
            //pass data to view employee to view employee's data
            conn.Close();
            return RedirectToAction("ViewEmployee",employee);
        }

        //Display employee's data
        public ActionResult ViewEmployee(Employee emp)
        {
            return View(emp);
        }

        //Save updated employee data to database
        [HttpPost]
        public ActionResult UpdateEmployee(Employee emp)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["empDbConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "delete from dbo.employee_details where ID = " + emp.id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            query = "insert into dbo.employee_details (fname,lname,gender,address,phone,email) values(@fname,@lname,@gender,@address,@phone,@email)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@fname", emp.fname);
            cmd.Parameters.AddWithValue("@lname", emp.lname);
            cmd.Parameters.AddWithValue("@gender", emp.gender);
            cmd.Parameters.AddWithValue("@address", emp.address);
            cmd.Parameters.AddWithValue("@phone", emp.phone);
            cmd.Parameters.AddWithValue("@email", emp.email);
            cmd.ExecuteNonQuery();
            conn.Close();
            ViewData["info"] = "Updated Successfully";
            return RedirectToAction("Index");
        }

        //Delete selected employee from database
        public ActionResult DeleteEmployee(int Id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["empDbConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "delete from dbo.employee_details where ID="+Id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            ViewData["info"] = "Deleted Sucessfully";
            return RedirectToAction("Index");
        }
    }
}