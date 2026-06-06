# API Reference

---

## 1. Авторизация

Все Admin-роуты требуют заголовок: `Authorization: Bearer <access_token>`

### `POST /api/auth/login`
* **Request Body**:
  ```json
  { "username": "admin", "password": "secret" }
  ```
* **Response (200)**:
  ```json
  {
    "access_token": "eyJhbGciOiJIUzI1NiIs...",
    "token_type": "bearer",
    "expires_in": 86400
  }
  ```


### `POST /api/auth/logout`
* **Response (200)**:
  ```json
  { "status": "success" }
  ```

---

## 2. Экраны (Admin)

### `GET /api/screens`
* **Response (200)**:
  ```json
  [
    {
      "id": "main_hall_01",
      "name": "Главный холл 1",
      "complex_id": 73,
      "building_id": 141,
      "entrance": 1,
      "current_template_id": "uuid-template-123"
    }
  ]
  ```

### `POST /api/screens`
* **Request Body**:
  ```json
  { "id": "lift_02", "name": "Лифтовой холл 2", "complex_id": 73, "building_id": 141, "entrance": 2 }
  ```
* **Response (200)**:
  ```json
  { "status": "success", "screen_id": "lift_02" }
  ```

### `POST /api/screens/assign`
* **Request Body**:
  ```json
  { "screen_ids": ["main_hall_01", "lift_02"], "template_id": "uuid-template-123" }
  ```
* **Response (200)**:
  ```json
  { "status": "success", "assigned_screens": ["main_hall_01", "lift_02"] }
  ```

---

## 3. Шаблоны (Admin)

### `GET /api/templates`
* **Response (200)**:
  ```json
  [
    {
      "id": "uuid-template-123",
      "name": "Стандартная сетка",
      "config": { "layout": "grid", "widgets": ["news", "parking"], "theme": "dark" }
    }
  ]
  ```

### `POST /api/templates`
* **Request Body**:
  ```json
  { "name": "Летний стиль", "config": { "layout": "split", "widgets": ["news", "storage"], "theme": "light" } }
  ```
* **Response (201)**:
  ```json
  { "status": "success", "template_id": "uuid-new" }
  ```

### `PUT /api/templates/{id}`
* **Request Body**:
  ```json
  { "name": "Обновлённый", "config": { "layout": "grid", "widgets": ["news", "parking", "storage"], "theme": "dark" } }
  ```
* **Response (200)**:
  ```json
  { "status": "success" }
  ```

### `PATCH /api/templates/{id}/delete`
* **Response (200)**:
  ```json
  { "status": "success", "deleted_id": "uuid-template-123" }
  ```

---

## 4. Чрезвычайные ситуации (Admin)

### `POST /api/emergency/activate`
* **Request Body**:
  ```json
  {
    "text": "ВНИМАНИЕ!!! Задымление в холле третьего подъезда!",
    "screen_ids": ["main_hall_01"],
    "priority": 1,
    "timeout_minutes": 30
  }
  ```
  *(передайте `"screen_ids": ["all"]` для всех экранов)*
* **Response (200)**:
  ```json
  { "status": "success", "emergency_id": "uuid-emergency" }
  ```

### `POST /api/emergency/reset`
* **Request Body**:
  ```json
  { "screen_ids": ["main_hall_01"] }
  ```
* **Response (200)**:
  ```json
  { "status": "success" }
  ```

### `GET /api/emergency/active`
* **Response (200)**:
  ```json
  [
    {
      "id": "uuid-emergency",
      "text": "ВНИМАНИЕ!!! Задымление...",
      "target_screens": ["main_hall_01"],
      "priority": 1,
      "timeout_minutes": 30,
      "activated_by": "admin",
      "created_at": "2026-06-06T15:30:00"
    }
  ]
  ```

### `GET /api/emergency/log`
* **Response (200)**:
  ```json
  [
    {
      "id": "uuid-log",
      "action": "activate",
      "user_id": "admin",
      "target_screens": ["main_hall_01"],
      "text": "ВНИМАНИЕ!!! Задымление...",
      "created_at": "2026-06-06T15:30:00"
    }
  ]
  ```

---

## 5. Дисплей клиента (React)

### `GET /api/client/screen/{screen_id}`
* **Response (200)**:
  ```json
  {
    "screen_id": "main_hall_01",
    "template": {
      "id": "uuid-template-123",
      "name": "Стандартная сетка",
      "config": { "layout": "grid", "widgets": ["news", "parking"], "theme": "dark" }
    },
    "active_emergency": {
      "text": "ВНИМАНИЕ!!! Задымление...",
      "priority": 1
    }
  }
  ```
  *(`active_emergency` равен `null`, если ЧС нет)*

### `GET /api/client/stream/{screen_id}`
Server-Sent Events (SSE) поток. Формат сообщений:

* **Запуск ЧС**:
  ```json
  event: emergency_start
  data: {"text": "ВНИМАНИЕ!!! Задымление...", "priority": 1}
  ```
* **Отмена ЧС**:
  ```json
  event: emergency_stop
  data: {}
  ```

---

## 6. Прокси Ujin API

### `GET /api/ujin/news?building_id=141`
* **Response (200)**:
  ```json
  [
    { "id": 135, "date": "2026-06-02", "title": "Заявки через приложение", "text": "<p>...</p>", "images": [] }
  ]
  ```

### `GET /api/ujin/parking/free?building_id=141`
* **Response (200)**:
  ```json
  {
    "total_free": 12,
    "zones": [
      { "name": "Наземный паркинг", "free_count": 4 },
      { "name": "Подземный паркинг", "free_count": 8 }
    ]
  }
  ```

### `GET /api/ujin/storage/free?building_id=141`
* **Response (200)**:
  ```json
  { "total_free": 9 }
  ```
