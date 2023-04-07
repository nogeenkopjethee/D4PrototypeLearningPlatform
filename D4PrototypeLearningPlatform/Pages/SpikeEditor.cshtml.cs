using D4PrototypeLearningPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages;

public class SpikeEditorModel : PageModel
{
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;


    public Opgave Opgave { get; set; } = default!;

    public bool EnableInBrowserCodeRunner { get; set; } = false;


	[FromQuery]
	[BindProperty(Name = "cursus", SupportsGet = true)]
	public string CursusId { get; set; } = string.Empty;


	public SpikeEditorModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _context.Opgave == null)
        {
            Opgave = new()
            {
                Id = Guid.Empty,
                Description = "Fallback Description",
                Name = "Example Name",
                InitialCode = """const test = "Hello World";""",
                Type = ProgrammingLanguage.Javascript,
            };

            EnableInBrowserCodeRunner = true;

            return Page();
        }

        var opgave = await _context.Opgave.FirstOrDefaultAsync(m => m.Id == id);
        if (opgave == null)
        {
            return NotFound();
        }
        else
        {
            Opgave = opgave;
            if (opgave.Type == ProgrammingLanguage.Javascript)
            {
				EnableInBrowserCodeRunner = true;
			}
        }
        return Page();
    }
}
