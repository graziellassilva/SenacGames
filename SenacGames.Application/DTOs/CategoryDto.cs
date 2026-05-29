using System;
using System.Collections.Generic;
using System.Text;

namespace SenacGames.Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //<Summary>
        //Quantidade de games nesta categoria
        //Útil para mostrar no dashboard e na listagem.
        //<Summary>
        public int GameCount { get; set; }
    }
    //<Summary>
    //DTO Para criação de uma nova categoria
    //<Summary>
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
       
    }
    //<Summary>
    //DTO para atualização de uma categoria existente
    //<Summary>
    public class UpdateCategoryDto
    {
         public string Name { get; set; } = string.Empty;
    }
}
