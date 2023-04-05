using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Modules;

public class DetailsModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public DetailsModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public Module Module { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _context.Module == null)
        {
            return NotFound();
        }

        var module = await _context.Module.FirstOrDefaultAsync(m => m.Id == id);
        if (module == null)
        {
            return NotFound();
        }
        else
        {
            Module = module;
        }
        return Page();
    }
}
