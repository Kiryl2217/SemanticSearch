using Microsoft.AspNetCore.Mvc;
using SemanticSearch.Models;
using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SemanticClassLibrary.Services;
using SemanticClassLibrary.Models;
using SemanticSearchLibrary.Classes;
using SemanticClassLibrary.Structures;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text;


namespace SemanticSearch.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly DocumentsService documentsService;

        

        public HomeController(IConfiguration config, IWebHostEnvironment appEnvironment)
        {
            this.config = config;
            this.appEnvironment = appEnvironment;
            var DB = new ApplicationContext(config.GetConnectionString("ConnectionString"));
            documentsService = new DocumentsService(DB);
        }


        public IActionResult Index()
        {

            ViewBag.NewAdd = true;
            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormFile inputFile)
        {
            ViewBag.NewAdd = true;

            // путь к папке files


            if (inputFile == null || inputFile.Length == 0)
            {
                ViewBag.ErrorFile = "Ошибка! Вы не выбрали файл или он пустой!";
                return View();
            }

            //string path = "/files/" + inputFile.FileName;


            if (!(inputFile.FileName.EndsWith(".txt")
                || inputFile.FileName.EndsWith(".docx")
                || inputFile.FileName.EndsWith(".pdf")))
            {
                ViewBag.ErrorFile = "Ошибка! Вы выбрали не .txt (.docx, .pdf) файл!";
                return View();
            }

            // Чтение текста документа.
            string documentText = "";







            if (inputFile.FileName.EndsWith(".txt"))
            {
                using (var reader = new StreamReader(inputFile.OpenReadStream()))
                {
                    documentText = reader.ReadToEnd();
                }
            } 
            else if (inputFile.FileName.EndsWith(".docx"))
            {
                //var WordApp = new Application();
                //Document doc = WordApp.Documents.Open(inputFile.FileName);
                using (var doc = WordprocessingDocument.Open(inputFile.OpenReadStream(), false)) 
                {

                    var paragraphs = doc.MainDocumentPart.Document.Body.Descendants<Paragraph>();

                    documentText = string.Empty;
                    paragraphs.ToList().ForEach(paragraph => documentText += paragraph.InnerText.ToString() + "\n");
                }
            }
            else if (inputFile.FileName.EndsWith(".pdf"))
            {
                // Чтение PDF с помощью iText 7
                var pdfParagraphs = new List<string>();

                using (var stream = inputFile.OpenReadStream())
                using (var pdfReader = new PdfReader(stream))
                using (var pdfDoc = new PdfDocument(pdfReader))
                {
                    int pageCount = pdfDoc.GetNumberOfPages();

                    for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                    {
                        var page = pdfDoc.GetPage(pageNum);

                        // LocationTextExtractionStrategy сохраняет взаимное пространственное положение текста
                        var strategy = new LocationTextExtractionStrategy();
                        string rawPageText = PdfTextExtractor.GetTextFromPage(page, strategy);

                        if (!string.IsNullOrWhiteSpace(rawPageText))
                        {
                            // Преобразуем строчный текст страницы в полноценные абзацы
                            var extractedParagraphs = NormalizePdfTextToParagraphs(rawPageText);
                            pdfParagraphs.AddRange(extractedParagraphs);
                        }
                    }
                }

                // Объединяем абзацы через \n, чтобы дальше сервис поиска работал идентично с docx
                documentText = string.Join("\n", pdfParagraphs);
            }
            //else
            //{
            //    using (var reader = inputFile.OpenReadStream())
            //    {
            //        using (var pdfDocument = UglyToad.PdfPig.PdfDocument.Open(reader))
            //        {
            //            var paragraphs = new List<string>();

            //            foreach (var page in pdfDocument.GetPages())
            //            {
            //                var words = page.GetWords();
            //                var blocks = UglyToad.PdfPig.DocumentLayoutAnalysis
            //                    .PageSegmenter.DocstrumBoundingBoxes.Instance.GetBlocks(words);

            //                foreach (var block in blocks)
            //                {
            //                    paragraphs.Add(block.Text.Trim());
            //                }
            //            }

            //            documentText = string.Join("\n", paragraphs);
            //        }
            //    }
            //}





            if (string.IsNullOrWhiteSpace(documentText))
            {
                ViewBag.ErrorDoc = "Ошибка! В файле нет текста!";
                return View();
            }
           
            var documentFromDB = documentsService.GetByText(documentText);

            if (documentFromDB != null)
            {
                ViewBag.ErrorDoc = "Ошибка! Такой текст уже есть в базе данных!";
            }
            else
            {
                // ========================================================
                // Работаем с синонимами
                // ========================================================
                var textFile = Constants.SYNONYMS.ToLower().Replace("ё", "е");
                
                // Разбор строк с синонимами.
                var stringsFromFile = textFile.Split("\n");
                
                stringsFromFile = stringsFromFile.Where(t => t.Contains("см.")).ToArray(); // Все с см.
                var arrWordAndSyn = stringsFromFile.Select(s =>
                    s.Split("см.").Select(s => s.Trim()).ToArray()) // Два элемнта массива: слово(строка) и набор синонимом (строка)
                    .ToArray();

                // Разбор текста + заносим в базу данных разбор слов.
                Analyzer analyzer = new Analyzer(config);

                var dicRootSyn = new Dictionary<string, List<string>>();
                foreach (var wordAndSyn in arrWordAndSyn)
                {
                    var word000 = wordAndSyn[0]; // слово
                    analyzer.DivisionSyomaEngine(word000, out _, out List<Root> roots, out _, out _);


                    var syns = wordAndSyn[1]
                        .Split(",")
                        .Select(s => s.Trim())
                        .Select(syn =>
                        {
                            analyzer.DivisionSyomaEngine(syn, out _, out List<Root> synRoots, out _, out _);
                            return synRoots;
                        })
                        .SelectMany(s => s)
                        .Select(s=>s.root)
                        .Distinct()
                        .ToList();

                    // roots может содержать одинаковые корни различных частей речи.
                    roots.Select(s => s.root).Distinct()
                        .Where(s => !dicRootSyn.ContainsKey(s))
                        .ToList()
                        .ForEach(root => dicRootSyn.Add(root, syns));
                }
               








                var words = analyzer.GetManyWithDB(documentText);
                analyzer.Division(words, dicRootSyn); // Здесь результат-словарь игнорируем. Добавляем синонимы

                ViewBag.message = "Успешно добавлено";
                ViewBag.linkToHome = "Добавить еще...";
                ViewBag.NewAdd = false;
            }
            
            return View();
        }


        // Восстанавливает смысловые абзацы из построчного потока текста страницы PDF.
        private static List<string> NormalizePdfTextToParagraphs(string rawText)
        {
            var paragraphs = new List<string>();
            var currentParagraph = new StringBuilder();

            var lines = rawText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line))
                {
                    // Пустая строка — четкий признак границы абзаца
                    if (currentParagraph.Length > 0)
                    {
                        paragraphs.Add(currentParagraph.ToString().Trim());
                        currentParagraph.Clear();
                    }
                    continue;
                }

                // Если есть перенос слова на следующую строку по дефису (например: "семантиче-")
                if (line.EndsWith("-") && line.Length > 1)
                {
                    currentParagraph.Append(line.Substring(0, line.Length - 1));
                    continue;
                }

                if (currentParagraph.Length > 0)
                {
                    // Если предыдущая строка заканчивалась знаками конца предложения,
                    // а текущая начинается с заглавной буквы — вероятно, это новый абзац
                    char lastChar = currentParagraph[currentParagraph.Length - 1];
                    bool isEndOfSentence = lastChar == '.' || lastChar == '!' || lastChar == '?';

                    // Проверяем, начинается ли текущая строка с заглавной или цифры пункта
                    if (isEndOfSentence && (char.IsUpper(line[0]) || char.IsDigit(line[0])))
                    {
                        // Фиксируем законченный абзац
                        paragraphs.Add(currentParagraph.ToString().Trim());
                        currentParagraph.Clear();
                        currentParagraph.Append(line);
                        continue;
                    }

                    // Иначе — это продолжение одной строки абзаца (склеиваем через пробел)
                    currentParagraph.Append(" ").Append(line);
                }
                else
                {
                    currentParagraph.Append(line);
                }
            }

            if (currentParagraph.Length > 0)
            {
                paragraphs.Add(currentParagraph.ToString().Trim());
            }

            return paragraphs;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
