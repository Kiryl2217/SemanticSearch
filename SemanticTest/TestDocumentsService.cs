using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;
using SemanticClassLibrary.Services;

namespace SemanticTest
{
    public class TestDocumentsService
    {
        private readonly DocumentsService documentsService;
        private readonly DocumentsRepository documentsRepository;

        public TestDocumentsService()
        {
            var DB = TestContext.GetApplicationContext();
            documentsRepository = new DocumentsRepository(DB);
            documentsService = new DocumentsService(DB);
        }

        [Fact]
        public void TestAddDocumentsService()
        {
            Documents doc1 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест6",
                OriginalText = "Тест6"
            };

            documentsService.Add(doc1);
            var resultDoc1 = documentsService.Get(doc1.Id);

            Assert.NotEqual(0, documentsService.Get().Count);
            Assert.Equal("Тест6", resultDoc1.Title);
            Assert.Equal("Тест6", resultDoc1.OriginalText);
        }

        [Fact]
        public void TestGetByIdDocumentsService()
        {
            Documents doc2 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест7",
                OriginalText = "Тест7"
            };

            documentsService.Add(doc2);
            var resultDoc2 = documentsService.Get(doc2.Id);

            Assert.Equal("Тест7", resultDoc2.Title);
            Assert.Equal("Тест7", resultDoc2.OriginalText);
        }

        [Fact]
        public void TestGetAllDocumentsService()
        {
            Documents doc3 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест8",
                OriginalText = "Тест8"
            };

            Documents doc4 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест9",
                OriginalText = "Тест9"
            };

            documentsService.Add(doc3);
            documentsService.Add(doc4);
            var resultDoc34 = documentsService.Get();
            
            Assert.NotNull(resultDoc34);
        }

        [Fact]
        public void TestUpdateDocumentsService()
        {
            Documents doc5 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест10",
                OriginalText = "Тест10"
            };

            documentsService.Add(doc5);
            var resultDoc5 = documentsService.Get(doc5.Id);
            resultDoc5.Title = "Тест11";
            resultDoc5.OriginalText = "Тест11";
            documentsService.Update(doc5.Id, doc5);
            var updatedDoc5 = documentsService.Get(doc5.Id);

            Assert.Equal("Тест11", updatedDoc5.Title);
            Assert.Equal("Тест11", updatedDoc5.OriginalText);
        }

        [Fact]
        public void TestDeleteDocumentsService()
        {
            Documents doc6 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест12",
                OriginalText = "Тест12"
            };

            documentsService.Add(doc6);
            documentsService.Delete(doc6.Id);
            var deletedDoc6 = documentsService.Get(doc6.Id);

            Assert.Null(deletedDoc6);
        }

        [Fact]
        public void TestGetByTextDocumentsService()
        {
            Documents doc6 = new Documents()
            {
                DateAdded = DateTime.Now,
                Title = "Тест13",
                OriginalText = "Тест13"
            };
            documentsService.Add(doc6);
            var resultDoc6 = documentsService.Get(doc6.Id);
            var resultTest = documentsService.GetByText("Тест13");

            Assert.Equal(resultTest?.OriginalText, resultDoc6.OriginalText);
        }
    }
}
