# Your API Project Name

Prequin Investor Backend API Project

## Table of Contents

- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Architecture](#architecture)
  - [Clean Architecture](#clean-architecture)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)

## Technologies Used

- [.NET Core](https://dotnet.microsoft.com/) - Framework for building applications
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM for database interactions, In-memory DB is being used in the project
- [Swagger](https://swagger.io/) - API documentation and testing
- [Moq](https://github.com/jbogard/MediatR) - Unit testing

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or above)
- A database management system (e.g., SQL Server or SQLite)
- [Postman](https://www.postman.com/) or any API testing tool (optional)

## Architecture

This section describes the architecture of the application, including its key components and their interactions.

### Clean Architecture

Clean Architecture is a software design pattern that promotes separation of concerns and makes the system more maintainable, testable, and scalable. The main principles of Clean Architecture are:

- **Independence of Frameworks**: The application should be independent of the frameworks used, allowing you to change frameworks with minimal impact.
- **Testability**: The application should be easy to test. Business rules should not depend on the UI or external agents.
- **Separation of Concerns**: Different aspects of the application should be separated into distinct layers.

#### Project Structure

The project is organized into the following layers:

1. **Core Layer (Domain)**:

   - **(Investor.Domain)**
   - Contains the business logic and domain entities.
   - Defines interfaces for repositories and services.

2. **Application Layer**:

   - **(Investor.Application)**
   - Implements use cases and orchestrates business logic.
   - Interacts with the core layer and communicates with the presentation layer.

3. **Infrastructure Layer**:

   - **(Investor.Infrastructure)**
   - Implements data access and external service integrations.
   - Contains Entity Framework Core DbContext and repository implementations.

4. **Presentation Layer (API)**:

   - **(InvestorApp)**
   - Exposes the API endpoints and handles HTTP requests.
   - Uses controllers to interact with the application layer.

## API Endpoints

The following API endpoints are available in this application:

### Investor Endpoints

#### Get All Investors

- **Endpoint:** `/api/investors`
- **Method:** `GET`
- **Description:** Retrieves a list of all investors.
- **Response:**
  - **Status 200 OK**
    - **Body:** A list of investors in JSON format.

### Commitments Endpoints

#### Get Commitment Info by Investor Name

- **Endpoint:** `/api/commitments/{name}`
- **Method:** `GET`
- **Description:** Retrieves a Commitment Info by investor Name.
- **Parameters:**
  - `Name` (required): investor Name.
- **Response:**
  - **Status 200 OK**
    - **Body:** Details of the commitment Info in JSON format.
  - **Status 404 Not Found**
    - **Body:** Error message if the investor is empty or not found.

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Karthik-081-keyan/Prequin-Investor-App.git
   ```

2. pull the latest changes:

   ```bash
   git pull
   ```

3. checkout to main branch:

   ```bash
   git checkout main
   ```
