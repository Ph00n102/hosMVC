using System.Text.Json.Serialization;
using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5094/api");
        private readonly HttpClient _client;
        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Ward> wardList = new List<Ward>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Hos/Get").Result;

            if(respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                wardList = JsonConvert.DeserializeObject<List<Ward>>(data);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            return View();
        }
    }
}