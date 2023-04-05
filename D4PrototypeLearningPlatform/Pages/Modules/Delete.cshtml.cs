using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Modules;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Module Module { get; set; } = default!;

    [FromQuery]
    [BindProperty(Name = "cursus", SupportsGet = true)]
    public string CursusId { get; set; } = string.Empty;

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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _context.Module == null)
        {
            return NotFound();
        }
        var module = await _context.Module.Include(x => x.Opgaves).FirstOrDefaultAsync(m => m.Id == id);

        if (module != null)
        {
            Module = module;
            _context.Module.Remove(Module);
            await _context.SaveChangesAsync();
        }
        if (CursusId == null)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return Redirect("/Cursussen/Edit?id=" + CursusId);
        }
    }
}
