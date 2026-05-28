using Api.Database;
using Api.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorFrontends",
        policy =>
        {
            policy.WithOrigins("http://localhost:7000","https://localhost:7000",
                               "http://localhost:7001",
                               "https://localhost:7001"
                               )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var key = builder.Configuration.GetValue<string>("MediatR:LicenceKey");

builder.Services.AddMediatR(cfg => {
    cfg.LicenseKey = key;
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScoped<IValidationProblemsHandler, ValidationProblemsHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "MovieList API";
        options.Theme = ScalarTheme.BluePlanet;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.CustomCss = "";
        options.ShowSidebar = true;
    });
}

app.UseCors("AllowBlazorFrontends");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
