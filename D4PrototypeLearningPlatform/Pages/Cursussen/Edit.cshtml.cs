using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Cursussen;

public class EditModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

    public EditModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
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

        var cursus = await _context.Cursus.Include(x => x.Modules).FirstOrDefaultAsync(m => m.Id == id);
        if (cursus == null)
        {
            return NotFound();
        }
        Cursus = cursus;
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

        _context.Attach(Cursus).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CursusExists(Cursus.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Redirect($"./Edit?Id={Cursus.Id}");
    }

    public async Task<IActionResult> OnPostAddModuleAsync()
    {
        Module module = new()
        {
            Name = "New Name",
        };

        var cursus = _context.Cursus.First(x => x.Id == Cursus.Id);
        cursus.Modules.Add(module);

        _context.Cursus.Update(cursus);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // this is not right
            if (!CursusExists(cursus.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return Redirect($"./Edit?Id={Cursus.Id}");
    }

    private bool CursusExists(Guid id)
    {
        return _context.Cursus.Any(e => e.Id == id);
    }
}
