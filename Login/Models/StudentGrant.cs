namespace MinApp.Models
{
    public class StudentGrant
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public double UsedAmount { get; set; }
        public double RemainingAmount => TotalAmount - UsedAmount;
        public int Year { get; set; }
        public double MonthlyIncome { get; set; }
        public double YearlyIncome { get; set; }
        public double AllowedIncome { get; set; }
        public double RemainingAllowedIncome => AllowedIncome - YearlyIncome;
    }
}