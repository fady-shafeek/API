using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TaskDay2.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            HttpResponseMessage Response = client.GetAsync("http://localhost:51375/api/company").Result;
            string Message = Response.Content.ReadAsStringAsync().Result;
            ViewBag.Message = JsonConvert.DeserializeObject<List<Employee>>(Message);
            return View();
        }

        public ActionResult About()
        {
            HttpResponseMessage Response = client.GetAsync("http://localhost:51375/api/company").Result;
            string Message = Response.Content.ReadAsStringAsync().Result;
            List<Employee> Emps = JsonConvert.DeserializeObject<List<Employee>>(Message);
            return View(Emps);
        }

        public ActionResult Contact()
        {
            Employee Dept = new Employee() { ID = 100, Name = "Sandra", Age = 22 };
            string Data = JsonConvert.SerializeObject(Dept);
            StringContent RequestBody = new StringContent(Data, Encoding.UTF8, "application/Json"); 
            HttpResponseMessage Response = client.PostAsync("http://localhost:51375/api/company", RequestBody).Result;
            if (Response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Added";
            }
            else
            {
                ViewBag.Message = "Not Added";
            }
            return View();
        }
    }
}