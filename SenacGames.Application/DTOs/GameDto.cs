using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace SenacGames.Application.DTOs
{
    public class GameDto
    {

        public int Id {  get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public int ReleaseYear { get; set; }    
        public string CoverImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName {  get; set; } = string.Empty; // Nome da categoria (ex: RPG)
        
        public bool IsFeatured { get; set; }

        public DateTime CreatedAt { get; set; }


    }
    public class CreateGameDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public int CategoryId {  get; set; }
        public bool IsFeatured { get; set; }  


    }
    public class UpdateGameDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public bool IsFeatured { get; set; }

    }

}
