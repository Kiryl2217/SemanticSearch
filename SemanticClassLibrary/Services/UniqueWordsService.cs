using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;


namespace SemanticClassLibrary.Services
{
    public class UniqueWordsService : IService<UniqueWords>
    {
        private readonly UniqueWordsRepository uniqueWordsRepository;

        public UniqueWordsService(ApplicationContext DB)
        {
            uniqueWordsRepository = new UniqueWordsRepository(DB);
        }

        public void Add(UniqueWords item)
        {
            uniqueWordsRepository.Create(item);
        }

        public UniqueWords Get(int id)
            => uniqueWordsRepository.Read(id);
        
        public List<UniqueWords> Get()
            => uniqueWordsRepository.Read().ToList();

        public void Update(int id, UniqueWords item)
        {
            uniqueWordsRepository.Update(id, item);
        }

        public void Delete(int id)
        {
            uniqueWordsRepository.Delete(id);
        }

        public UniqueWords? GetByWord(string word)
            => uniqueWordsRepository.Read()
                .FirstOrDefault(uw => uw.OriginalWord.Equals(word));
    }
}
