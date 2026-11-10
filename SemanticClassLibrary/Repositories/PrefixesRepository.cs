using Microsoft.EntityFrameworkCore;
using SemanticClassLibrary.Models;

namespace SemanticClassLibrary.Repositories
{
    public class PrefixesRepository : IRepository<Prefixes>
    {
        private readonly ApplicationContext DB;
        public PrefixesRepository(ApplicationContext DB)
        {
            this.DB = DB;
        }

        public void Create(Prefixes item)
        {
            DB.Prefixes.Add(item);
            DB.SaveChanges();
        }

        public Prefixes Read(int id)
            => DB.Prefixes.Find(id);

        public DbSet<Prefixes> Read()
            => DB.Prefixes;

        public void Update(int id, Prefixes item)
        {
            DB.Prefixes.Update(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var prefix = DB.Prefixes.Find(id);
            DB.Prefixes.Remove(prefix);
            DB.SaveChanges();
        }

    }
}
