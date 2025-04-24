namespace MinApp.Models
{
    public class HolidayPay
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public double Amount { get; set; }
        public DateTime AccrualStart { get; set; }
        public DateTime AccrualEnd { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}