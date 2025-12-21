FROM node:20-alpine

WORKDIR /app


COPY package*.json ./
RUN npm ci

COPY . .

EXPOSE 3000

ENV NODE_ENV=development
ENV CHOKIDAR_USEPOLLING=true
ENV VITE_API_URL=http://localhost:7033


CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0", "--port", "3000"]