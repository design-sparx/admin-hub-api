# API Testing Guide: Accounts Endpoints

This guide provides step-by-step instructions for testing all authentication, account management, user administration, and role management endpoints in the AdminHub API.

## Prerequisites

- [Postman](https://www.postman.com/) or similar API testing tool
- .NET 9 SDK installed
- Application running locally or in a test environment
- PostgreSQL database configured and migrated

## Initial Setup

1. Ensure your database is properly migrated:
   ```bash
   dotnet ef database update
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. In Postman, create a collection for your API tests
4. Set up an environment variable called `baseUrl` (e.g., `https://localhost:5001`)
5. Create another environment variable called `token` (will be populated during testing)

## 2. Account Endpoints (Self-Management)

These endpoints require authentication. Make sure you're using the token from step 1.2.

### 2.1 Get Current User Profile

**Request:**
- Method: `GET`
- URL: `{{baseUrl}}/api/account/profile`
- Headers:
  - `Authorization: Bearer {{token}}`

**Expected Response:**
- Status: `200 OK`
- Body: User information

### 2.2 Update Current User Profile

**Request:**
- Method: `PUT`
- URL: `{{baseUrl}}/api/account/profile`
- Headers:
  - `Authorization: Bearer {{token}}`
- Body:
  ```json
  {
    "email": "updated.email@example.com",
    "phoneNumber": "1234567890"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body: Updated user information

### 2.3 Change Current User Password

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/account/change-password`
- Headers:
  - `Authorization: Bearer {{token}}`
- Body:
  ```json
  {
    "currentPassword": "Test123!",
    "newPassword": "NewPassword456!",
    "confirmPassword": "NewPassword456!"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body:
  ```json
  {
    "message": "Password changed successfully"
  }
  ```

**Action:**
- Login again with the new password to get a fresh token
- Update your `token` environment variable

### 2.4 Get Current User Claims

**Request:**
- Method: `GET`
- URL: `{{baseUrl}}/api/account/claims`
- Headers:
  - `Authorization: Bearer {{token}}`

**Expected Response:**
- Status: `200 OK`
- Body: List of user claims
