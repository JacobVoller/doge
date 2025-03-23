var addSwagger = true;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

if (addSwagger)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
var isDevEnv = app.Environment.IsDevelopment(); //TODO

if (addSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();