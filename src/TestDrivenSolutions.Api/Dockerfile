# TODO: build image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . ./
RUN dotnet restore
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /src
COPY --from=build /src/out .
ENTRYPOINT ["dotnet", "TestDrivenSolutions.Api.dll"]