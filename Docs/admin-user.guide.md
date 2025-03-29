# API Testing Guide: Admin User Endpoints

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

## 3. Admin User Endpoints

For these tests, you need to be logged in as an admin user.

### 3.0 Login as Admin

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/auth/login`
- Body:
  ```json
  {
    "username": "admin",
    "password": "Admin123!"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body: Token response

**Action:**
- Update your `token` environment variable with the admin token

### 3.1 Get All Users

**Request:**
- Method: `GET`
- URL: `{{baseUrl}}/api/users?page=1&pageSize=10&search=test`
- Headers:
    - `Authorization: Bearer {{token}}`

**Expected Response:**
- Status: `200 OK`
- Body: List of users with pagination

### 3.2 Get User by ID

**Request:**
- Method: `GET`
- URL: `{{baseUrl}}/api/users/{userId}`
- Headers:
    - `Authorization: Bearer {{token}}`

**Expected Response:**
- Status: `200 OK`
- Body: User details

### 3.3 Create User

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/users`
- Headers:
    - `Authorization: Bearer {{token}}`
- Body:
  ```json
  {
    "userName": "adminuser",
    "email": "adminuser@example.com",
    "password": "Admin456!",
    "phoneNumber": "9876543210",
    "roles": ["User"]
  }
  ```

**Expected Response:**
- Status: `201 Created`
- Body: Created user details

**Action:**
- Save the `id` from the response for subsequent tests

### 3.4 Update User

**Request:**
- Method: `PUT`
- URL: `{{baseUrl}}/api/users/{userId}`
- Headers:
    - `Authorization: Bearer {{token}}`
- Body (partial update):
  ```json
  {
    "phoneNumber": "5555555555"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body: Updated user details

### 3.5 Admin Reset User Password

**Request:**
- Method: `POST`
- URL: `{{baseUrl}}/api/users/{userId}/reset-password`
- Headers:
    - `Authorization: Bearer {{token}}`
- Body:
  ```json
  {
    "userId": "user-id-here",
    "newPassword": "Reset789!"
  }
  ```

**Expected Response:**
- Status: `200 OK`
- Body: Success message

### 3.6 Update User Roles

**Request:**
- Method: `PUT`
- URL: `{{baseUrl}}/api/users/{userId}/roles`
- Headers:
    - `Authorization: Bearer {{token}}`
- Body:
  ```json
  ["User", "Manager"]
  ```

**Expected Response:**
- Status: `200 OK`
- Body: User with updated roles

### 3.7 Update User Claims

**Request:**
- Method: `PUT`
- URL: `{{baseUrl}}/api/users/{userId}/claims`
- Headers:
    - `Authorization: Bearer {{token}}`
- Body:
  ```json
  [
    {
      "type": "permission",
      "value": "Permissions.Users.View"
    },
    {
      "type": "department",
      "value": "Engineering"
    },
    {
      "type": "subscription_level",
      "value": "Premium"
    }
  ]
  ```

**Expected Response:**
- Status: `200 OK`
- Body: Success message

### 3.8 Delete User

**Request:**
- Method: `DELETE`
- URL: `{{baseUrl}}/api/users/{userId}`
- Headers:
    - `Authorization: Bearer {{token}}`

**Expected Response:**
- Status: `200 OK`
- Body: Success message

## Testing Error Scenarios

### Authentication Failures

1. **Invalid Login**
    - Attempt login with incorrect credentials
    - Expected: `401 Unauthorized`

2. **Access Protected Endpoint Without Token**
    - Call an endpoint without providing a token
    - Expected: `401 Unauthorized`

3. **Access Admin Endpoint With Regular User Token**
    - Login as regular user, then attempt to access admin endpoint
    - Expected: `403 Forbidden`

### Validation Failures

1. **Register with Invalid Data**
    - Missing fields
    - Password too short
    - Non-matching passwords
    - Expected: `400 Bad Request` with validation errors

2. **Create User with Invalid Data**
    - Invalid email format
    - Missing required fields
    - Expected: `400 Bad Request` with validation errors

### Business Logic Failures

1. **Create Duplicate Username**
    - Attempt to register/create user with existing username
    - Expected: `400 Bad Request` with message about username already existing

2. **Delete Non-Existent User**
    - Attempt to delete a user with an invalid ID
    - Expected: `404 Not Found`

## Automated Testing Setup

For more efficient testing, consider creating a Postman collection that:

1. Runs tests in sequence
2. Captures tokens and IDs automatically
3. Uses the Postman test script feature to validate responses

Example test script for login:

```javascript
// Test successful login
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

// Save token to environment variable
var jsonData = pm.response.json();
pm.environment.set("token", jsonData.token);

// Test that token exists
pm.test("Token is present", function () {
    pm.expect(jsonData.token).to.exist;
});
```

## Security Testing Recommendations

1. **Token Expiration**
    - Wait for token to expire, then attempt to use it
    - Expected: `401 Unauthorized`

2. **CSRF Protection**
    - Verify anti-forgery token requirements where appropriate

3. **Rate Limiting**
    - Make multiple failed login attempts to test lockout

4. **Permission Boundaries**
    - Test access to all endpoints with different user roles
    - Verify role-specific access control works correctly