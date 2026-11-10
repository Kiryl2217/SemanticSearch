using Microsoft.EntityFrameworkCore;
using SemanticClassLibrary.Models;

namespace SemanticClassLibrary.Repositories
{
    public class SequentialWordsRepository : IRepository<SequentialWords>
    {
        private readonly ApplicationContext DB;
        public SequentialWordsRepository(ApplicationContext DB)
        {
            this.DB = DB;
        }

        public void Create(SequentialWords item)
        {
            DB.SequentialWords.Add(item);
            DB.SaveChanges();
        }

        public SequentialWords Read(int id)
            => DB.SequentialWords.Find(id);

        public DbSet<SequentialWords> Read()
            => DB.SequentialWords;

        public void Update(int id, SequentialWords item)
        {
            DB.SequentialWords.Update(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var sequentialWord = DB.SequentialWords.Find(id);
            DB.SequentialWords.Remove(sequentialWord);
            DB.SaveChanges();
        }

    }
}
