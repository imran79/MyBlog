# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["Blog.Api/Blog.Api.csproj", "Blog.Api/"]
# COPY ["Blog.Data/Blog.Data.csproj", "Blog.Data/"]
# COPY ["Blog.Domain/Blog.Domain.csproj", "Blog.Domain/"]
# COPY ["Blog.Service/Blog.Service.csproj", "Blog.Service/"]
# COPY ["Blog.Messaging.Receive/Blog.Messaging.Receive.csproj", "Blog.Messaging.Receive/"]
# COPY ["Tests/Blog.Api.Test/Blog.Api.Test.csproj", "Tests/Blog.Api.Test/"]  
# COPY ["Tests/Blog.Service.Test/Blog.Service.Test.csproj", "Tests/Blog.Service.Test/"]  
# COPY ["Tests/Blog.Data.Test/Blog.Data.Test.csproj", "Tests/Blog.Data.Test/"] 
# COPY ["*.props", "./"]

# RUN dotnet restore "Blog.Api/Blog.Api.csproj"
# RUN dotnet restore "Tests/Blog.Api.Test/Blog.Api.Test.csproj"
# RUN dotnet restore "Tests/Blog.Service.Test/Blog.Service.Test.csproj"
# RUN dotnet restore "Tests/Blog.Data.Test/Blog.Data.Test.csproj"
# COPY . .

# RUN dotnet build "Blog.Api/Blog.Api.csproj" -c Release -o /app/build --no-restore
# RUN dotnet build "Tests/Blog.Api.Test/Blog.Api.Test.csproj" -c Release --no-restore
# RUN dotnet build "Tests/Blog.Service.Test/Blog.Service.Test.csproj" -c Release --no-restore
# RUN dotnet build "Tests/Blog.Data.Test/Blog.Data.Test.csproj" -c Release --no-restore

# # FROM build AS test  
# # ARG BuildId
# # LABEL test=${BuildId}
# # RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/Blog.Api.Test/Blog.Api.Test.csproj  
# # RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/Blog.Service.Test/Blog.Service.Test.csproj  
# # RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results3.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/Blog.Data.Test/Blog.Data.Test.csproj

# FROM build AS publish
# RUN dotnet publish "Blog.Api/Blog.Api.csproj" --no-restore -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Blog.Api.dll"]


FROM mcr.microsoft.com/dotnet/sdk:5.0 as debug

#install debugger for NET Core
RUN apt-get update
RUN apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

RUN mkdir /blog/
WORKDIR /blog/


COPY . .
# COPY ["./Blog.Api/Blog.Api.csproj", "Blog.Api/"]
# COPY ["./Blog.Data/Blog.Data.csproj", "Blog.Data/"]
# COPY ["./Blog.Domain/Blog.Domain.csproj", "Blog.Domain/"]
# COPY ["./Blog.Service/Blog.Service.csproj", "Blog.Service/"]
# COPY ["./Blog.Messaging.Receive/Blog.Messaging.Receive.csproj", "Blog.Messaging.Receive/"]

RUN dotnet restore "Blog.Api/Blog.Api.csproj"



ENV DOTNET_USE_POLLING_FILE_WATCHER 1

WORKDIR /blog/Blog.Api

ENTRYPOINT dotnet watch run --urls=https://+:3001;http://+:3000


# RUN apt-get update \
#     && apt-get install -y --no-install-recommends \
#             unzip \
#     && rm -rf /var/lib/apt/lists/* \
#     && curl -sSL https://aka.ms/getvsdbgsh \
#         | bash /dev/stdin -v latest -l /vsdbg

# ENV DOTNET_USE_POLLING_FILE_WATCHER 1

# WORKDIR /app/Blog.Api

# ENTRYPOINT dotnet watch run --urls=http://+:3000