using Blog.Api.Configuration;
using Blog.Application;
using Blog.Application.Common.Interfaces;
using Blog.Infrastructure;
using System.Text.Json.Serialization;
using Blog.Api.WebSocket.Hubs;
using Blog.Api.WebSocket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSignalR();
builder.Services.AddSingleton<IPostNotifierService, PostNotifierService>();


builder.Services.AddApi(builder.Configuration);

builder.Services.AddSecurityConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<PostHub>("/posthub");

await app.RunAsync();