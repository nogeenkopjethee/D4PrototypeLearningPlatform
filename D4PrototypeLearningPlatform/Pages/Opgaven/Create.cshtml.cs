using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using D4PrototypeLearningPlatform.Data;

namespace D4PrototypeLearningPlatform.Pages.Opgaven
{
    public class CreateModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public CreateModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Opgave Opgave { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Opgave.Add(Opgave);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
