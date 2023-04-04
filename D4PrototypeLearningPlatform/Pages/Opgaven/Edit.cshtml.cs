using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D4PrototypeLearningPlatform.Data;

namespace D4PrototypeLearningPlatform.Pages.Opgaven
{
    public class EditModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public EditModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Opgave Opgave { get; set; } = default!;

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

            var opgave =  await _context.Opgave.FirstOrDefaultAsync(m => m.Id == id);
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
}
