using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;
using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Structures;

namespace SemanticClassLibrary.Services
{
    public class SuffixesService : IService<Suffixes>
    {
        private readonly SuffixesRepository suffixesRepository;

        public SuffixesService(ApplicationContext DB)
        {
            suffixesRepository = new SuffixesRepository(DB);
        }

        public void Add(Suffixes item)
        {
            suffixesRepository.Create(item);
        }

        public Suffixes Get(int id)
            => suffixesRepository.Read(id);
        
        public List<Suffixes> Get()
            => suffixesRepository.Read().ToList();

        public void Update(int id, Suffixes item)
        {
            suffixesRepository.Update(id, item);
        }

        public void Delete(int id)
        {
            suffixesRepository.Delete(id);
        }

        public List<Suffix> GetSuffixes(int uniqueWordsId)
        {
            return suffixesRepository
                .Read()
                .Where(s => s.UniqueWordsId == uniqueWordsId)
                // Запись из таблицы преобразовываем в структуру.
                .Select(s => new Suffix
                {
                    suffix = s.Suffix,
                    partOfSpeech = (PartOfSpeech)s.PartOfSpeechId,
                    suffixType = (SuffixType)s.SuffixTypeId,
                    suffixWithEnding = (SuffixWithEnding)s.SuffixWithEndingId
                })
                .ToList();
        }

        public void WriteSuffixes(int uniqueWordsId, List<Suffix> suffixes)
        {
            suffixes.Select(s => new Suffixes()
            {
                PartOfSpeechId = (int)s.partOfSpeech,
                SuffixTypeId = (int)s.suffixType,
                SuffixWithEndingId = (int)s.suffixWithEnding,
                Suffix = s.suffix,
                UniqueWordsId = uniqueWordsId
            })
            .ToList()
            .ForEach(suffixesRepository.Create);
        }
    }
}
