using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;
using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Structures;

namespace SemanticClassLibrary.Services
{
    public class RootsService : IService<Roots>
    {
        private readonly RootsRepository rootsRepository;

        public RootsService(ApplicationContext DB)
        {
            rootsRepository = new RootsRepository(DB);
        }

        public void Add(Roots item)
        {
            rootsRepository.Create(item);
        }

        public Roots Get(int id)
            => rootsRepository.Read(id);
        
        public List<Roots> Get()
            => rootsRepository.Read().ToList();

        public void Update(int id, Roots item)
        {
            rootsRepository.Update(id, item);
        }

        public void Delete(int id)
        {
            rootsRepository.Delete(id);
        }

        public List<Root> GetRoots(int uniqueWordsId)
        {
            return rootsRepository
                .Read()
                .Where(r => r.UniqueWordsId == uniqueWordsId)
                // Запись из таблицы преобразовываем в структуру.
                .Select(r => new Root
                {
                    root = r.Root,
                    partOfSpeech = (PartOfSpeech)r.PartOfSpeechId
                })
                .ToList();
        }

        public void WriteRoots(int uniqueWordsId, List<Root> roots)
        {
            roots.Select(r => new Roots()
            {
                PartOfSpeechId = (int)r.partOfSpeech,
                Root = r.root,
                UniqueWordsId = uniqueWordsId
            })
            .ToList()
            .ForEach(rootsRepository.Create);
        }
    }
}
