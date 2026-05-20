# Атлетика — Веб-CRM система для фитнес-клуба

Курсовая работа. Веб-CRM система для управления клиентами, тренировками и абонементами фитнес-клуба «Атлетика».

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

## Структура проекта

```
FitnessClub/
├── FitnessClub/                  ← ASP.NET Web API
│   └── FitnessClub/
│       ├── Controllers/          ← 7 контроллеров
│       ├── Models/               ← 7 моделей
│       ├── DTOs/
│       ├── Data/
│       ├── Migrations/
│       └── Dockerfile
├── frontend/                     ← HTML + CSS + JS
│   ├── css/
│   ├── js/
│   ├── pages/
│   ├── index.html
│   ├── login.html
│   ├── register.html
│   ├── Dockerfile
│   └── nginx.conf
├── docker-compose.yml
├── .dockerignore
└── .gitignore
```

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

После запуска открой в браузере:

| Что | Адрес |
|---|---|
| Сайт | http://localhost:3000 |
| Swagger | http://localhost:8080/swagger |

### Остановка
```bash
docker-compose down
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

### Frontend

1. Открой папку `frontend/` в VS Code
2. Убедись что в `frontend/js/api.js` стоит порт `5000`:
```javascript
const API_BASE = 'http://localhost:5000/api';
```
3. Правой кнопкой на `frontend/index.html` → **Open with Live Server**

---

## Аккаунты для входа

| Роль | Email | Пароль |
|---|---|---|
| Администратор | admin@atletika.ru | admin123 |
| Администратор | marina@atletika.ru | admin123 |
| Тренер | petrov@atletika.ru | trainer123 |
| Тренер | ivanova@atletika.ru | trainer123 |
| Тренер | zaharov@atletika.ru | trainer123 |
| Клиент | novikov@mail.ru | client123 |
| Клиент | sokolova@mail.ru | client123 |
| Клиент | morozov@mail.ru | client123 |
| Клиент | lebedeva@mail.ru | client123 |
| Клиент | volkov@mail.ru | client123 |

> Для регистрации как Тренер или Администратор нужен код сотрудника: `atletika2026`

---

## Роли и доступ

| Функция | Администратор | Тренер | Клиент |
|---|:---:|:---:|:---:|
| Дашборд | ✅ | ✅ | ✅ |
| Клиенты / Тренеры / Администраторы | ✅ | ❌ | ❌ |
| Тренировки — просмотр | ✅ | ✅ | ✅ |
| Тренировки — добавление/редактирование | ✅ | ✅ | ❌ |
| Записи клиентов | ✅ | ✅ | только свои |
| Отметка посещения | ✅ | ✅ | ❌ |
| Абонементы | ✅ | ❌ | только свои |
| Оформление абонементов | ✅ | ❌ | ❌ |
| Финансы и аналитика | ✅ | ❌ | ❌ |

---

## Репозиторий

[https://github.com/ekztasii/FitnessClub](https://github.com/ekztasii/FitnessClub)
