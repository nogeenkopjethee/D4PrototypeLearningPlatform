using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Cursussen;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Cursus Cursus { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _context.Cursus == null)
        {
            return NotFound();
        }
        // 
        var cursus = await _context.Cursus.FirstOrDefaultAsync(m => m.Id == id);

        if (cursus == null)
        {
            return NotFound();
        }
        else
        {
            Cursus = cursus;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _context.Cursus == null)
        {
            return NotFound();
        }
        // findasync is not working with the include statement for no reason!
        var cursus = await _context.Cursus.Include(x => x.Modules).FirstOrDefaultAsync(m => m.Id == id);

        if (cursus != null)
        {
            Cursus = cursus;
            _context.Cursus.Remove(Cursus);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
