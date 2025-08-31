# UserManagementAPI

A simple ASP.NET Core Web API for managing users, featuring custom error handling, authentication, and logging middleware.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Postman](https://www.postman.com/downloads/)

### Running the API

1. Clone the repository.
2. Open a terminal in the project directory.
3. Run the API:

   ```sh
   dotnet run
   ```

   The API will start on `http://localhost:5254` (or as configured in `Properties/launchSettings.json`).

## Using Postman

### Authorization

All endpoints require a Bearer token for authorization.  
Use the following test token:

```
test-token
```

#### How to Pass the Bearer Token

1. In Postman, select the request.
2. Go to the **Authorization** tab.
3. Set **Type** to `Bearer Token`.
4. Enter `test-token` as the token value.

Alternatively, add the following header manually:

```
Authorization: Bearer test-token
```

### Example Requests

#### Create a User

- **POST** `http://localhost:5254/api/users`
- **Body (JSON):**
  ```json
  {
    "fullName": "Jane Doe",
    "email": "jane.doe@example.com",
    "department": "Engineering"
  }
  ```

#### Get All Users

- **GET** `http://localhost:5254/api/users`

#### Get User by ID

- **GET** `http://localhost:5254/api/users/1`

#### Update a User

- **PUT** `http://localhost:5254/api/users/1`
- **Body (JSON):**
  ```json
  {
    "fullName": "Jane Doe",
    "email": "jane.doe@example.com",
    "department": "Product"
  }
  ```

#### Delete a User

- **DELETE** `http://localhost:5254/api/users/1`

## Swagger UI

When running in development, you can also test endpoints via Swagger at:

```
http://localhost:5254/swagger
```

Use the "Authorize" button and enter `Bearer test-token` as the value.

---

For more details, see [Controllers/UserController.cs](Controllers/UserController.cs) and