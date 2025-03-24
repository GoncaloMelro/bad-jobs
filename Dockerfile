FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/out .

EXPOSE 8080 443

ENTRYPOINT ["dotnet", "badjobs.dll"]
