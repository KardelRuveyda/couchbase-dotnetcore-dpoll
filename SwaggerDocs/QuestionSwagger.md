1. Create Question
**URL:** POST /api/question

**Query Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)

**Request Body:**

```json
{
  "Text": "What is the main topic of this slide?",
  "Order": 1,
  "CreatedAt": "2024-01-01T00:00:00Z",
  "UpdatedAt": "2024-01-01T00:00:00Z",
  "IsActive": true
}
```

2. Get Question By Id
**URL:** GET /api/question/{userId}/{presentationId}/{slideId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `id`: Question ID'si (ör. question1)

**Request URL Örneği:** GET /api/question/user1/presentation1/slide1/question1

**Not:** Bu istek için request body gerekmez.

3. Update Question
**URL:** PUT /api/question/{userId}/{presentationId}/{slideId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `id`: Question ID'si (ör. question1)

**Request Body:**

```json
{
  "Text": "Updated question text",
  "Order": 2,
  "CreatedAt": "2024-01-01T00:00:00Z",
  "UpdatedAt": "2024-01-02T00:00:00Z",
  "IsActive": true
}
```

4. Delete Question
**URL:** DELETE /api/question/{userId}/{presentationId}/{slideId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `id`: Question ID'si (ör. question1)

**Request URL Örneği:** DELETE /api/question/user1/presentation1/slide1/question1

**Not:** Bu istek için request body gerekmez.
