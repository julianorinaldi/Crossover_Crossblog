FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

WORKDIR /app/out

ENV ASPNETCORE_ENVIRONMENT=Docker

ENTRYPOINT ["./wait-for-it.sh", "db:3306", "-t", "60", "--", "dotnet", "crossblog.dll"]