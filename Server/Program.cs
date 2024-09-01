using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.Data;

var seed = args.Contains("/seed");

if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

if (seed)
{
    SeedData.EnsureSeedData(builder.Configuration.GetConnectionString("DefaultConnection"));
}

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b =>
        b.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), opt => opt.MigrationsAssembly(assembly));
        })
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
