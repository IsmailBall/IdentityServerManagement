using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//JWT Auth

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.Authority = "http://localhost:5179";
    opt.Audience = "ApiTwo";
    opt.RequireHttpsMetadata = false;
});
//Auth Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadPolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_two_read");
    });

    options.AddPolicy("AddPolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_two_write");
    });

    options.AddPolicy("UpdatePolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_two_update");
    });

    options.AddPolicy("RemovePolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_two_remove");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
