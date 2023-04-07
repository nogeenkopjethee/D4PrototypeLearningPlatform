using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace D4PrototypeLearningPlatform.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<project>().HasMany(i => i.feature).WithMany(); // We could use this if the relation is not working like we would like.





        base.OnModelCreating(builder);
    }

    public DbSet<Module> Module { get; set; }
    public DbSet<Cursus> Cursus { get; set; }
    public DbSet<Opgave> Opgave { get; set; }
    public DbSet<UserProgress> UserProgress { get; set; }

    public DbSet<EnroledCurses> EnroledCurses { get; set; }
}