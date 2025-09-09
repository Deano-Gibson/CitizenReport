using CitizenReportWeb.Models;
using System;
using System.Collections.Generic;

namespace CitizenReportWeb.Models
{
    public class Issue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Location { get; set; } = "";
        public IssueCategory Category { get; set; }
        public string Description { get; set; } = "";
        public List<string> Attachments { get; set; } = new(); // saved file names
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
