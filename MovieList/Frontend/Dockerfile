FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

COPY Common/Common.csproj Common/
COPY Frontend/Frontend.csproj Frontend/

RUN dotnet restore Frontend/Frontend.csproj

COPY Common/ Common/
COPY Frontend/ Frontend/

RUN dotnet publish Frontend/Frontend.csproj -c Release -o /app/publish

FROM nginx:alpine AS final

COPY Frontend/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80