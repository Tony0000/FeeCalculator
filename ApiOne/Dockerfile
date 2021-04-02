FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY /Api/Api.csproj .

RUN dotnet restore Api.csproj

COPY /Api/ .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]
