# CitizenReport — Quick Start (Main Branch / MVC‑focused)

> **Important:** This repo contains **two** implementations (MVC and WinForms). The PoE requires WinForms, but presentation guidelines allow freedom. **If either is acceptable, please mark the *MVC* version. Thank you!**

---

## Links to branches

* **MVC branch:** `https://github.com/Deano-Gibson/CitizenReport/tree/mvc`
* **WinForms branch:** `https://github.com/Deano-Gibson/CitizenReport/tree/winForm`

---

## What’s on this branch (main)

* A short **landing README** with links to the active branches.
* **MVC is the default** target for marking.

---

## MVC — Quick Start

### Prerequisites

* .NET **8** SDK
* Visual Studio 2022 / VS Code / Rider

### Run (Visual Studio)

1. Checkout the MVC branch: `git checkout mvc`
2. Set **CitizenReportWeb** as *Startup Project*.
3. **Build** → *Run (F5)*.
4. Navigate to **Home → Report Issues**.

### Run (CLI)

```bash
git checkout mvc
cd CitizenReportWeb
dotnet restore
dotnet run
# open the printed URL (e.g., https://localhost:7001)
```

### Where uploads go

* Files are saved to `wwwroot/uploads/` during testing (demo data only).

---

## WinForms — Quick Start (if needed)

1. Checkout the WinForms branch: `git checkout winform`
2. Open `CitizenReportWinForms.sln` in Visual Studio.
3. Set **CitizenReportWinForms** as *Startup Project* → Run (F5).

---

## Notes for Markers

* Only **Report Issues** is fully implemented in Part‑1.
* **Local Events** and **Service Status** are visible but disabled as specified.
* MVC includes a civic theme, inline progress/nudges, validation, and attachment uploads.

---

## Git commands (handy)

```bash
# list branches
git branch -a

# create local tracking branches if needed
git checkout -b mvc origin/mvc
git checkout -b winform origin/winform

# switch
git checkout mvc
```

---

## Disclaimer

This is a **mock/student** municipal services site for learning; it is not affiliated with any government entity and does not process real requests.
