using BookingBackend.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingBackend.Data.Service
{
    public class GenerarTokenClaim
    {
       
        private readonly IConfiguration _configuration;

        public GenerarTokenClaim(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(CustomerModel customer)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, customer.FullName),
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString())
            // Puedes añadir más claims aquí si es necesario
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
