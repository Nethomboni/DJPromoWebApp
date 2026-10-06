using DJPromoWebApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register the DbContext. On Azure the "DJContext" connection string comes from
// App Service > Environment variables > Connection strings (it overrides appsettings.json).
builder.Services.AddDbContext<DJContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DJContext"),
        sql => sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));

var app = builder.Build();

// Create the database/tables and seed data. Wrapped in try/catch so a database problem
// shows up in the logs instead of crashing the whole site (which Azure reports as 503).
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<DJContext>();
        context.Database.EnsureCreated();
        DbInitializer.Seed(context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database setup failed. Check the DJContext connection string and SQL firewall rules.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
