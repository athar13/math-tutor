# 📚 Math Tutor (Offline-First)

A web-based math tutor for children to practice math problems.

* 🎯 Offline-first — works without internet after first load
* 🧮 Local backend — all business logic runs in a local Web API (mirrors future cloud API)
* 🚀 Future-ready — same API design when upgrading to a central backend

---

## Project Structure

```
math-tutor/
├── README.md
├── src/
│   ├── MathTutor.Api/          # Backend (Minimal API)
│   │   ├── Controllers/        # Optional, structured endpoints
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   └── SeedData.cs
│   │   ├── Models/
│   │   │   ├── MathProblem.cs
│   │   │   └── Progress.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│   └── mathtutor-client/       # Frontend (Vanilla JS / React)
│       ├── index.html
│       ├── app.js
│       ├── storage.js
│       ├── sw.js               # Service worker for offline caching
│       └── manifest.json
└── MathTutor.sln
```

> Notes:
>
> * `MathTutor.Api` contains backend logic, database, and API endpoints.
> * `mathtutor-client` contains frontend UI and service worker.

---

## Quick start

### Backend (.NET)
```bash
cd src

# initialise database creation (sqlite)
dotnet ef migrations add InitialCreate

# create database
dotnet ef database update

# build project
dotnet build

# run the API
dotnet run
```

Swagger UI: [http://localhost:5132/swagger](http://localhost:5132/swagger)


### Frontend (TBD)
```bash
cd src

```
Not finalized yet.

---

## API — endpoints & examples

### POST `/api/session/start`
**Request**
```http
POST /api/session/start HTTP/1.1
Host: localhost:5132
Content-type: application/json

{
  "numberOfQuestions": 10
}
```
**Response (example)**
```json
{
  "sessionId": "c3a44d89-bd1c-4985-8eb6-3c508d6ffce8",
  "createdAt": "2020-01-01T09:30:30.1234567Z",
  "message": "Session started"
}
```

---

### GET `/api/session/{sessionId}/next`
**Request**
```http
GET /api/session/c3a44d89-bd1c-4985-8eb6-3c508d6ffce8/next HTTP/1.1
Host: localhost:5132
```
**Response (example)**
```json
{
  "id": 1,
  "question": "9 + 9",
  "answer": 18
}
```

---

### POST `/api/session/{sessionId}/answer`
**Request**
```http
POST /api/session/c3a44d89-bd1c-4985-8eb6-3c508d6ffce8/answer HTTP/1.1
Host: localhost:5132
Content-Type: application/json

{
  "id": 1,
  "answer": 18
}
```
**Response (example)**
```json
{
  "correct": true,
  "correctAnswer": 18,
  "totalAnswered": 1,
  "totalQuestions": 1,
  "score": 1,
  "currentStreak": 1,
  "maxStreak": 1,
  "sessionCompleted": false
}
```
---

### GET `/api/session/{sessionId}/summary`
**Request**
```http
GET /api/session/c3a44d89-bd1c-4985-8eb6-3c508d6ffce8/summary HTTP/1.1
Host: localhost:5132
```
**Response (example)**
```json
{
  "id": 18,
  "testSessionIdentifier": "c3a44d89-bd1c-4985-8eb6-3c508d6ffce8",
  "startTime": "2020-01-01T09:30:30.1234567Z",
  "totalQuestions": 15,
  "questionsAnswered": 3,
  "correctAnswers": 2,
  "longestStreak": 5,
  "isActive": true,
  "questions": [
    {
      "problemId": 1,
      "question": "9 + 9",
      "answer": 18
    },
    {
      "problemId": 2,
      "question": "5 + 9",
      "answer": 14,
      "givenAnswer": 14,
      "isCorrect": true
    },
	{
      "problemId": 3,
      "question": "5 + 7",
      "answer": 12,
      "givenAnswer": 13,
      "isCorrect": false
    }
  ]
}
```
