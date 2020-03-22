FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM node:12.2.0  AS client 
ARG skip_client_build=false 
WORKDIR /app 
COPY WebApp/ClientApp . 
RUN [[ ${skip_client_build} = true ]] && echo "Skipping npm install" || npm install 
RUN [[ ${skip_client_build} = true ]] && mkdir dist || npm run-script build

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq \
    npm
RUN npm install -g npm
RUN npm install -g @angular/cli
WORKDIR /
COPY *.sln ./
COPY Business/Business.csproj Business/
COPY DataAccess/DataAccess.csproj DataAccess/
COPY Models/Models.csproj Models/
COPY WebApp/WebApp.csproj WebApp/
RUN dotnet restore

COPY . .
WORKDIR "/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=client /app/dist /app/dist
ENTRYPOINT ["dotnet", "WebApp.dll"]