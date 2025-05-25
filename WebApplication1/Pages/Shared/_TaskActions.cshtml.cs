using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.Shared
{
    public class _TaskActionsModel : PageModel
    {
        public class TaskActionsModel
        {
            public int TaskId { get; set; }
            public string CurrentStatus { get; set; }
        }
    }
}
