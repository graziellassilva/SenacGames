// =============================================================================
// SenacGames.Domain - Entidade Game
// =============================================================================
// Esta classe representa a entidade principal do sistema: um jogo (Game).
// Ela pertence à camada de DOMÍNIO, que é responsável por definir as entidades
// e regras de negócio do sistema.
//
// 📌 CONCEITO IMPORTANTE:
// A camada Domain NÃO depende de nenhuma outra camada.
// Ela é o "coração" da aplicação e define O QUE o sistema é.
// =============================================================================

namespace SenacGames.Domain.Entities
{
    /// <summary>
    /// Representa um jogo no catálogo do SenacGames.
    /// Cada game possui um título, descrição, ano de lançamento,
    /// imagem de capa e pertence a uma categoria.
    /// </summary>
    public class Game
    {
       /// <summary>
       /// ID único do jogo
       /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do jogo
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do jogo
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Ano de lançamento do jogo
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Link da imagem de capa
        /// </summary>
        public string CoverImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// ID da categoria do jogo
        ///  FK = liga o jogo à categoria
        /// </summary>

        public int CategoryId { get; set; }

        /// <summary>
        ///  Define se o jogo aparece em destaque
        /// </summary>
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Data de cadastro do jogo
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Categoria deste jogo
        // virtual = carrega os dados relacionados
        // ? = pode ser nulo
        public virtual Category? Category { get; set; }

        // =====================================================================
        // NAVIGATION PROPERTY (Propriedade de Navegação)
        // =====================================================================
        // 📌 CONCEITO IMPORTANTE:
        // Navigation Properties permitem que o Entity Framework carregue
        // automaticamente os dados relacionados de outra tabela.
        // Aqui, cada Game "navega" até sua Category correspondente.
        // =====================================================================
    }
}