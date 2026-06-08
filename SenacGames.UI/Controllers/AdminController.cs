using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenacGames.Application.DTOs;
using SenacGames.Application.Interfaces;
using SenacGames.Application.ViewModels;
using System.Reflection;

namespace SenacGames.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        public AdminController(IGameService gameService, ICategoryService categoryService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
        }

        // ==========================================
        // DASHBOARD ADMINISTRATIVO
        // ==========================================
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveMenu"] = "Dashboard";
            ViewData["Title"] = "Dashboard";
            ViewData["Subtitle"] = "Resumo do sistema SenacGames";

            var viewModel = new DashboardViewModel
            {
                TotalGames = await _gameService.CountAsync(),
                TotalCategories = await _categoryService.CountAsync(),
                FeaturedGames = (await _gameService.GetFeaturedAsync()).Count(),
                RecentGames = (await _gameService.GetAllAsync()).Take(5)
            };
            return View(viewModel);
        }

        // ==========================================
        // CRUD DE GAMES
        // ==========================================

        public async Task<IActionResult> Games()
        {
            ViewData["ActiveMenu"] = "Games";
            ViewData["Title"] = "Gerenciar Games";
            ViewData["Subtitle"] = "Cadastre, edite e exclua games do catálogo";

            var games = await _gameService.GetAllAsync();
            return View(games);
        }

        /// <summary>
        /// Formulário para criação de um novo game.
        /// GET : /Admin/CreateGame
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateGame()
        {
            ViewData["ActiveMenu"] = "Games";
            ViewData["Title"] = "Cadastrar Novo Game";

            var categories = await _categoryService.GetAllAsync();
            var viewModel = new GameFormViewModel
            {
                Categories = categories,
                ReleaseYear = DateTime.Now.Year
            };

            return View(viewModel);


        }

        //Processa a criação de um novo game.
        // POST : /Admin/CreateGame

        /// <summary>
        /// Processa a criação de um novo game.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGame(GameFormViewModel viewModel)
        {

            var dto = new CreateGameDto
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                ReleaseYear = viewModel.ReleaseYear,
                CoverImageUrl = viewModel.CoverImageUrl,
                CategoryId = viewModel.CategoryId,
                IsFeatured = viewModel.IsFeatured

            };
            await _gameService.CreateAsync(dto);
            TempData["Success"] = "Game cadastrado com sucesso!";
            return RedirectToAction(nameof(Games));

        }

        /// <summary>
        /// Formulário para edição de um game existente.
        /// GET : /Admin/EditGame/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditGame(int id)
        {
            ViewData["ActiveMenu"] = "Games";
            ViewData["Title"] = "Editar Game";

            var game = await _gameService.GetByIdAsync(id);
            if (game == null) return NotFound();

            var categories = await _categoryService.GetAllAsync();

            var viewmodel = new GameFormViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ReleaseYear = game.ReleaseYear,
                CoverImageUrl = game.CoverImageUrl,
                CategoryId = game.CategoryId,
                IsFeatured = game.IsFeatured,
                Categories = categories
            };
            return View(viewmodel);
        }
        /// <summary>
        /// Processa a edição de um game existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditGame(int id, GameFormViewModel viewModel)
        {

            var dto = new UpdateGameDto
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                ReleaseYear = viewModel.ReleaseYear,
                CoverImageUrl = viewModel.CoverImageUrl,
                CategoryId = viewModel.CategoryId,
                IsFeatured = viewModel.IsFeatured
            };

            var result = await _gameService.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            TempData["Success"] = "Game atualizado com sucesso!";
            return RedirectToAction(nameof(Games));
        }
        /// <summary>
        /// Formulário para confirmação de exclusão de um game.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> DeleteGame(int id)
        {
            ViewData["ActiveMenu"] = "Games";
            ViewData["Title"] = "Excluir Game";

            var game = await _gameService.GetByIdAsync(id);
            if (game == null) return NotFound();

            return View(game);

        }

        /// <summary>
        /// Processa a exclusão de um game existente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGameConfirmed(int id)
        {
            await _gameService.DeleteAsync(id);
            TempData["Success"] = "Game excluído com sucesso!";
            return RedirectToAction(nameof(Games));
        }

        // =========================================
        // CRUD DE CATEGORIAS
        // =========================================
            
        /// <summary>
        /// Exibe a lista de categorias para gerenciamento.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Categories()
        {
            ViewData["ActiveMenu"] = "Categories";
            ViewData["Title"] = "Gerenciar Categorias";
            ViewData["Subtitle"] = "Cadastre, edite e exclua categorias de games";

            ///TODO: Implementar a listagem de categorias
            var categories = await _categoryService.GetAllAsync();
            return View(categories);

        }
        /// <summary>
        /// Formulário para criação de uma nova categoria.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewData["ActiveMenu"] = "Categories";
            ViewData["Title"] = "Cadastrar Nova Categoria";
            return View();
        }

        /// <summary>
        /// Processa a criação de uma nova categoria.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)

        {
            await _categoryService.CreateAsync(dto);
            TempData["Success"] = "Categoria cadastrada com sucesso!";
            return RedirectToAction(nameof(Categories)); //Redireciona para a lista de categorias após criação
        }

 
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            ViewData["ActiveMenu"] = "Categories";
            ViewData["Title"] = "Editar Categoria";

            var category = await _categoryService.GetByIdAsync(id);
            if(category == null)   return NotFound();
              
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, UpdateCategoryDto dto)
        {
            var result = await _categoryService.UpdateAsync(id, dto);
            if (result == null) return NotFound();

            TempData["Success"] = "Categoria atualizada com sucesso!";
            return RedirectToAction(nameof(Categories));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id) 
        
        {

            ViewData["ActiveMenu"] = "Categories";
            ViewData["Title"] = "Excluir Categoria";

            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted)
            {
                TempData["Error"] = "Não foi possível excluit a categoria. Verifique se há games associados";
                return RedirectToAction(nameof(Categories));
            }
            TempData["Sucess"] = "Categoria excluida com sucesso!";
            return RedirectToAction(nameof(Categories));
        }


    }
}
