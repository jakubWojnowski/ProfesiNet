using ProfesiNet.Posts.Api.Extension;
using ProfesiNet.Users.Api.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle=
builder.Services.AddSwaggerGen();
builder.Services
    .AddUserModule()
    .AddPostModule()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.UseHttpsRedirection();



app.RunAsync();

