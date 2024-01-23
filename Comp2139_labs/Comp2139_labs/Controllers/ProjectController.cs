using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp2139_labs.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comp2139_labs.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        // GET: /<controller>/
        public IActionResult Index()
        {
            var projects = new List<Project>()
            {
                new Project { ProjectId = 1, Name= "Project 1" , Description = "First Project"}
            };
            return View(projects);
        }
    }
}

