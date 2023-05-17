#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/BoilerPlate.Application/BoilerPlate.Application.csproj", "src/BoilerPlate.Application/"]
COPY ["src/BoilerPlate.Service/BoilerPlate.Service.csproj", "src/BoilerPlate.Service/"]
COPY ["src/BoilerPlate.Domain/BoilerPlate.Domain.csproj", "src/BoilerPlate.Domain/"]
COPY ["src/BoilerPlate.Infrastructure/BoilerPlate.Infrastructure.csproj", "src/BoilerPlate.Infrastructure/"]
RUN dotnet restore "src/BoilerPlate.Application/BoilerPlate.Application.csproj"
COPY . .
WORKDIR "/src/src/BoilerPlate.Application"
RUN dotnet build "BoilerPlate.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoilerPlate.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoilerPlate.Application.dll"]