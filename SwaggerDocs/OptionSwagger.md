1. Create Option
**URL:** POST /api/option

**Query Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `questionId`: Soru ID'si (ör. question1)

**Request Body:**

```json
{
  "id": "string",
  "optionText": "B-)Kardel",
  "order": 2,
  "createdAt": "2024-08-03T21:30:52.209Z",
  "updatedAt": "2024-08-03T21:30:52.209Z",
  "isActive": true
}
```

2. Get Option By Id
**URL:** GET /api/option/{userId}/{presentationId}/{slideId}/{questionId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `questionId`: Soru ID'si (ör. question1)
- `id`: Seçenek ID'si (ör. option1)

**Request URL Örneği:** GET /api/option/user1/presentation1/slide1/question1/option1

**Not:** Bu istek için request body gerekmez.

3. Update Option
**URL:** PUT /api/option/{userId}/{presentationId}/{slideId}/{questionId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `questionId`: Soru ID'si (ör. question1)
- `id`: Seçenek ID'si (ör. option1)

**Request Body:**

```json
{
  "id": "string",
  "optionText": "B-)Kardel",
  "order": 2,
  "createdAt": "2024-08-03T21:30:52.209Z",
  "updatedAt": "2024-08-03T21:30:52.209Z",
  "isActive": true
}
```

4. Delete Option
**URL:** DELETE /api/option/{userId}/{presentationId}/{slideId}/{questionId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `slideId`: Slayt ID'si (ör. slide1)
- `questionId`: Soru ID'si (ör. question1)
- `id`: Seçenek ID'si (ör. option1)

**Request URL Örneği:** DELETE /api/option/user1/presentation1/slide1/question1/option1

**Not:** Bu istek için request body gerekmez.
