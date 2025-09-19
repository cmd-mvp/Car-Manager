using Car_Manager.Data;
using Car_Manager.Models;
using Car_Manager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("CarConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<CarServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<RentServices>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarManager", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<CarContext>(options =>
options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));

builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<IdentityOptions>(options =>
{
    
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    
    options.User.AllowedUserNameCharacters =
    "dfrevtsy6u7nei7n52322232rvxthvr3556767 jnuiubgyjgfy@#$%%ggsggtSDSD$%¨&*76543456789*7¨%6uifkubtRV&¨&*7(N67";
    options.User.RequireUniqueEmail = false;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
