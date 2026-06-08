// =============================================================================
// SenacGames.Domain - Interface ICategoryRepository
// =============================================================================
// Contrato do repositório de categorias.
// Segue o mesmo padrão do IGameRepository.
// =============================================================================

using SenacGames.Domain.Entities;

namespace SenacGames.Domain.Interfaces
{
    // Contrato = define o que deve ser feito, mas não como fazer
    // Async = operação sem travar o sistema

    /// <summary>
    /// Contrato do repositório de Categorias.
    /// Define as operações disponíveis para acessar dados de categorias.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// IEnumerable = lista de categorias 
        ///  Lista todas as categorias
        /// </summary>
        /// <returns></returns>
       
        Task<IEnumerable<Category>> GetAllAsync();

        /// <summary>
        /// Busca uma categoria pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Category?> GetByIdAsync(int id);

        /// <summary>
        /// Adiciona uma categoria
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task AddAsync(Category category);

        /// <summary>
        /// Atualiza uma categoria
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task UpdateAsync(Category category);

        /// <summary>
        /// Exclui uma categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Conta quantas categorias existem
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
    }
}