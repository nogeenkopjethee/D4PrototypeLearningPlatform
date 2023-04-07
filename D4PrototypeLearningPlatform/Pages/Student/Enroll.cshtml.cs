using D4PrototypeLearningPlatform.Model;
using D4PrototypeLearningPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Pages.Student;

public class EnrollModel : PageModel
{
    private readonly LearnService learnService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<EnrollModel> logger;

    public EnrollModel(LearnService learnService, UserManager<ApplicationUser> userManager, ILogger<EnrollModel> logger)
    {
        this.learnService = learnService;
        this.userManager = userManager;
        this.logger = logger;
    }

    public Cursus Cursus { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        Tuple<bool, Cursus> result = await learnService.TryGetCursusAsync(id);

        if (result.Item1)
        {
            Cursus = result.Item2;
            return Page();
        }

        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        Tuple<bool, Cursus> result = await learnService.TryGetCursusAsync(id);
        if (!result.Item1)
        {
            logger.LogWarning("No result!");
            return BadRequest();
        }

        string? userId = userManager.GetUserId(User);
        if (userId == null) { return BadRequest(); }
        await learnService.EnrolCursusAsync(Guid.Parse(userId), id);

        //ApplicationUser? user = await userManager.GetUserAsync(User);


        //if (user.EnroledCurses.Contains(result.Item2))
        //{
        //    logger.LogError($"User tried to re enroll for a already inrolled course! user='{user.Id}', course='{id}'");
        //    return BadRequest();
        //}

        //user.EnroledCurses.Add(result.Item2);

        //await userManager.UpdateAsync(user);

        return RedirectToPage("./Index");
    }
}
