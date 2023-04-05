using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Opgaven;

public class DetailsModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public DetailsModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public Opgave Opgave { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _context.Opgave == null)
        {
            return NotFound();
        }

        var opgave = await _context.Opgave.FirstOrDefaultAsync(m => m.Id == id);
        if (opgave == null)
        {
            return NotFound();
        }
        else
        {
            Opgave = opgave;
        }
        return Page();
    }
}
