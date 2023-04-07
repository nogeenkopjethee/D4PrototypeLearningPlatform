using D4PrototypeLearningPlatform.Data;
using D4PrototypeLearningPlatform.Model;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Services;

public class LearnService
{
    private readonly ILogger<LearnService> logger;
    private readonly D4PrototypeLearningPlatform.Data.ApplicationDbContext context;

    public LearnService(ApplicationDbContext context, ILogger<LearnService> logger)
    {
        this.context = context;
        this.logger = logger;
    }



    public async Task EnrolCursusAsync(Guid UserId, Guid CursusId)
    {
        bool res = await context.EnroledCurses.Where(x => x.UserId == UserId && x.CursusId == CursusId).AnyAsync();

        if (!res)
        {

            context.EnroledCurses.Add(new EnroledCurses() { CursusId = CursusId, UserId = UserId });
            context.SaveChanges();
        }
    }


    public async Task<IList<Cursus>> GetEnrolForUserAsync(Guid userId)
    {
		var resultEnroledCurses = await context.EnroledCurses.Where(x => x.UserId == userId).ToListAsync();
        IList<Cursus> result = new List<Cursus>();


        // I fergot how to do this the fast way, so replace this later (it won't scale well) or just cache the results!
        foreach (var item in resultEnroledCurses)
        {
            Cursus? r = await context.Cursus
                .Include(c => c.Modules)
                .ThenInclude(module => module.Opgaves).Where(x => x.Id == item.CursusId).FirstOrDefaultAsync();

            if (r != null)
            {
                result.Add(r);
            }
            else
            {
                logger.LogError("HOW WAS THIS NULL WTF");
            }
        }

        return result;
	}

    public async Task<List<Tuple<bool, Cursus>>> GetEnrolStatusForUserAsync(Guid UserId)
    {
        List<EnroledCurses> resultEnroledCurses = await context.EnroledCurses.Where(x => x.UserId == UserId).ToListAsync();
        var resultCursus = await context.Cursus.ToListAsync();

        List<Tuple<bool, Cursus>> result = new();

        foreach (var item in resultCursus)
        {
            bool isFound = false;
            foreach (var itemEnroled in resultEnroledCurses)
            {
                if (item.Id == itemEnroled.CursusId)
                {
                    isFound = true;
                }
            }
            result.Add(new(isFound, item));
        }


        return result;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Guid of Cursus!</param>
    /// <returns>A Tuple with bool for if the result was good, and cursus with a result or default value!</returns>
    public async Task<Tuple<bool, Cursus>> TryGetCursusAsync(Guid? id)
    {
        bool result = true;
        Cursus? cursus;

        if (id == null) { result = false; }
        if (context == null) {
            logger.LogWarning("TryGetCursusAsync context was null");
            return new(false,default!); 
        }

        cursus = await context.Cursus
            .Include(x => x.Modules)
            .ThenInclude(module => module.Opgaves)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (cursus == null) 
        { 
            logger.LogWarning("TryGetCursusAsync could not be found");
            result = false;
            cursus = default!;
        }

        return new (result, cursus);
    }
}
