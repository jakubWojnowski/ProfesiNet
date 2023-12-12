#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#RUN apt-get update && apt-get install -y libicu63
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#COPY ["ProfesiNetApi/ProfesiNetApi.csproj", "ProfesiNetApi/"]
#RUN dotnet restore "ProfesiNetApi/ProfesiNetApi.csproj"
#COPY . .
#WORKDIR "/src/ProfesiNetApi"
#RUN dotnet build "ProfesiNetApi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ProfesiNetApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ProfesiNetApi.dll"]

#///////////////////////

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "ProfesiNetApi/ProfesiNetApi.csproj" --disable-parallel
RUN dotnet publish "ProfesiNetApi/ProfesiNetApi.csproj" -c release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final

WORKDIR /app
EXPOSE 80
COPY --from=build /publish ./
ENTRYPOINT ["dotnet", "ProfesiNetApi.dll"]
