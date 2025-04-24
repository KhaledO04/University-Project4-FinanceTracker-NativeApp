using MinApp.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MinApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost5140/";
        private string _token;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { Email = email, Password = password };
                var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    _token = result.Token;

                    // Set the token for future requests
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RegisterAsync(User user)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("auth/register", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("jobs");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Job>>();
                }
                return new List<Job>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get jobs error: {ex.Message}");
                return new List<Job>();
            }
        }

        public async Task<Job> AddJobAsync(Job job)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("jobs", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Job>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add job error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<WorkHours>> GetWorkHoursAsync(int jobId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"jobs/{jobId}/hours");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<WorkHours>>();
                }

                return new List<WorkHours>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get work hours error: {ex.Message}");
                return new List<WorkHours>();
            }
        }

        public async Task<WorkHours> AddWorkHoursAsync(WorkHours workHours)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(workHours), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"jobs/{workHours.JobId}/hours", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<WorkHours>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add work hours error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Paycheck>> GetPaychecksAsync(int jobId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"jobs/{jobId}/paychecks");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Paycheck>>();
                }

                return new List<Paycheck>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get paychecks error: {ex.Message}");
                return new List<Paycheck>();
            }
        }

        public async Task<List<HolidayPay>> GetHolidayPayAsync(int jobId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"jobs/{jobId}/holidaypay");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<HolidayPay>>();
                }

                return new List<HolidayPay>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get holiday pay error: {ex.Message}");
                return new List<HolidayPay>();
            }
        }

        public async Task<StudentGrant> GetStudentGrantAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("studentgrant");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<StudentGrant>();
                }

                return new StudentGrant();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get student grant error: {ex.Message}");
                return new StudentGrant();
            }
        }

        private class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}