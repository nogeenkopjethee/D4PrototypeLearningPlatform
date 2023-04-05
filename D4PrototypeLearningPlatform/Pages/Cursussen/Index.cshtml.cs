using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Cursussen;

public class IndexModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public IndexModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Cursus> Cursus { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Cursus != null)
        {
            Cursus = await _context.Cursus.ToListAsync();
        }
    }
}
