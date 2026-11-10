using Microsoft.EntityFrameworkCore;
using SemanticClassLibrary.Models;

namespace SemanticClassLibrary.Repositories
{
    public class UniqueWordsRepository : IRepository<UniqueWords>
    {
        private readonly ApplicationContext DB;
        public UniqueWordsRepository(ApplicationContext DB)
        {
            this.DB = DB;
        }

        public void Create(UniqueWords item)
        {
            DB.UniqueWords.Add(item);
            DB.SaveChanges();
        }

        public UniqueWords Read(int id)
            => DB.UniqueWords.Find(id);

        public DbSet<UniqueWords> Read()
            => DB.UniqueWords;        

        public void Update(int id, UniqueWords item)
        {
            DB.UniqueWords.Update(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var uniqueWord = DB.UniqueWords.Find(id);
            DB.UniqueWords.Remove(uniqueWord);
            DB.SaveChanges();
        }
    }
}
