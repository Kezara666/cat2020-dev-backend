#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CAT20.WebApi/CAT20.WebApi.csproj", "CAT20.WebApi/"]
COPY ["CAT20.Common/CAT20.Common.csproj", "CAT20.Common/"]
COPY ["CAT20.Core/CAT20.Core.csproj", "CAT20.Core/"]
COPY ["CAT20.Data/CAT20.Data.csproj", "CAT20.Data/"]
COPY ["CAT20.Reports/CAT20.Reports.csproj", "CAT20.Reports/"]
COPY ["CAT20.Services/CAT20.Services.csproj", "CAT20.Services/"]
COPY ["FastReport.OpenSource.HtmlExporter/FastReport.OpenSource.HtmlExporter.csproj", "FastReport.OpenSource.HtmlExporter/"]
COPY ["FastReport.OpenSource.HtmlExporter.Core/FastReport.OpenSource.HtmlExporter.Core.csproj", "FastReport.OpenSource.HtmlExporter.Core/"]
RUN dotnet restore "CAT20.WebApi/CAT20.WebApi.csproj"
COPY . .
WORKDIR "/src/CAT20.WebApi"
RUN dotnet build "CAT20.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CAT20.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CAT20.WebApi.dll"]