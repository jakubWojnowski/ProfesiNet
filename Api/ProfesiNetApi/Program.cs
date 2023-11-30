using FluentValidation;
using FluentValidation.AspNetCore;
using NLog.Web;
using ProfesiNet.LiveChats.Api.Extension;
using ProfesiNet.Posts.Api.Extension;
using ProfesiNet.Shared.Configurations;
using ProfesiNet.Users.Api.Extension;
using ProfesiNetApi.Configurations.DocumentationConfiguration;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.WebHost.UseNLog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle=
builder.Services
    .AddUserModule()
    .AddPostModule()
    .AddLiveChatsModule()
    .RegisterDocumentation()
    .AddProfesiNetShared()
    .AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();
builder.Services.AddCors();

var app = builder.Build();

//app.UseExceptionHandler(_ => { });
// Configure the HTTP request pipeline.
    app.UseInfrastructure();
    app.UseSwagger();
    app.UseSwaggerUI();

app.MapControllers();

app.UseHttpsRedirection();




await app.RunAsync();

