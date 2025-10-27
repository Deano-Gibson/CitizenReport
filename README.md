# ⚙️ CitizenReport — Quick Start & User Guide (MVC Branch)

> **Scope:** ASP.NET Core MVC app (`CitizenReportWeb`). Focused on fast setup and clear use.

---

## 🧰 1) Prerequisites

* **.NET 8 SDK** or later
* **IDE:** Visual Studio 2022 / VS Code / JetBrains Rider
* **Modern browser:** Edge, Chrome, or Firefox

---

## 📦 2) Get the Code

* **If you received a ZIP:**

  1. Unzip the archive to a local folder with a short path (e.g., `C:\CitizenReport`).
  2. Open `CitizenReportWeb.sln` or `CitizenReportWeb.csproj` in your IDE.
* **If you prefer Git:**

```bash
git clone https://github.com/Deano-Gibson/CitizenReport.git
cd CitizenReport/CitizenReportWeb
```

Open the solution or `CitizenReportWeb.csproj` in your IDE.

---

## 🗂️ 3) Project Structure (folders)

```
CitizenReportWeb/
├─ Controllers/         # MVC controllers
├─ Data/                # In‑memory data stores and helpers
├─ Models/              # Domain and ViewModels (or separate ViewModels/)
├─ Properties/          # launchSettings.json etc.
├─ ViewModels/          # View model types used by the Views
├─ Views/               # Razor views
│  ├─ Home/             # Index, ReportIssues, LocalEvents, ServiceRequestStatus, Privacy
│  └─ Shared/           # _Layout, _ViewImports, _ViewStart, partials
├─ wwwroot/             # Static assets
│  ├─ css/
│  ├─ js/
│  ├─ lib/
│  └─ uploads/          # User attachments at runtime
├─ Program.cs           # App bootstrap
└─ CitizenReportWeb.csproj
```

---

## 🏗️ 4) Build

**Visual Studio**: Set **CitizenReportWeb** as Startup Project → Build Solution (`Ctrl+Shift+B`).

**CLI**:

```bash
cd CitizenReportWeb
dotnet restore
dotnet build
```

---

## ▶️ 5) Run

**Visual Studio**: F5 (debug) or Ctrl+F5 (no debug).

**CLI**:

```bash
dotnet run
# open the shown URL (e.g. https://localhost:7001)
```

> If the app reports that the uploads folder is missing, create `wwwroot/uploads`.

---

## 🧭 6) Using CitizenReport (first‑run path)

1. **Home** → choose a section: **Report Issues**, **Local Events**, or **Service Status**.
2. **Report an Issue**:

   * Enter **Location**, select **Category**, add **Description**.
   * Attach files *(optional)* then **Submit**.
   * A short **reference ID** is displayed for tracking.
3. **Browse & Filter**:

   * Go to **Service Status** or **Issues** list to view items.
   * Use the **search bar** to filter by words like `water`, `pothole`, or a ward.
4. **Attachments**:

   * Uploaded files are saved under `wwwroot/uploads`.

> Default mode uses in‑memory data. Restarting clears demo entries.

---

## Images 

<img width="2538" height="1357" alt="image" src="https://github.com/user-attachments/assets/dbd7b1bb-dcb7-4987-bd05-d080c584f256" />

<img width="1436" height="671" alt="image" src="https://github.com/user-attachments/assets/7fc52e79-3300-441e-adca-57edfb774e69" />

<img width="1568" height="1003" alt="image" src="https://github.com/user-attachments/assets/62c7bf61-4eeb-4ef3-a82b-cd3cf3c8267b" />

<img width="1552" height="513" alt="image" src="https://github.com/user-attachments/assets/77686a40-a927-4994-bf8a-9bb63d9a83ce" />


