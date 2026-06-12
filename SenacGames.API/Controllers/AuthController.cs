using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SenacGames.Application.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace SenacGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        //UserManager e SignManager são serviços do Identity
        //UserManager : gerencia operações com usuário (criar,buscar...)
        //SignManager : gerencia operações de autenticação (login, logout..)

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController
            (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registra um novo usuáro
        /// POST /api/auth/register
        /// </summary>


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            //Validação de senha
            if (dto.Password != dto.ConfirmPassword)
               return BadRequest(new {massage= "As senhas não coincidem"});

            var user = new IdentityUser
            { 
                UserName =dto.Email,
                Email = dto.Email
            };

            //Cria o usuário usando o UserManager
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { massage = "Erro ao registrar usuário.", errors });
            }
            return Ok(new { message = "Usuário registrado com sucesso." });
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync
                (dto.Email, dto.Password, false, lockoutOnFailure: false);
            //Ispersistent : se o cookie de autenticação deve ser persistente (permanecer após fechar o navegador)
            //lockoutOnFailure : se deve bloquear o usuário após falhas consecutivas de login
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
              
            }
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var roles = await _userManager.GetRolesAsync(user!); //  ! serve para indicar que é o campo é obrigatório ser preenchido, ou seja, não pode ser nulo

            return Ok(new UserDto
            {
                Email = user!.Email,
                Id = user!.Id,
                Roles = roles
            });
        }

        ///<summary>
        ///Faz logout do usuário
        ///</summary>
        [HttpPost("Logout")]
        
        [Authorize] //autorização

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout realizado com sucesso!" });
        }


        /// <summuary>
        /// Retorna os dados do usuário autenticado
        /// GET /api/auth/me
        /// </summuary>

        [HttpGet("me")] 
        [Authorize]
        public async Task<ActionResult<UserDto>> Me()
        {
            
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return Unauthorized(new { message = "usuário não autenticado." });

              
            }
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }
    }
}
