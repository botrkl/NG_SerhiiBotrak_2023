FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SkeletonApi/SkeletonApi.csproj", "SkeletonApi/"]
RUN dotnet restore "SkeletonApi/SkeletonApi.csproj"
COPY . .
WORKDIR "/src/SkeletonApi"
RUN dotnet build "SkeletonApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkeletonApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkeletonApi.dll"]
