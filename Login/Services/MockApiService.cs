using MinApp.Models;

namespace MinApp.Services
{
    public class MockApiService : IApiService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = "1", Email = "test@example.com", FullName = "Test User", Phone = "12345678", Password = "password" }
        };

        private readonly List<Job> _jobs = new List<Job>();
        private readonly List<WorkHours> _workHours = new List<WorkHours>();
        private readonly List<Paycheck> _paychecks = new List<Paycheck>();
        private readonly List<HolidayPay> _holidayPays = new List<HolidayPay>();
        private readonly StudentGrant _studentGrant = new StudentGrant();

        private int _nextJobId = 1;
        private int _nextWorkHoursId = 1;
        private int _nextPaycheckId = 1;
        private int _nextHolidayPayId = 1;

        public MockApiService()
        {
            // Initialize with sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Add sample jobs
            _jobs.Add(new Job
            {
                Id = _nextJobId++,
                Name = "Supermarket Cashier",
                EmploymentType = "Part-time",
                IsBoard = true,
                IsCard = false,
                TaxNumber = "123456",
                HourlyRate = "120"
            });

            _jobs.Add(new Job
            {
                Id = _nextJobId++,
                Name = "IT Support",
                EmploymentType = "Student job",
                IsBoard = false,
                IsCard = true,
                TaxNumber = "654321",
                HourlyRate = "150"
            });

            // Add sample work hours
            _workHours.Add(new WorkHours
            {
                Id = _nextWorkHoursId++,
                JobId = 1,
                Date = DateTime.Today.AddDays(-7),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                IsPaid = true
            });

            _workHours.Add(new WorkHours
            {
                Id = _nextWorkHoursId++,
                JobId = 1,
                Date = DateTime.Today.AddDays(-6),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                IsPaid = true
            });

            _workHours.Add(new WorkHours
            {
                Id = _nextWorkHoursId++,
                JobId = 1,
                Date = DateTime.Today.AddDays(-1),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                IsPaid = false
            });

            // Add sample paychecks
            _paychecks.Add(new Paycheck
            {
                Id = _nextPaycheckId++,
                JobId = 1,
                JobName = "Supermarket Cashier",
                PayPeriodStart = new DateTime(2023, 4, 1),
                PayPeriodEnd = new DateTime(2023, 4, 30),
                HoursWorked = 80,
                GrossPay = 9600,
                NetPay = 7200,
                TaxWithheld = 2400,
                AMBidrag = 768,
                PayItems = new List<PayItem>
                {
                    new PayItem { Description = "Regular Hours", Hours = 80, Rate = 120, Amount = 9600 }
                }
            });

            // Add sample holiday pay
            _holidayPays.Add(new HolidayPay
            {
                Id = _nextHolidayPayId++,
                JobId = 1,
                JobName = "Supermarket Cashier",
                Amount = 5000,
                AccrualStart = new DateTime(2023, 1, 1),
                AccrualEnd = new DateTime(2023, 3, 31),
                IsPaid = true,
                PaymentDate = new DateTime(2023, 5, 1)
            });

            _holidayPays.Add(new HolidayPay
            {
                Id = _nextHolidayPayId++,
                JobId = 1,
                JobName = "Supermarket Cashier",
                Amount = 3000,
                AccrualStart = new DateTime(2023, 4, 1),
                AccrualEnd = new DateTime(2023, 6, 30),
                IsPaid = false
            });

            // Initialize student grant
            _studentGrant.Id = 1;
            _studentGrant.TotalAmount = 50000;
            _studentGrant.UsedAmount = 20000;
            _studentGrant.Year = 2023;
            _studentGrant.MonthlyIncome = 8000;
            _studentGrant.YearlyIncome = 96000;
            _studentGrant.AllowedIncome = 120000;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            // Simulate network delay
            await Task.Delay(1000);

            return _users.Any(u => u.Email == email && u.Password == password);
        }

        public async Task<bool> RegisterAsync(User user)
        {
            // Simulate network delay
            await Task.Delay(1000);

            if (_users.Any(u => u.Email == user.Email))
            {
                return false; // User already exists
            }

            user.Id = (_users.Count + 1).ToString();
            _users.Add(user);

            return true;
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            // Simulate network delay
            await Task.Delay(1000);

            return _jobs.ToList();
        }

        public async Task<Job> AddJobAsync(Job job)
        {
            // Simulate network delay
            await Task.Delay(1000);

            job.Id = _nextJobId++;
            _jobs.Add(job);

            return job;
        }

        public async Task<List<WorkHours>> GetWorkHoursAsync(int jobId)
        {
            // Simulate network delay
            await Task.Delay(1000);

            return _workHours.Where(w => w.JobId == jobId).ToList();
        }

        public async Task<WorkHours> AddWorkHoursAsync(WorkHours workHours)
        {
            // Simulate network delay
            await Task.Delay(1000);

            workHours.Id = _nextWorkHoursId++;
            _workHours.Add(workHours);

            return workHours;
        }

        public async Task<List<Paycheck>> GetPaychecksAsync(int jobId)
        {
            // Simulate network delay
            await Task.Delay(1000);

            return _paychecks.Where(p => p.JobId == jobId).ToList();
        }

        public async Task<List<HolidayPay>> GetHolidayPayAsync(int jobId)
        {
            // Simulate network delay
            await Task.Delay(1000);

            return _holidayPays.Where(h => h.JobId == jobId).ToList();
        }

        public async Task<StudentGrant> GetStudentGrantAsync()
        {
            // Simulate network delay
            await Task.Delay(1000);

            return _studentGrant;
        }
    }
}