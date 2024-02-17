using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp2139_labs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comp2139_labs.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult Index(int projectId)
        {
            var tasks = _context.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .ToList();

            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        [HttpGet]

        public IActionResult Details(int id)
        {
            var task = _context.ProjectTasks
                      .Include(t => t.Project)
                      .FirstOrDefault(task => task.ProjectId == id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpGet]

        public IActionResult Create(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var task = new ProjectTask
            {
                ProjectId = projectId
            };
            return View(task);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost]

        public IActionResult Edit(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefault(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (id != task.ProjectTaskId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);

        }
        [HttpPost]

        public IActionResult Delete(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefault(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost, ActionName("DeleteConfirmed")]

        public IActionResult DeleteConfirmed(int ProjectTaskId)
        {
            var task = _context.ProjectTasks.Find(ProjectTaskId);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });

            }
            return NotFound();
        }


        public async Task<IActionResult> Search(int? projectId, string searchString)
        {
            var taskQuery = _context.ProjectTasks.AsQueryable();
            bool searchPerformed = !String.IsNullOrEmpty(searchString);

            if (projectId.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.ProjectId == projectId);

            }
            if(searchPerformed)
            {
                taskQuery = taskQuery.Where(t => t.Title.Contains(searchString)
                                              || t.Description.Contains(searchString));
            }

            var tasks = await taskQuery.ToListAsync();

            ViewBag.ProjectId = projectId;
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return NotFound();

        }
    }

}

