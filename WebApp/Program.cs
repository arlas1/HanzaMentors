using System.IdentityModel.Tokens.Jwt;
using System.Text;
using App.BLL;
using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DAL.EF;
using App.Domain.Identity;
using Base.BLL.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppBLL, AppBLL>();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services
    .AddAuthentication()
    .AddCookie(options => { options.SlidingExpiration = true; })
    .AddJwtBearer(optinos =>
    {
        optinos.RequireHttpsMetadata = false;
        optinos.SaveToken = true;
        optinos.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("JWT:audience"),
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration.GetValue<string>("JWT:key")!
                    )
                ),
            ClockSkew = TimeSpan.Zero,
        };

    });

builder.Services
    .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddControllersWithViews();


builder.Services.AddAutoMapper(
    typeof(App.DAL.EF.AutoMapperProfile),
    typeof(App.BLL.AutoMapperProfile)
    );

// ===================================================
var app = builder.Build();
// ===================================================

// SetupAppData(app);


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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();



// static void SetupAppData(WebApplication app)
// {
//     using var serviceScope = ((IApplicationBuilder) app).ApplicationServices
//         .GetRequiredService<IServiceScopeFactory>()
//         .CreateScope();
//     using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
//
//     context.Database.Migrate();
//
//     using var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//     using var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
//
//     var res = roleManager.CreateAsync(new AppRole()
//     {
//         Name = "Admin"
//     }).Result;
//
//     if (!res.Succeeded)
//     {
//         Console.WriteLine(res.ToString());
//     }
//
//     var user = new AppUser()
//     {
//         Email = "admin@eesti.ee",
//         UserName = "admin@eesti.ee",
//         FirstName = "asd",
//         LastName = "qwe",
//         PersonalCode = 123
//     };
//     res = userManager.CreateAsync(user, "Kala.maja1").Result;
//     if (!res.Succeeded)
//     {
//         Console.WriteLine(res.ToString());
//     }
//
//
//     res = userManager.AddToRoleAsync(user, "Admin").Result;
//     if (!res.Succeeded)
//     {
//         Console.WriteLine(res.ToString());
//     }
// }
