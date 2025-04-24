namespace MinApp.Models
{
    public class WorkHours
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Hours => (EndTime - StartTime).TotalHours;
        public bool IsPaid { get; set; }
    }
}