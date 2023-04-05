using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Opgaven;

public class EditModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public EditModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
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
        Opgave = opgave;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Opgave).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OpgaveExists(Opgave.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }


        // FIXME: we could do better for this!
        if (string.IsNullOrEmpty(CursusId) || string.IsNullOrEmpty(ModuleId))
        {
            return RedirectToPage("./Index");
        }
        return Redirect($"./Edit?Id={Opgave.Id}&module={ModuleId}&cursus={CursusId}");
    }

    private bool OpgaveExists(Guid id)
    {
        return _context.Opgave.Any(e => e.Id == id);
    }
}
