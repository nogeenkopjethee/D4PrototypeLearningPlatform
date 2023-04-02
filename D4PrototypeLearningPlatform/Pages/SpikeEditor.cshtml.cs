using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using D4PrototypeLearningPlatform.Data;
using Microsoft.EntityFrameworkCore;
using D4PrototypeLearningPlatform.Model;

namespace D4PrototypeLearningPlatform.Pages
{
    public class SpikeEditorModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;


        public Opgave? Opgave { get; set; }

        public SpikeEditorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //public void OnGet( )
        //{
        //    Opgave = new()
        //    {
        //        Id = Guid.Empty,
        //        Description = "Fallback Description",
        //        Name = "Example Name",
        //        InitialCode = "const test = \"Hello World\";",
        //        Type = ProgrammingLanguage.Javascript,
        //    };
        //}

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
            }
            return Page();
        }
    }
}
