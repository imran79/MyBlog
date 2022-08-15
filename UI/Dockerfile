FROM mcr.microsoft.com/dotnet/sdk:5.0 as debug

#install debugger for NET Core
RUN apt-get update
RUN apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

RUN mkdir /ui/
WORKDIR /ui/

RUN mkdir /Common/
COPY ./Common/.  /ui/Common

RUN dotnet restore "Common/Common.csproj"

RUN mkdir /blogui/
COPY ./UI/. /ui/blogui/

RUN dotnet restore "blogui/ApplicationShell.Server/ApplicationShell.Server.csproj"

ENV DOTNET_USE_POLLING_FILE_WATCHER 1

WORKDIR /ui/blogui/ApplicationShell.Server

ENTRYPOINT dotnet watch run --urls=https://+:44348