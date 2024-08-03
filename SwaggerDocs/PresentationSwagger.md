1. Create Presentation
**URL:** POST /api/presentation

**Query Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)

**Request Body:**

```json
{
  "id": "string",
  "title": "string",
  "order": 0,
  "createdAt": "2024-08-03T13:44:22.137Z",
  "updatedAt": "2024-08-03T13:44:22.137Z",
  "isActive": true
}
```

2. Get Presentation By Id
**URL:** GET /api/presentation/{userId}/{presentationId}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)

**Request URL Örneği:** GET /api/presentation/user1/presentation1

**Not:** Bu istek için request body gerekmez.

3. Update Presentation
**URL:** PUT /api/presentation/{userId}/{presentationId}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)

**Request Body:**

```json
{
  "id": "string",
  "title": "string",
  "order": 0,
  "createdAt": "2024-08-03T13:44:22.137Z",
  "updatedAt": "2024-08-03T13:44:22.137Z",
  "isActive": true
}
```

4. Delete Presentation
**URL:** DELETE /api/presentation/{userId}/{presentationId}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)

**Request URL Örneği:** DELETE /api/presentation/user1/presentation1

**Not:** Bu istek için request body gerekmez.
