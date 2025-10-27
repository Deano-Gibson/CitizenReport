# ⚙️ CitizenReport — Quick Start (MVC Branch)

> **Important:** This repository documents the **MVC (ASP.NET Core)** version of CitizenReport, the preferred implementation for marking and final submission.

---

## 🧰 1) Prerequisites

* .NET 8 SDK (or later)
* Visual Studio 2022 / VS Code / JetBrains Rider
* Modern web browser (Edge, Chrome, Firefox)

---

## 📂 2) Open the Project

Open the solution containing `CitizenReportWeb`, or directly open `CitizenReportWeb.csproj` in Visual Studio.

---

## 🏗️ 3) Build / Compile

### Visual Studio

1. Set **CitizenReportWeb** as the **Startup Project** (right-click → *Set as StartUp Project*).
2. Go to **Build → Build Solution** (`Ctrl+Shift+B`).

### Command Line

```bash
cd CitizenReportWeb
dotnet restore
dotnet build
```

---

## ▶️ 4) Run

### Visual Studio

> **Debug → Start Debugging (F5)**

### Command Line

```bash
cd CitizenReportWeb
dotnet run
# open the printed URL (e.g. https://localhost:7001)
```

---

## 💡 5) Use the Software

* **Main Menu:** Displays three buttons — *Report Issues*, *Local Events*, and *Service Status*.
* **Report Issues:** Fill in *Location*, select *Category*, add a *Description* and optional *Attachments*, then click **Submit**.
* **Feedback:** A confirmation message appears with a generated short reference ID.

> **Note:** Attachments are stored under `wwwroot/uploads/`. The app uses in-memory lists for demonstration purposes (no database persistence).

---

**Developer:** Dean Gibson (ST10326084)
**Module:** PROG7312 / PROG7311 — Municipal Services Application
**Institution:** Varsity College, 2025
