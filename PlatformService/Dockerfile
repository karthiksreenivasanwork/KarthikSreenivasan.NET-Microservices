# Details on mcr.microsoft.com/dotnet/sdk:6.0 can be found in the below link
# https://hub.docker.com/_/microsoft-dotnet-sdk
# This is a .Net SDK image which helps us setup the environment.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#Create a working directory
WORKDIR /app 

#Copy csproj details to our working directory.
COPY *.csproj ./
#Pull down the packages contained above that
#we need to build our image
RUN dotnet restore

#Copy from source to destination.
COPY . ./
#Publish the service
RUN dotnet publish -c Release -o out

# Setup the runtime environment
# Official images for the ASP.NET Core runtime
# Link: https://hub.docker.com/_/microsoft-dotnet-aspnet
# 
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

#Set the entry point for our image.
#Hence, when we run our image, the below will load
#with the environment above.
ENTRYPOINT [ "dotnet",  "PlatformService.dll" ]