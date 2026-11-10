using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;
using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Structures;

namespace SemanticClassLibrary.Services
{
    public class EndingsService : IService<Endings>
    {
        private readonly EndingsRepository endingsRepository;

        public EndingsService(ApplicationContext DB)
        {
            endingsRepository = new EndingsRepository(DB);
        }

        public void Add(Endings item)
        {
            endingsRepository.Create(item);
        }

        public Endings Get(int id)
            => endingsRepository.Read(id);
        
        public List<Endings> Get()
            => endingsRepository.Read().ToList();

        public void Update(int id, Endings item)
        {
            endingsRepository.Update(id, item);
        }

        public void Delete(int id)
        {
            endingsRepository.Delete(id);
        }

        public List<Ending> GetEndings(int uniqueWordsId)
        {
            return endingsRepository
                .Read()
                .Where(e => e.UniqueWordsId == uniqueWordsId)
                // Запись из таблицы преобразовываем в структуру.
                .Select(e => new Ending
                {
                    ending = e.Ending,
                    partOfSpeech = (PartOfSpeech)e.PartOfSpeechId,
                    gender = (Gender)e.GenderId,
                    plural = (Plurality)e.PluralityId,
                    declension = (Declension)e.DeclensionId
                })
                .ToList();
        }

        public void WriteEndings(int uniqueWordsId, List<Ending> endings)
        {
            endings.Select(e => new Endings()
            {
                PartOfSpeechId = (int)e.partOfSpeech,
                Ending = e.ending,
                GenderId = (int)e.gender,
                PluralityId = (int)e.plural,
                DeclensionId = (int)e.declension,
                UniqueWordsId = uniqueWordsId
            })
            .ToList()
            .ForEach(endingsRepository.Create);
        }
    }
}
