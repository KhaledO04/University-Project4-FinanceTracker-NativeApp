using System;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using MinApp.Models;

namespace MinApp.Services;

// Service til at håndtere authentication (login, logout, brugerinfo) med JWT-token
public class AuthService
{
    private readonly HttpClient _httpClient;
    private string _token;           // Gemmer det modtagne JWT-token
    private User _currentUser;       // Gemmer info om den aktuelle bruger (dekodet fra JWT)

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _token = string.Empty;
        _currentUser = null;
    }

    public async Task<User> LoginAsync(string email, string adgangskode)
    {
        // Kalder Web API (AccountController) for at validere login og modtage JWT-token
        var credentials = new { Username = email, Password = adgangskode };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Account", credentials);
        if (!response.IsSuccessStatusCode)
        {
            return null; // Login fejlede (forkert login eller serverfejl)
        }

        // Læs JWT-token (forventet som string i response body)
        string jwtToken = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            return null;
        }

        // Gem token sikkert i SecureStorage til senere brug
        await SecureStorage.Default.SetAsync("authToken", jwtToken);
        _token = jwtToken;

        // Dekod token for at udtrække brugeroplysninger (fx Id og Email)
        _currentUser = DecodeJwtToken(jwtToken);
        return _currentUser;
    }

    public void Logout()
    {
        // Fjern JWT-token fra hukommelse og SecureStorage
        _token = null;
        _currentUser = null;
        SecureStorage.Default.Remove("authToken");
    }

    public User GetCurrentUser()
    {
        // Returnér aktuel bruger (hvis logget ind)
        return _currentUser;
    }

    public bool IsAuthenticated
    {
        get => !string.IsNullOrEmpty(_token);
    }

    // Hjælpefunktion til at dekode JWT-token uden at verificere signatur
    private User DecodeJwtToken(string jwtToken)
    {
        var user = new User();
        try
        {
            string payload = jwtToken.Split('.')[1]; // JWT payload er mellemste del
            // Base64-decode (JWT bruger URL-safe base64 uden padding)
            string paddedPayload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            byte[] jsonBytes = Convert.FromBase64String(paddedPayload.Replace('-', '+').Replace('_', '/'));
            string json = Encoding.UTF8.GetString(jsonBytes);

            // Parse JSON for at hente relevante felter (kræver at token indeholder disse claims)
            using JsonDocument doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("id", out JsonElement idElement))
                user.Id = idElement.GetInt32();
            if (doc.RootElement.TryGetProperty("email", out JsonElement emailElement))
                user.Email = emailElement.GetString();
        }
        catch
        {
            // Hvis dekodning fejler, returnér en tom/bruger uden data
        }
        return user;
    }
}
