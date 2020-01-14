using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aks_vote_app.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace aks_vote_app.Controllers
{
    public class HomeController : Controller
    {
        private static IConfiguration configuration;

        public HomeController(IConfiguration iconfiguration)
        {
            configuration = iconfiguration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string EndPoint = configuration.GetSection("REDIS").Value;
            var manager = new RedisManagerPool(EndPoint);
            ViewBag.value1 = 0;
            ViewBag.value2 = 0;
            string cats, dogs;
            using (var client = manager.GetClient())
            {
                cats = client.Get<string>("Cats");
                dogs = client.Get<string>("Dogs");
                if (cats != null)
                {
                    ViewBag.value1 = cats;
                }
                if (dogs != null)
                {
                    ViewBag.value2 = dogs;
                }                
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Index(string vote)
        {
            string EndPoint = configuration.GetSection("REDIS").Value;
            var manager = new RedisManagerPool(EndPoint);
            string cats, dogs;
            ViewBag.value1 = 0;
            ViewBag.value2 = 0;
            if (vote.Equals("reset"))            {
                
                using (var client = manager.GetClient())
                {
                    client.Set("Cats", 0);
                    client.Set("Dogs", 0);
                }
            }
            else
            {
                using (var client = manager.GetClient())
                {
                    client.IncrementValue(vote);
                    cats = client.Get<string>("Cats");
                    dogs = client.Get<string>("Dogs");
                    if (cats != null)
                    {
                        ViewBag.value1 = cats;
                    }
                    if (dogs != null)
                    {
                        ViewBag.value2 = dogs;
                    }
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
