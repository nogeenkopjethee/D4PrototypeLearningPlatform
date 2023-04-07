using D4PrototypeLearningPlatform.Model;
using D4PrototypeLearningPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;

namespace D4PrototypeLearningPlatform.Pages;

public class UserDashboardModel : PageModel
{
    private readonly LearnService learnService;

    private readonly ILogger<UserDashboardModel> logger;

    private readonly UserManager<ApplicationUser> userManager;


    public IList<Cursus> Cursus { get; set; } = new List<Cursus>();
    public UserDashboardModel(LearnService learnService, ILogger<UserDashboardModel> logger, UserManager<ApplicationUser> userManager)
    {
        this.learnService = learnService;
        this.logger = logger;
        this.userManager = userManager;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        string? userId = userManager.GetUserId(User);

        if (userId == null)
        {
            logger.LogInformation("WTF, why did this not find the user. Oh you can get here without logging in duhh");
            return Page();
        }



        Cursus = await learnService.GetEnrolForUserAsync(Guid.Parse(userId));


        logger.LogInformation($"We got {Cursus.Count} cursus for this user!");


        return Page();
    }
}
