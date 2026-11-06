using System.Diagnostics;
using MoviesApi.Application.Movies;
using MoviesApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// впровадження залежностей DI (dependency injection)
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);


builder.Services.AddSingleton<IMovieService, InMemoryMovieService>();




var app = builder.Build();

// конвеєр обробки запитів (middleware pipeline)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


// логувати метод, шлях, статус код та час виконання запиту
app.Use(async (context, next) =>
{
    // ── Код ДО next: запит іде "всередину" ──
    var stopwatch = Stopwatch.StartNew();

    // Передаємо запит наступній станції конвеєра і ЧЕКАЄМО, поки вона (і всі після неї) закінчать.
    // ⚠️ Якщо в якійсь гілці не викликати next і нічого не записати у відповідь —
    //    запит далі не піде, а клієнт отримає порожню відповідь 200.
    await next(context);

    // ── Код ПІСЛЯ next: відповідь повертається "назовні" ──
    stopwatch.Stop();
    app.Logger.LogInformation(
        "{Method} {Path} → {StatusCode} за {ElapsedMs} мс",
        context.Request.Method,
        context.Request.Path,
        context.Response.StatusCode,
        stopwatch.ElapsedMilliseconds);
});


app.MapControllers();



app.Run();
