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

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#COPY . .
#RUN dotnet restore "ProfesiNetApi/ProfesiNetApi.csproj" --disable-parallel
#RUN dotnet publish "ProfesiNetApi/ProfesiNetApi.csproj" -c release -o /publish --no-restore
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
#
#WORKDIR /app
#EXPOSE 80
#COPY --from=build /publish ./
#ENTRYPOINT ["dotnet", "ProfesiNetApi.dll"]

# Use the SDK image to build the solution
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /src

# Copy csproj and sln files
COPY ["ProfesiNet.sln", "./"]
COPY ["ProfesiNetApi/ProfesiNetApi.csproj", "ProfesiNetApi/"]
COPY ["modules/Users/ProfesiNet.Users.Api/ProfesiNet.Users.Api.csproj", "modules/Users/ProfesiNet.Users.Api/"]
COPY ["modules/Users/ProfesiNet.Users.Application/ProfesiNet.Users.Application.csproj", "modules/Users/ProfesiNet.Users.Application/"]
COPY ["modules/Users/ProfesiNet.Users.Infrastructure/ProfesiNet.Users.Infrastructure.csproj", "modules/Users/ProfesiNet.Users.Infrastructure/"]
COPY ["modules/Users/ProfesiNet.Users.Domain/ProfesiNet.Users.Domain.csproj", "modules/Users/ProfesiNet.Users.Domain/"]
COPY ["modules/Posts/ProfesiNet.Posts.Api/ProfesiNet.Posts.Api.csproj", "modules/Posts/ProfesiNet.Posts.Api/"]
COPY ["modules/Posts/ProfesiNet.Posts.Core/ProfesiNet.Posts.Core.csproj", "modules/Posts/ProfesiNet.Posts.Core/"]
COPY ["modules/Shared/ProfesiNet.Shared/ProfesiNet.Shared.csproj", "modules/Shared/ProfesiNet.Shared/"]
COPY ["modules/LiveChats/ProfesiNet.LiveChats.Api/ProfesiNet.LiveChats.Api.csproj", "modules/LiveChats/ProfesiNet.LiveChats.Api/"]
COPY ["modules/LiveChats/ProfesiNet.LiveChats.Core/ProfesiNet.LiveChats.Core.csproj", "modules/LiveChats/ProfesiNet.LiveChats.Core/"]

# Restore the packages
RUN dotnet restore "ProfesiNet.sln" --disable-parallel

# Copy the rest of the source code
COPY . .

# Build and publish the main web project
RUN dotnet publish "ProfesiNetApi/ProfesiNetApi.csproj" -c Release -o /app/publish --no-restore

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copy the published app from the build image
COPY --from=build /app/publish .

# Open the port your app runs on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ProfesiNetApi.dll"]


