using EJCRUD4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EJCRUD4.Controllers
{
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; }
        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            List<cAccount> AccountList = new List<cAccount>();

            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sql = "select * from [DB1].[dbo].[Account]";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        cAccount clsAccount = new cAccount();
                        clsAccount.ID = Convert.ToInt32(dataReader["ID"]);
                        clsAccount.Description = Convert.ToString(dataReader["Description"]);
                        AccountList.Add(clsAccount);
                    }
                }

                connection.Close();
            }
            return View(AccountList);
        }
        public IActionResult Edit(int id)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];

            cAccount clsAccount = new cAccount();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From [DB1].[dbo].[Account] Where ID='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsAccount.ID = Convert.ToInt32(dataReader["ID"]);
                        clsAccount.Description = Convert.ToString(dataReader["Description"]);
                   
                    }
                }
                connection.Close();
            }
            return View(clsAccount);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit_Post(cAccount clsAccount)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update [DB1].[dbo].[Account] SET ID='{clsAccount.ID}', Description='{clsAccount.Description}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];

            cAccount clsAccount = new cAccount();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From [DB1].[dbo].[Account] Where ID='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsAccount.ID = Convert.ToInt32(dataReader["ID"]);
                        clsAccount.Description = Convert.ToString(dataReader["Description"]);
                    }
                }
            }
            return View(clsAccount);
        }

        //[HttpPost]//Se puede usar directo, invocando desde el view Index a través de un boton con HttpPost
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From [DB1].[dbo].[Account] Where ID='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Error en la operación:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(cAccount clsAccount)
        {
            if (ModelState.IsValid)
            {
                string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into [DB1].[dbo].[Account](ID, Description) Values('{clsAccount.ID}', '{clsAccount.Description}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View();
        }

    }
}
