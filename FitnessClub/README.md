# FitnessClub Web API

ASP.NET Core 8 Web API — CRM для фитнес-клуба «Атлетика».

## Структура проекта

```
FitnessClub/
├── Controllers/
│   ├── RolesController.cs
│   ├── UsersController.cs
│   ├── WorkoutTypesController.cs
│   ├── WorkoutsController.cs
│   ├── WorkoutRegistrationsController.cs
│   ├── MembershipPlansController.cs
│   └── ClientMembershipsController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── DTOs/
│   └── Dtos.cs
├── Models/
│   ├── Role.cs
│   ├── User.cs
│   ├── WorkoutType.cs
│   ├── Workout.cs
│   ├── WorkoutRegistration.cs
│   ├── MembershipPlan.cs
│   └── ClientMembership.cs
├── appsettings.json
└── Program.cs
```

## Порядок таблиц (правильный для FK)

1. **Roles** — нет зависимостей
2. **Users** → Roles
3. **Membership_Plans** — нет зависимостей
4. **Workout_Type** → Users
5. **Workouts** → Workout_Type, Users
6. **Workout_Registrations** → Workouts, Users
7. **Client_Membership** → Users, Membership_Plans

## Настройка и запуск

### 1. Строка подключения
Откройте `appsettings.json` и при необходимости измените строку подключения:
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FitnessClubDb;Trusted_Connection=True;"
```
Для SQL Server Express используйте:
```
Server=.\\SQLEXPRESS;Database=FitnessClubDb;Trusted_Connection=True;
```

### 2. Миграция (Package Manager Console в Visual Studio)
```
Add-Migration InitialCreate
Update-Database
```

Или через .NET CLI:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Запуск
```bash
dotnet run
```
Swagger UI: https://localhost:{port}/swagger

## Seed-данные (создаются автоматически при миграции)

| Id | RoleName        |
|----|-----------------|
| 1  | Администратор   |
| 2  | Тренер          |
| 3  | Клиент          |

## Эндпоинты

### Roles
| Метод  | URL                    | Описание               |
|--------|------------------------|------------------------|
| GET    | /api/roles             | Все роли               |
| GET    | /api/roles/{id}        | Роль по Id             |
| POST   | /api/roles             | Создать роль           |
| PUT    | /api/roles/{id}        | Обновить роль          |
| DELETE | /api/roles/{id}        | Удалить роль           |

### Users
| Метод  | URL                         | Описание                  |
|--------|-----------------------------|---------------------------|
| GET    | /api/users                  | Все пользователи          |
| GET    | /api/users/{id}             | Пользователь по Id        |
| GET    | /api/users/by-role/{roleId} | Пользователи по роли      |
| POST   | /api/users                  | Создать пользователя      |
| PUT    | /api/users/{id}             | Обновить пользователя     |
| DELETE | /api/users/{id}             | Удалить пользователя      |

### WorkoutTypes
| Метод  | URL                                | Описание               |
|--------|------------------------------------|------------------------|
| GET    | /api/workouttypes                  | Все типы тренировок    |
| GET    | /api/workouttypes/{id}             | Тип по Id              |
| GET    | /api/workouttypes/by-trainer/{uid} | Типы по тренеру        |
| POST   | /api/workouttypes                  | Создать тип            |
| PUT    | /api/workouttypes/{id}             | Обновить тип           |
| DELETE | /api/workouttypes/{id}             | Удалить тип            |

### Workouts
| Метод  | URL                              | Описание                  |
|--------|----------------------------------|---------------------------|
| GET    | /api/workouts                    | Все тренировки            |
| GET    | /api/workouts/{id}               | Тренировка по Id          |
| GET    | /api/workouts/by-trainer/{uid}   | Тренировки тренера        |
| GET    | /api/workouts/upcoming           | Предстоящие тренировки    |
| POST   | /api/workouts                    | Создать тренировку        |
| PUT    | /api/workouts/{id}               | Обновить тренировку       |
| DELETE | /api/workouts/{id}               | Удалить тренировку        |

### WorkoutRegistrations
| Метод  | URL                                         | Описание                    |
|--------|---------------------------------------------|-----------------------------|
| GET    | /api/workoutregistrations                   | Все записи                  |
| GET    | /api/workoutregistrations/{id}              | Запись по Id                |
| GET    | /api/workoutregistrations/by-workout/{wid}  | Записи на тренировку        |
| GET    | /api/workoutregistrations/by-client/{uid}   | Записи клиента              |
| POST   | /api/workoutregistrations                   | Записать клиента            |
| PATCH  | /api/workoutregistrations/{id}/attendance   | Отметить посещение          |
| DELETE | /api/workoutregistrations/{id}              | Отменить запись             |

### MembershipPlans
| Метод  | URL                        | Описание             |
|--------|----------------------------|----------------------|
| GET    | /api/membershipplans       | Все планы            |
| GET    | /api/membershipplans/{id}  | План по Id           |
| POST   | /api/membershipplans       | Создать план         |
| PUT    | /api/membershipplans/{id}  | Обновить план        |
| DELETE | /api/membershipplans/{id}  | Удалить план         |

### ClientMemberships
| Метод  | URL                                          | Описание                  |
|--------|----------------------------------------------|---------------------------|
| GET    | /api/clientmemberships                       | Все абонементы            |
| GET    | /api/clientmemberships/{id}                  | Абонемент по Id           |
| GET    | /api/clientmemberships/by-client/{uid}       | Абонементы клиента        |
| GET    | /api/clientmemberships/active                | Активные абонементы       |
| POST   | /api/clientmemberships                       | Оформить абонемент        |
| PATCH  | /api/clientmemberships/{id}/increment-visit  | Списать посещение         |
| DELETE | /api/clientmemberships/{id}                  | Удалить абонемент         |
