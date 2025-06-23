// Tournament.Api/Extensions/ApplicationBuilderExtensions.cs
using Microsoft.EntityFrameworkCore;

using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TournamentDbContext>();

        await db.Database.MigrateAsync();

        if (await db.Tournaments.AnyAsync()) return;   //Database full Exit

        // Constant Data
        var tournaments = new List<TournamentDetails>
{
    new()   //  Auto Id
    {
        Title      = "Spring Cup",
        StartDate  = DateTime.Today.AddDays(5),

        Games = new List<Game>
        {
            new() { Title = "Lions vs Tigers", Time = DateTime.Today.AddDays(5).AddHours(14) },
            new() { Title = "Eagles vs Sharks", Time = DateTime.Today.AddDays(12).AddHours(16) }
        }
    },

    new()
    {
        Title      = "Summer League",
        StartDate  = DateTime.Today.AddDays(15),

        Games = new List<Game>
        {
            new() { Title = "Bears vs Wolves", Time = DateTime.Today.AddDays(15).AddHours(13) }
        }
    }
};

        db.Tournaments.AddRange(tournaments);
        await db.SaveChangesAsync();
    }
}
