using CRUD_JWT.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Claims;
using System.Text;

namespace CRUD_JWT.Services
{
    public class JWTServices
    {
        private readonly IConfiguration _config;
        public JWTServices(IConfiguration _configuration)
        {
            _config=_configuration;
        }

     // Method >> Generate Token
        public (string token, DateTime experitiondate) GenerateToken(User user_obj)
        {

            // Experation Date
            var experation =  DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt: DurationInMinutes")
                                                        );


            // Claims Contains user Info
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user_obj.Id.ToString()),
            new Claim(ClaimTypes.Name, user_obj.Name),
            new Claim(ClaimTypes.Email, user_obj.Email),
            new Claim(ClaimTypes.Role, user_obj.Role)
        };


            // Create a security key
            var key = new SymmetricSecurityKey(
                Encoding.UTF32.GetBytes(_config["Jwt: Key"]) 
                                               );

            // Create a Signing Credientials
             var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //Combine all the information to create a token 
            var token = new JwtSecurityToken(
                issuer: _config["Jwt: Issuer"],
                audience: _config["Jwt: Audience"],
                claims: claims,
                expires: experation,
                signingCredentials: Credentials
                );


            var TokenString = new JwtSecurityTokenHandler().WriteToken(token);

            //Finally Returned!
            return (TokenString, experation);   
        }

    }
}
