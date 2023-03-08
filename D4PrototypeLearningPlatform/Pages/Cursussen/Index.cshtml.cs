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
    public class IndexModel : PageModel
    {
        private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext _context;

        public IndexModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cursus> Cursus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cursus != null)
            {
                Cursus = await _context.Cursus.ToListAsync();
            }
        }
    }
}
