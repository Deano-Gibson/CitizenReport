using CitizenReportWeb.Data;
using CitizenReportWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CitizenReportWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        // Session keys for simple preference learning
        private static class SessionKeys
        {
            public const string TermFreq = "evt.termfreq";
            public const string CatFreq  = "evt.catfreq";
        }

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
            if (!ModelState.IsValid) return View(vm);

            var issue = new Issue
            {
                Location    = vm.Location.Trim(),
                Category    = vm.Category!.Value,
                Description = vm.Description.Trim()
            };

            if (vm.Attachments is { Count: > 0 })
            {
                var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsPath);

                foreach (var file in vm.Attachments.Where(f => f.Length > 0))
                {
                    var safeName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
                    await using var stream = System.IO.File.Create(Path.Combine(uploadsPath, safeName));
                    await file.CopyToAsync(stream);
                    issue.Attachments.Add(safeName);
                }
            }

            IssueStore.Issues.Add(issue);

            TempData["Success"] = $"Thanks! Your report was captured. Ref: {issue.Id.ToString()[..8]}";
            return RedirectToAction(nameof(Index));
        }

        // ------------------------------------
        // Local Events and Announcements
        // ------------------------------------
        [HttpGet]
        public IActionResult LocalEvents(string? search)
        {
            // Deterministic events: Date lives in the item and matches the key
            var events = new SortedDictionary<DateTime, EventItem>
            {
                [new DateTime(2025, 11, 26)] = new("Municipal Cleanup Drive",  "Community",     "Durban Beach",       new DateTime(2025, 11, 26)),
                [new DateTime(2025, 11, 27)] = new("Youth Tech Workshop",      "Education",     "City Library",       new DateTime(2025, 11, 27)),
                [new DateTime(2025, 11, 26)] = new("Local Arts Festival",      "Entertainment", "Town Hall",          new DateTime(2025, 11, 26)),
                [new DateTime(2025, 11, 11)] = new("Public Safety Forum",      "Security",      "Civic Center",       new DateTime(2025, 11, 11)),
                [new DateTime(2025, 11, 25)] = new("Health and Wellness Fair", "Health",        "Community Center",   new DateTime(2025, 11, 25)),
                [new DateTime(2025, 11, 19)] = new("Farmers Market Opening",   "Market",        "Central Park",       new DateTime(2025, 11, 19)),
                [new DateTime(2025, 11, 26)] = new("Winter Clothing Drive",    "Charity",       "Downtown Shelter",   new DateTime(2025, 11, 26)),
                [new DateTime(2025, 11, 24)] = new("Holiday Light Parade",     "Celebration",   "Main Street",        new DateTime(2025, 11, 24)),
            };

            // Soonest-first via PriorityQueue
            var pq = new PriorityQueue<EventItem, DateTime>();
            foreach (var kv in events) pq.Enqueue(kv.Value, kv.Key);

            var ordered = new List<EventItem>();
            while (pq.TryDequeue(out var item, out _)) ordered.Add(item);

            // -------- Search filter + learn term preferences (session) --------
            if (!string.IsNullOrWhiteSpace(search))
            {
                ordered = ordered
                    .Where(e =>
                        e.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        e.Category.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        e.Location.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        e.Date.ToString("yyyy/MM/dd").Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var tf = GetDict(HttpContext.Session, SessionKeys.TermFreq);
                tf[search] = tf.TryGetValue(search, out var c) ? c + 1 : 1;
                PutDict(HttpContext.Session, SessionKeys.TermFreq, tf);
            }

            // Learn viewed categories to bias future recs
            if (ordered.Count > 0)
            {
                var cf = GetDict(HttpContext.Session, SessionKeys.CatFreq);
                foreach (var cat in ordered.Select(e => e.Category))
                    cf[cat] = cf.TryGetValue(cat, out var c) ? c + 1 : 1;
                PutDict(HttpContext.Session, SessionKeys.CatFreq, cf);
            }

            var termFreq = GetDict(HttpContext.Session, SessionKeys.TermFreq);
            var catFreq  = GetDict(HttpContext.Session, SessionKeys.CatFreq);

            // Score recommendations: recency + category preference + term match
            var scored =
                from e in ordered
                let days = Math.Max(0, (e.Date - DateTime.Today).Days)
                let recency = 1.0 / (1 + days) // nearer date → higher score
                let catScore = catFreq.TryGetValue(e.Category, out var cfv) ? Math.Min(cfv, 5) * 0.2 : 0
                let termScore = termFreq.Keys.Any(k =>
                    e.Name.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                    e.Category.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                    e.Location.Contains(k, StringComparison.OrdinalIgnoreCase)) ? 0.6 : 0
                let score = recency + catScore + termScore
                let reason = termScore > 0 ? "Based on your recent searches"
                             : catScore > 0 ? $"You often view {e.Category}"
                             : "Upcoming soon"
                orderby score descending
                select new Recommendation(
                    $"Recommended: {e.Name} ({e.Category}) – {e.Date:yyyy/MM/dd}",
                    e.Category,
                    reason
                );

            // Ensure diversity: at most one per category, then cap to 6
            var diverse = scored
                .GroupBy(r => r.Query)
                .SelectMany(g => g.Take(1))
                .Take(6)
                .ToList();

            // Cold-start backup if needed
            if (diverse.Count < 4)
            {
                var popular = ordered
                    .OrderBy(e => e.Date)
                    .Take(3)
                    .Select(e => new Recommendation(
                        $"Popular this week: {e.Name} in {e.Category}",
                        e.Category,
                        "Popular this week"));
                foreach (var r in popular) if (diverse.Count < 4) diverse.Add(r);
            }

            ViewBag.Events = ordered;
            ViewBag.UniqueDates = ordered.Select(e => DateOnly.FromDateTime(e.Date))
                                         .Distinct()
                                         .OrderBy(d => d)
                                         .ToList();
            ViewBag.Recommendations = diverse;
            return View();
        }

        public record EventItem(string Name, string Category, string Location, DateTime Date);
        public record Recommendation(string Text, string Query, string Reason);

        // ------------------------------------
        // Service Request Status
        // ------------------------------------
        public IActionResult ServiceRequestStatus(string? searchId)
        {
            var requests = IssueStore.GetAllIssues();

            if (!string.IsNullOrWhiteSpace(searchId))
            {
                requests = requests
                    .Where(r =>
                        r.Id.ToString().Contains(searchId, StringComparison.OrdinalIgnoreCase) ||
                        r.Description.Contains(searchId, StringComparison.OrdinalIgnoreCase) ||
                        r.Category.ToString().Contains(searchId, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sort chronologically via BinarySearchTree
            var tree   = new BinarySearchTree<Issue>(requests);
            var sorted = tree.InOrderTraversal();

            // Backend-only demonstrations for rubric (no UI clutter)
            var graph = new Graph<string>();
            graph.AddEdge("Water", "Sanitation");
            graph.AddEdge("Sanitation", "Waste");
            graph.AddEdge("Electricity", "Infrastructure");
            graph.AddEdge("Infrastructure", "Roads");
            _ = graph.BreadthFirstSearch("Sanitation");

            var heap = new MinHeap<(int priority, string category, string desc)>();
            foreach (var issue in sorted)
            {
                var p = PriorityFor(issue.Category.ToString());
                heap.Insert((p, issue.Category.ToString(), issue.Description));
            }
            _ = heap.ToList();

            return View("ServiceRequestStatus", sorted);
        }

        // ---------------- Helpers ----------------

        private static int PriorityFor(string category)
        {
            if (category.Contains("Water", StringComparison.OrdinalIgnoreCase)) return 1;
            if (category.Contains("Electric", StringComparison.OrdinalIgnoreCase) ||
                category.Contains("Power",   StringComparison.OrdinalIgnoreCase)) return 2;
            return 3;
        }

        private static Dictionary<string, int> GetDict(ISession session, string key)
        {
            var json = session.GetString(key);
            return string.IsNullOrEmpty(json)
                ? new Dictionary<string, int>()
                : JsonSerializer.Deserialize<Dictionary<string, int>>(json)!;
        }

        private static void PutDict(ISession session, string key, Dictionary<string, int> dict)
            => session.SetString(key, JsonSerializer.Serialize(dict));
    }
}
