# Умный дисплей ЖК — Orion API Gateway

Асинхронный шлюз API (Gateway), обеспечивающий интеграцию с внешним Ujin API, управление конфигурациями (шаблонами) информационных дисплеев в подъездах и рассылку мгновенных оповещений (режим ЧС).

---

## Стек технологий

* **Backend Core**: FastAPI (Python 3.12)
* **Database**: SQLite (асинхронный драйвер `aiosqlite`)
* **ORM & Migrations**: SQLAlchemy 2.0 (Async) + Alembic
* **Integrations**: HTTPX (AsyncClient)
* **Real-time Push**: Server-Sent Events (SSE)
* **Containerization**: Docker & Docker Compose (с автоматическим Healthcheck)
* **Security**: OAuth2 Password Flow, JWT-токены с блэклистингом при выходе (Logout)

---

## Быстрый запуск (Docker) backend_api

Для сборки и запуска всей инфраструктуры в фоновом режиме выполните:
```bash
cd api_gateway
./local_up.sh
```

После этого:
* API шлюза будет доступен по адресу: `http://localhost:8000`
* Интерактивная документация Swagger UI: `http://localhost:8000/docs`
* ReDoc документация: `http://localhost:8000/redoc`

Для остановки всех контейнеров:
```bash
./local_down.sh
```

---

## Локальный запуск (без Docker)

1. **Создайте и активируйте виртуальное окружение:**
   ```bash
   python3 -m venv .venv
   source .venv/bin/activate
   ```
2. **Установите зависимости:**
   ```bash
   pip install -r requirements.txt
   ```
3. **Настройте переменные окружения:**
   Создайте файл `.env` в папке `api_gateway/` на основе `.env.example`:
   ```bash
   cp .env.example .env
   ```
   Укажите актуальные токены доступа к Ujin API.
4. **Запустите миграции базы данных:**
   ```bash
   alembic upgrade head
   ```
5. **Запустите сервер:**
   ```bash
   uvicorn app.main:app --reload
   ```

---

## Быстрый запуск (.NET) frontend_web

Для сборки и запуска всей инфраструктуры в фоновом режиме выполните:
```bash
cd fronted
dotnet build
dotnet watch
```

После этого:
* Web-интерфейс будет доступен по адресу: `http://localhost:7776`

```

## Маршруты API (Endpoints)

### Авторизация и Безопасность
* `POST /api/auth/login` — Вход администратора (получение JWT токена).
* `POST /api/auth/logout` — Выход администратора (отзыв и блэклистинг токена).

### Управление экранами (Админка)
* `GET /api/screens` — Получить список зарегистрированных дисплеев.
* `POST /api/screens` — Регистрация нового экрана в системе.
* `POST /api/screens/assign` — Привязать выбранный шаблон к экранам (вызывает мгновенное событие `template_update` по SSE).

### Шаблоны конфигураций (Админка)
* `GET /api/templates` — Получить список шаблонов.
* `POST /api/templates` — Создать новый шаблон (виджеты: погода, пробки, парковки, новости).
* `PUT /api/templates/{id}` — Изменить шаблон.
* `PATCH /api/templates/{id}/delete` — Мягкое удаление шаблона (soft delete).

### Экстренное оповещение (ЧС)
* `POST /api/emergency/activate` — Активировать режим ЧС (глобально или для конкретных экранов). Моментально рассылает события `emergency_start` на клиенты.
* `POST /api/emergency/reset` — Деактивировать режим ЧС (событие `emergency_stop`).
* `GET /api/emergency/active` — Список активных ЧС.
* `GET /api/emergency/logs` — Журнал аудита действий операторов ЧС.

### Клиентские дисплеи (Client API)
* `GET /api/client/screen/{screen_id}` — Получить полную конфигурацию экрана (активный шаблон + статус ЧС) при первоначальной загрузке.
* `GET /api/client/stream/{screen_id}` — Долгоживущее SSE-соединение (Server-Sent Events) для получения реалтайм-команд (смена шаблона, запуск/отмена ЧС).

### Проксирование Ujin API
* `GET /api/ujin/complexes` — Список жилых комплексов.
* `GET /api/ujin/buildings` — Список зданий комплекса.
* `GET /api/ujin/parking/free` — Свободные парковочные места.
* `GET /api/ujin/storage/free` — Свободные кладовые помещения.
* `GET /api/ujin/news` — Новости ЖК для информационного виджета.

---

## Архитектура реалтайм-обновлений
В шлюзе используется **гибридная модель**:
1. **Server-Sent Events (SSE):** Сервер удерживает постоянный канал к каждому дисплею. Это нативный HTTP-протокол. При действиях администратора (запуск ЧС, смена шаблона) оповещение доставляется на экран за миллисекунды. При обрыве связи браузер на экране автоматически восстанавливает подключение.
2. **Client-side Polling:** Медленно меняющиеся данные Ujin (новости, погода, парковки) экран запраширует самостоятельно по REST-интерфейсу раз в 60 секунд. Это бережет ресурсы шлюза и исключает перегрузку внешнего API Ujin.
