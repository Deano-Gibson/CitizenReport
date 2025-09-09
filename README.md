# CitizenReport — Municipal Services Web App & Desktop App (Draft)

A two-part solution for reporting community issues to a South African municipality. It ships as:

- **CitizenReport (WinForms, .NET Framework 4.8)** — a desktop client with drag-and-drop UI.
- **CitizenReportWeb (ASP.NET Core MVC, .NET 8)** — a responsive web app with file uploads.

Both apps implement **Task 1/2 (Part 1)** of the brief: **Report Issues** only. “Local Events” and “Service Status” are present in the UI but intentionally disabled for later phases.

---

## Contents

- [Architecture & How It Works](#architecture--how-it-works)
- [Repo Structure](#repo-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [WinForms (Desktop)](#winforms-desktop)
  - [ASP.NET Core MVC (Web)](#aspnet-core-mvc-web)
- [Features](#features)
  - [Shared](#shared)
  - [WinForms UI](#winforms-ui)
  - [Web UI](#web-ui)
- [Data Model](#data-model)
- [Design & Accessibility](#design--accessibility)
- [Troubleshooting](#troubleshooting)
- [Roadmap (Next Part)](#roadmap-next-part)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## Architecture & How It Works

### Core idea
Users capture an issue with **Location**, **Category**, **Description**, and optional **attachments**. Each submission becomes an `Issue` object stored in memory (Part 1 requirement). The UI gives **immediate feedback** via messages and a **progress indicator** (engagement strategy).

### Flow (both apps)

1. **Main Menu**  
   Shows three tasks. Only **Report Issues** is active; the others are disabled.

2. **Report Issues form/page**  
   - Validates required fields.
   - Lets users attach files.
   - Shows **progress** ("Start by entering the location → Ready to submit!").
   - On submit, creates an `Issue` with a reference ID and stores it in memory.
   - Displays a success message with a short reference (first 8 chars of `Guid`).

3. **Storage**  
   - **WinForms:** static `IssueStore.Issues : List<Issue>` (in-memory).  
   - **Web:** `IssueStore.Issues : ConcurrentBag<Issue>` (thread-safe). Files are saved under `wwwroot/uploads`.

> Part 1 does **not** require a database. Persistence (JSON/DB) can be added in Part 2.

---

## Repo Structure

```
CitizenReport/                 # solution root
├─ CitizenReportWinForms/      # WinForms .NET Framework 4.8 app (desktop)
│  ├─ Forms/
│  │  ├─ MainForm.cs           # main menu
│  │  └─ ReportIssueForm.cs    # issue reporting
│  ├─ Models/
│  │  ├─ Issue.cs
│  │  └─ IssueCategory.cs
│  └─ IssueStore.cs
└─ CitizenReportWeb/           # ASP.NET Core MVC .NET 8 web app
   ├─ Controllers/
   │  └─ HomeController.cs
   ├─ Models/
   │  ├─ Issue.cs
   │  ├─ IssueCategory.cs
   │  └─ ReportIssueViewModel.cs
   ├─ Data/
   │  └─ IssueStore.cs
   ├─ Views/
   │  └─ Home/
   │     ├─ Index.cshtml
   │     └─ ReportIssues.cshtml
   ├─ wwwroot/
   │  ├─ css/site.css
   │  └─ uploads/              # saved attachments (created at runtime)
   └─ Program.cs
```

*(Folder names may vary slightly based on your local solution.)*

---

## Prerequisites

**For WinForms**
- Windows 10/11
- Visual Studio 2019/2022 with **“.NET desktop development”**
- **.NET Framework 4.8** (or 4.7.2+)

**For Web (MVC)**
- Windows/macOS/Linux
- **.NET 8 SDK**
- Visual Studio 2022 / VS Code / Rider
- Web browser

---

## Getting Started

### WinForms (Desktop)

1. Open `CitizenReportWinForms.sln` in Visual Studio.  
2. **Set as StartUp Project** → *CitizenReportWinForms*.  
3. **Build** → **Run (F5)**.  
4. In the main menu, click **Report Issues** to open the form.

### ASP.NET Core MVC (Web)

**Visual Studio**
1. Open `CitizenReportWeb.csproj` or the solution containing it.  
2. **Set as StartUp Project** → *CitizenReportWeb*.  
3. **Build** → **Run (F5)**.  
4. Home page shows **Report Issues** and two disabled buttons.

**CLI**
```bash
cd CitizenReportWeb
dotnet restore
dotnet run
# browse to the URL shown (e.g., https://localhost:7001)
```

---

## Features

### Shared
- **Report Issues** with:
  - Location (TextBox)
  - Category (Sanitation, Roads, Electricity, Water, Safety, Other)
  - Description (Rich text / multiline)
  - Attachments (images/docs)
- **Engagement strategy**: real-time **progress** + friendly nudges.
- **Clear feedback**: success MessageBox/Alert with reference number.
- **Accessible, simple language** UI.

### WinForms UI
- **Drag-and-drop** Designer with named controls:
  - `txtLocation`, `cmbCategory`, `rtbDescription`, `btnAttach`, `lstAttachments`, `progress`, `lblNudge`, `btnSubmit`, `btnBack`.
- **OpenFileDialog** for attachments (multi-select).
- **In-memory store** (`IssueStore.Issues`) to hold submitted issues.

### Web UI
- **MVC pattern** with `HomeController`.
- **ViewModel validation** (`[Required]`) with client & server validation.
- **File uploads** to `wwwroot/uploads` (auto-created).
- **Progress bar** updated with a tiny JS script as the user fills fields.
- **Civic theme** (navbar + footer) in `wwwroot/css/site.css`.

---

## Data Model

```csharp
public enum IssueCategory { Sanitation, Roads, Electricity, Water, Safety, Other }

public class Issue
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Location { get; set; } = "";
    public IssueCategory Category { get; set; }
    public string Description { get; set; } = "";
    public List<string> Attachments { get; set; } = new(); // WinForms: file paths; Web: saved file names
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// WinForms
public static class IssueStore { public static readonly List<Issue> Issues = new(); }

// Web
public static class IssueStore { public static readonly ConcurrentBag<Issue> Issues = new(); }
```

**Web upload handling (simplified):**
```csharp
var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
Directory.CreateDirectory(uploadsPath);
foreach (var file in vm.Attachments.Where(f => f.Length > 0))
{
    var name = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
    await using var stream = System.IO.File.Create(Path.Combine(uploadsPath, name));
    await file.CopyToAsync(stream);
    issue.Attachments.Add(name);
}
```

---

## Design & Accessibility

- **Consistency**: shared palette, spacing, headings; disabled buttons for future features.
- **Clarity**: plain wording (“Location”, “Category”, “Description”); explicit success/error messages.
- **Engagement**: progress bar + nudges (“Great — choose a category next.”).
- **Responsiveness (Web)**: Bootstrap grid; centered civic navbar & footer; high-contrast colors.
- **Keyboard**: WinForms sets `AcceptButton = Submit`, `CancelButton = Back`.

---

## Troubleshooting

- **“View ‘ReportIssues’ was not found” (Web)**  
  Ensure the file is `Views/Home/ReportIssues.cshtml`. Clean/Rebuild. Confirm `GET ReportIssues()` returns `View(new ReportIssueViewModel())`. Ensure `Views/_ViewImports.cshtml` contains:
  ```cshtml
  @using CitizenReportWeb.Models
  @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
  @using System
  ```

- **Duplicate assembly attributes (Web)**  
  Remove `Properties/AssemblyInfo.cs`, or set `<GenerateAssemblyInfo>false</GenerateAssemblyInfo>` in the `.csproj`.

- **Uploads not saving (Web)**  
  Confirm `wwwroot/uploads` exists (it’s auto-created). Check file permissions and that `app.UseStaticFiles();` is in `Program.cs`.

---

## Roadmap (Next Part)

- **Local Events & Announcements** (enable + content feed).
- **Service Request Status** (view, filter by reference ID).
- **Persistence** (JSON/DB), authentication/roles, export/reporting.

---

## Contributing

1. Fork the repo  
2. Create a branch: `git checkout -b feature/your-feature`  
3. Commit: `git commit -m "Add your feature"`  
4. Push: `git push origin feature/your-feature`  
5. Open a Pull Request

---

## License

MIT — see `LICENSE`.

---

## Contact

Questions? Open an issue on the repository or start a discussion.

