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
    public class MainController : Controller
    {
        public IConfiguration Configuration { get; }
        public MainController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            List<cCuentas> CuentasList = new List<cCuentas>();

            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sql = "select * from [DB2].[dbo].[Cuentas]";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        cCuentas clsCuentas = new cCuentas();
                        clsCuentas.ID = Convert.ToInt32(dataReader["ID"]);
                        clsCuentas.Description = Convert.ToString(dataReader["Description"]);
                        clsCuentas.ID_Origen = Convert.ToInt32(dataReader["ID_Origen"]);
                        CuentasList.Add(clsCuentas);
                    }
                }

                connection.Close();
            }
            return View(CuentasList);
        }
        public IActionResult Edit(int id)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];

            cCuentas clsCuentas = new cCuentas();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From [DB2].[dbo].[Cuentas] Where ID='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsCuentas.ID = Convert.ToInt32(dataReader["ID"]);
                        clsCuentas.Description = Convert.ToString(dataReader["Description"]);
                        clsCuentas.ID_Origen = Convert.ToInt32(dataReader["ID_Origen"]);
                    }
                }
                connection.Close();
            }
            return View(clsCuentas);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit_Post(cCuentas clsCuentas)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update [DB2].[dbo].[Cuentas] SET ID='{clsCuentas.ID}', Description='{clsCuentas.Description}', ID_Origen='{clsCuentas.ID_Origen}'";
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

            cCuentas clsCuentas = new cCuentas();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From [DB2].[dbo].[Cuentas] Where ID='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsCuentas.ID = Convert.ToInt32(dataReader["ID"]);
                        clsCuentas.Description = Convert.ToString(dataReader["Description"]);
                        clsCuentas.ID_Origen = Convert.ToInt32(dataReader["ID_Origen"]);
                    }
                }
            }
            return View(clsCuentas);
        }

        //[HttpPost]//Se puede usar directo, invocando desde el view Index a través de un boton con HttpPost
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From [DB2].[dbo].[Cuentas] Where ID='{id}'";
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
        public IActionResult Create(cCuentas clsCuentas)
        {
            if (ModelState.IsValid)
            {
                string connectionString = @"Data Source=DESKTOP-B3VTHPB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into [DB2].[dbo].[Cuentas](ID, Description,ID_Origen) Values('{clsCuentas.ID}', '{clsCuentas.Description}', '{clsCuentas.ID_Origen}')";

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
