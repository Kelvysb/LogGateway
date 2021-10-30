using LogGateway.Extenssions;
using LogGateway.Models;
using LogGateway.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();
var app = builder.Build();
var serviceProvider = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider;
var service = serviceProvider.GetService<ILogService>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/info", (LogMessage message) => service?.Info(message));
app.MapPost("/error", (LogMessage message) => service?.Error(message));
app.MapPost("/debug", (LogMessage message) => service?.Debug(message));
app.MapPost("/warning", (LogMessage message) => service?.Warning(message));

app.Run();
  