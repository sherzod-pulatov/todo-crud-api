using Microsoft.EntityFrameworkCore;

namespace ToDo.Data
{
    public class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
    {
        public DbSet<ToDo> ToDos => Set<ToDo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDo>().HasData(
                new ToDo { Id = 1, Task = "First task", IsDone = false },
                new ToDo { Id = 2, Task = "Second task", IsDone = true },
                new ToDo { Id = 3, Task = "Third task", IsDone = false }
            );
        }
    }
}
