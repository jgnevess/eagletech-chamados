using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eagletechapi.models;
using eagletechapi.models.chamado;
using eagletechapi.models.usuario;
using Microsoft.EntityFrameworkCore;

// Contexto do banco de dados.
// Responsável por gerenciar a conexão com o banco e mapear as entidades do modelo através do Entity Framework.

namespace eagletechapi.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}