using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Modules;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // https://localhost:7117/Cursussen/Edit?id=9f674a56-5f98-4c97-97fd-51efdc36f73e
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

        var module = await _context.Module.Include(x => x.Opgaves).FirstOrDefaultAsync(m => m.Id == id);
        if (module == null)
        {
            return NotFound();
        }
        Module = module;
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

        _context.Attach(Module).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ModuleExists(Module.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        if (string.IsNullOrEmpty(CursusId))
        {
            return Redirect($"./Edit?Id={Module.Id}");
        }
        return Redirect($"./Edit?Id={Module.Id}&cursus={CursusId}");
    }

    public async Task<IActionResult> OnPostAddOpgaveAsync()
    {
        Opgave opgave = new()
        {
            Name = "New Name",
        };
        var module = _context.Module.Include(m => m.Opgaves).First(x => x.Id == Module.Id);
        module.Opgaves.Add(opgave);

        _context.Module.Update(module);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // this is not right
            if (!ModuleExists(Module.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        if (string.IsNullOrEmpty(CursusId))
        {
            return Redirect($"./Edit?Id={Module.Id}");
        }
        return Redirect($"./Edit?Id={Module.Id}&cursus={CursusId}");
    }

    private bool ModuleExists(Guid id)
    {
        return _context.Module.Any(e => e.Id == id);
    }
}
