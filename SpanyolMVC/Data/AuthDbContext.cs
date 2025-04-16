using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SpanyolMVC.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seeding roles
        var adminRoleId = "f07f938d-6ad3-4ed9-1179-08da6959dddd";
        var managerRoleId = "d4e5f678-90ab-cdef-1234-567890abcd12";
        var userRoleId = "b2c3d4e5-f678-90ab-cdef-1234567890ab";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole
            {
                Name = "Manager",
                NormalizedName = "MANAGER",
                Id = managerRoleId,
                ConcurrencyStamp = managerRoleId
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);

        // Seeding admin user
        var adminId = "e5f67890-abcd-ef12-3456-7890abcdef12";
        var adminUser = new IdentityUser()
        {
            UserName = "admin@spanyolmvc.com",
            Email = "admin@spanyolmvc.com",
            NormalizedEmail = "ADMIN@SPANYOLMVC.COM",
            NormalizedUserName = "ADMIN@SPANYOLMVC.COM",
            Id = adminId,
            EmailConfirmed = true
        };
        adminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(adminUser, "12345");

        builder.Entity<IdentityUser>().HasData(adminUser);

        // Assign all roles to the admin user
        var adminRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            },
            new IdentityUserRole<string>
            {
                RoleId = managerRoleId,
                UserId = adminId
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = adminId
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        
        // Seeding a new hardcoded user
        var newUserId = "a1b2c3d4-e5f6-7890-abcd-ef1234567890";
        var newUser = new IdentityUser()
        {
            UserName = "newuser@spanyolmvc.com",
            Email = "newuser@spanyolmvc.com",
            NormalizedEmail = "NEWUSER@SPANYOLMVC.COM",
            NormalizedUserName = "NEWUSER@SPANYOLMVC.COM",
            Id = newUserId,
            EmailConfirmed = true
        };
        newUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(newUser, "password123");

        builder.Entity<IdentityUser>().HasData(newUser);

        // Optionally assign roles to the new user
        var newUserRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = userRoleId, // Assuming "User" role already exists
                UserId = newUserId
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(newUserRoles);
    }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}