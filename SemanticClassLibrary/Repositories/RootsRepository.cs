using Microsoft.EntityFrameworkCore;
using SemanticClassLibrary.Models;

namespace SemanticClassLibrary.Repositories
{
    public class RootsRepository : IRepository<Roots>
    {
        private readonly ApplicationContext DB;
        public RootsRepository(ApplicationContext DB)
        {
            this.DB = DB;
        }

        public void Create(Roots item)
        {
            DB.Roots.Add(item);
            DB.SaveChanges();
        }

        public Roots Read(int id)
            => DB.Roots.Find(id);

        public DbSet<Roots> Read()
            => DB.Roots;

        public void Update(int id, Roots item)
        {
            DB.Roots.Update(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var root = DB.Roots.Find(id);
            DB.Roots.Remove(root);
            DB.SaveChanges();
        }

    }
}
