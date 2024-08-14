using System.Text.Json.Serialization;
using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
    public class ProductController : Controller
    {
        // Uri baseAddress = new Uri("http://172.16.200.202");
        private readonly HttpClient _client;
        public ProductController()
        {
            _client = new HttpClient();
            // _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // List<Ward> wardList = new List<Ward>();
            // HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/api/Hos/Get").Result;

            // if(respone.IsSuccessStatusCode)
            // {
            //     string data = respone.Content.ReadAsStringAsync().Result;
            //     wardList = JsonConvert.DeserializeObject<List<Ward>>(data);
            // }
            // return View(wardList);
            IEnumerable<Ward> hos = null;
            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://172.16.200.202:1380");

                var responseTask = client.GetAsync("/api/Hos/Get");
                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Ward>>();
                    readTask.Wait();

                    hos = readTask.Result;
                }
                else
                {
                    hos = Enumerable.Empty<Ward>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return View(hos);
            }
        }
        
        // public IActionResult Create()
        // {
        //     return View();
        // }
        [HttpGet]
        public ActionResult Create([FromQuery] string paraHN)
        {
            IEnumerable<ProductViewModel> patient = null;

            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://172.16.200.202:1380");

                var responseTask = client.GetAsync($"/api/Hos/getHos?paraHN={paraHN}");
                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductViewModel>>();
                    readTask.Wait();

                    patient = readTask.Result;
                }
                else
                {
                    patient = Enumerable.Empty<ProductViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return View(patient);
            }
        }
    }
}