using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<BoardTask> Tasks { get; set; }

        public async Task OnGetAsync()
        {
            // ��������� ������ �� ������� ������ ������ �������
            Tasks = await _context.BoardTasks
                .OrderBy(t => t.Status)
                .ThenBy(t => t.Position)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddTask(string title, string status)
        {
            // �������� ������� ���������� ����� � ������� �������
            int position = await _context.BoardTasks
                .CountAsync(t => t.Status == status);

            var newTask = new BoardTask
            {
                Title = title,
                Description = "",
                Status = status,
                Position = position // ������������� ������� = ������� ���������� �����
            };

            _context.BoardTasks.Add(newTask);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveTask(int taskId, string newStatus)
        {
            var task = await _context.BoardTasks.FindAsync(taskId);
            if (task != null)
            {
                // �������� ����� ������� � ������� �������
                int newPosition = await _context.BoardTasks
                    .CountAsync(t => t.Status == newStatus);

                task.Status = newStatus;
                task.Position = newPosition;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
