using Microsoft.EntityFrameworkCore;
using SemanticClassLibrary.Models;
using SemanticSearch.Models;

namespace SemanticClassLibrary.Repositories
{
    public class EndingsRepository : IRepository<Endings>
    {
        private readonly ApplicationContext DB;
        public EndingsRepository(ApplicationContext DB)
        {
            this.DB = DB;
        }

        public void Create(Endings item)
        {
            DB.Endings.Add(item);
            DB.SaveChanges();
        }

        public Endings Read(int id)
            => DB.Endings.Find(id);
        

        public DbSet<Endings> Read()
            => DB.Endings;
        

        public void Update(int id, Endings item)
        {
            DB.Endings.Update(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var ending = DB.Endings.Find(id);
            DB.Endings.Remove(ending);
            DB.SaveChanges();
        }

    }
}
