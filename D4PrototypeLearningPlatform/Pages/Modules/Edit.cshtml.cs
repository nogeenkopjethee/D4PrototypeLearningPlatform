using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;

namespace D4PrototypeLearningPlatform.Pages.Modules
{
    public class EditModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public EditModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Module == null)
            {
                return NotFound();
            }

            var module =  await _context.Module.FirstOrDefaultAsync(m => m.Id == id);
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

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAddOpgaveAsync()
        {
            Opgave opgave = new()
            {
                Name = "New Name",
            };
            var module = _context.Module.First(x => x.Id == Module.Id);
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
            // Oh what is this code bad
            module = await _context.Module.Include(x => x.Opgaves).FirstOrDefaultAsync(m => m.Id == Module.Id);
            if (module == null)
            {
                return NotFound();
            }
            Module = module;
            return Page();
        }

        private bool ModuleExists(Guid id)
        {
          return _context.Module.Any(e => e.Id == id);
        }
    }
}
