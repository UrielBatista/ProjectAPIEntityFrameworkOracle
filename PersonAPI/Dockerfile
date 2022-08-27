#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonAPI/PersonAPI.csproj", "PersonAPI/"]
COPY ["Pro.Search.Commands/Pro.Search.Commands.csproj", "Pro.Search.Commands/"]
COPY ["BuldBlocks.Domain/BuldBlocks.Domain.csproj", "BuldBlocks.Domain/"]
COPY ["Pro.Search.Infraestructure/Pro.Search.Infraestructure.csproj", "Pro.Search.Infraestructure/"]
COPY ["Pro.Search.PersonDomains/Pro.Search.PersonDomains.csproj", "Pro.Search.PersonDomains/"]
RUN dotnet restore "PersonAPI/PersonAPI.csproj"
COPY . .
WORKDIR "/src/PersonAPI"
RUN dotnet build "PersonAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonAPI.dll"]