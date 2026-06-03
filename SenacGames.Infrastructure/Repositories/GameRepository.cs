using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SenacGames.Domain.Entities;
using SenacGames.Domain.Interfaces;
using SenacGames.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenacGames.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de Games usando o
    /// Entity FrameWork Core para acessar o banco de dados
    /// </summary>


    public class GameRepository : IGameRepository
    {
        private readonly SenacGamesDbContext _context;

        public GameRepository(SenacGamesDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna todos os Games incluindo categoria relacionada.]
        /// OBS: Includ() = carrega dados de tabelas relacionadas
        /// </summary>
        /// <returns>
        /// Lista de jogos por ordem de data/criação 
        /// </returns>
        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .Include(g => g.Category) // Faz JOIN com a tabrla Categories
                .OrderByDescending(g => g.CreatedAt) // Ordena por data de criação
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um game por ID, incluindo a categoria relacionada
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Game com id específico </returns>
        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Category) // Faz JOIN com a tabrla Categories
                .FirstOrDefaultAsync(g => g.Id == id); // Busca o jogo pelo ID

        }

        /// <summary>
        /// Retorna apenas os games marcados como destaque
        /// OBS: WHERE() = Filtra os dados com base em uma condição (equivalente ao WHERE do SQL)
        /// </summary>


        public async Task<IEnumerable<Game>> GetFeaturedAsync()
        {
            return await _context.Games
                .Include(g => g.Category) // Faz JOIN com a tabrla Categories
                .Where(g => g.IsFeatured) // Filtra apenas os games em destaque
                .ToListAsync();
        }

        /// <summary>
        /// retorna todos os games de uma categoria especifica
        /// </summary>

        public async Task<IEnumerable<Game>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Games
                .Include(g => g.Category) // Faz JOIN com a tabrla Categories
                .Where(g => g.CategoryId == categoryId) // Filtra apenas os games em destaque
                .ToListAsync();
        }

        ///<summary>
        /// Adicona um novo game ao banco de dados e salva as alterações.
        /// OBS: AddAsync() = adiciona um novo resgistro ao bancp de dados de forma assíncrona
        /// OBS; SaveChangesAsync () = equivalente ao Insert do SQL, salva as alterações feitas no contexto do banco
        /// </summary>

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// atualiza um game existente no banco de dados e salva as alterações.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game); //o Update não necessita de await pois ele apenas marca a entidade como modificada, sem realizar uma operação assíncrona
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///  Remove um game do banco de dados com base no ID e salva as alterações.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id) 
        { 
            var game = await _context.Games.FindAsync(id); 
            if(game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Conta o número total de games no banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _context.Games.CountAsync();
        }
    }
}
