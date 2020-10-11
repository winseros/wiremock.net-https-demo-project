FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /build

COPY . .

RUN dotnet test