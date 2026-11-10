using Microsoft.EntityFrameworkCore;

namespace SemanticClassLibrary.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        T Read(int id);
        DbSet<T> Read();
        void Update(int id, T item);
        void Delete(int id);
    }
}