using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Modules;

public class IndexModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public IndexModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Module> Module { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Module != null)
        {
            Module = await _context.Module.ToListAsync();
        }
    }
}
