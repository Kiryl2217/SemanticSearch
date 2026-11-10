using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;
using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Structures;

namespace SemanticClassLibrary.Services
{
    public class PrefixesService : IService<Prefixes>
    {
        private readonly PrefixesRepository prefixesRepository;

        public PrefixesService(ApplicationContext DB)
        {
            prefixesRepository = new PrefixesRepository(DB);
        }

        public void Add(Prefixes item)
        {
            prefixesRepository.Create(item);
        }

        public Prefixes Get(int id)
            => prefixesRepository.Read(id);
        
        public List<Prefixes> Get()
            => prefixesRepository.Read().ToList();

        public void Update(int id, Prefixes item)
        {
            prefixesRepository.Update(id, item);
        }

        public void Delete(int id)
        {
            prefixesRepository.Delete(id);
        }

        public List<Prefix> GetPrefixes(int uniqueWordsId)
        {
            return prefixesRepository
                .Read()
                .Where(p => p.UniqueWordsId == uniqueWordsId)
                // Запись из таблицы преобразовываем в структуру.
                .Select(p => new Prefix
                {
                    prefix = p.Prefix,
                    partOfSpeech = (PartOfSpeech)p.PartOfSpeechId
                })
                .ToList();
        } 

        public void WritePrefixes(int uniqueWordsId, List<Prefix> prefixes)
        {
            prefixes.Select(p => new Prefixes()
            {
                PartOfSpeechId = (int)p.partOfSpeech,
                Prefix = p.prefix,
                UniqueWordsId = uniqueWordsId
            })
            .ToList()
            .ForEach(prefixesRepository.Create);
        }
    }
}
