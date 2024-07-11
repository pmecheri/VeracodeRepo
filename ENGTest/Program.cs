using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

class Program
{
    static void Main(string[] args)
    {
        // Define some claims for the JWT token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "user_id_here"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("custom_claim", "custom_claim_value")
        };

        // Generate a random byte array with 32 bytes (256 bits) for the key
        var keyBytes = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(keyBytes);
        }

        // Define the security key used to sign the token
        var key = new SymmetricSecurityKey(keyBytes);

        // Define the credentials used to sign the token
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the JWT token
        var token = new JwtSecurityToken(
            issuer: "issuer_here",
            audience: "audience_here",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Expiry time
            signingCredentials: creds
        );

        // Write the token to the console
        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine(tokenHandler.WriteToken(token));
    }
}