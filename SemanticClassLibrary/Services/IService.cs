namespace SemanticClassLibrary.Services
{
    public interface IService<T> where T : class
    {
        void Add(T item);

        T Get(int id);
        List<T> Get();

        void Update(int id, T item);
        void Delete(int id);
    }
}