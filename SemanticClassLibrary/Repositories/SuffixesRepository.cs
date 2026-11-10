using Microsoft.EntityFrameworkCore;
using SemanticClassLibrary.Models;

namespace SemanticClassLibrary.Repositories
{
    public class SuffixesRepository : IRepository<Suffixes>
    {
        private readonly ApplicationContext DB;
        public SuffixesRepository(ApplicationContext DB)
        {
            this.DB = DB;
        }

        public void Create(Suffixes item)
        {
            DB.Suffixes.Add(item);
            DB.SaveChanges();
        }

        public Suffixes Read(int id)
            => DB.Suffixes.Find(id);

        public DbSet<Suffixes> Read()
            => DB.Suffixes;        

        public void Update(int id, Suffixes item)
        {
            DB.Suffixes.Update(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var suffix = DB.Suffixes.Find(id);
            DB.Suffixes.Remove(suffix);
            DB.SaveChanges();
        }

    }
}
