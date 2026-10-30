Створення нового проєкту
dotnet new webapi --use-controllers -n MoviesApi
MoviesApi - назва проєкту

Запуск проєкту:
dotnet run
dotnet watch run





Створення .gitignore
dotnet new gitignore

Після завантаження проєкту з git
dotnet build

Swagger:
dotnet remove package Microsoft.AspNetCore.OpenApi
dotnet add package Swashbuckle.AspNetCore --version 10.3.1

