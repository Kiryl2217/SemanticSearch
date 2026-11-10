using Microsoft.AspNetCore.Mvc;
using SemanticClassLibrary.Structures;
using SemanticSearchLibrary.Classes;

namespace SemanticSearch.Controllers
{
    public class WordMorphemesController : BaseController
    {
        private readonly IConfiguration config;
        public WordMorphemesController(IConfiguration config)
        {
            this.config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string inputWord)
        {
            if (string.IsNullOrWhiteSpace(inputWord))
            {
                ViewBag.Error = "Ошибка! Вы не ввели слово!";
                return View();
            } 

            string inputWordToLower = inputWord.ToLower();

            bool isWord = inputWordToLower.All(l => 
                Constants.RUS_LETTERS.Contains(l));

            if (!isWord)
            {
                ViewBag.Error = "Ошибка! Ваш запрос содержит" +
                    " посторонние символы или введено" +
                    " больше одного слова!";
                return View();
            }

            Analyzer analyzer = new Analyzer(config);

            analyzer.DivisionSyomaEngine(inputWord, out List<Prefix> inputWordPrefixes, 
                out List<Root> inputWordRoots, out List<Suffix> inputWordSuffixes, 
                out List<Ending> inputWordEndings);

            var model = new VECT
            {
                word = inputWord,
                wordPrefixes = inputWordPrefixes,
                wordEndings = inputWordEndings,
                wordSuffixes = inputWordSuffixes,
                roots = inputWordRoots,
            };

            return View(model);
        }
    }
}
