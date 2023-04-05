using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Opgaven;

public class IndexModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public IndexModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Opgave> Opgave { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Opgave != null)
        {
            Opgave = await _context.Opgave.ToListAsync();
        }
    }
}
