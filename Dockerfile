FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /asb-onyx-dsl-consumer

# Copy everything
COPY /asb-onyx-dsl-consumer ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /asb-onyx-dsl-consumer
COPY --from=build /asb-onyx-dsl-consumer/out .
COPY ./secrets.json /asb-onyx-dsl-consumer/run/secrets.json
ENTRYPOINT ["dotnet", "asb-onyx-dsl-consumer.dll"]