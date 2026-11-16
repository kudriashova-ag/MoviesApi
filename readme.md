Створення нового проєкту
dotnet new webapi --use-controllers -n MoviesApi
MoviesApi - назва проєкту

Запуск проєкту:
dotnet run
dotnet watch run
Якщо запуск з Solution:
dotnet watch --project MoviesApi

Створення solution
dotnet new sln -n MoviesApi
dotnet sln add MoviesApi/MoviesApi.csproj
тоді запуск проєкта:
dotnet watch --project MoviesApi

Створення .gitignore
dotnet new gitignore

Після завантаження проєкту з git
dotnet build

Swagger:
dotnet remove package Microsoft.AspNetCore.OpenApi
dotnet add package Swashbuckle.AspNetCore --version 10.3.1

Міграції:
Перший раз:
dotnet ef migrations add InitialCreate --output-dir Infrastructure/Persistence/Migrations
де InitialCreate - назва міграції (повинна бути унікальною)

Наступні:
dotnet ef migrations add [name]

Якщо запуск проєкта відбувається з папки Movies - в кінець команди додавати
--project MoviesApi

запуск міграції
dotnet ef database update

Список міграцій:
dotnet ef migrations list

Створити нову таблицю:

1. Створити нову сутність в Domain
2. MoviesApi\Infrastructure\Persistence\AppDbContext.cs додати DbSet
3. MoviesApi\Infrastructure\Persistence\Configurations створити конфігупацію
4. Міграція dotnet ef migrations add [name] та dotnet ef database update
