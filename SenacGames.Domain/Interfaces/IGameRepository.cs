using SenacGames.Domain.Entities;

namespace SenacGames.Domain.Interfaces
{
    // Interface com os métodos para trabalhar com jogos
    public interface IGameRepository
    {
        // Task = operação assíncrona
        // IEnumerable = lista de jogos

        // Lista todos os jogos
        Task<IEnumerable<Game>> GetAllAsync();

        // Busca um jogo pelo ID
        // Pode retornar um Game ou null
        Task<Game?> GetByIdAsync(int id);

        // Lista os jogos em destaque
        Task<IEnumerable<Game>> GetFeaturedAsync();

        // Lista os jogos de uma categoria
        Task<IEnumerable<Game>> GetByCategoryAsync(int categoryId);

        // Adiciona um jogo
        Task AddAsync(Game game);

        // Atualiza um jogo
        Task UpdateAsync(Game game);

        // Exclui um jogo
        Task DeleteAsync(int id);

        // Conta quantos jogos existem
        Task<int> CountAsync();
    }
}