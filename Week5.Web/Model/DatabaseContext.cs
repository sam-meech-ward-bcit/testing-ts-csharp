using Microsoft.EntityFrameworkCore;

namespace Week5.Web.Models;

public class DatabaseContext : DbContext
{
  public DatabaseContext(DbContextOptions<DatabaseContext> options)
      : base(options) { }

  public DbSet<Todo> Todos => Set<Todo>();
}