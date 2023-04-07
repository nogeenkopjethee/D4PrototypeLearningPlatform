using D4PrototypeLearningPlatform.Model;
using D4PrototypeLearningPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D4PrototypeLearningPlatform.Pages.Student;

[Authorize]
public class ListModel : PageModel
{
    private readonly LearnService learnService;

    private readonly UserManager<ApplicationUser> userManager;

    public Cursus Cursus { get; set; } = default!;
    public ListModel (LearnService learnService, UserManager<ApplicationUser> userManager)
    {
        this.learnService = learnService;
        this.userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        string? userId = userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest();
        }
        var result = await learnService.TryGetCursusAsync(id);

        if (result.Item1)
        {
            Cursus = result.Item2;
            return Page();
        }

        return NotFound();
    }
}
