



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SenacGames.Application.DTOs;


namespace SenacGames.UI.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Controlador responsável por gerenciar as ações relacionadas à conta do usuário,
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;
        /// <summary>
        /// Controlador responsável por gerenciar as ações relacionadas à autenticação do usuário,
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //=====================
        //LOGIN
        //=====================

        //Exibe o formulario do login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        /// Processa o login do usuário, verificando as credenciais fornecidas e redirecionando para a URL anterior ou para a home se o login for bem-sucedido. 
        /// Caso contrário, exibe uma mensagem de erro.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Login(LoginDto dto, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            //tenta fazer login
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //Redireciona para a URL anterior ou para a home

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }
            //Se falhou , exibe mensagem de erro
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            return View(dto);

        }


        //=====================
        //Registro
        //=====================

        //Exibe o formulario de registro
        // GET /Account/Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //processa o registro de um novo usuário

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "As senhas não coincidem.");
                return View(dto);
            }
            var User = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };
            var result = await _userManager.CreateAsync(User, dto.Password);
            if (result.Succeeded)

            {   //Faz Login automático após o registro
                await _signInManager.SignInAsync(User, isPersistent: false);
                return RedirectToAction("Index", "Home");

            }// Se falhou, exibe mensagens de erro
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                return View(dto);
            }


        }

        //=====================
        //Logout
        //=====================

        //faz Login do usuario
        // POST /Account/Logout

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //=====================
        // ACESS DENIED
        //=====================

        //Página do acesso negado

        [HttpGet]

        public IActionResult AccessDenied()
        {
            return View();
        } 
    }
}
