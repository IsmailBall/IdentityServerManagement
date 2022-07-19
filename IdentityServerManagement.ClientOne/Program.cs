using IdentityServerManagement.ClientOne.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IApiResourceHttpClient, ApiResourceHttpClient>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    //opt.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", opts =>
{
    opts.AccessDeniedPath = "/Home/AccessDenied";
    opts.LoginPath = "/Login/LogIn";

});
//.AddOpenIdConnect("oidc", opts =>
//{
//    opts.SignInScheme = "Cookies";
//    opts.Authority = "https://localhost:7179";
//    opts.ClientId = "ClientOne-Mvc";
//    opts.ClientSecret = "secret";
//    opts.ResponseType = "code id_token";
//    opts.GetClaimsFromUserInfoEndpoint = true;
//    opts.SaveTokens = true;

//    opts.Scope.Add("api_one_read");
//    opts.Scope.Add("offline_access");
//    opts.Scope.Add("CountryAndCity");
//    opts.Scope.Add("Roles");
//    opts.Scope.Add("email");

//    opts.ClaimActions.MapUniqueJsonKey("country", "country");
//    opts.ClaimActions.MapUniqueJsonKey("city", "city");
//    opts.ClaimActions.MapUniqueJsonKey("role", "role");

//    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        RoleClaimType = "role",
//        NameClaimType = "name"
//    };
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
