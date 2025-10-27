# ⚙️ CitizenReport — Quick Start (Main Branch / MVC-Focused)

> **Important:** This repository contains **two project implementations**:
>
> 1. **MVC (ASP.NET Core)** — the modern, structured web version (preferred for marking)
> 2. **WinForms (C# Desktop)** — the original prototype version (decommissioned)
>
> The MVC version is the focus of the final PoE submission. If both qualify, please **mark the MVC version**.

---

## 📂 Repository Overview

| Branch                                                                  | Description                                       |
| ----------------------------------------------------------------------- | ------------------------------------------------- |
| [`mvc`](https://github.com/Deano-Gibson/CitizenReport/tree/mvc)         | ASP.NET MVC Web Application (current, maintained) |
| [`winform`](https://github.com/Deano-Gibson/CitizenReport/tree/winForm) | Windows Forms version (deprecated)                |

---

## 🌐 MVC — Quick Start

### 🧰 Prerequisites

* .NET **8 SDK** or later
* Visual Studio 2022 / VS Code / JetBrains Rider
* Git installed

### ▶️ Run using Visual Studio

1. Clone the repository:

   ```bash
   git clone https://github.com/Deano-Gibson/CitizenReport.git
   ```
2. Checkout the MVC branch:

   ```bash
   git checkout mvc
   ```
3. Open **CitizenReportWeb.sln**
4. Set `CitizenReportWeb` as the **Startup Project**
5. Click **Run (F5)** or use `Ctrl+F5`
6. Navigate to:

   ```
   https://localhost:7001
   ```
7. Explore pages:

   * **Home:** Project introduction
   * **Report Issues:** Submit a municipal report with category and attachments
   * **Local Events:** Browse and filter upcoming community events
   * **Service Request Status:** Track issues (uses data structures internally)

### ▶️ Run using CLI

```bash
git checkout mvc
cd CitizenReportWeb
dotnet restore
dotnet run
# Open printed localhost URL (example: https://localhost:7001)
```

### 📁 File Uploads

Attachments are stored in:

```
CitizenReportWeb/wwwroot/uploads/
```

(Used for local testing only — no external data transfer.)

---

## 💻 WinForms — Quick Start (Legacy Version)

If required for comparison or verification:

1. Checkout the WinForms branch:

   ```bash
   git checkout winform
   ```
2. Open `CitizenReportWinForms.sln`
3. Set **CitizenReportWinForms** as the startup project
4. Build and run the application (F5)

**Note:**

* The WinForms version implements only the base reporting and event listing logic.
* It lacks web-based improvements and modern UI consistency.

---

## 🧩 Features Summary (MVC)

| Feature         | Description                                    | Data Structure                                 |
| --------------- | ---------------------------------------------- | ---------------------------------------------- |
| Report Issues   | Capture location, description, and attachments | `Dictionary`, `List`                           |
| Local Events    | Filter events by date, location, or category   | `SortedDictionary`, `PriorityQueue`, `HashSet` |
| Service Status  | View and search submitted reports              | `BinarySearchTree`, `Graph`, `MinHeap`         |
| Recommendations | Suggest related events                         | `HashSet`, `Dictionary`                        |

---

## 🧠 About the Project

**CitizenReport** is a learning-focused municipal services app that simulates how residents can:

* Report issues like water leaks or power faults
* View local municipal events
* Track service request progress and prioritisation

The app demonstrates data-driven design, algorithmic efficiency, and responsive UI principles.

---

## 🧾 Disclaimer

This system is for **academic and demonstration purposes only**. It does **not** connect to real municipal servers or process actual reports.

Developed by **Dean Gibson (ST10326084)** for **PROG7312 / PROG7311 — Municipal Services Application (POE Part 3)** at **Varsity College, 2025**.

---

## 🧮 Helpful Git Commands

```bash
# List all branches
git branch -a

# Create local tracking branches if needed
git checkout -b mvc origin/mvc
git checkout -b winform origin/winform

# Switch between versions
git checkout mvc
```

---

## 📚 Related Files in Repository

| File                           | Purpose                                          |
| ------------------------------ | ------------------------------------------------ |
| `README.md`                    | Main landing file (this one)                     |
| `DATA_STRUCTURES.md`           | Explanation of Graph, Heap, and Tree logic       |
| `ImplementationReport.md`      | Technical breakdown and test notes               |
| `ProjectCompletionReport.md`   | Challenges, learnings, and reflections           |
| `TechnologyRecommendations.md` | Future improvement suggestions                   |
| `CHANGELOG.md`                 | Updates based on feedback from Part 1 and Part 2 |
