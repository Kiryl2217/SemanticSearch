using Microsoft.AspNetCore.Mvc;
using SemanticSearch.Classes;
using SemanticSearchLibrary.Classes;
using RemoteDataTransfer.Classes;
using RemoteDataTransfer.Enums;

namespace SemanticSearch.Controllers
{
    public class SearchController : BaseController
    {
        public static object obj = new object(); 
        public static int paragraphsCount = 0;
        public static int currentParagraph = 0;
        public static int countFinishedThread = 0;

        private readonly IConfiguration config;
        public SearchController(IConfiguration config)
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

        private static void ThreadFun(int nodeNumber, string paragraph, out int rating, /*MyDic dictionaryWithUniqueWords,*/ out string error)
        {
            error = string.Empty;
            if (!NodeWorker.SendCommand(10000 + nodeNumber, SendCommandCase.SEND_PARAGRAPH))
            {
                 error = "Ошибка подключения и(или) пересылки команды на вычислительный узел " + nodeNumber;
            }
            if (!NodeWorker.SendString(10000 + nodeNumber, paragraph))
            {
                error = "Ошибка подключения и(или) пересылки параграфа на вычислительный узел " + nodeNumber;
            }

            rating = NodeWorker.RequestInt(10000 + nodeNumber, SendCommandCase.GET_VALUE);

            lock (obj)
            {
                countFinishedThread++;
            }
        }


        [HttpGet]
        public IActionResult SearchAction(string userRequest)
        {
            var ini = new IniFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "config.ini"));
            string countOfNodes = ini.Read("Setting", "CountOfNodes");
            int N = int.Parse(countOfNodes);


            /*

            if (!SendCommand(10000, SendCommandCase.SEND_QUERY))
            {
                View((object?)"Ошибка подключения");
            }
            if (!SendString(10000, "Это запрос"))
            {
                View((object?)"Ошибка подключения");
            }

            if (!SendCommand(10000, SendCommandCase.SEND_PARAGRAPH))
            {
                View((object?)"Ошибка подключения");
            }
            if (!SendString(10000, "Это параграф"))
            {
                View((object?)"Ошибка подключения");
            }

            var value = RequestInt(10000, SendCommandCase.GET_VALUE);

            return View((object?)value.ToString());
            */


            // Всем узлам рассылаем запрос пользователя.
            for (int i = 1; i <= N; i++)
            {
                // Команда.
                if (!NodeWorker.SendCommand(10000 + i, SendCommandCase.SEND_QUERY))
                {
                    View((object?)"Ошибка подключения и(или) команды.");
                }
                // Данные.
                if (!NodeWorker.SendString(10000 + i, userRequest))
                {
                    View((object?)"Ошибка подключения и(или) данных.");
                }
            }


            Analyzer analyzer = new Analyzer(config);
            var documents = analyzer.GetAllDocuments(); // получим все записи документов через сервис

            // Рассчитываем общее кол-во всех абзацев для поиска 
            lock (obj)
            {
                paragraphsCount = 0;
            }

            var ratingsList = new List<int>();
            var paragraphsList = new List<string>();

            foreach (var document in documents)
            {
                var originalText = document.OriginalText;
                var originalParagraphs = originalText.Split('\n').Where(p => p.Trim().Any()).ToList(); // оригинал текста

                lock (obj)
                {
                    paragraphsCount += originalParagraphs.Count;
                }
            }

            lock (obj)
            {
                //Заменить название!!!!!!!!!!
                paragraphsCount /= N;
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
                //var dictionaryWithUniqueWords = analyzer.DivisionFromDB(words);

                // разбиваем текст на абзацы
                var originalParagraphs = originalText.Split('\n').Where(p => p.Trim().Any()).ToList(); // оригинал текста

                // сравниваем userRequest с каждым абзацем в оригинале 
                // но передаём словарь dic с уникальными уже разобранными словами
                int[] paragraphRatings = new int[originalParagraphs.Count];


                for (int i = 0; i < originalParagraphs.Count; i+=N)
                {

                    lock (obj)
                    {
                        countFinishedThread = 0;
                    }

                    var errors = new string[N];

                    int countReal = 0;
                    for (int node = 1; node <= N; node++)
                    {
                        if (i + node - 1 < originalParagraphs.Count)                        
                        {
                            countReal++;
                            var n = node;
                            // Счётчик цикла нельзя отправлять в потоки, так как педаётся ссылка на память node,
                            // а node измениться при выходе из цикла либо переходе к новой итерации.
                            var thread = new Thread(() => ThreadFun(n, originalParagraphs[i + n - 1], out paragraphRatings[i + n - 1], /*dictionaryWithUniqueWords, */out errors[n - 1]));
                            thread.Start();
                        }
                    }


                    // Ожидание завершения всех потоков (вычислений).
                    var endWait = false;
                    while (!endWait)
                    {
                        Thread.Sleep(1);
                        lock(obj)
                        {
                            // так как при нехватки параграфа вычислительным узлам, countFinishedThread меньше.
                            if (countFinishedThread == countReal) 
                                endWait = true;
                        }
                    }

                    // Анализируем ошибки.
                    if (errors.Any(e => e != null && e.Length > 0))
                    {
                        View((object?)errors.First(e => e != null && e.Length > 0));
                    }
                    


                        /*
                    paragraphRatings[i] = analyzer.GetCompareParagraph(userRequest,
                        originalParagraphs[i], dictionaryWithUniqueWords);
                        */



                    lock (obj)
                    {
                        currentParagraph++;
                    }
                }

                for (int i = 0; i < paragraphRatings.Length; i++)
                {
                    if (paragraphRatings[i] > 0)
                    {
                        ratingsList.Add(paragraphRatings[i]);
                        paragraphsList.Add(originalParagraphs[i]);
                    }
                }





                // находим номер релевантного абзаца
                // indexMaxRating - индекс
                // globalMaxRating - число рейтинга
                // globalString - релевантный абзац
                //var indexMaxRating = 0;
                //var indexMaxRating2 = 0;
                //for (var i = 1; i < paragraphRatings.Count(); i++)
                //    if (paragraphRatings[indexMaxRating] < paragraphRatings[i])
                //        indexMaxRating = i;

                //for (var j = 1; j < paragraphRatings.Count(); j++)
                //    if ((paragraphRatings[indexMaxRating2] < paragraphRatings[j]) &&
                //        paragraphRatings[j] != paragraphRatings[indexMaxRating])
                //        indexMaxRating2 = j;

                //if (paragraphRatings[indexMaxRating] > globalMaxRating)
                //{
                //    globalMaxRating = paragraphRatings[indexMaxRating];
                //    if (indexMaxRating2 != 0)
                //    {
                //        globalString = originalParagraphs[indexMaxRating] +
                //            "\n\n Второй по релевантности абзац: \n\n" +
                //            originalParagraphs[indexMaxRating2];
                //    }
                //    else
                //    {
                //        globalString = originalParagraphs[indexMaxRating];
                //    }
                //}

            }

            int[] resultRating = ratingsList.ToArray();
            string[] resultParagraph = paragraphsList.ToArray();
            int countOfRatings = resultRating.Length;

            for (int i = 0; i < countOfRatings - 1; i++)
            {
                for (int j = i + 1; j < countOfRatings; j++)
                {
                    if (resultRating[i] < resultRating[j])
                    {
                        int tempRating = resultRating[i];
                        resultRating[i] = resultRating[j];
                        resultRating[j] = tempRating;

                        string tempParagraph = resultParagraph[i];
                        resultParagraph[i] = resultParagraph[j];
                        resultParagraph[j] = tempParagraph;
                    }
                    
                }
            }

            if (countOfRatings == 0)
            {
                globalString = "Ничего не найдено!";
            }
            else
            {
                int paragraphsOutput = 3;
                if (countOfRatings < paragraphsOutput)
                {
                    paragraphsOutput = countOfRatings;
                }

                for (int i = 0; i < paragraphsOutput; i++)
                {
                    if (i == 0)
                    {
                        globalString += resultParagraph[i]; 
                    }
                    else
                    {
                        globalString += "\n\n Еще результат:" + resultParagraph[i];
                    }
                }
            }
                
            return Ok(globalString); // Returns text to the fetch call
        }
    }
}
