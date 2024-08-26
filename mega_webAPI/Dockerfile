# Usa la imagen base del SDK de .NET para la etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo csproj y restaura las dependencias
COPY mega_webAPI.csproj mega_webAPI/
WORKDIR /src/mega_webAPI
RUN dotnet restore

# Copia el resto del código y construye la aplicación
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Usa la imagen base del runtime para la etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "mega_webAPI.dll"]