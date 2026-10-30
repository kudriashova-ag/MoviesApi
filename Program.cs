using MoviesApi.Application.Movies;
using MoviesApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// впровадження залезностей DI (dependency injection)
builder.Services.AddSingleton<IMovieService, InMemoryMovieService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
