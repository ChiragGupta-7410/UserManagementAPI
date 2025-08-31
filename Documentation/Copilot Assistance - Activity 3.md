# How Microsoft Copilot Helped

1. **Error Handling Middleware**: Created middleware to catch exceptions and return consistent JSON error responses.
1. **Authentication Middleware**: Implemented token-based validation using a static token for testing.
1. **Logging Middleware**: Added middleware to log HTTP method, request path, and response status code.
1. **Middleware Pipeline Setup**: Guided correct ordering of error handling, authentication, and logging middleware.
1. **Swagger Token Support**: Configured Swagger to accept Bearer tokens for authenticated testing.
1. **Duplicate Email Check**: Updated CreateUser endpoint to prevent multiple users with the same email.
1. **Testing Instructions**: Provided curl, Postman, and Swagger-based testing guidance for middleware and endpoints.
