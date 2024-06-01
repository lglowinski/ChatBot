## bookworm images are more secure compared to alpine
#FROM node:20.11-bookworm-slim as build
#
#ENV NODE_ENV=production 
#
#WORKDIR /app
#
#COPY package.json ./
#COPY pnpm-lock.yaml ./
#
## pnpm must be installed as it doesn't come with the default image
#RUN npm i -g pnpm
#
#COPY . ./
#
#RUN pnpm install
#
#FROM node:20.11-bookworm-slim
#
#WORKDIR /app
#COPY --from=build /app .
## ENV HOST is not though you can uncomment if if needed
## ENV HOST=0.0.0.0
#EXPOSE 4173
#
#CMD ["node","build"]


FROM node:20 as build

WORKDIR /app

COPY package.json package.json
COPY package-lock.json package-lock.json

RUN npm install

COPY . .

RUN npm run build

FROM nginx:alpine

COPY --from=build /app/default.conf.template /etc/nginx/templates/default.conf.template
COPY --from=build /app/dist /usr/share/nginx/html

# Expose the default nginx port
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]