using System;
using System.Collections.Generic;

namespace CitizenReportWeb.Models
{
    public class Issue : IComparable<Issue>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Location { get; set; } = string.Empty;
        public IssueCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Attachments { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- Added for Part 3 ---
        public string Status { get; set; } = "Pending";
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;

        // For BinarySearchTree ordering (by DateSubmitted)
        public int CompareTo(Issue? other)
        {
            if (other == null) return 1;
            return DateSubmitted.CompareTo(other.DateSubmitted);
        }
    }
}
