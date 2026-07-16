using Application.Interfaces.Services.TokenServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Extension.Service.TokenSerivces;

public class TokenSerivce: ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenSerivce(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> GenerateToken(int UserId, string Role)
    {
        var key = _configuration["JwtSettings:SecretKey"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var claims = new Claim[]
        {
            new Claim ("role", Role),
            new Claim ("UserId", UserId.ToString())
        };
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        
    }

}

