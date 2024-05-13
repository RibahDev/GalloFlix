using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RibahFlix.Models;

namespace RibahFlix.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {   
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Configuracão de Muitos para Muitos do MovieGenre
        builder.Entity<MovieGenre>().HasKey(
            mg => new { mg.MovieId, mg.GenreId }
        );

        builder.Entity<MovieGenre>()
        .HasOne(mg => mg.Movie)
        .WithMany(m => m.Genres)
        .HasForeignKey(mg => mg.MovieId);

        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(mg => mg.Movies)
            .HasForeignKey(mg => mg.GenreId);
        #endregion


        #region Popular usuário
        List<IdentityRole> roles = new() 
        {
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },

            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Usuário",
                NormalizedName = "USUÁRIO"
            }

        };
        builder.Entity<IdentityRole>().HasData(roles);

        List<IdentityUser> users = new()
        {
             new IdentityUser()
             {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@ribahflix.com",
                NormalizedEmail = "ADMIN@RIBAHFLIX.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                LockoutEnabled = false,
                EmailConfirmed = true
             },
            new IdentityUser()
             {
                Id = Guid.NewGuid().ToString(),
                Email = "user@ribahflix.com",
                NormalizedEmail = "USER@HOTMAIL.COM",
                UserName = "user",
                NormalizedUserName = "USER",
                LockoutEnabled = true,
                EmailConfirmed = true
             }

        };
        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass= new();
            user.PasswordHash = pass.HashPassword(user, "@Etec123");
        }
        builder.Entity<IdentityUser>().HasData(users);

        List<AppUser> appUsers = new() 
        {
            new AppUser()
            {
                AppUserId = users[0].Id,
                Name = "Ribah",
                Birthday = DateTime.Parse("17/04/2000"),
                Photo = ""
            },
             new AppUser()
            {
                AppUserId = users[1].Id,
                Name = "Fulaninho",
                Birthday = DateTime.Parse("21/11/2008"),
                Photo = ""
            }
            
        };
        builder.Entity<AppUser>().HasData(appUsers);

        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>()
            { 
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>()
            { 
                UserId = users[0].Id,
                RoleId = roles[1].Id
            },
            new IdentityUserRole<string>()
            { 
                UserId = users[1].Id,
                RoleId = roles[1].Id
            }
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }
}