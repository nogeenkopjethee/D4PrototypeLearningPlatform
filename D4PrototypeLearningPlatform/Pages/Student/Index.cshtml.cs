using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;
using D4PrototypeLearningPlatform.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Student;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext context;
    private readonly LearnService learnService;
    private readonly UserManager<ApplicationUser> userManager;
    public IList<Cursus> Cursus { get; set; } = default!;
    public IList<Tuple<bool, Cursus>> UserCursusData { get; set; } = new List<Tuple<bool, Cursus>>();



    public IndexModel(ApplicationDbContext context, LearnService learnService, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.learnService = learnService;
        this.userManager = userManager;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        if (context.Cursus != null)
        {
            Cursus = await context.Cursus.ToListAsync();

			string? userId = userManager.GetUserId(User);

            if (userId == null) { return BadRequest(); }

			UserCursusData = await learnService.GetEnrolStatusForUserAsync(Guid.Parse(userId));

        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id  == null)
        {
            return NotFound();
        }
        var result = await learnService.TryGetCursusAsync(id);
        if (result.Item1)
        {
            return BadRequest();
        }

        ApplicationUser? user = await userManager.GetUserAsync(User);

        if (user == null) { return BadRequest(); }

        return Page();
    }
}
