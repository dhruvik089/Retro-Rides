using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace DhruvikLimbasiya_0415.Api.JwtToken
{
    public class Authentication
    {
        public static string GenerateJWTAuthetication(string userName )

        {

            var claims = new List<Claim>

            {

            new Claim(JwtHeaderParameterNames.Jku, userName),

            new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),

            new Claim(ClaimTypes.NameIdentifier, userName)

            };

           

            var key = new SymmetricSecurityKey(

            Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"])));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires =

            DateTime.Now.AddMinutes(

            Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["config:JwtExpireMinutes"])));

            var token = new JwtSecurityToken(

            Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),

            Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),

            claims,

            expires: expires,

            signingCredentials: creds

            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public static string ValidateToken(string token)

        {

            if (token == null)

                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));

            try

            {

                tokenHandler.ValidateToken(token, new TokenValidationParameters

                {

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,

                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var jku = jwtToken.Claims.First(claim => claim.Type == "jku").Value;

                var userName = jwtToken.Claims.First(claim => claim.Type == "kid").Value;

                return userName;

            }

            catch

            {

                return null;

            }

        }
    }
}