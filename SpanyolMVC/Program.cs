using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Data;
using SpanyolMVC.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using SpanyolMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var identityConnectionString = builder.Configuration.GetConnectionString("IdentityDbConnection") ??
                               throw new InvalidOperationException(
                                   "Connection string 'IdentityDbConnection' not found.");

var connectionString = builder.Configuration.GetConnectionString("SpanishDbConnectionString") ??
                       throw new InvalidOperationException("Connection string 'SpanishDbConnectionString' not found.");


builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(identityConnectionString));
builder.Services.AddDbContext<SpanishDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<
        IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) //csak megerősített felhasználók
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IWordsRepository, WordsRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();