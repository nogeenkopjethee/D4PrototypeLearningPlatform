using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;

namespace D4PrototypeLearningPlatform.Pages.Opgaven
{
    public class DeleteModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public DeleteModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Opgave Opgave { get; set; }

        [BindProperty]
        public string ModuleId { get; set; } = string.Empty;

        [BindProperty]
        public string CursusId { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(Guid? id, string? cursus = null, string? module = null)
        {
            if (cursus != null) { CursusId = cursus; }
            if (module != null) { ModuleId = module; }
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

        public async Task<IActionResult> OnPostAsync(Guid? id, string? cursus = null, string? module = null)
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


            if (module == null || cursus == null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Redirect($"/Modules/Edit?id={module}&cursus={cursus}");
            }
        }
    }
}
