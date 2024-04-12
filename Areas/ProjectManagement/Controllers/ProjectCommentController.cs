using COMP2139_Labs.Areas.ProjectManagement.Models;
using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace COMP2139_Labs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ProjectCommentController : Controller
    {

        private ApplicationDbContext _context;

        public ProjectCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int projectId)
        {
            var comments = await _context.comments
                .Where(c => c.ProjectId == projectId)
                .OrderByDescending(c => c.DatePosted)
                .ToListAsync();

            return Json(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] ProjectComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DatePosted = DateTime.Now;
                await _context.comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Comment added successfuly" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid comment data", error = errors });
        }

    }
}
