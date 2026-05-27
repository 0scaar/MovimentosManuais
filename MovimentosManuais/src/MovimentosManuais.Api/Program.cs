using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Api.Middlewares;
using MovimentosManuais.Application;
using MovimentosManuais.Infrastructure;
using MovimentosManuais.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                builder.Environment.IsDevelopment() &&
                Uri.TryCreate(origin, UriKind.Absolute, out var uri) &&
                (uri.Host == "localhost" || uri.Host == "127.0.0.1"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularApp");

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();
