using FluentValidation;
using FluentValidation.AspNetCore;
using NLog.Web;
using ProfesiNet.Shared.Configurations;
using ProfesiNetApi;
using ProfesiNetApi.Configurations.DocumentationConfiguration;


var builder = WebApplication.CreateBuilder(args);
var assemblies = ModuleLoader.LoadAssemblies();
var modules = ModuleLoader.LoadModules(assemblies);
builder.Logging.ClearProviders();
builder.WebHost.UseNLog();
builder.Services.RegisterDocumentation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure();

foreach (var module in modules)
{
    module.Register(builder.Services);
}
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();
builder.Services.AddCors(opt=>opt.AddPolicy("CorsPolicy",policy=>policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000")));

var app = builder.Build();
    
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseInfrastructure();
app.UseCors("CorsPolicy");
foreach (var module in modules)
{
    module.Use(app);
}
app.MapControllers();

app.UseHttpsRedirection();


await app.RunAsync();

modules.Clear();
assemblies.Clear();

