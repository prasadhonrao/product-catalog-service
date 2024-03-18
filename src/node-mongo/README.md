# Node Product Catalog Service

This service is a Node.js application that provides a RESTful API for a product catalog.

## Features

- List all products
- List all proudcts of a specific category
- Get details of a specific product
- Add a new product
- Update a product
- Delete a product
- List all categories
- Get details of a specific category
- Add a new category
- Update a category
- Delete a category

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- Node.js
- npm
- MongoDB

### Setting Up MongoDB with Docker

If you don't have MongoDB installed on your machine, you can run it in a Docker container. Follow these steps:

1. Install Docker: Follow the instructions on the [Docker website](https://docs.docker.com/get-docker/) to download and install Docker for your operating system.

2. Pull the MongoDB image: Run `docker pull mongo` in your terminal to pull the latest MongoDB image from Docker Hub.

3. Run the MongoDB container: Run the following command to start a MongoDB container:

```bash
docker run --name mongodb -p 27017:27017 -d mongo
```

## Installing

1. Clone the repository: `git clone https://github.com/yourusername/node-product-catalog-service.git`
2. Install dependencies: `npm install`
3. Start the server: `npm start`

## API Endpoints

## API Endpoints

- `GET /products`: List all products
- `GET /products?category=:category`: List all products of a specific category
- `GET /products/:id`: Get details of a specific product
- `POST /products`: Add a new product
- `PUT /products/:id`: Update a product
- `DELETE /products/:id`: Delete a product
- `GET /categories`: List all categories
- `GET /categories/:id`: Get details of a specific category
- `POST /categories`: Add a new category
- `PUT /categories/:id`: Update a category
- `DELETE /categories/:id`: Delete a category

## Built With

- [Node.js](https://nodejs.org/) - The runtime environment
- [Express](https://expressjs.com/) - The web framework used
- [Mongoose](https://mongoosejs.com/) - Object Modeling for Node.js

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
