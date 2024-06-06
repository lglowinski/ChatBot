# bookworm images are more secure compared to alpine
FROM node:20.11-bookworm-slim as build

ENV NODE_ENV=production 

WORKDIR /app

COPY package.json pnpm-lock.yaml ./

# pnpm must be installed as it doesn't come with the default image
COPY . .

RUN rm -rf node_modules


# Stage for serving the application
FROM node:20.11-bookworm-slim

WORKDIR /app
COPY --from=build /app ./

RUN npm install -g pnpm
RUN npm i
RUN pnpm install

# Uncomment the following line if you want to bind the container to all network interfaces (useful for local development)
# ENV HOST=0.0.0.0

# Port on which the application will be served
EXPOSE 4173

CMD ["npm", "run", "dev"]