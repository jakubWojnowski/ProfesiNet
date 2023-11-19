using FluentValidation;
using FluentValidation.AspNetCore;
using ProfesiNet.Posts.Api.Extension;
using ProfesiNet.Shared.Configurations;
using ProfesiNet.Users.Api.Extension;
using ProfesiNetApi.Configurations.DocumentationConfiguration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle=
builder.Services
    .AddUserModule()
    .AddPostModule()
    .RegisterDocumentation()
    .AddProfesiNetShared()
    .AddEndpointsApiExplorer();
builder.Services.AddControllers().
    AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<IValidator>();
    });

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.UseHttpsRedirection();



app.Run();

