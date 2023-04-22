using Microsoft.EntityFrameworkCore;
using TexoIT.Teste.Domain.Models;

namespace TexoIT.Teste.Repository;

public class DataBaseContext: DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        
    }
    public DbSet<Movie> Movies { get; set; }
}