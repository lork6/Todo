using System;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(@"Data Source=C:\foo_db\Todo.db");
            }
            base.OnConfiguring(options);
        }
           

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
