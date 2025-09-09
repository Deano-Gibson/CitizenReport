# CitizenReport — Quick Start

> **Important:** This repo contains both an MVC and a Winform application. This is due to the POE requiring winform, but the powerpoint allowing freedom. If either is allowed Please mark the **MVC** version. Thank You!

---

## 1) Prerequisites

* **WinForms:** Windows 10/11, Visual Studio 2019/2022, **.NET Framework 4.8** (or 4.7.2+).
* **MVC:** .NET **8** SDK, Visual Studio 2022 / VS Code / Rider, modern browser.

---

## 2) Open the Projects

* **WinForms:** Open `CitizenReportWinForms.sln` in Visual Studio.
* **MVC:** Open the solution containing `CitizenReportWeb` (or open `CitizenReportWeb.csproj`).

---

## 3) Build / Compile

**Visual Studio**

* Set the desired project as **Startup Project** (right‑click → *Set as StartUp Project*).
* **Build** → *Build Solution* (Ctrl+Shift+B).

**CLI (MVC only)**

```bash
cd CitizenReportWeb
dotnet restore
dotnet build
```

---

## 4) Run

**WinForms (VS):** *Debug* → *Start Debugging* (F5).

**MVC (VS):** *Debug* → *Start Debugging* (F5).

**MVC (CLI):**

```bash
cd CitizenReportWeb
dotnet run
# open the printed URL (e.g., https://localhost:7001)
```

---

## 5) Use the Software

* **Main Menu:** Three options are shown; only **Report Issues** is enabled (per Part‑1 spec).
* **Report Issues:** Enter *Location*, choose *Category*, add a brief *Description* and optional *Attachments*, then **Submit**.
* **Feedback:** A success message with a short reference ID confirms capture.

> **Note:** MVC uploads are saved to `wwwroot/uploads/`. In Part‑1 both apps use in‑memory lists for issues (no DB).

---

