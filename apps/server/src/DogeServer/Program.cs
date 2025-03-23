using DogeServer.Config;
using DogeServer.Services;

var addSwagger = true;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

if (addSwagger)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (addSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// app.run runs indefinitely. This must be executed before
AppConfiguration.Init(); 

if (AppConfiguration.Startup.SeedOnStartup)
{
    await SeedService.Seed();
}

app.Run();