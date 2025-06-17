# 📅 AI Calendar WebAPI

AI Calendar is an ASP.NET Core WebAPI for managing personal calendar events. It supports Natural Language Prompts, multi-user scheduling, and LLM integration via MCP.

## 🚀 Features

- 👤 Simple user authentication
- 📆 Event CRUD: create, update, delete events
- 👥 Manage participants for events
- 🔍 Find the earliest available slot for all participants
- 💬 Natural Language Prompt Support:
  - `"Add meeting with Anna on Friday from 10:00 to 11:00"`
  - `"Cancel all events with title 'Coffee break'"`

## 🛠️ Tech Stack

- .NET 9 ASP.NET Core
- MCP SDK + LLM Integration
- React + Vite + TypeScript
- Swagger / OpenAPI
- FullCalendar (for frontend UI)

## 📌 Main Endpoints

### 📑 Events

```http
GET    /api/v1/events
GET    /api/v1/events/{id}
POST   /api/v1/events
PUT    /api/v1/events/{id}
DELETE /api/v1/events/{id}
```

### 👥 Event Participants

```http
GET    /api/v1/events/{eventId}/participants
POST   /api/v1/events/{eventId}/participants
PUT    /api/v1/events/{eventId}/participants/{userId}
DELETE /api/v1/events/{eventId}/participants/{userId}
```

### 💬 Prompt Executor
```http
POST /api/v1/events/prompt
```

### 🧪 Free Slot Finder
```http
POST /api/v1/events/find-slot
```

### 🧠 Architecture (MCP + LLM Integration)

- Users interact via Console / Web / Native apps
- Prompts go to MCP Server
- MCP Server calls your WebAPI
- WebAPI performs scheduling logic and CRUD actions
- Optionally integrates with Google, Microsoft, Apple Calendars

### 🌐 Frontend UI
- React + TailwindCSS + Vite
- FullCalendar for event display
- PromptExecutor.tsx for entering natural language instructions

### 🧪 Testing
✅ Unit-tested free-slot finder algorithm (TimeSlotFinderService)

✅ Prompt processor tested manually and via Swagger

### ⚙️ Getting Started
```
# 1. Clone the repo
git clone https://github.com/OlesiaKubska/ai-calendar.git
cd ai-calendar

# 2. Start the backend
cd AICalendar
dotnet run
```

### 💻 Frontend (React + TailwindCSS + Vite)
- The frontend source code is available in a separate repository:

-- 🔗 GitHub: [ai-calendar-ui](https://github.com/OlesiaKubska/ai-calendar-ui)

-- 🌐 Live site: [GitHub Pages deployment](https://olesiakubska.github.io/ai-calendar-ui/)

- To run the frontend locally:

```
# 1. Clone the frontend repository
git clone https://github.com/OlesiaKubska/ai-calendar-ui.git
cd ai-calendar-ui

# 2. Install dependencies
npm install

# 3. Run development server
npm run dev
```

### ✅ Completed Tasks
 - Full WebAPI for events and participants
 - Prompt endpoint for LLM integration
 - Frontend UI for calendar and prompt
 - Free-slot search algorithm
 - Unit testing

# 👩‍💻 Author
Olesia Kubska
-  🔗 GitHub: github.com/OlesiaKubska
-  📧 Email: kublesia0908@gmail.com
