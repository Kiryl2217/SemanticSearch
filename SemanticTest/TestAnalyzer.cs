using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Services;
using SemanticClassLibrary.Structures;
using SemanticSearchLibrary.Classes;

namespace SemanticTest
{
    public class TestAnalyzer
    {
        private readonly DocumentsService documentsService;
        private readonly UniqueWordsService uniqueWordsService;
        private readonly PrefixesService prefixesService;
        private readonly SequentialWordsService sequentialWordsService;
        private readonly RootsService rootsService;
        private readonly EndingsService endingsService;
        private readonly SuffixesService suffixesService;

        public TestAnalyzer()
        {

            var DB = TestContext.GetApplicationContext();
            documentsService = new DocumentsService(DB);
            uniqueWordsService = new UniqueWordsService(DB);
            prefixesService = new PrefixesService(DB);
            sequentialWordsService = new SequentialWordsService(DB);
            rootsService = new RootsService(DB);
            endingsService = new EndingsService(DB);
            suffixesService = new SuffixesService(DB);
        }

        [Fact]
        public void TestGetWordWithoutEndingAndBeginAnalyzer()
        {
            var DB = TestContext.GetApplicationContext();
            Analyzer analyzer = new Analyzer(DB);
            string testString1 = "собака";
            string ending = "а";
            string testString2 = "приготовить";
            string prefix = "при";

            var resultTestString1 = analyzer.GetWithoutEnding(testString1, ending);
            var resultTestString2 = analyzer.GetWithoutBegin(testString2, prefix);

            Assert.Equal("собак", resultTestString1);
            Assert.Equal("готовить", resultTestString2);
        }

        [Fact]
        public void TestIsEndingOrBeginOfWordAnalyzer()
        {
            var DB = TestContext.GetApplicationContext();
            Analyzer analyzer = new Analyzer(DB);
            string testString1 = "собака";
            string ending = "а";
            string testString2 = "приготовить";
            string prefix = "при";

            var resultTestString1 = analyzer.IsEndOfWord(testString1, ending);
            var resultTestString2 = analyzer.IsBeginOfWord(testString2, prefix);
            var resultTestString3 = analyzer.IsEndOfWord(testString1, prefix);
            var resultTestString4 = analyzer.IsBeginOfWord(testString2, ending);

            Assert.True(resultTestString1);
            Assert.True(resultTestString2);
            Assert.False(resultTestString3);
            Assert.False(resultTestString4);
        }

        [Fact]
        public void TestGetCompareValueAnalyzer()
        {
            var DB = TestContext.GetApplicationContext();
            Analyzer analyzer = new Analyzer(DB);
            var vector = new VECT()
            {
                word = "собака",
                roots = new List<Root>() { new Root()
                {
                    root = "собак",
                    partOfSpeech = PartOfSpeech.NOUN
                }},
                wordPrefixes = new List<Prefix>(),
                wordSuffixes = new List<Suffix>(),
                wordEndings = new List<Ending>()
            };

            var result = analyzer.GetCompareValue("кошка", vector);

            Assert.False(result >= 40000);
        }

        [Fact]
        public void TestGetCompareParagraphAnalyzer()
        {
            var DB = TestContext.GetApplicationContext();
            Analyzer analyzer = new Analyzer(DB);

            string testUserRequest1 = "собака сидит";
            string testUserRequest2 = "кошка сидит";
            string testParagraph1 = "собака сидит на подоконнике";

            var dic = new Dictionary<string, List<string>>();
            var words = analyzer.GetManyWithDB(testParagraph1);
            var testDictionary = analyzer.Division(words, dic);

            var result1 = analyzer.GetCompareParagraph(testUserRequest1,
                testParagraph1, testDictionary);
            var result2 = analyzer.GetCompareParagraph(testUserRequest2,
                testParagraph1, testDictionary);

            Assert.Equal(402, result1);
            Assert.Equal(1, result2);
            Assert.True(result1 > result2);
        }
    }
}
