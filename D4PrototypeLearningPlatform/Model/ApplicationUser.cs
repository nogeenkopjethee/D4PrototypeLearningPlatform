using Microsoft.AspNetCore.Identity;

namespace D4PrototypeLearningPlatform.Model;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public JavascriptInfo JavascriptInfo { get; set; } = JavascriptInfo.None;

    //public IList<Cursus> EnroledCurses { get; set; } = new List<Cursus>();
}