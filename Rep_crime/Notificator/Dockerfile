FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 587

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Notificator.csproj", "./"]
RUN dotnet restore "Notificator.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Notificator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notificator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notificator.dll"]
