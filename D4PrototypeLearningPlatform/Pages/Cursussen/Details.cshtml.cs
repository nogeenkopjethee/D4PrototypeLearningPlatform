using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;

namespace D4PrototypeLearningPlatform.Pages.Cursussen
{
    public class DetailsModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public DetailsModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Cursus Cursus { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Cursus == null)
            {
                return NotFound();
            }

            var cursus = await _context.Cursus.FirstOrDefaultAsync(m => m.Id == id);
            if (cursus == null)
            {
                return NotFound();
            }
            else 
            {
                Cursus = cursus;
            }
            return Page();
        }
    }
}
