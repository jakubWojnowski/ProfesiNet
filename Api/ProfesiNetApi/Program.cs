using FluentValidation;
using FluentValidation.AspNetCore;
using NLog.Web;
using ProfesiNet.Shared.Configurations;
using ProfesiNet.Shared.Modules;
using ProfesiNetApi;


var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureModules();
var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies);
// builder.Logging.ClearProviders();
// builder.WebHost.UseNLog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(assemblies, modules);

foreach (var module in modules)
{
    module.Register(builder.Services);
}
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();

var app = builder.Build();
    


app.UseInfrastructure();
foreach (var module in modules)
{
    module.Use(app);
}
app.MapControllers();
app.MapGet("/", () => "ProfesiNet API!");

// app.UseRouting();// tu jest jakis problem wywala apke
modules.Clear();
assemblies.Clear();
await app.RunAsync();



