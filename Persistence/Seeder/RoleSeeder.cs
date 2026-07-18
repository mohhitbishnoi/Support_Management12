using Domain.Entitis;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;

public static class RoleSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Roles.AnyAsync())
            return;

        var roles = new List<Role>
        {
            new Role
            {
                Id = (int)RoleId.Admin,
                RoleName = "Admin",
                RoleDescription = "Administrator"
            },
            new Role
            {
                Id = (int)RoleId.Employee,
                RoleName = "Employee",
                RoleDescription = "Employee"
            },
            new Role
            {
                Id = (int)RoleId.Customer,
                RoleName = "Customer",
                RoleDescription = "Customer"
            }
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }
}