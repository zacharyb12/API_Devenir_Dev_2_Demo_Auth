using Application_Devenir_Dev_2.DTOS;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace Application_Devenir_Dev_2.Tools
{
    public class JwtHelpers(IOptions<JwtSettings> jwtOptions)
    {

        public string GenerateJwtToken(string login, string role)
        {
            //1 - générer la clé de sécurité
            SymmetricSecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

            //1.1 - Créer les credentials
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            //2 - créer mes claims (jetons) tel que role, email, ...
            Claim[] mesClaims = new[]
            {
                new Claim(ClaimTypes.Country,"Belgium"),
                new Claim(ClaimTypes.Role,role),//pour les clients Microsoft
                new Claim("Role",role), //Interoperability (clients autres)
                new Claim("Pseudo",login)

            };

            //3 - Générer mon token
            JwtSecurityToken token = new JwtSecurityToken
                (
                  issuer: jwtOptions.Value.Issuer,
                  audience: jwtOptions.Value.Audience,
                  claims: mesClaims,
                  expires: DateTime.Now.AddMinutes(10),
                  signingCredentials: credentials
                );

            //4 - renvoyer le token sous forme de string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
