using CitizenReportWeb.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CitizenReportWeb.Data
{
    public static class IssueStore
    {
        public static readonly ConcurrentBag<Issue> Issues = new();

        public static List<Issue> GetAllIssues()
        {
            // ConcurrentBag doesn't support indexing; copy to list
            return Issues.ToList();
        }
    }
}
