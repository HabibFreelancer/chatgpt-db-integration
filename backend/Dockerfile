# Use official .NET SDK image for build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy remaining files and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variable for Render
ENV ASPNETCORE_URLS=http://+:${PORT}

# Expose the port Render will use
EXPOSE 10000

# Use the PORT env variable or fallback
ENTRYPOINT ["dotnet", "ChatGPTIntegration.dll"]
