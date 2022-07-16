using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

//JWT Auth

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.Authority = "http://localhost:5179";
    opt.Audience = "ApiOne";
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadPolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_one_read");
    });

    options.AddPolicy("AddPolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_one_write");
    });

    options.AddPolicy("UpdatePolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_one_update");
    });

    options.AddPolicy("RemovePolicy", optionsPolicy =>
    {
        optionsPolicy.RequireClaim("scope", "api_one_remove");
    });
});

// Add services to the container.

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
