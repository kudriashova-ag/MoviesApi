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

