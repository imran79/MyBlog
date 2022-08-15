FROM mcr.microsoft.com/dotnet/aspnet:5.0.3 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Common/Common.csproj", "Common/"]
COPY ["Services/User/User.Api/User.Api.csproj", "Services/User/User.Api/"]
COPY ["Services/User/User.Data/User.Data.csproj", "Services/User/User.Data/"]
COPY ["Services/User/User.Domain/User.Domain.csproj", "Services/User/User.Domain/"]
COPY ["Services/User/User.Service/User.Service.csproj", "Services/User/User.Service/"]
COPY ["Services/User/User.Messaging.Sender/User.Messaging.Sender.csproj", "Services/User/User.Messaging.Sender/"]

COPY ["Services/User/*.props", "Services/User/"]

RUN dotnet restore "Services/User/User.Api/User.Api.csproj"

COPY . .
WORKDIR /src/Services/User/User.Api

RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" --no-restore -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]