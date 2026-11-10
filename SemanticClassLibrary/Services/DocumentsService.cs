using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;


namespace SemanticClassLibrary.Services
{
    public class DocumentsService : IService<Documents>
    {
        private readonly DocumentsRepository documentsRepository;

        public DocumentsService(ApplicationContext DB)
        {
            documentsRepository = new DocumentsRepository(DB);
        }

        public void Add(Documents item)
        {
            documentsRepository.Create(item);
        }

        public Documents Get(int id)
            => documentsRepository.Read(id);
        
        public List<Documents> Get()
            => documentsRepository.Read().ToList();

        public void Update(int id, Documents item)
        {
            documentsRepository.Update(id, item);
        }

        public void Delete(int id)
        {
            documentsRepository.Delete(id);
        }


        public Documents? GetByText(string text)
            => documentsRepository.Read()
                .FirstOrDefault(e => e.OriginalText.Equals(text));
    }
}
