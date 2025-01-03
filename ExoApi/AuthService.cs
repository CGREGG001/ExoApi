using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ExoApi;

public class AuthService
{
    public IConfiguration Config;

    public AuthService(IConfiguration config)
    {
        Config = config;
    }

    public string GenerateToken(User user)
    {
        // Création d'un objet de sercurité avec les informations à stocker dans le token (pas d'info sensibles !!! password,...)
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        // Récupération de la clé pour faire la sécurité
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]!));
        // Object qui connait la clé et l'algo pour signer le token
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Génération du token 
        JwtSecurityToken validatedToken = new JwtSecurityToken(
            Config["Jwt:Issuer"],  
            Config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);
        // export du token en chaîne de caractères (string)
        return new JwtSecurityTokenHandler().WriteToken(validatedToken);
    }
}