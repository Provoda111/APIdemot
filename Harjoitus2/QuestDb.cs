using Microsoft.EntityFrameworkCore;

namespace Harjoitus2
{
    public class QuestDb : DbContext
    {
        public QuestDb(DbContextOptions<QuestDb> options)
        : base(options) { }

        public DbSet<Quest> Quests => Set<Quest>();
    }
}
