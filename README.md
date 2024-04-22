# LOTApp

Flight Management API project that allows users to view, add, update, and delete flight information. The API use ASP.NET Core with following features:

- JWT-based authentication mechanism
- Input data validation
- Entity Framework ORM for object-to-database mapping
- Unit tests

## **Flight Model**

The Flight model contain the following fields:

- **Id:** Unique flight identifier
- **FlightNumber:** Flight identification number (e.g., LO1234) (IATA marketing code)
- **DepartTime:** Date and time of departure
- **DepartLocation:** Departure airport (IATA code)
- **ArrivalLocation:** Arrival airport (IATA code)
- **PlaneType:** Plane type (e.g., Embraer, Boeing, Airbus)

## **API Endpoints**

- **GET /flights:** Get a list of all flights
- **GET /flights/{id}:** Get detailed information about a flight with the specified ID
- **POST /flights:** Add a new flight
- **PUT /flights/{id}:** Update the information about a flight with the specified ID
- **DELETE /flights/{id}:** Delete a flight with the specified ID

## **Authentication and Authorization**

This API utilizes JWT-based authentication for secure user access. Users can register for accounts and log in to obtain JWT tokens for authorization. The API also implements refresh tokens for extending token validity and a mechanism for revoking refresh tokens.

### Tools

- **Input data validation with FluentValidator:** This library is used to validate the input data for the API.
- **Object-relational mapping with Entity Framework Core:** This ORM is used to map C# objects to database tables.
- **Automatic object mapping with AutoMapper:** This library is used to automatically map C# objects between each other.
- **Microsoft.AspNetCore.Identity:** Library for user registration, login, and management.
- **System.IdentityModel.Tokens.Jwt (JWT):** Library for generating, validating, and parsing JWT tokens for secure authentication.
- **Microsoft.AspNetCore.Authorization:** Library for implementing authorization logic based on user roles or claims.
