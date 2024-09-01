using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddIdentityServer()
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
