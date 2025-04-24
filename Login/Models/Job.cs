namespace MinApp.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmploymentType { get; set; }
        public bool IsBoard { get; set; }
        public bool IsCard { get; set; }
        public string TaxNumber { get; set; }
        public string HourlyRate { get; set; }
    }
}