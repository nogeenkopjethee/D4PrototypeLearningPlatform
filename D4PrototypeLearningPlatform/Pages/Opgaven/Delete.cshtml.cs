using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Opgaven;

public class DeleteModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public DeleteModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] 
    public Opgave Opgave { get; set; } = default!;

    [FromQuery]
    [BindProperty(Name = "module", SupportsGet = true)]
    public string ModuleId { get; set; } = string.Empty;

    [FromQuery]
    [BindProperty(Name = "cursus", SupportsGet = true)]
    public string CursusId { get; set; } = string.Empty;

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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _context.Opgave == null)
        {
            return NotFound();
        }
        var opgave = await _context.Opgave.FindAsync(id);

        if (opgave != null)
        {
            Opgave = opgave;
            _context.Opgave.Remove(Opgave);
            await _context.SaveChangesAsync();
        }


        if (ModuleId == null || CursusId == null)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return Redirect($"/Modules/Edit?id={ModuleId}&cursus={CursusId}");
        }
    }
}
