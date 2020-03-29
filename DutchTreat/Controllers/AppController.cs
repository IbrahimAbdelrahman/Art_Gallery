using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController:Controller
    {
        public IActionResult Index()
        {
            return View();

        }

        // use attribute routing method
       [HttpGet("contact")]
        public IActionResult Contact()
        {
            
            
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // send the email
            }
            else
            {
                //show the error
            }
            
            return View();
        }
        public IActionResult About()
        {
            
            return View();
        }

    }
}
