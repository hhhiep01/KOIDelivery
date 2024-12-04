using Application.Interface;
using Application.Request.UserAccount;
using Application.Response;
using Domain;
using Domain.Entity;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Security.Cryptography;


namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;
        private AppSetting _appSettings;
        private IClaimService _claimService;
        private IEmailService _emailService;
        public AuthService(IUnitOfWork unitOfWork, AppSetting appSettings, IClaimService claimService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _claimService = claimService;
            _emailService = emailService;
        }
        public async Task<ApiResponse> RegisterAsync(UserRegisterRequest userRequest)
        {
            ApiResponse response = new ApiResponse();
            var checkPassword = CheckUserPassword(userRequest);
            if (!checkPassword)
            {
                response.SetBadRequest(message: "Confirm password is wrong !");
                return response;
            }
            var existingUser = await _unitOfWork.UserAccounts.GetAsync(x => x.Email == userRequest.Email);
            if (existingUser != null)
            {
                response.SetBadRequest(message: "The email address is already register");
                return response;
            }
            // Create password hash and save user details
            var pass = CreatePasswordHash(userRequest.Password);
            UserAccount user = new UserAccount()
            {
                //UserName = userRequest.UserName,
                PasswordHash = pass.PasswordHash,
                PasswordSalt = pass.PasswordSalt,
                Email = userRequest.Email,
                FirstName = userRequest.FistName,
                LastName = userRequest.LastName,
                Role = userRequest.Role,
                IsEmailVerified = false // Initially, email is not verified
            };

            await _unitOfWork.UserAccounts.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();

            // Generate verification code
            var verificationCode = GenerateVerificationCode(); // Method to generate the code
            var emailVerification = new EmailVerification
            {
                UserId = user.Id,
                VerificationCode = verificationCode,
                ExpiresAt = DateTime.Now.AddMinutes(30), // Code valid for 30 minutes
                IsUsed = false
            };

            // Save verification code to the database
            await _unitOfWork.EmailVerifications.AddAsync(emailVerification);
            await _unitOfWork.SaveChangeAsync();

            // Prepare email content
            string emailContent = $"Dear {user.FirstName},<br/>Please use the following verification code to validate your email: <strong>{verificationCode}</strong>.<br/>The code will expire in 30 minutes.";

            // Send validation email
            var emailResponse = await _emailService.SendValidationEmail(user.Email, emailContent);
            if (!emailResponse.IsSuccess)
            {
                response.SetBadRequest("Failed to send verification email.");
                return response;
            }

            response.SetOk(user.Id);
            return response;
        }
        public async Task<ApiResponse> VerifyEmailAsync(int userId, string verificationCode)
        {
            ApiResponse response = new ApiResponse();

            // Retrieve the verification record
            var verificationRecord = await _unitOfWork.EmailVerifications
                .GetAsync(x => x.UserId == userId && x.VerificationCode == verificationCode && x.IsUsed == false);

            // Verification record not found or code already used
            if (verificationRecord == null)
            {
                response.SetBadRequest("Invalid or expired verification code.");
                return response;
            }

            // Check if the code has expired
            if (verificationRecord.ExpiresAt < DateTime.Now)
            {
                response.SetBadRequest("The verification code has expired.");
                return response;
            }

            // Mark the verification code as used
            verificationRecord.IsUsed = true;
            await _unitOfWork.SaveChangeAsync();

            // Mark the user's email as verified
            var user = await _unitOfWork.UserAccounts.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                response.SetBadRequest("User not found.");
                return response;
            }

            user.IsEmailVerified = true;
            await _unitOfWork.SaveChangeAsync();

            response.SetOk("Email verified successfully.");
            return response;
        }
        private PasswordDTO CreatePasswordHash(string password)
        {
            PasswordDTO pass = new PasswordDTO();
            using (var hmac = new HMACSHA512())
            {
                pass.PasswordSalt = hmac.Key;
                pass.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return pass;
        }
        public async Task<ApiResponse> LoginAsync(LoginRequest request)
        {
            ApiResponse response = new ApiResponse();
            var account = await _unitOfWork.UserAccounts.GetAsync(u => u.Email == request.UserEmail);
            if (account == null || !VerifyPasswordHash(request.Password, account.PasswordHash, account.PasswordSalt))
            {
                response.SetBadRequest(message: "Email or password is wrong");
                return response;
            }

            if (account.IsEmailVerified == false)
            {
                response.SetBadRequest(message: "Please Verify your email");
                return response;
            }

            response.SetOk(CreateToken(account));
            return response;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }



        private string CreateToken(UserAccount user)
        {
            var fullName = user.FirstName + " " + user.LastName;
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Role", user.Role.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim( "Email" , user.Email!),
                new Claim("UserId", user.Id.ToString()),
                new Claim("FullName", fullName),
                new Claim(ClaimTypes.Name, fullName),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                 _appSettings!.SecretToken.Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generate a 6-digit code
        }
        public bool CheckUserPassword(UserRegisterRequest user)
        {
            if (user.Password is null) return (false);
            return (user.Password.Equals(user.ConfirmPassword));
        }

        public class PasswordDTO
        {
            public byte[] PasswordHash { get; set; } = new byte[32];
            public byte[] PasswordSalt { get; set; } = new byte[32];
        }


    }
}
