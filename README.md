# Атлетика — Веб-CRM система для фитнес-клуба

Курсовая работа. Веб-CRM система для управления клиентами, тренировками и абонементами фитнес-клуба «Атлетика».

---

## Стек технологий

| Часть | Технология |
|---|---|
| Backend | ASP.NET Web API (.NET 8) |
| База данных | MS SQL Server (LocalDB) |
| ORM | Entity Framework Core |
| Frontend | HTML + CSS + JavaScript (без фреймворков) |
| Документация API | Swagger / OpenAPI |
| Контейнеризация | Docker + Docker Compose |

---

## Структура проекта

```
FitnessClub/
├── FitnessClub/                  ← ASP.NET Web API
│   ├── Controllers/              ← 7 контроллеров
│   │   ├── UsersController.cs
│   │   ├── RolesController.cs
│   │   ├── WorkoutsController.cs
│   │   ├── WorkoutTypesController.cs
│   │   ├── WorkoutRegistrationsController.cs
│   │   ├── MembershipPlansController.cs
│   │   └── ClientMembershipsController.cs
│   ├── Models/                   ← 7 моделей (по ER-диаграмме)
│   ├── DTOs/                     ← Data Transfer Objects
│   ├── Data/                     ← ApplicationDbContext (EF Core)
│   ├── Migrations/               ← Миграции БД
│   └── Program.cs
│
├── css/style.css                 ← Стили (тёмная тема)
├── js/
│   ├── api.js                    ← Работа с API, функции ролей
│   └── layout.js                 ← Навигация по ролям, guard
├── index.html                    ← Дашборд
├── login.html                    ← Страница входа
├── register.html                 ← Страница регистрации
└── pages/
    ├── clients.html              ← Клиенты (только администратор)
    ├── trainers.html             ← Тренеры (только администратор)
    ├── admins.html               ← Администраторы (только администратор)
    ├── workouts.html             ← Тренировки
    ├── workout-types.html        ← Типы тренировок
    ├── registrations.html        ← Записи на тренировки
    ├── memberships.html          ← Абонементы
    ├── plans.html                ← Планы абонементов (только администратор)
    └── analytics.html            ← Финансы и аналитика (только администратор)
```

---

## База данных (ER-диаграмма)

```
Roles ──────────── Users ───────────── Workout_Registrations
                     │                         │
                     │                         │
              Workout_Type              Workouts ──── Workout_Type
                                            │
                                     Client_Membership ── Membership_Plans
```

**Таблицы:**
- `Roles` — роли пользователей (Администратор, Тренер, Клиент)
- `Users` — все пользователи системы
- `Workout_Type` — типы тренировок с уровнем сложности
- `Workouts` — расписание тренировок
- `Workout_Registrations` — записи клиентов на тренировки
- `Membership_Plans` — тарифные планы абонементов
- `Client_Membership` — абонементы клиентов

---

## Роли и доступ

| Функция | Администратор | Тренер | Клиент |
|---|:---:|:---:|:---:|
| Просмотр дашборда | ✅ | ✅ | ✅ |
| Управление клиентами | ✅ | ❌ | ❌ |
| Управление тренерами | ✅ | ❌ | ❌ |
| Управление администраторами | ✅ | ❌ | ❌ |
| Все тренировки (CRUD) | ✅ | ✅ | 👁 просмотр |
| Типы тренировок (CRUD) | ✅ | ✅ | 👁 просмотр |
| Все записи клиентов | ✅ | ✅ | только свои |
| Отметка посещения | ✅ | ✅ | ❌ |
| Все абонементы | ✅ | ❌ | только свои |
| Оформление абонементов | ✅ | ❌ | ❌ |
| Планы абонементов | ✅ | ❌ | ❌ |
| Финансы и аналитика | ✅ | ❌ | ❌ |

---

## Запуск без Docker (локально)

### Требования
- Visual Studio 2022
- .NET 8 SDK
- SQL Server LocalDB (устанавливается вместе с VS)
- Live Server (расширение для VS Code)

### Шаг 1 — Запуск Backend

1. Открой `FitnessClub/FitnessClub.csproj` в Visual Studio 2022
2. Открой `appsettings.json` и убедись что строка подключения правильная:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FitnessClubDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
3. В **Package Manager Console** выполни миграцию:
```
Update-Database
```
4. Нажми **F5** — API запустится на `http://localhost:5000`
5. Swagger доступен по адресу: `http://localhost:5000/swagger`

### Шаг 2 — Запуск Frontend

1. Открой папку `fitness-frontend` в **VS Code**
2. Открой файл `js/api.js` и убедись что порт совпадает:
```javascript
const API_BASE = 'http://localhost:5000/api';
```
3. Установи расширение **Live Server** в VS Code (если ещё нет)
4. Нажми правой кнопкой на `index.html` → **Open with Live Server**
5. Сайт откроется на `http://127.0.0.1:5500`

### Шаг 3 — Создание первого пользователя

1. Открой `http://127.0.0.1:5500/register.html`
2. Выбери тип **Администратор**
3. Введи **Код сотрудника**: `atletika2026`
4. Заполни ФИО, email, пароль → нажми **Зарегистрироваться**

Или создай через Swagger (`POST /api/Users`):
```json
{
  "roleId": 1,
  "fullName": "Администратор",
  "email": "admin@fitness.ru",
  "phoneNumber": "+7 999 000 00 00",
  "password": "admin123"
}
```

---

## API эндпоинты

### Users `/api/Users`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/Users` | Все пользователи |
| GET | `/api/Users/{id}` | Пользователь по ID |
| GET | `/api/Users/by-role/{roleId}` | Пользователи по роли |
| POST | `/api/Users` | Создать пользователя |
| PUT | `/api/Users/{id}` | Обновить пользователя |
| DELETE | `/api/Users/{id}` | Удалить пользователя |

### Workouts `/api/Workouts`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/Workouts` | Все тренировки |
| GET | `/api/Workouts/upcoming` | Предстоящие тренировки |
| GET | `/api/Workouts/by-trainer/{userId}` | Тренировки тренера |
| POST | `/api/Workouts` | Создать тренировку |
| PUT | `/api/Workouts/{id}` | Обновить тренировку |
| DELETE | `/api/Workouts/{id}` | Удалить тренировку |

### WorkoutTypes `/api/WorkoutTypes`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/WorkoutTypes` | Все типы |
| GET | `/api/WorkoutTypes/by-trainer/{userId}` | Типы тренера |
| POST | `/api/WorkoutTypes` | Создать тип |
| PUT | `/api/WorkoutTypes/{id}` | Обновить тип |
| DELETE | `/api/WorkoutTypes/{id}` | Удалить тип |

### WorkoutRegistrations `/api/WorkoutRegistrations`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/WorkoutRegistrations` | Все записи |
| GET | `/api/WorkoutRegistrations/by-workout/{id}` | Записи на тренировку |
| GET | `/api/WorkoutRegistrations/by-client/{id}` | Записи клиента |
| POST | `/api/WorkoutRegistrations` | Записать на тренировку |
| PATCH | `/api/WorkoutRegistrations/{id}/attendance` | Отметить посещение |
| DELETE | `/api/WorkoutRegistrations/{id}` | Удалить запись |

### ClientMemberships `/api/ClientMemberships`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/ClientMemberships` | Все абонементы |
| GET | `/api/ClientMemberships/active` | Активные абонементы |
| GET | `/api/ClientMemberships/by-client/{id}` | Абонементы клиента |
| POST | `/api/ClientMemberships` | Оформить абонемент |
| PATCH | `/api/ClientMemberships/{id}/increment-visit` | Списать посещение |
| DELETE | `/api/ClientMemberships/{id}` | Удалить абонемент |

### MembershipPlans `/api/MembershipPlans`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/MembershipPlans` | Все планы |
| POST | `/api/MembershipPlans` | Создать план |
| PUT | `/api/MembershipPlans/{id}` | Обновить план |
| DELETE | `/api/MembershipPlans/{id}` | Удалить план |

---

## Запуск через Docker

### Требования
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Windows/Mac/Linux)

### Структура файлов Docker
```
FitnessClub/
├── FitnessClub/
│   └── Dockerfile          ← образ для ASP.NET API
├── fitness-frontend/
│   ├── Dockerfile          ← образ для nginx (frontend)
│   └── nginx.conf          ← конфиг nginx
├── docker-compose.yml      ← оркестрация всех сервисов
├── .dockerignore
└── .env.example            ← шаблон переменных окружения
```

### Запуск

**1. Клонируй репозиторий:**
```bash
git clone https://github.com/quarazok/kursovaya.git
cd kursovaya
```

**2. Создай файл `.env` из шаблона:**
```bash
cp .env.example .env
```

**3. Запусти все сервисы одной командой:**
```bash
docker-compose up --build
```

**4. Открой в браузере:**
- Frontend: `http://localhost:3000`
- Swagger API: `http://localhost:8080/swagger`

**5. Остановка:**
```bash
docker-compose down
```

### Полезные команды Docker
```bash
# Пересобрать и запустить
docker-compose up --build

# Запустить в фоне
docker-compose up -d

# Посмотреть логи backend
docker-compose logs -f backend

# Статус всех сервисов
docker-compose ps

# Зайти внутрь контейнера
docker-compose exec backend bash

# Остановить и удалить контейнеры
docker-compose down

# Почистить всё (если место кончилось)
docker system prune -a
```

---

## Работа с системой

### Регистрация

На странице регистрации доступны три типа аккаунта:

| Тип | Код сотрудника | Доступ |
|---|---|---|
| Клиент | не нужен | Свои записи и абонемент |
| Тренер | `atletika2026` | Тренировки и записи |
| Администратор | `atletika2026` | Полный доступ |

> Код сотрудника защищает от случайной регистрации посторонних людей как персонал.

### Рекомендуемый порядок заполнения системы

1. **Зарегистрируйся как администратор**
2. **Добавь тренеров** → раздел «Тренеры»
3. **Создай типы тренировок** → раздел «Типы тренировок» (укажи тренера)
4. **Создай планы абонементов** → раздел «Планы»
5. **Добавь тренировки** → раздел «Тренировки»
6. **Зарегистрируй клиентов** → раздел «Клиенты» или через регистрацию
7. **Оформи абонементы** → раздел «Абонементы»
8. **Запиши клиентов на тренировки** → раздел «Записи»

### Удаление данных

Система поддерживает каскадное удаление — при удалении объекта автоматически удаляются все связанные данные:

| Удаляешь | Автоматически удаляет |
|---|---|
| Клиента | его записи на тренировки + его абонементы |
| Тренера | его тренировки + записи клиентов + типы тренировок |
| Тип тренировки | связанные тренировки + записи клиентов |
| Тренировку | записи клиентов на неё |
| План абонемента | все абонементы с этим тарифом |

---

## Скриншоты

### Вход в систему
Тёмный интерфейс с формой авторизации. Пароли хранятся в виде SHA-256 хеша.

### Дашборд администратора
Статистика клиентов, тренеров, тренировок и абонементов. Таблицы ближайших тренировок и активных абонементов.

### Дашборд тренера
Список предстоящих тренировок тренера.

### Дашборд клиента
Расписание ближайших тренировок и информация о своём абонементе.

### Аналитика и финансы
Выручка по тарифам, посещаемость по типам тренировок, эффективность тренеров, статистика посещений клиентов.

---

## Требования системы

| Компонент | Версия |
|---|---|
| .NET SDK | 8.0+ |
| Visual Studio | 2022 |
| SQL Server LocalDB | Любая актуальная |
| Браузер | Chrome, Firefox, Edge (современные версии) |
| Docker Desktop | 4.0+ (для контейнеризации) |

---

## Автор

Курсовая работа по дисциплине «Разработка веб-приложений».  
Проект: **Веб-CRM система для фитнес-клуба «Атлетика»**.
