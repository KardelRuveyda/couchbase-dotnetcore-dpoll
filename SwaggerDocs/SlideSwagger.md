1. Create Slide
**URL:** POST /api/slide

**Query Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)

**Request Body:**

```json
{
  "Title": "Sample Slide",
  "Order": 1,
  "CreatedAt": "2024-01-01T00:00:00Z",
  "UpdatedAt": "2024-01-01T00:00:00Z",
  "IsActive": true
}
```

2. Get Slide By Id
**URL:** GET /api/slide/{userId}/{presentationId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `id`: Slide ID'si (ör. slide1)

**Request URL Örneği:** GET /api/slide/user1/presentation1/slide1

**Not:** Bu istek için request body gerekmez.

3. Update Slide
**URL:** PUT /api/slide/{userId}/{presentationId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `id`: Slide ID'si (ör. slide1)

**Request Body:**

```json
{
  "Title": "Updated Slide Title",
  "Order": 1,
  "CreatedAt": "2024-01-01T00:00:00Z",
  "UpdatedAt": "2024-01-02T00:00:00Z",
  "IsActive": true
}
```

4. Delete Slide
**URL:** DELETE /api/slide/{userId}/{presentationId}/{id}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)
- `id`: Slide ID'si (ör. slide1)

**Request URL Örneği:** DELETE /api/slide/user1/presentation1/slide1

**Not:** Bu istek için request body gerekmez.

5. Delete All Slides
**URL:** DELETE /api/slide/all/{userId}/{presentationId}

**Path Parameters:**

- `userId`: Kullanıcı ID'si (ör. user1)
- `presentationId`: Sunum ID'si (ör. presentation1)

**Request URL Örneği:** DELETE /api/slide/all/user1/presentation1

**Not:** Bu istek için request body gerekmez.

Bu bilgilerle Swagger'da tüm slaytları silme işlevini de test edebilirsiniz.
