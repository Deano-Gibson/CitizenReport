namespace CitizenReportWeb.Models
{
    public class ServiceRequestStatusViewModel
    {
        public int RequestId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
