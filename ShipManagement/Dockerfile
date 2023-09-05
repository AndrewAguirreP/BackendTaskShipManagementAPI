#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShipManagement/ShipManagement.csproj", "ShipManagement/"]
RUN dotnet restore "ShipManagement/ShipManagement.csproj"
COPY . .
WORKDIR "/src/ShipManagement"
RUN dotnet build "ShipManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShipManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipManagement.dll"]