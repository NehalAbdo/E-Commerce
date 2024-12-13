using E_Commerce.Repository.Data;
using Microsoft.EntityFrameworkCore;

public class Program {
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Services

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DataContext>(o =>
        {
            o.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
        });
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        

        #endregion
        var app = builder.Build();
        await InitializeDbAsync(app);


        #region Pipelines
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.MapControllers();

        app.Run();
        #endregion
    }

    private static async Task InitializeDbAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = service.GetRequiredService<DataContext>();
                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                    await context.Database.MigrateAsync();
                await DataContextSeed.SeedDataAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message);
            }
        }
    }
}