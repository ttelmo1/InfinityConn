using System;
using InfinityConn.Application.DTOs;
using InfinityConn.Infraestructure.Models;
using MediatR;

namespace InfinityConn.Application.Commands.Register;

public class RegisterCommand : IRequest<ResultViewModel<string>>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public RegisterCommand(RegisterDto dto)
    {
        Username = dto.Username;
        Email = dto.Email;
        Password = dto.Password;
        ConfirmPassword = dto.ConfirmPassword;
    }
}
