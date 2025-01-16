FROM node:20.17.0-alpine

WORKDIR /usr/src/app

COPY . /usr/src/app

RUN npm install -g @angular/cli

RUN npm install

ENV ASPNETCORE_HTTPS_PORT 5001

CMD ["ng", "serve",  "--proxy-config", "proxy.conf.docker.json"]
