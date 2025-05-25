using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public class TaskActionsModel
        {
            public int TaskId { get; set; }
            public string CurrentStatus { get; set; }
        }

        public List<BoardTask> Tasks { get; set; }

        public async Task OnGetAsync()
        {
            Tasks = await _context.BoardTasks
                .OrderBy(t => t.Status)
                .ThenBy(t => t.Position)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddTask(
        [FromForm] string title,
        [FromForm] string description,
        [FromForm] string status)
        {
            var cleanDescription = (description ?? "")
                .Replace("&nbsp;", " ")
                .Trim();


            var newTask = new BoardTask
            {
                Title = title,
                Description = description ?? string.Empty,
                Status = status,
                Position = _context.BoardTasks.Count(t => t.Status == status)
            };

            _context.BoardTasks.Add(newTask);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Received: {title} | {description} | {status}");
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveTask(int taskId, string newStatus)
        {
            var task = await _context.BoardTasks.FindAsync(taskId);
            if (task != null)
            {
                int newPosition = await _context.BoardTasks
                    .CountAsync(t => t.Status == newStatus);

                task.Status = newStatus;
                task.Position = newPosition;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteTask(int taskId)
        {
            var task = await _context.BoardTasks.FindAsync(taskId);
            if (task != null)
            {
                _context.BoardTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
