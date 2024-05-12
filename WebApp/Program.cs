using System.IdentityModel.Tokens.Jwt;
using System.Text;
using App.BLL;
using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DAL.EF;
using App.Domain.Identity;
using App.Helpers;
using App.Helpers.EmailService;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Base.BLL.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppBLL, AppBLL>();
builder.Services.AddScoped<IEmailService, EmailService>(); // aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa


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

var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    }
);
apiVersioningBuilder.AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

// ===================================================
var app = builder.Build();
// SetupAppData(app);
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

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>(); 
    foreach ( var description in provider.ApiVersionDescriptions )
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant() 
        );
    }
    // serve from root
    // opt ions.RoutePrefix = string.Empty;
});

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
//         Name = "Mentor"
//     }).Result;
//     var res1 = roleManager.CreateAsync(new AppRole()
//     {
//         Name = "Mentee"
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
