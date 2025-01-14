# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]

COPY ["src/APIGateway.Web/APIGateway.Web.csproj", "src/APIGateway.Web/"]
RUN dotnet restore "./src/APIGateway.Web/APIGateway.Web.csproj"
COPY . .
WORKDIR "/src/src/APIGateway.Web"
RUN dotnet build "./APIGateway.Web.csproj" -c Release -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
RUN dotnet publish "./APIGateway.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Generate dev certificate
USER root
RUN apt-get update && apt-get install -y openssl
RUN mkdir -p /https
RUN openssl req -x509 -newkey rsa:4096 -keyout /https/key.pem -out /https/cert.pem -days 365 -nodes -subj '/CN=localhost'
RUN chown -R app:app /https
USER app

ENV ASPNETCORE_URLS=https://+:8081;http://+:8080
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/cert.pem
ENV ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/https/key.pem

ENTRYPOINT ["dotnet", "APIGateway.Web.dll"]
