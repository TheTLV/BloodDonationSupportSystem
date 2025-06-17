FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

# Copy csproj và restore
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# 🚀 Runtime stage - Dùng ASP.NET runtime 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "BloodDonationSupportSystem.dll"]
