using COMP2139_Labs.Areas.ProjectManagement.Models;
using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]

    public class TaskController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TaskController(ApplicationDbContext context)
		{
			_context = context;
		}

        [HttpGet("")]
        public async Task<IActionResult> Index(int projectId)
		{
			var tasks = await _context.tasks
				.Where(t => t.ProjectId == projectId)
				.ToListAsync();
			ViewBag.ProjectId = projectId;

			return View(tasks);
		}
        [HttpGet("Details/{id}")]

        public async Task<IActionResult> Details(int id)
		{
			var task = await _context.tasks
				.Include(t => t.Project)
				.FirstOrDefaultAsync(t => t.ProjectTaskId == id);
			if (task == null)
			{
				return NotFound();
			}
			return View(task);
		}

        [HttpGet("Create")]

        public async Task<IActionResult> Create(int projectId)
		{
			var project = await _context.projects.FindAsync(projectId);
			if (project == null)
			{
				return NotFound();
			}
			var task = new ProjectTask
			{
				ProjectId = projectId,
			};
			return View(task);
		}

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
		{
			if (ModelState.IsValid)
			{
				await _context.tasks.AddAsync(task);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", new { task.ProjectId });
			}

			var projects = await _context.projects.ToListAsync();

			ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", task.ProjectId);
			return View(task);
		}
        [HttpGet("Edit/{id}")]

        public async Task<IActionResult> Edit(int id)
		{

			var task = await _context.tasks
				.Include(t => t.Project)
				.FirstOrDefaultAsync(t => t.ProjectTaskId == id);
			if (task == null)
			{
				return NotFound();
			}
			ViewBag.Projects = new SelectList(await _context.projects.ToListAsync(), "ProjectId", "Name", task.ProjectId);
			return View(task);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
		{
			if (id != task.ProjectTaskId)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				_context.tasks.Update(task);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", new { task.ProjectId });
			}
			return View(task);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var task = await _context.tasks
				.Include(t => t.Project)
				.FirstOrDefaultAsync(t => t.ProjectTaskId == id);
			if (task == null)
			{
				return NotFound();
			}
			ViewBag.Projects = new SelectList(await _context.projects.ToListAsync(), "ProjectId", "Name", task.ProjectId);
			return View(task);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var task = _context.tasks.Find(id);
			if (task != null)
			{
				_context.tasks.Remove(task);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", new { task.ProjectId });
			}
			return NotFound();
		}

		public async Task<IActionResult> Search(int? projectId, string searchString)
		{
			var taskQuery = _context.tasks.AsQueryable();
			bool searchPerformed = !string.IsNullOrEmpty(searchString);

			if (projectId.HasValue)
			{
				taskQuery = taskQuery.Where(t => t.ProjectId == projectId);
			}

			if (searchPerformed)
			{
				taskQuery = taskQuery.Where(t => t.Title.Contains(searchString)
											  || t.Description.Contains(searchString));
			}

			var tasks = await taskQuery.ToListAsync();
			ViewBag.ProjectId = projectId;
			ViewData["searchPerformed"] = searchPerformed;
			ViewData["searchString"] = searchString;

			return View("Index", tasks);

		}
	}
}
