using Microsoft.EntityFrameworkCore;
using SenacGames.Domain.Entities;
using SenacGames.Domain.Interfaces;
using SenacGames.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenacGames.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SenacGamesDbContext _context;

        public CategoryRepository(SenacGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                 .Include(c => c.Games) // Inclui os games para contar
                 .OrderBy(c => c.Name) // Ordena as categorias por nome
                 .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Games) // Faz JOIN com a tabrla Categories
                .FirstOrDefaultAsync(c => c.Id == id); // Busca o jogo pelo ID

        }
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category); //o Update não necessita de await pois ele apenas marca a entidade como modificada, sem realizar uma operação assíncrona
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            return await _context.Categories.CountAsync();
        }

    }
}
