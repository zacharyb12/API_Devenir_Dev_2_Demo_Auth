# Example HTTP Requests

This file provides example `curl` and Postman-style requests for common endpoints in the project. Replace placeholders (`<...>`) with real values.

1) Login (get JWT)

curl example:

curl -X POST "https://localhost:5001/api/account/login" \
 -H "Content-Type: application/json" \
 -d '{
   "email": "user@example.com",
   "password": "P@ssw0rd"
 }'

Response (example):
{
  "token": "<JWT_TOKEN>",
  "expiresIn": 3600
}

Note: adjust the body keys to match your `LoginForm` DTO (check `Application_Devenir_Dev_2/DTOS/LoginForm.cs`).

2) Use JWT to call protected endpoint (GET movies)

curl example:

curl -X GET "https://localhost:5001/api/movies" \
 -H "Authorization: Bearer <JWT_TOKEN>" \
 -H "Accept: application/json"

3) Create a movie (POST)

Example body (adapt to your DTO fields):
{
  "title": "The Example Movie",
  "description": "A short description",
  "releaseYear": 2024
}

curl:

curl -X POST "https://localhost:5001/api/movies" \
 -H "Authorization: Bearer <JWT_TOKEN>" \
 -H "Content-Type: application/json" \
 -d '{
   "title": "The Example Movie",
   "description": "A short description",
   "releaseYear": 2024
 }'

4) Update a movie (PUT)

curl -X PUT "https://localhost:5001/api/movies/1" \
 -H "Authorization: Bearer <JWT_TOKEN>" \
 -H "Content-Type: application/json" \
 -d '{
   "title": "Updated Title",
   "description": "Updated description",
   "releaseYear": 2025
 }'

5) Delete a movie (DELETE)

curl -X DELETE "https://localhost:5001/api/movies/1" \
 -H "Authorization: Bearer <JWT_TOKEN>"

6) Register (if implemented)

curl -X POST "https://localhost:5001/api/account/register" \
 -H "Content-Type: application/json" \
 -d '{
   "email": "newuser@example.com",
   "password": "P@ssw0rd",
   "confirmPassword": "P@ssw0rd"
 }'

Postman notes
- For requests that require authentication, add a header `Authorization: Bearer <JWT_TOKEN>` or use the Authorization tab to set Bearer Token.
- Use environment variables for `baseUrl` and `token` to re-use across requests.

Swagger
- When running in development, Swagger UI is available and is useful to explore exact request/response models and required fields. Use the Swagger UI to copy sample requests.

Adjustments
- Check controller routes and DTOs for exact property names and routes; the examples above use conventional route names (`/api/account/*`, `/api/movies`).

Security reminder
- Never expose `JWT:secretKey` or other secrets in shared examples for production. Use the development secret only locally and secure production secrets with a secret manager.