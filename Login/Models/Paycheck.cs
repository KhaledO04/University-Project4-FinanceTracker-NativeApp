namespace MinApp.Models
{
    public class Paycheck
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public double HoursWorked { get; set; }
        public double GrossPay { get; set; }
        public double NetPay { get; set; }
        public double TaxWithheld { get; set; }
        public double AMBidrag { get; set; }
        public List<PayItem> PayItems { get; set; } = new List<PayItem>();
    }

    public class PayItem
    {
        public string Description { get; set; }
        public double Hours { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}