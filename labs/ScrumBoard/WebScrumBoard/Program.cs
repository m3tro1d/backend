using Microsoft.OpenApi.Models;
using WebScrumBoard.Modules.ScrumBoard.App;
using WebScrumBoard.Modules.ScrumBoard.App.Query;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ScrumBoard API",
        Description = "An ASP.NET Core Web API for managing Scrum boards, columns and tasks.",
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("http://www.wtfpl.net/")
        }
    });
});
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IBoardQueryService, BoardQueryService>();
builder.Services.AddScoped<IBoardStore, BoardStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
