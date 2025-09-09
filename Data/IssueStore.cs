using CitizenReportWeb.Models;
using System.Collections.Concurrent;

namespace CitizenReportWeb.Data
{
    
    public static class IssueStore
    {
        public static readonly ConcurrentBag<Issue> Issues = new();
    }
}
