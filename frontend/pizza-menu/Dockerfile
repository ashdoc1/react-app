# for production we give name as build 
FROM node:20.1.0 as build

# Set working directory to /app 
WORKDIR /app

# Copy package.json to /app
COPY package*.json .

# copy dependencies . . means all files in current directory with node_modules and other files
COPY . .

RUN ls -la



# run npm install to install dependencies
RUN npm install


# for production 
RUN npm run build

RUN ls -la

# after run npm run build we get build folder in our project
# pull nginx image from docker hub

FROM nginx:1.25.3 

# node name as build 
#  take local folder directory /app/build which is create in this command npm run build above  and copy to nginx folder /usr/share/nginx/html
# the default folder for nginx is /usr/share/nginx/html because nginx require the static files to run the app. when i try to npm run build it create stic files in build folder and we copy that build folder to nginx folder

COPY --from=build /app/build /usr/share/nginx/html


# default port for nginx is 80
EXPOSE 80

CMD [ "nginx","-g","daemon off;" ]

# # build environment
# FROM node:16-alpine as build
# WORKDIR /app
# ENV PATH /app/node_modules/.bin:$PATH
# COPY package.json ./
# COPY package-lock.json ./
# RUN npm ci
# RUN npm install react-scripts@5.0.1 -g
# COPY . ./
# RUN npm run build

# # production environment
# FROM nginx:stable-alpine
# COPY --from=build /app/build /usr/share/nginx/html
# EXPOSE 80
# CMD ["nginx", "-g", "daemon off;"]

##################


# # pull official base image
# FROM node:16-alpine

# # set working directory
# WORKDIR /app

# # add `/app/node_modules/.bin` to $PATH
# ENV PATH /app/node_modules/.bin:$PATH

# # install app dependencies
# COPY package.json ./
# COPY package-lock.json ./
# RUN npm install
# RUN npm install react-scripts@5.0.1 -g

###################
# add app
# COPY . ./

# # start app
# CMD ["npm", "start"]

# # Base image
# FROM node:16-alpine

# # Working directory
# WORKDIR /app

# # Copy package.json and package-lock.json
# COPY package*.json ./

# # Install dependencies
# RUN npm install

# # Copy the rest of the project files
# COPY . .

# # Start the application
# CMD ["npm", "start"]