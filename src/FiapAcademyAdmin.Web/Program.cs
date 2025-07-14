using FiapAcademyAdmin.Application.Extensions;
using FiapAcademyAdmin.Infrastructure.Extensions;
using FiapAcademyAdmin.Web.Configuration;
using FiapAcademyAdmin.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

app.ConfigureWebPipeline()
   .ConfigureEndpoints();

app.Services.SeedData();

app.Run();
