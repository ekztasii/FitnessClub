# Атлетика — Веб-CRM система для фитнес-клуба

Курсовая работа. Веб-CRM система для автоматизации учёта клиентов, управления абонементами и расписанием тренировок фитнес-клуба «Атлетика».

---

## Стек технологий

| Часть | Технология |
|---|---|
| Backend | ASP.NET Web API (.NET 8) |
| База данных | MS SQL Server (LocalDB / Docker) |
| ORM | Entity Framework Core 8 |
| Frontend | HTML + CSS + JavaScript |
| Документация API | Swagger / OpenAPI |
| Контейнеризация | Docker + Docker Compose |

---

## Структура репозитория

```
FitnessClub/
├── FitnessClub/                    ← ASP.NET Web API проект
│   ├── FitnessClub/
│   │   ├── Controllers/            ← 7 контроллеров
│   │   ├── Models/                 ← 7 моделей по ER-диаграмме
│   │   ├── DTOs/                   ← Data Transfer Objects
│   │   ├── Data/                   ← ApplicationDbContext (EF Core)
│   │   ├── Migrations/             ← Миграции БД
│   │   ├── Program.cs
│   │   └── FitnessClub.csproj
│   └── Dockerfile                  ← Docker образ для backend
│
├── css/style.css                   ← Стили (тёмная тема)
├── js/
│   ├── api.js                      ← Работа с API + функции ролей
│   └── layout.js                   ← Навигация по ролям + guard
├── pages/
│   ├── clients.html                ← Клиенты (только администратор)
│   ├── trainers.html               ← Тренеры (только администратор)
│   ├── admins.html                 ← Администраторы
│   ├── workouts.html               ← Тренировки
│   ├── workout-types.html          ← Типы тренировок
│   ├── registrations.html          ← Записи на тренировки
│   ├── memberships.html            ← Абонементы
│   ├── plans.html                  ← Планы абонементов
│   └── analytics.html              ← Финансы и аналитика
├── index.html                      ← Дашборд
├── login.html                      ← Вход
├── register.html                   ← Регистрация
├── Dockerfile.frontend             ← Docker образ для frontend (nginx)
├── nginx.conf                      ← Конфигурация nginx
├── docker-compose.yml              ← Оркестрация всех сервисов
└── .dockerignore
```

---

## Роли и доступ

| Функция | Администратор | Тренер | Клиент |
|---|:---:|:---:|:---:|
| Дашборд | ✅ | ✅ | ✅ |
| Клиенты (CRUD) | ✅ | ❌ | ❌ |
| Тренеры (CRUD) | ✅ | ❌ | ❌ |
| Администраторы (CRUD) | ✅ | ❌ | ❌ |
| Тренировки — просмотр | ✅ | ✅ | ✅ |
| Тренировки — редактирование | ✅ | ✅ | ❌ |
| Типы тренировок | ✅ | ✅ | 👁 |
| Все записи клиентов | ✅ | ✅ | ❌ |
| Свои записи | ✅ | ✅ | ✅ |
| Отметка посещения | ✅ | ✅ | ❌ |
| Все абонементы | ✅ | ❌ | ❌ |
| Свой абонемент | ✅ | ❌ | ✅ |
| Оформление абонементов | ✅ | ❌ | ❌ |
| Планы абонементов | ✅ | ❌ | ❌ |
| Финансы и аналитика | ✅ | ❌ | ❌ |

---

## Запуск через Docker

### Требования
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### 3 команды — и всё работает

```bash
git clone https://github.com/ekztasii/FitnessClub.git
cd FitnessClub
docker-compose up --build
```

Первый запуск занимает 3–5 минут. Когда в консоли появится:

```
fitnessclub_api | Now listening on: http://[::]:8080
fitnessclub_api | Миграция выполнена успешно.
```

Открывай в браузере:

| Что | Адрес |
|---|---|
| Сайт | http://localhost:3000 |
| Swagger | http://localhost:8080/swagger |

### Команды управления

```bash
# Пересобрать после изменений в коде
docker-compose up --build

# Запустить в фоне
docker-compose up -d

# Логи backend
docker-compose logs -f backend

# Статус контейнеров
docker-compose ps

# Остановить (данные сохранятся)
docker-compose down

# Остановить и удалить БД
docker-compose down -v
```

---

## Запуск локально (Visual Studio)

### Требования
- Visual Studio 2022
- .NET 8 SDK
- SQL Server LocalDB
- VS Code + расширение Live Server

### Backend

1. Открой `FitnessClub/FitnessClub/FitnessClub.csproj` в Visual Studio 2022
2. В **Package Manager Console** выполни:
```
Update-Database
```
3. Нажми **F5** — API запустится на `http://localhost:5000`
4. Swagger: `http://localhost:5000/swagger`

### Frontend

1. В `js/api.js` поменяй порт на `5000`:
```javascript
const API_BASE = 'http://localhost:5000/api';
```
2. Открой корневую папку в VS Code
3. Правой кнопкой на `index.html` → **Open with Live Server**

---

## Первый запуск — создание пользователей

Открой страницу регистрации и создай пользователей:

| Тип | Код сотрудника | Доступ |
|---|---|---|
| Клиент | не нужен | Свои записи и абонемент |
| Тренер | `atletika2026` | Тренировки и записи |
| Администратор | `atletika2026` | Полный доступ |

Или через Swagger `POST /api/Users`:

```json
{ "roleId": 1, "fullName": "Администратор", "email": "admin@fitness.ru", "phoneNumber": "+7 999 000 00 00", "password": "admin123" }
```
```json
{ "roleId": 2, "fullName": "Петров Пётр Петрович", "email": "trainer@fitness.ru", "phoneNumber": "+7 999 111 11 11", "password": "trainer123" }
```
```json
{ "roleId": 3, "fullName": "Иванов Иван Иванович", "email": "client@fitness.ru", "phoneNumber": "+7 999 222 22 22", "password": "client123" }
```

---

## Рекомендуемый порядок заполнения

1. Зарегистрируйся как **Администратор**
2. Добавь **тренеров** → «Тренеры»
3. Создай **типы тренировок** → «Типы тренировок»
4. Создай **планы абонементов** → «Планы»
5. Добавь **тренировки** → «Тренировки»
6. Зарегистрируй **клиентов** → «Клиенты»
7. Оформи **абонементы** → «Абонементы»
8. Запиши клиентов на тренировки → «Записи»

---

## API эндпоинты

### Users `/api/Users`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/Users` | Все пользователи |
| GET | `/api/Users/{id}` | Пользователь по ID |
| GET | `/api/Users/by-role/{roleId}` | По роли |
| POST | `/api/Users` | Создать |
| PUT | `/api/Users/{id}` | Обновить |
| DELETE | `/api/Users/{id}` | Удалить |

### Workouts `/api/Workouts`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/Workouts` | Все тренировки |
| GET | `/api/Workouts/upcoming` | Предстоящие |
| GET | `/api/Workouts/by-trainer/{userId}` | По тренеру |
| POST | `/api/Workouts` | Создать |
| PUT | `/api/Workouts/{id}` | Обновить |
| DELETE | `/api/Workouts/{id}` | Удалить |

### WorkoutTypes `/api/WorkoutTypes`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/WorkoutTypes` | Все типы |
| GET | `/api/WorkoutTypes/by-trainer/{userId}` | По тренеру |
| POST | `/api/WorkoutTypes` | Создать |
| PUT | `/api/WorkoutTypes/{id}` | Обновить |
| DELETE | `/api/WorkoutTypes/{id}` | Удалить |

### WorkoutRegistrations `/api/WorkoutRegistrations`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/WorkoutRegistrations` | Все записи |
| GET | `/api/WorkoutRegistrations/by-workout/{id}` | По тренировке |
| GET | `/api/WorkoutRegistrations/by-client/{id}` | По клиенту |
| POST | `/api/WorkoutRegistrations` | Записать |
| PATCH | `/api/WorkoutRegistrations/{id}/attendance` | Отметить посещение |
| DELETE | `/api/WorkoutRegistrations/{id}` | Удалить |

### ClientMemberships `/api/ClientMemberships`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/ClientMemberships` | Все абонементы |
| GET | `/api/ClientMemberships/active` | Активные |
| GET | `/api/ClientMemberships/by-client/{id}` | По клиенту |
| POST | `/api/ClientMemberships` | Оформить |
| PATCH | `/api/ClientMemberships/{id}/increment-visit` | Списать посещение |
| DELETE | `/api/ClientMemberships/{id}` | Удалить |

### MembershipPlans `/api/MembershipPlans`
| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/MembershipPlans` | Все планы |
| POST | `/api/MembershipPlans` | Создать |
| PUT | `/api/MembershipPlans/{id}` | Обновить |
| DELETE | `/api/MembershipPlans/{id}` | Удалить |

---

## Каскадное удаление

| Удаляешь | Автоматически удаляется |
|---|---|
| Клиента | его записи на тренировки + его абонементы |
| Тренера | его тренировки + записи клиентов + типы тренировок |
| Тип тренировки | связанные тренировки + записи клиентов |
| Тренировку | записи клиентов на неё |
| План абонемента | все абонементы с этим тарифом |

---

## Требования к системе

| Компонент | Версия |
|---|---|
| .NET SDK | 8.0+ |
| Visual Studio | 2022 |
| SQL Server LocalDB | Любая |
| Docker Desktop | 4.0+ |
| Браузер | Chrome, Firefox, Edge |
