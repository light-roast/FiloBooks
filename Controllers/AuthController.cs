using ControlboxLibreriaAPI.Entities;
using ControlboxLibreriaAPI.Modelo;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly FirebaseAuth _firebaseAuth;
    private readonly IConfiguration _configuration;
    private readonly FiloBookContext _context;

    public AuthController(FirebaseApp firebaseApp, IConfiguration configuration, FiloBookContext context)
    {
        _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
    {
        try
        {
            var userRecord = await _firebaseAuth.CreateUserAsync(new UserRecordArgs
            {
                Email = model.Email,
                Password = model.Password,
                DisplayName = model.DisplayName
            });

            // Create a new user in your database
            var newUser = new Usuario
            {
                FirebaseUserId = userRecord.Uid,
                CorreoElectronico = userRecord.Email,
                Username = userRecord.DisplayName,
            };

            _context.Usuario.Add(newUser);
            await _context.SaveChangesAsync();

            var token = await _firebaseAuth.CreateCustomTokenAsync(userRecord.Uid);
            return Ok(new { Token = token, User = newUser });
        }
        catch (FirebaseAuthException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "An error occurred while creating the user in the database.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var apiKey = _configuration["Firebase:ApiKey"];

        // Check if API key is null or empty
        if (string.IsNullOrEmpty(apiKey))
        {
            return BadRequest("API key is not configured.");
        }

        try
        {
            // 1. Verify Password using Firebase Authentication REST API
            var verificationUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
            var verificationBody = new
            {
                email = model.Email,
                password = model.Password,
                returnSecureToken = true
            };

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(verificationBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(verificationUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return BadRequest(errorResponse);
                }

                // Parse the successful response to get the user information and token
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(responseContent);

                // Ensure you access the correct fields from the response
                var localId = (string)responseData.localId; // User ID
                var idToken = (string)responseData.idToken; // Firebase ID token
                var refreshToken = (string)responseData.refreshToken; // Firebase refresh token
                var expiresIn = (string)responseData.expiresIn; // Token expiry time in seconds
                var email = (string)responseData.email; // User email

                // Return the required information
                return Ok(new
                {
                    LocalId = localId,
                    Token = idToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = expiresIn,
                    Email = email
                });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class SignUpModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}