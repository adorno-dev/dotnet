using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Models;

public sealed class TodoContext : DbContext
{
    public TodoContext(DbContextOptions options) : base(options) {}

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}