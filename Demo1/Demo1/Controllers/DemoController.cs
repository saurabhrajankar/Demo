using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using Microsoft.Data.SqlClient;

namespace Demo1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : Controller
    {
        private readonly IConfiguration iconfiguration;

        public DemoController(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        [HttpPost]
        public IActionResult AddEmployee(Model model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(iconfiguration.GetConnectionString("newdata")))
                {
                    SqlCommand cmd = new SqlCommand("adda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", model.name);
                    cmd.Parameters.AddWithValue("@city", model.city);
                    cmd.Parameters.AddWithValue("@mobileno", model.mobileno);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();         
                    con.Close();

                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
