#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0
   COPY bin/Release/net6.0/publish/ App/
   WORKDIR /App
   ENTRYPOINT ["dotnet", "WebAPI.dll"]