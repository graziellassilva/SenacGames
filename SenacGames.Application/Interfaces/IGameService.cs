using SenacGames.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenacGames.Application.Interfaces
{
    //Contrato de serviçi de Games.
    //Define as opeações de negócio disponíveis para o game
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllsync();
        Task<GameDto?> GetByIdAsync(int id);
        Task<IEnumerable<GameDto>> GetFeaturedAsync();
        Task<IEnumerable<GameDto>> GetByCategoryAsync(int categoryId);
        Task<GameDto> CreateAsync(GameDto dto);

        Task<GameDto?> UpdateAsync(int id, UpdateGameDto dto);
        Task<bool> DeleteAsync(int id);
        Task<int> CountAsync();


    }
}
