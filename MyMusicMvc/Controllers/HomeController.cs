using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyMusicMvc.Models;
using MyMusicMvc.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyMusicMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _Config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
          
            _logger = logger;
            _Config = config;
        }
        
        /*
         * cette fonction doit renvoyer List
         */
        public async Task<IActionResult >Index()
        {
            var listMusicViewModel = new ListMusicViewModel();
            var listMusic = new List<Music>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(URlBase + "Music"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listMusic = JsonConvert.DeserializeObject<List<Music>>(apiResponse);
                }
            }
            listMusicViewModel.ListMusic = listMusic;
            return View(listMusicViewModel);
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

        private string URlBase
        {
            get
            {
                return _Config.GetSection("BaseURl").GetSection("URL").Value;
            }
        }
    }
}
