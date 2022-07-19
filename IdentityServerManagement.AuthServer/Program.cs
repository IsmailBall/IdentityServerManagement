using IdentityServerManagement.AuthServer;
using IdentityServerManagement.AuthServer.Models;
using IdentityServerManagement.AuthServer.Repo;
using IdentityServerManagement.AuthServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICustomUserRepository, CustomUserRepository>();

//DbContext

var path = builder.Configuration["ConnectionString:SqlServer"];

builder.Services.AddDbContext<CustomerDbContext>(opt =>
{
    opt.UseSqlServer(path);
});

//Identity Server Configuration
builder.Services
    .AddIdentityServer()
    .AddInMemoryApiResources(ConfigurationIdentity.GetApiResources())
    .AddInMemoryApiScopes(ConfigurationIdentity.GetApiScopes())
    .AddInMemoryClients(ConfigurationIdentity.GetClients())
    .AddInMemoryIdentityResources(ConfigurationIdentity.GetIdentityResources())
    //.AddTestUsers(ConfigurationIdentity.GetTestUsers().ToList())
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
