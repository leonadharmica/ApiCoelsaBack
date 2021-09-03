using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ApiCoelsa.Models;

namespace ApiCoelsa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ContactsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetContactByCompany()
        {
            string query = @"
                            select ContactID, FirstName, LastName, Company, Email, PhoneNumber from
                            dbo.Contacts
                            order by Company
                            offset 0 rows fetch next 10 rows only
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactsApp");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Contacts contacts)
        {
            string query = @"
                            insert into dbo.Contacts 
                            values(@FirstName, @LastName, @Company, @Email, @PhoneNumber)                   
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactsApp");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", contacts.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", contacts.LastName);
                    myCommand.Parameters.AddWithValue("@Company", contacts.Company);
                    myCommand.Parameters.AddWithValue("@Email", contacts.Email);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", contacts.PhoneNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }

        [HttpPut]
        public JsonResult Put(Contacts contacts)
        {
            string query = @"
                            update dbo.Contacts 
                            set FirstName=@FirstName, 
                                LastName=@LastName, 
                                Company=@Company, 
                                Email=@Email,
                                PhoneNumber=@PhoneNumber 
                            where ContactId=@ContactId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactsApp");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactId", contacts.ContactID);
                    myCommand.Parameters.AddWithValue("@FirstName", contacts.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", contacts.LastName);
                    myCommand.Parameters.AddWithValue("@Company", contacts.Company);
                    myCommand.Parameters.AddWithValue("@Email", contacts.Email);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", contacts.PhoneNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Succesfully");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Contacts 
                            where ContactId=@ContactId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactsApp");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Succesfully");
        }
    }
}
