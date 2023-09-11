FROM ubuntu:22.10
ENV DEBIAN_FRONTEND noninteractive
RUN echo 'Book Store Webserver v1.0.0'

WORKDIR /root

#Build the enviorment
RUN apt-get update\
    && DEBIAN_FRONTEND=noninteractive TZ=US/Pacific apt-get -y install tzdata\
    && apt-get -y install\
        dotnet7

COPY . ./

# RUN dotnet restore

# Build and publish a release
# RUN dotnet run