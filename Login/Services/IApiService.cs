using MinApp.Models;

namespace MinApp.Services
{
    public interface IApiService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(User user);
        Task<List<Job>> GetJobsAsync();
        Task<Job> AddJobAsync(Job job);
        Task<List<WorkHours>> GetWorkHoursAsync(int jobId);
        Task<WorkHours> AddWorkHoursAsync(WorkHours workHours);
        Task<List<Paycheck>> GetPaychecksAsync(int jobId);
        Task<List<HolidayPay>> GetHolidayPayAsync(int jobId);
        Task<StudentGrant> GetStudentGrantAsync();
    }
}