using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InfinityConn.Infraestructure.Identity;
using InfinityConn.Infraestructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InfinityConn.Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultViewModel<string>>
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _signInManager = signInManager;
    }

    public async Task<ResultViewModel<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
           var result = await _signInManager.PasswordSignInAsync(
                request.Username,
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false);
           

            var user = await _userManager.FindByNameAsync(request.Username);
            if (result.Succeeded == false || user == null)
            {
                return ResultViewModel<string>.Error("Invalid username or password");
            }
            var token = GenerateJwtToken(user);
            return ResultViewModel<string>.Success(token);

        }
        catch (Exception ex)
        {
            return ResultViewModel<string>.Error(ex.Message);
        }
    }


    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),        
            new Claim(ClaimTypes.NameIdentifier, user.Id), 
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
