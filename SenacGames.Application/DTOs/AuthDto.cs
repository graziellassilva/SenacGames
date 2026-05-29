using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SenacGames.Application.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;

    }
    public class RegisterDto 
    {
        public string Email { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;

        public string ConfirmPassWord {  get; set; } = string.Empty;
    }
    public class UsertDto 
    { 
        public string Id { get; set; } = string.Empty;
     public string Email { get; set; } = string.Empty;
        public IList <string> Roles { get; set; } = new List<string>();


    }

}
