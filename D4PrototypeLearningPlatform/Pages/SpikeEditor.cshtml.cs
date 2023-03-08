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

        public SpikeEditorModel(D4PrototypeLearningPlatform.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet( )
        {
        }

        //public async Task<IActionResult> OnGetAsync(Guid? id)
        //{
        //    if (id == null || _context.Module == null)
        //    {
        //        return NotFound();
        //    }

        //    // var module = await _context.Module.FirstOrDefaultAsync(m => m.Id == id);
        //    // if (module == null)
        //    // {
        //    //     return NotFound();
        //    // }
        //    // else 
        //    // {
        //    //     Module = module;
        //    // }
        //    return Page();
        //}
    }
}
