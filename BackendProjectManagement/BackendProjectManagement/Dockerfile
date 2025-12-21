FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app


RUN dotnet tool install --global dotnet-watch
ENV PATH="${PATH}:/root/.dotnet/tools"


COPY *.csproj ./
RUN dotnet restore


COPY . .

EXPOSE 7033

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:7033


CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:7033"]