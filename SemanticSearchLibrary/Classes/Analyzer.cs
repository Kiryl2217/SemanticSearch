using Microsoft.Extensions.Configuration;
using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Models;
using SemanticClassLibrary.Services;
using SemanticClassLibrary.Structures;

namespace SemanticSearchLibrary.Classes
{
    public class Analyzer
    {
        private readonly DocumentsService documentsService;
        private readonly UniqueWordsService uniqueWordsService;
        private readonly PrefixesService prefixesService;
        private readonly SequentialWordsService sequentialWordsService;
        private readonly RootsService rootsService;
        private readonly EndingsService endingsService;
        private readonly SuffixesService suffixesService;


        public Analyzer(ApplicationContext DB)
        {
            documentsService = new DocumentsService(DB);
            uniqueWordsService = new UniqueWordsService(DB);
            prefixesService = new PrefixesService(DB);
            sequentialWordsService = new SequentialWordsService(DB);
            rootsService = new RootsService(DB);
            endingsService = new EndingsService(DB);
            suffixesService = new SuffixesService(DB);
        }

        public Analyzer(IConfiguration config)
        {
            var DB = new ApplicationContext(config.GetConnectionString("ConnectionString"));
            documentsService = new DocumentsService(DB);
            uniqueWordsService = new UniqueWordsService(DB);
            prefixesService = new PrefixesService(DB);
            sequentialWordsService = new SequentialWordsService(DB);
            rootsService = new RootsService(DB);
            endingsService = new EndingsService(DB);
            suffixesService = new SuffixesService(DB);
        }


        // Проверяем концовку слова
        public bool IsEndOfWord(string word, string ending)
            => word.EndsWith(ending) && (word.Length > ending.Length);

        // получаем начало слова
        public bool IsBeginOfWord(string word, string begin)
            => word.StartsWith(begin) && (word.Length > begin.Length); // приставка должна быть меньше, чем само слово



        // Возвращаем часть слова без указанной концовки
        public string GetWithoutEnding(string word, string ending)
            => word.Substring(0, word.Length - ending.Length);

        // Возвращаем часть слова без указанного начала
        public string GetWithoutBegin(string word, string begin)
            => word.Substring(begin.Length, word.Length - begin.Length);

        // Ищем все возможные окончания слова
        public List<Ending> GetEndings(string word)
            => Constants.endings.FindAll(e => IsEndOfWord(word, e.ending))
                .ToList();

        // Ищем все возможные приставки слова
        public List<Prefix> GetPrefixes(string word)
            => Constants.prefixes.FindAll(p => IsBeginOfWord(word, p.prefix))
                .ToList();

        // Ищем все возможные суффиксы слов.
        // wordEndings - окончания заданного слова word.
        public List<Suffix> GetSuffixes(string word, List<Ending> wordEndings)
        {
            var result = new List<Suffix>();


            // Составляем комбинации суффиксов с окончаниями
            var suffixVariants = new List<Suffix>();
            foreach (var suffix1 in Constants.suffixes) // Перебираем все суффиксы
            {
                if (suffix1.suffixWithEnding == SuffixWithEnding.WITH_ENDING)
                {
                    suffixVariants.Add(suffix1);
                    foreach (var suffix2 in Constants.suffixes) // Перебираем все суффиксы
                    {
                        if ((suffix2.suffixWithEnding == SuffixWithEnding.WITH_ENDING) && (suffix1.partOfSpeech == suffix2.partOfSpeech))
                        {
                            Suffix doubleSuffix;
                            suffixVariants.Add(doubleSuffix = new Suffix(
                                suffix1.suffix + suffix2.suffix,
                                SuffixType.DIMINUTIVE,
                                SuffixWithEnding.WITH_ENDING,
                                suffix1.partOfSpeech));

                            foreach (var suffix3 in Constants.suffixes) // Перебираем все суффиксы
                            {
                                if ((suffix3.suffixWithEnding == SuffixWithEnding.WITH_ENDING) && (doubleSuffix.partOfSpeech == suffix3.partOfSpeech))
                                {
                                    suffixVariants.Add(new Suffix(
                                        doubleSuffix.suffix + suffix3.suffix,
                                        SuffixType.DIMINUTIVE,
                                        SuffixWithEnding.WITH_ENDING,
                                        doubleSuffix.partOfSpeech));
                                }
                            }
                        }
                    }
                }
            }


            // Для суффиксов с возможными окончаниями
            foreach (var suffix in suffixVariants) // Перебираем все суффиксы.
            {
                foreach (var ending in wordEndings)
                {
                    if ((suffix.partOfSpeech == ending.partOfSpeech) && (word.Length > ending.ending.Length))
                    {
                        var wordWithoutEnding = GetWithoutEnding(word, ending.ending);

                        if (IsEndOfWord(wordWithoutEnding, suffix.suffix))
                        {
                            result.Add(suffix);
                        }
                    }
                }
            }


            // Для суффиксов без дальнейших окончаний
            foreach (var suffix in Constants.suffixes) // Перебираем все суффиксы
            {
                if (suffix.suffixWithEnding == SuffixWithEnding.WITHOUT_ENDING)
                {
                    if (IsEndOfWord(word, suffix.suffix))
                    {
                        result.Add(suffix);
                    }
                }
            }
            return result;
        }



        // Добавляем корни, удаляя суффиксы, полагая что они все являются концовкой части переданного слова.
        private void AddRoots(string partOfWord, PartOfSpeech partOfSpeech,
            List<Suffix> wordSuffixes, List<Root> roots,
            SuffixWithEnding suffixWithEnding)
        {
            foreach (var suffix in wordSuffixes) // по всем суффиксам
            {
                if ((suffix.partOfSpeech == partOfSpeech) &&
                            (suffix.suffixWithEnding == suffixWithEnding) &&
                            (partOfWord.Length > suffix.suffix.Length))
                {
                    var wordWithoutSuffix = GetWithoutEnding(partOfWord, suffix.suffix);
                    roots.Add(new Root(wordWithoutSuffix, suffix.partOfSpeech));
                }
            }
        }



        private void AddRoots(string partOfWord,
            PartOfSpeech partOfSpeech, List<Ending> wordEndings,
            List<Suffix> wordSuffixes, List<Root> roots)
        {
            foreach (var ending in wordEndings) // по всем окончаниям
            {
                if (partOfWord.Length > ending.ending.Length && ending.partOfSpeech == partOfSpeech)
                {
                    var wordWithoutEnding = GetWithoutEnding(partOfWord, ending.ending);

                    if (wordSuffixes.Any())
                        AddRoots(wordWithoutEnding, ending.partOfSpeech, wordSuffixes, roots, SuffixWithEnding.WITH_ENDING);

                    roots.Add(new Root(wordWithoutEnding, ending.partOfSpeech));
                }
            }
        }


        public List<Root> GetRoots(string word,
            List<Prefix> wordPrefixes, List<Suffix> wordSuffixes,
            List<Ending> wordEndings)
        {
            List<Root> result = new List<Root>();

            // Если есть приставки
            if (wordPrefixes.Any())
            {
                foreach (var prefix in wordPrefixes) // по всем приставкам
                {
                    if (word.Length > prefix.prefix.Length)
                    {
                        var wordWithoutPrefix = GetWithoutBegin(word, prefix.prefix);
                        if (wordEndings.Any())
                        {
                            AddRoots(wordWithoutPrefix, prefix.partOfSpeech, wordEndings, wordSuffixes, result);
                        }
                        else // Нет окончаний
                        {
                            if (wordSuffixes.Any())
                                AddRoots(wordWithoutPrefix, prefix.partOfSpeech, wordSuffixes, result, SuffixWithEnding.WITHOUT_ENDING);


                            // Нет суффикса и окончания, либо он ошибочный
                            result.Add(new Root(wordWithoutPrefix, prefix.partOfSpeech));
                        }
                    }
                }
            }

            // Если нет приставок 
            // Или в любом случае добавляем корни, полагая, что приставки найдены ошибочно.
            // То есть приставки не рассматриваем

            if (wordEndings.Any()) // Есть окончания
            {
                AddRoots(word, PartOfSpeech.NOUN, wordEndings, wordSuffixes, result);
                AddRoots(word, PartOfSpeech.VERB, wordEndings, wordSuffixes, result);
                AddRoots(word, PartOfSpeech.OTHER, wordEndings, wordSuffixes, result);
            }

            if (wordSuffixes.Any()) // Есть суффиксы
            {
                AddRoots(word, PartOfSpeech.NOUN, wordSuffixes, result, SuffixWithEnding.WITHOUT_ENDING);
                AddRoots(word, PartOfSpeech.VERB, wordSuffixes, result, SuffixWithEnding.WITHOUT_ENDING);
                AddRoots(word, PartOfSpeech.OTHER, wordSuffixes, result, SuffixWithEnding.WITHOUT_ENDING);
            }

            // И как единый корень
            result.Add(new Root(word, PartOfSpeech.NOUN));
            result.Add(new Root(word, PartOfSpeech.VERB));
            result.Add(new Root(word, PartOfSpeech.OTHER));

            // Сразу удаляем дубликаты
            return result.Distinct().ToList();
        }















        public void DivisionSyomaEngine(string word,
            out List<Prefix> wordPrefixes,
            out List<Root> wordRoots,
            out List<Suffix> wordSuffixes,
            out List<Ending> wordEndings)
        {
            word = word.ToLower();
            word = word.Replace("ё", "е");

            wordEndings = GetEndings(word);
            wordSuffixes = GetSuffixes(word, wordEndings);
            wordPrefixes = GetPrefixes(word);
            wordRoots = GetRoots(word, wordPrefixes, wordSuffixes, wordEndings);
        }




        // Разбор оригинального текста файлы базы с занесением морфем в БД.       
        // Выполняется при загрузке файла.
        // dic - словарь корней слов и их корней-синонимов слов.

        public MyDic Division(string[] words, Dictionary<string, List<string>> dic)
        {

            var result = new MyDic();

            foreach (var word in words)
            {

                result.Add(word, DivisionCore(word, dic));
            }

            return result;
        }



        // Выполняется при разборе оригинального текста из файла, для слов которого морфемы уже занесены в БД.
        public MyDic DivisionFromDB(string[] words)
        {
            var result = new MyDic();

            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                {
                    result.Add(word, DivisionFromDB(word));
                }
            }

            return result;
        }

        public int GetCompareValue(string word1, VECT v2) // v2 - по второму слову.
        {
            DivisionSyomaEngine(word1, out List<Prefix> wordPrefixes1, out List<Root> roots1, out List<Suffix> wordSuffixes1,
            out List<Ending> wordEndings1);

            var values = new int[4] { 0, 0, 0, 0 };

            foreach (var root1 in roots1)
            {
                foreach (var root2 in v2.roots)
                {
                    if (root1.root == root2.root /*&& root1.partOfSpeech == root2.partOfSpeech*/)
                    {
                        values[0] = 1;
                    }
                }
            }

            foreach (var prefix1 in wordPrefixes1)
            {
                foreach (var prefix2 in v2.wordPrefixes)
                {
                    if ((prefix1.prefix == prefix2.prefix) &&
                        prefix1.partOfSpeech == prefix2.partOfSpeech)
                    {
                        values[1] = 1;
                    }
                }
            }


            foreach (var suffix1 in wordSuffixes1)
            {
                foreach (var suffix2 in v2.wordSuffixes)
                {
                    if ((suffix1.suffix == suffix2.suffix) &&
                        suffix1.partOfSpeech == suffix2.partOfSpeech)
                    {
                        values[2] = 1;
                    }
                }
            }

            foreach (var ending1 in wordEndings1)
            {
                foreach (var ending2 in v2.wordEndings)
                {
                    if ((ending1.ending == ending2.ending) &&
                        (ending1.partOfSpeech == ending2.partOfSpeech) &&
                        ending1.plural == ending2.plural)
                    {
                        values[3] = 1;
                    }
                }
            }


            var result = 0;
            for (int i = 0; i < 4; i++)
            {
                result += values[i] * (4 - i) * (int)Math.Pow(10, 4 - i);
            }

            return result;
        }


        // Получаем разбиение из базы или НОВОЕ и заносим в базу
        public VECT DivisionCore(string word, Dictionary<string, List<string>> dic)
        {
            word = word.ToLower().Replace("ё", "е");

            VECT v = new VECT();

            // проверяем, есть ли слово в таблице "Слова не дубликаты"
            var uniqueWords = uniqueWordsService.GetByWord(word);

            if (uniqueWords != null)
            {
                v.wordPrefixes = prefixesService.GetPrefixes(uniqueWords.Id);
                v.roots = rootsService.GetRoots(uniqueWords.Id);
                v.wordEndings = endingsService.GetEndings(uniqueWords.Id);
                v.wordSuffixes = suffixesService.GetSuffixes(uniqueWords.Id);
            }
            else
            {

                DivisionSyomaEngine(word, out v.wordPrefixes, out v.roots, out v.wordSuffixes, out v.wordEndings);

                if (dic != null) // проверяем массив на существование
                {

                    var newRoots = new List<string>();
                    foreach (var root in v.roots)
                    {
                        var wordRoot = root.root;
                        if (dic.ContainsKey(wordRoot))
                        {
                            newRoots.AddRange(dic[wordRoot]);
                        }
                    }
                    foreach (var newRoot in newRoots)
                    {
                        v.roots.Add(new Root
                        {
                            root = newRoot,
                            partOfSpeech = PartOfSpeech.NONE /* Без разницы часть речи. */
                        });
                    }


                }

                // записываем слово
                uniqueWordsService.Add(new UniqueWords()
                {
                    OriginalWord = word,
                });

                // получаем его ID
                uniqueWords = uniqueWordsService.GetByWord(word);

                // Здесь уже никак не может быть null, иначе уже ошибка БД
                if (uniqueWords == null)
                    throw new Exception("Ошибка БД");

                // записываем морфемы в БД
                prefixesService.WritePrefixes(uniqueWords.Id, v.wordPrefixes);
                rootsService.WriteRoots(uniqueWords.Id, v.roots);
                endingsService.WriteEndings(uniqueWords.Id, v.wordEndings);
                suffixesService.WriteSuffixes(uniqueWords.Id, v.wordSuffixes);

            }

            return v;
        }

        // Получаем разбиение из базы или НОВОЕ и заносим в базу
        public VECT DivisionFromDB(string word)
        {
            word = word.ToLower().Replace("ё", "е");

            VECT v = new VECT();

            var uniqueWords = uniqueWordsService.GetByWord(word);
            v.wordPrefixes = prefixesService.GetPrefixes(uniqueWords.Id);
            v.roots = rootsService.GetRoots(uniqueWords.Id);
            v.wordEndings = endingsService.GetEndings(uniqueWords.Id);
            v.wordSuffixes = suffixesService.GetSuffixes(uniqueWords.Id);
            return v;
        }

        public List<Documents> GetAllDocuments()
        {
            return documentsService.Get();
        }


        // Получаем форматированные слова без дублей по тексту
        public string[] GetManyWithDB(string originalText)
        {
            // Ищем уже занесённый документ с текстом в базе данных.            
            var document = documentsService.GetByText(originalText);

            // Текст в базе данных
            if (document != null)
            {

                // получить выборку слов из базы данных, связанных c document по documents.Id 
                return sequentialWordsService
                    .GetUniqueWordsByDocumentsId(document.Id);
            }

            else
            {
                var textInLower = originalText.ToLower().Replace("ё", "е");
                var words1 = textInLower.Split(Constants.EXCEPTION_ELEMENTS); // Слов с пробелами быть не может!
                var all = words1
                        .Where(w => w.Length > 2)
                        .Where(w => !Constants.NEEDLESS_WORDS.Contains(w))
                        .Distinct();

                // записываем ОРИГИНАЛЬНЫЙ текст в документ
                documentsService.Add(new Documents()
                {
                    OriginalText = originalText,
                    DateAdded = DateTime.Now,
                    Title = "Документ на " + DateTime.Now.ToString("dd.MM.yyyy")
                });

                // получаем ID этого документа
                document = documentsService.GetByText(originalText);

                if (document == null)
                    throw new("Ошибка БД");

                //записываем все слова в БД
                sequentialWordsService.WriteWordsDB(document.Id, all);

                return all.ToArray();
            }
        }



        // text - оригинал.
        public string[] GetManyWithoutDB(string text)
        {
            text = text.ToLower().Replace("ё", "е");

            var words1 = text.Split(Constants.EXCEPTION_ELEMENTS);

            // Уникальные не делаем, сохраняем последовательность и дубли
            var all = words1
                    .Select(w => w.Trim())
                    .Where(w => w.Length > 2)
                    .Where(w => !Constants.NEEDLESS_WORDS.Contains(w));

            return all.ToArray();
        }


        // Главный начальный метод получения числовой оценки критерия абзаца text2
        // text1 - оригинал запроса
        // text2 - оригинал абзаца из текста - возможного ответа.
        public int GetCompareParagraph(string textQuery, string textDocument, MyDic dic)
        {
            var wordsQuery = GetManyWithoutDB(textQuery); // Дубли не убираем
            var wordsDocument = GetManyWithoutDB(textDocument);

            // Список найденных (встречающихся) слов.
            var foundWords = new List<string>();

            // 1 * x - кол-во слов из запроса в тексте
            var vector1 = 0;


            // Таблица взаимооценок слов.
            var table = new Dictionary<string, Dictionary<string, int>>();


            foreach (var wordQuery in wordsQuery)
            {
                int r_max = 0;
                foreach (var wordDocument in wordsDocument)
                {
                    int r_cur = GetCompareValue(wordQuery, dic[wordDocument]);


                    // ====================================================
                    // Оценки слов в запросе по отношению к словам в тексте хранив словаре.
                    // ====================================================
                    if (!table.ContainsKey(wordQuery))
                    {
                        table.Add(wordQuery, new Dictionary<string, int>());
                    }
                    var tableWordDic = table[wordQuery];
                    if (!tableWordDic.ContainsKey(wordDocument))
                    {
                        tableWordDic.Add(wordDocument, r_cur);
                    }
                    // ====================================================
                    // ====================================================
                    // ====================================================



                    r_max = r_cur > r_max ? r_cur : r_max;
                }
                if (r_max >= 40000)
                {
                    vector1++;
                    // Добавляем в список найденных слов.
                    foundWords.Add(wordQuery);
                }
            }

            // Подменяем запрос на найденные слова для
            // дальнейшего анализа последовательностей!!!!
            wordsQuery = foundWords.ToArray();



            var vector2 = 0;
            if (vector1 > 0) // Если слова есть (хотя бы одно)
            {
                // Количество слов из запроса
                for (var sequenceLength = 2; sequenceLength <= wordsQuery.Count(); sequenceLength++)
                {
                    var sequenceStart = 0;

                    // такую же последовательность слов в тексте
                    for (var indexWords2 = 0; indexWords2 <= wordsDocument.Length - sequenceLength; indexWords2++)
                    {
                        // проверяем совпадение последовательности
                        var indexSequence = 0;
                        while (indexSequence < sequenceLength)
                        {

                            var wordQ = wordsQuery[sequenceStart + indexSequence];
                            var wordD = wordsDocument[indexWords2 + indexSequence];

                            // Наверняка есть взаимооценка двух слов!
                            if (table[wordQ][wordD] < 40000) break;

                            indexSequence++;
                        }
                        if (indexSequence == sequenceLength)
                        {
                            vector2 += (int)Math.Pow(2, indexSequence);
                        }
                    }
                }
            }

            vector2 *= 100; // нахождение последовательностей делаем более релевантнее

            return vector1 + vector2;
            /*
            1 * x - кол-во слов из запроса в тексте           
            100 * x - кол-во последовательностей слов запроса в тексте от 2 до кол-ва слов
            */
        }
    }
}
