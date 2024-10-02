using Microsoft.EntityFrameworkCore;

namespace Harjoitus1
{
    class ToDoDB : DbContext
    {
        public ToDoDB(DbContextOptions<ToDoDB> options) : base(options)
        {
            
        }
        public DbSet<ToDo> Todos => Set<ToDo>();
    }
}
