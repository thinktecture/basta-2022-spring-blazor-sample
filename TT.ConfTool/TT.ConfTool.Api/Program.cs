using ConfTool.Server.Hubs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TT.ConfTool.Api.Models;
using TT.ConfTool.Api.Utils;
using TT.ConfTool.Shared.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConferencesDbContext>(
    options => options.UseInMemoryDatabase(databaseName: "ConfTool"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();

builder.Services.AddMvc()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ConferenceDetailsValidator>());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    builder.Configuration.Bind("Oidc", options);
                    options.RefreshOnIssuerKeyNotFound = true;
                });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("api", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireScope("api");
    });
});

builder.Services.AddSignalR().AddMessagePackProtocol();


builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<ConferencesDbContext>();
    DataGenerator.Initialize(services);
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conferences API V1");
});


app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(host => true));


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ConferencesHub>("/conferencesHub");
    endpoints.MapControllers();
});

app.Run();
