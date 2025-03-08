FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Banking.Web/Banking.Web.csproj", "src/Banking.Web/"]
COPY ["src/UserAccounts/Banking.UserAccounts.Domain/Banking.UserAccounts.Domain.csproj", "src/UserAccounts/Banking.UserAccounts.Domain/"]
COPY ["src/Shared/Banking.Core/Banking.Core.csproj", "src/Shared/Banking.Core/"]
COPY ["src/Shared/Banking.SharedKernel/Banking.SharedKernel.csproj", "src/Shared/Banking.SharedKernel/"]
COPY ["src/UserAccounts/Banking.UserAccounts.Contracts/Banking.UserAccounts.Contracts.csproj", "src/UserAccounts/Banking.UserAccounts.Contracts/"]
COPY ["src/UserAccounts/Banking.UserAccounts.Application/Banking.UserAccounts.Application.csproj", "src/UserAccounts/Banking.UserAccounts.Application/"]
COPY ["src/Shared/Banking.Framework/Banking.Framework.csproj", "src/Shared/Banking.Framework/"]
COPY ["src/BankAccounts/Banking.BankAccounts.Domain/Banking.BankAccounts.Domain.csproj", "src/BankAccounts/Banking.BankAccounts.Domain/"]
COPY ["src/BankAccounts/Banking.BankAccounts.Contracts/Banking.BankAccounts.Contracts.csproj", "src/BankAccounts/Banking.BankAccounts.Contracts/"]
COPY ["src/BankAccounts/Banking.BankAccounts.Application/Banking.BankAccounts.Application.csproj", "src/BankAccounts/Banking.BankAccounts.Application/"]
COPY ["src/UserAccounts/Banking.UserAccounts.Presentation/Banking.UserAccounts.Presentation.csproj", "src/UserAccounts/Banking.UserAccounts.Presentation/"]
COPY ["src/BankAccounts/Banking.BankAccounts.Presentation/Banking.BankAccounts.Presentation.csproj", "src/BankAccounts/Banking.BankAccounts.Presentation/"]
COPY ["src/BankAccounts/Banking.BankAccounts.Infrastructure/Banking.BankAccounts.Infrastructure.csproj", "src/BankAccounts/Banking.BankAccounts.Infrastructure/"]
COPY ["src/UserAccounts/Banking.UserAccounts.Infrastructure/Banking.UserAccounts.Infrastructure.csproj", "src/UserAccounts/Banking.UserAccounts.Infrastructure/"]
RUN dotnet restore "src/Banking.Web/Banking.Web.csproj"
COPY . .
WORKDIR "/src/src/Banking.Web"
RUN dotnet build "Banking.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Banking.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Web.dll"]
