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

namespace D4PrototypeLearningPlatform.Pages.Cursussen
{
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

            var cursus =  await _context.Cursus.Include(x => x.Modules).FirstOrDefaultAsync(m => m.Id == id);
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

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAddModuleAsync()
        {
            Module module = new()
            {
                Name = "New Name",
            };
            //if (Cursus.Modules == null)
            //{
            //    Cursus.Modules = new List<Module>();
            //}
            //var a = _context.Module.Add(module);

            var cursus = _context.Cursus.First(x => x.Id == Cursus.Id);
            cursus.Modules.Add(module);
            //_context.Attach(Cursus).State = EntityState.Modified;
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
            // Oh what is this code bad
            cursus = await _context.Cursus.Include(x => x.Modules).FirstOrDefaultAsync(m => m.Id == Cursus.Id);
            if (cursus == null)
            {
                return NotFound();
            }
            Cursus = cursus;
            return Page();
        }

        private bool CursusExists(Guid id)
        {
          return _context.Cursus.Any(e => e.Id == id);
        }
    }
}
