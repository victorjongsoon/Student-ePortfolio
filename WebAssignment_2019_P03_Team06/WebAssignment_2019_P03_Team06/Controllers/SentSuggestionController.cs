using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAssignment_2019_P03_Team06.Models;

namespace WebAssignment_2019_P03_Team06.Controllers
{
    public class SentSuggestionController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}