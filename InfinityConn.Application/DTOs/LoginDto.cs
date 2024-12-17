using System;
using System.ComponentModel.DataAnnotations;

namespace InfinityConn.Application.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "The username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
