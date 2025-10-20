using CitizenReportWeb.Data;
using CitizenReportWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenReportWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        // Static structures for demonstration persistence
        private static readonly Dictionary<string, int> _searchStats = new();

        public HomeController(IWebHostEnvironment env) => _env = env;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Privacy() => View();

        [HttpGet]
        public IActionResult ReportIssues() => View(new ReportIssueViewModel());

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

        // ----------------------------
        // Local Events and Announcements
        // ----------------------------
        [HttpGet]
        public IActionResult LocalEvents(string? search)
        {
            // 1. SortedDictionary for efficient event storage
            var events = new SortedDictionary<DateTime, EventItem>
            {
                [new DateTime(2025, 10, 15)] = new EventItem("Municipal Cleanup Drive", "Community", "Durban Beach"),
                [new DateTime(2025, 10, 20)] = new EventItem("Youth Tech Workshop", "Education", "City Library"),
                [new DateTime(2025, 10, 25)] = new EventItem("Local Arts Festival", "Entertainment", "Town Hall"),
                [new DateTime(2025, 11, 05)] = new EventItem("Public Safety Forum", "Security", "Civic Center"),
                [new DateTime(2025, 11, 12)] = new EventItem("Health and Wellness Fair", "Health", "Community Center"),
                [new DateTime(2025, 11, 18)] = new EventItem("Farmers Market Opening", "Market", "Central Park"),
                [new DateTime(2025, 11, 22)] = new EventItem("Winter Clothing Drive", "Charity", "Downtown Shelter"),
                [new DateTime(2025, 12, 01)] = new EventItem("Holiday Light Parade", "Celebration", "Main Street"),
            };

            // 2. PriorityQueue for "soonest first" ordering
            var pq = new PriorityQueue<EventItem, DateTime>();
            foreach (var kv in events)
                pq.Enqueue(kv.Value, kv.Key);

            var ordered = new List<EventItem>();
            while (pq.TryDequeue(out var item, out _))
                ordered.Add(item);

            // 3. Filter based on user search
            if (!string.IsNullOrEmpty(search))
            {
                ordered = ordered
                    .Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                             || e.Category.Contains(search, StringComparison.OrdinalIgnoreCase)
                             || e.Location.Contains(search, StringComparison.OrdinalIgnoreCase)
                             || e.Date.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Track search term frequency
                _searchStats[search] = _searchStats.ContainsKey(search)
                    ? _searchStats[search] + 1
                    : 1;
            }

            // 4. HashSet for unique categories and dates
            var categories = new HashSet<string>(ordered.Select(e => e.Category));
            var uniqueDates = new HashSet<DateOnly>(ordered.Select(e => DateOnly.FromDateTime(e.Date)));

            // 5. Recommendation engine (based on categories + top search terms)
            var recommendations = new List<string>();
            recommendations.AddRange(categories.Select(c => $"Explore more {c} events in your area!"));

            var topSearches = _searchStats
                .OrderByDescending(k => k.Value)
                .Take(3)
                .Select(k => $"Because you searched '{k.Key}' often, check related events!");
            recommendations.AddRange(topSearches);

            // Pass data to view
            ViewBag.Events = ordered;
            ViewBag.UniqueDates = uniqueDates;
            ViewBag.Recommendations = recommendations;
            return View();
        }

        // Record model for event data
        public record EventItem(string Name, string Category, string Location)
        {
            public DateTime Date { get; set; } = DateTime.Now.AddDays(new Random().Next(1, 20));
        }

        public IActionResult ServiceStatus() => Content("Service Request Status: coming soon.");



        // Service stuff (part 3)
        public IActionResult ServiceRequestStatus(string searchId)
        {
            var requests = IssueStore.GetAllIssues();

            if (!string.IsNullOrEmpty(searchId))
                requests = requests
                    .Where(r => r.Id.ToString().Contains(searchId, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // Data structure example: using Binary Search Tree to organise
            var tree = new BinarySearchTree<Issue>(requests);
            var sorted = tree.InOrderTraversal();

            return View("ServiceRequestStatus", sorted);
        }

   }
}
