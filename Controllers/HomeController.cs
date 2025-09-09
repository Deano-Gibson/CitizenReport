using CitizenReportWeb.Data;
using CitizenReportWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenReportWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env) => _env = env;

        // Main Menu
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Privacy Info
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        // we pass the viewmodel instead of the view
        [HttpGet]
        public IActionResult ReportIssues()
        {
            return View(new ReportIssueViewModel());   
        }


        // Report Issues (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportIssues(ReportIssueViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var issue = new Issue
            {
                Location = vm.Location.Trim(),
                Category = vm.Category!.Value,
                Description = vm.Description.Trim()
            };

            // Save uploads to wwwroot/uploads
            if (vm.Attachments is { Count: > 0 })
            {
                var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsPath);

                foreach (var file in vm.Attachments.Where(f => f.Length > 0))
                {
                    var safeName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadsPath, safeName);
                    await using var stream = System.IO.File.Create(filePath);
                    await file.CopyToAsync(stream);
                    issue.Attachments.Add(safeName);
                }
            }

            IssueStore.Issues.Add(issue);

            TempData["Success"] = $"Thanks! Your report was captured. Ref: {issue.Id.ToString()[..8]}";
            return RedirectToAction(nameof(Index));
        }

        // Optional: stub pages (disabled in UI)
        public IActionResult LocalEvents() => Content("Local Events: coming soon.");
        public IActionResult ServiceStatus() => Content("Service Request Status: coming soon.");
    }
}
