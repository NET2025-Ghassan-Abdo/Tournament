using Tournament.Data.Data;

namespace Tournament.Api.Extentions
{
    public static  class ApplicationBuilderExtensions
    {
        public static async Task SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TournamentApiContext>();
        }
    }
}
