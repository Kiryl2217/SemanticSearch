using Microsoft.AspNetCore.Mvc;
using SemanticSearchLibrary.Classes;

namespace SemanticSearch.Controllers
{
    public class NoDistributedController : BaseController
    {
        public static object obj = new object();
        public static int paragraphsCount = 0;
        public static int currentParagraph = 0;

        private readonly IConfiguration config;
        public NoDistributedController(IConfiguration config)
        {
            this.config = config;
        }
        public IActionResult Index()
        {
            return View(null);
        }

        [HttpGet]
        public IActionResult SearchParam()
        {
            int v;
            int m;

            // obj - объект синхронизации.
            // currentParagraph, paragraphsCount - общий ресурс для потоков.
            lock (obj)
            {
                // Критическая секция.
                v = currentParagraph;
                m = paragraphsCount;
            }


            // Анонимный объект.
            var model = new { value = v, max = m };

            return Ok(model);
        }



        [HttpGet]
        public IActionResult SearchAction(string userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest))
            {
                return Ok("Ошибка! введите строку для поиска!");
            }

            Analyzer analyzer = new Analyzer(config);
            var documents = analyzer.GetAllDocuments(); // получим все записи документов через сервис

            // Рассчитываем общее кол-во всех абзацев для поиска 
            lock (obj)
            {
                paragraphsCount = 0;
            }

            foreach (var document in documents)
            {
                var originalText = document.OriginalText;
                var originalParagraphs = originalText.Split('\n').ToList(); // оригинал текста

                lock (obj)
                {
                    paragraphsCount += originalParagraphs.Count;
                }
            }

            lock (obj)
            {
                // Сюда входит только один поток
                currentParagraph = 0;
            }

            var globalMaxRating = 0;
            var globalString = string.Empty;
            foreach (var document in documents)
            {

                var originalText = document.OriginalText;

                // Получаем из текста все уникальные форматированные слова
                // или из базы данных уже готовые форматированные 
                var words = analyzer.GetManyWithDB(originalText);

                // получаем словарь, где каждому слову из текста
                // сопоставлен его вектор
                var dictionaryWithUniqueWords = analyzer.DivisionFromDB(words);

                // разбиваем текст на абзацы
                var originalParagraphs = originalText.Split('\n').ToList(); // оригинал текста

                // сравниваем userRequest с каждым абзацем в оригинале 
                // но передаём словарь dic с уникальными уже разобранными словами
                int[] paragraphRatings = new int[originalParagraphs.Count];

                for (int i = 0; i < originalParagraphs.Count; i++)
                {
                    paragraphRatings[i] = analyzer.GetCompareParagraph(userRequest,
                        originalParagraphs[i], dictionaryWithUniqueWords);

                    lock (obj)
                    {
                        currentParagraph++;
                    }
                }





                // находим номер релевантного абзаца
                // indexMaxRating - индекс
                // globalMaxRating - число рейтинга
                // globalString - релевантный абзац
                var indexMaxRating = 0;
                for (var i = 1; i < paragraphRatings.Count(); i++)
                    if (paragraphRatings[indexMaxRating] < paragraphRatings[i])
                        indexMaxRating = i;

                if (paragraphRatings[indexMaxRating] > globalMaxRating)
                {
                    globalMaxRating = paragraphRatings[indexMaxRating];
                    globalString = originalParagraphs[indexMaxRating];
                }

            }

            if (globalMaxRating == 0)
                globalString = "не найдено";

            return Ok(globalString); // Возвращает текст вызванный fetch
        }
    }
}

