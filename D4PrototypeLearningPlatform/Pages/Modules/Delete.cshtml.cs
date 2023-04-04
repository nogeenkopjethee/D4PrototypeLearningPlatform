using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;
using Microsoft.CodeAnalysis.Differencing;

namespace D4PrototypeLearningPlatform.Pages.Modules
{
    public class DeleteModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public DeleteModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; }

        [BindProperty]
        public string CursusId { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(Guid? id, string? cursus = null)
        {
            if (cursus != null) { CursusId = cursus; }
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

        public async Task<IActionResult> OnPostAsync(Guid? id, string? cursus = null)
        {
            if (id == null || _context.Module == null)
            {
                return NotFound();
            }
            var module = await _context.Module.FindAsync(id);

            if (module != null)
            {
                Module = module;
                _context.Module.Remove(Module);
                await _context.SaveChangesAsync();
            }
            if (cursus == null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Redirect("/Cursussen/Edit?id=" + cursus);
            }
        }
    }
}
