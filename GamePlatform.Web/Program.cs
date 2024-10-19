using GamePlatform.Repository;
using GamePlatform.Domain.Domain;
using GamePlatform.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GamePlatform.Repository.Interface;
using GamePlatform.Repository.Implementation;
using NuGet.Protocol.Core.Types;
using GamePlatform.Service.Interface;
using GamePlatform.Service.Implementation;
using GamePlatform.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

builder.Services.AddTransient<IDeveloperService, DeveloperService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
//builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
