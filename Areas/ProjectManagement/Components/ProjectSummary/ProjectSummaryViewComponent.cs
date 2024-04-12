using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Components.ProjectSummary
{
    public class ProjectSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectID)
        {
            var project = await _context.projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(project => project.ProjectId == projectID);

            if (project == null)
            {
                // Handle the case when the project is not found, return html content
                return Content("Project not found");
            }

            return View(project);

        }
    }
}
