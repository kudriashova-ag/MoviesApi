using System.Diagnostics;
using System.Reflection;
using AutoMapper;
using MoviesApi.Application.Movies;
using MoviesApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// впровадження залежностей DI (dependency injection)
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Movies API",
        Version = "v1",
        Description = "Навчальний API для роботи з фільмами"
    });

    options.SwaggerDoc("v2", new()
    {
        Title = "Movies API",
        Version = "v2",
        Description = "Друга версія API для роботи з фільмами"
    });

     // документ містить усі ендпоинти своєї групи
    options.DocInclusionPredicate((docName, apiDesc) => 
        apiDesc.GroupName == docName || apiDesc.GroupName == null
    );


    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Реєструємо AutoMapper
builder.Services.AddAutoMapper(typeof(MovieProfile));

builder.Services.AddSingleton<IMovieService, InMemoryMovieService>();




var app = builder.Build();

// конвеєр обробки запитів (middleware pipeline)
if (app.Environment.IsDevelopment())
{
    // перевіряємо маппінги на помилки
    app.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
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
