# ��. ������ �� ������ https://aka.ms/customizecontainer, ����� ������ ��� ��������� ��������� ������� � ��� Visual Studio ���������� ���� Dockerfile ��� �������� ������� ��� ��������� �������.

# ���� ���� ������������ ��� ������� �� VS � ������� ������ (�� ��������� ��� ������������ �������)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# ���� ���� ������������ ��� ������ ������� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
RUN dotnet restore "./WebApplication1/WebApplication1.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ���� ���� ������������ ��� ���������� ������� ������, ������� ����� ���������� �� ��������� ����
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN mkdir -p /app/data && chmod 777 /app/data
COPY ./WebApplication1/Kanban.db app/data/Kanban.db

WORKDIR /app
COPY --from=build /app .
RUN mkdir -p /app/Data && \
    chmod 777 /app/Data && \
    touch /app/Data/Kanban.db && \
    chmod 666 /app/Data/Kanban.db


ENV ASPNETCORE_ENVIRONMENT=Docker
ENV DOTNET_RUNNING_IN_CONTAINER=true

# ���� ���� ������������ � ������� ����� ��� ��� ������� �� VS � ������� ������ (�� ���������, ����� ������������ ������� �� ������������)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]