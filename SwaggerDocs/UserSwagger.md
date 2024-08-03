# API Kullanım Kılavuzu

## 1. Create User
**URL:** POST /api/userinfo/add

**Request Body:**
{
 "id": "string",
  "firstName": "string",
  "lastName": "string",
  "emailAddress": "string",
  "phoneNumber": "string",
  "clerkId": "string",
  "createdAt": "2024-08-03T13:27:58.538Z",
  "updatedAt": "2024-08-03T13:27:58.538Z",
  "isActive": true
}

**Response:**
- 200 OK: "User added successfully."
- 400 Bad Request: "User information is null."

## 2. Get User By Id
**URL:** GET /api/userinfo/{id}

**Path Parameters:**
- `id`: Kullanıcı ID'si (ör. user1)

**Response:**
- 200 OK: Kullanıcı bilgileri döner.
- 404 Not Found: Kullanıcı bulunamadı.

## 3. Update User
**URL:** PUT /api/userinfo/update/{id}

**Path Parameters:**
- `id`: Kullanıcı ID'si (ör. user1)

**Request Body:**
{
 "id": "string",
  "firstName": "string",
  "lastName": "string",
  "emailAddress": "string",
  "phoneNumber": "string",
  "clerkId": "string",
  "createdAt": "2024-08-03T13:27:58.538Z",
  "updatedAt": "2024-08-03T13:27:58.538Z",
  "isActive": true
}

**Response:**
- 200 OK: "User updated successfully."
- 400 Bad Request: "User information is null or ID mismatch."
- 404 Not Found: "User not found."

## 4. Delete User
**URL:** DELETE /api/userinfo/delete/{id}

**Path Parameters:**
- `id`: Kullanıcı ID'si (ör. user1)

**Response:**
- 200 OK: "User deleted successfully."
- 404 Not Found: "User not found."
