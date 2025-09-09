using CitizenReportWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CitizenReportWeb.Models
{
    public class ReportIssueViewModel
    {
        [Required, Display(Name = "Location")]
        public string Location { get; set; } = "";

        [Required, Display(Name = "Category")]
        public IssueCategory? Category { get; set; }

        [Required, Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = "";

        [Display(Name = "Attachments")]
        public List<IFormFile>? Attachments { get; set; }
    } 
}
