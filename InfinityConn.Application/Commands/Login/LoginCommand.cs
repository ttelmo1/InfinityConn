using System;
using InfinityConn.Application.DTOs;
using InfinityConn.Infraestructure.Models;
using MediatR;

namespace InfinityConn.Application.Commands.Login;

public class LoginCommand : IRequest<ResultViewModel<string>>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginCommand(LoginDto dto)
    {
        Username = dto.Username;
        Password = dto.Password;
    }
}
