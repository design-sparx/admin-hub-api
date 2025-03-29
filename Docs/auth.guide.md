# API Testing Guide: Authentication Endpoints

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

## 1. Authentication Endpoints

### 1.1 Register a New User

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/auth/register`
- Body:
  ```json
  {
    "username": "testuser",
    "email": "testuser@example.com",
    "password": "Test123!",
    "confirmPassword": "Test123!"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body:
  ```json
  {
    "message": "User registered successfully"
  }
  ```

### 1.2 Login

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/auth/login`
- Body:
  ```json
  {
    "username": "testuser",
    "password": "Test123!"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body:
  ```json
  {
    "token": "eyJhbGciOiJIUzI1...",
    "expiration": "2025-03-29T12:00:00.000Z",
    "username": "testuser",
    "roles": ["User"]
  }
  ```

**Action:**
- Copy the token value to your environment variable `token`
- Create a Pre-request Script for subsequent requests:
  ```javascript
  pm.request.headers.add({
    key: 'Authorization',
    value: 'Bearer ' + pm.environment.get('token')
  });
  ```

### 1.3 Forgot Password

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/auth/forgot-password`
- Body:
  ```json
  {
    "email": "testuser@example.com"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body:
  ```json
  {
    "message": "If your email is registered, you will receive a password reset link",
    "userId": "user-id-here",
    "token": "reset-token-here"
  }
  ```

**Action:**
- Save the `userId` and `token` for the password reset test

### 1.4 Reset Password

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/auth/reset-password`
- Body:
  ```json
  {
    "userId": "user-id-from-previous-response",
    "token": "token-from-previous-response",
    "newPassword": "NewPassword123!",
    "confirmPassword": "NewPassword123!"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body:
  ```json
  {
    "message": "Password reset successful"
  }
  ```

### 1.5 Refresh Token

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/auth/refresh-token`
- Body:
  ```json
  {
    "token": "{{token}}"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body: New token information

**Action:**
- Update your `token` environment variable with the new token