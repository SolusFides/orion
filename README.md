# Умный дисплей ЖК

Информационная система для отображения виджетов на жк панелях в подъездах и управления ими из панели УК.

---

## Стек технологий

* **Backend**: FastAPI, SQLite
* **Frontend (Админка УК)**: Blazor
* **Frontend (Мониторы)**: React

---

## API

### 0. Авторизация
* `POST /api/auth/login` — Вход администратора по токену Ujin.
* `POST /api/auth/logout` — Выход администратора.

### 1. Админ панель
* `GET /api/screens` — Получить список подключенных экранов.
* `POST /api/screens` — Добавить/обновить экран.
* `POST /api/screens/assign` — Привязать шаблон к экранам.
* `GET /api/templates` — Получить список шаблонов.
* `POST /api/templates` — Создать новый шаблон.
* `PUT /api/templates/{id}` — Обновить существующий шаблон.
* `PATCH /api/templates/{id}/delete` — Мягкое удаление шаблона.

### 2. Экстренные оповещения
* `POST /api/emergency/activate` — Активировать режим ЧС.
* `POST /api/emergency/reset` — Деактивировать режим ЧС.
* `GET /api/emergency/active` — Получить список активных ЧС.

### 3. Дисплеи клиентов
* `GET /api/client/screen/{screen_id}` — Получить текущие данные для отображения.
* `GET /api/client/stream/{screen_id}` — Потоковая передача обновлений.

### 4. Ujin API
* `GET /api/ujin/news` — Новости и объявления.
* `GET /api/ujin/parking/free` — Свободные парковочные места.
* `GET /api/ujin/storage/free` — Свободные кладовые помещения.
