using Prometheus;
using Web.Api.DI;
using Web.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddJsonOptions()
    .AddPersistence(builder.Configuration)
    .AddDomainModel(builder.Configuration)
    .AddServices(builder.Configuration)
    .AddProblemDetails()
    .AddAuthentication(builder.Configuration);

var app = builder.Build();
app.MapEndpoints();
app.UseHttpMetrics();
app.MapMetrics();
app.BuildApp();
app.Run();