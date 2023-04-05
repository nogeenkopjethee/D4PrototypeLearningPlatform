using D4PrototypeLearningPlatform.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace D4PrototypeLearningPlatform.Data;

public class DefaultDatabaseData
{
	public static async Task InitializeAsync(IServiceProvider serviceProvider)
	{
		var context = serviceProvider.GetService<ApplicationDbContext>();

		if (context == null)
		{
			throw new ArgumentNullException(nameof(context));
		}


		var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

		if (roleManager == null)
			throw new ArgumentNullException(nameof(roleManager));


		// List of default roles!
		string[] roles = new string[] { "Owner", "Administrator", "Teacher" };

		foreach (string role in roles)
		{
			var roleStore = new RoleStore<IdentityRole>(context);

			if (!context.Roles.Any(r => r.Name == role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
				//await roleStore.CreateAsync(new IdentityRole(role));
			}
		}

#if DEBUG
		var user = new ApplicationUser
		{
			Email = "Admin@example.com",
			NormalizedEmail = "ADMIN@EXAMPLE.COM",
			UserName = "Admin",
			NormalizedUserName = "ADMIN",
			//PhoneNumber = "+111111111111",
			EmailConfirmed = true,
			//PhoneNumberConfirmed = true,
			SecurityStamp = Guid.NewGuid().ToString("D") // no clue
		};


		if (!context.Users.Any(u => u.UserName == user.UserName))
		{
			var password = new PasswordHasher<ApplicationUser>();
			var hashed = password.HashPassword(user, "secret");
			user.PasswordHash = hashed;

			var userStore = new UserStore<ApplicationUser>(context);
			var result = userStore.CreateAsync(user);

		}

		await AssignRoles(serviceProvider, user.Email, roles);
#endif
		context.SaveChangesAsync();
	}

	public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
	{
		UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
		ApplicationUser user = await _userManager.FindByEmailAsync(email);
		var result = await _userManager.AddToRolesAsync(user, roles);

		return result;
	}
}
