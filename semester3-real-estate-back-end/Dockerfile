FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["semester3-real-estate-back-end/semester3-real-estate-back-end.csproj", "semester3-real-estate-back-end/"]
RUN dotnet restore "semester3-real-estate-back-end/semester3-real-estate-back-end.csproj"
COPY . .
WORKDIR "/src/semester3-real-estate-back-end"
RUN dotnet build "semester3-real-estate-back-end.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "semester3-real-estate-back-end.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "semester3-real-estate-back-end.dll"]
