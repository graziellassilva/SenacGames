using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SenacGames.Domain.Entities;
using SenacGames.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenacGames.Infrastructure.Identity
{
    internal class SeedData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();//  Cria um escopo para resolver serviços com tempo de vida adequado
            var context = scope.ServiceProvider.GetRequiredService<SenacGamesDbContext>();//Obtém o contexto do banco de dados 
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>(); // Obtém o UserManager para gerenciar os usuários do Identity
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();// Obtém o RolerManager para gerenciar as funções de usuário

            await context.Database.MigrateAsync(); // Aplica as migrações pendentes para garantir que o banco de dados esteja atualizado

            // ==============================================
            // 1. SEED DE CATEGORIAS
            // ==============================================

            if (context.Categories.Any()) 
            {
                var categories = new List<Category>
                {
                    new Category {Name = "Ação"},
                    new Category{Name = "Aventura"},
                    new Category{Name = "RPG"},
                    new Category{Name = "Corrida"},
                    new Category{Name = "Esportes"},
                    new Category{Name = "Terror"},
                    new Category{Name = "Multiplayer"},
                    new Category{Name = "Indie"}
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            //=====================================
            // 2. SEED DE GAMES
            // ====================================
            if (!context.Categories.Any()) 
            {
                var acao = await context.Categories.FirstAsync(c => c.Name == "Ação");
                var aventura = await context.Categories.FirstAsync(c => c.Name == "Aventura");
                var rpg = await context.Categories.FirstAsync(c => c.Name == "RPG");
                var corrida = await context.Categories.FirstAsync(c => c.Name == "Corrida");
                var esportes = await context.Categories.FirstAsync(c => c.Name == "Esportes");
                var terror = await context.Categories.FirstAsync(c => c.Name == "Terror");
                var multiplayer = await context.Categories.FirstAsync(c => c.Name == "Multiplayer");
                var indie = await context.Categories.FirstAsync(c => c.Name == "Indie");

            }
        }
    }
}
