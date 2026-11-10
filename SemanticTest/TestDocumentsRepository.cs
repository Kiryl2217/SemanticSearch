using SemanticClassLibrary.Models;
using SemanticClassLibrary.Repositories;

namespace SemanticTest
{
    public class TestDocumentsRepository
    {
        private readonly DocumentsRepository documentsRepository;

        public TestDocumentsRepository()
        {
            var DB = TestContext.GetApplicationContext();
            documentsRepository = new DocumentsRepository(DB);
        }

        [Fact]
        public void TestCreateDocumentsRepository()
        {
            Documents doc1;

            documentsRepository.Create(doc1 = new Documents
            {
                DateAdded = DateTime.Now,
                Title = "Тест0",
                OriginalText = "Тест0"
            });

            var resultDoc1 = documentsRepository.Read(doc1.Id);

            Assert.NotNull(resultDoc1);
            Assert.Equal("Тест0", resultDoc1.Title);
            Assert.Equal("Тест0", resultDoc1.OriginalText);
        }

        [Fact]
        public void TestReadByIdDocumentsRepository()
        {
            Documents doc2;

            documentsRepository.Create(doc2 = new Documents
            {
                DateAdded = DateTime.Now,
                Title = "Тест1",
                OriginalText = "Тест1"
            });

            var result = documentsRepository.Read(doc2.Id);

            Assert.NotNull(result);
            Assert.Equal("Тест1", result.Title);
        }

        [Fact]
        public void TestReadDocumentsRepository()
        {
            Documents doc3;
            Documents doc4;

            documentsRepository.Create(doc3 = new Documents
            {
                DateAdded = DateTime.Now,
                Title = "Тест2",
                OriginalText = "Тест2"
            });

            documentsRepository.Create(doc4 = new Documents
            {
                DateAdded = DateTime.Now,
                Title = "Тест3",
                OriginalText = "Тест3"
            });

            var result = documentsRepository.Read();

            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateDocumentsRepository()
        {
            Documents doc5;

            documentsRepository.Create(doc5 = new Documents
            {
                DateAdded = DateTime.Now,
                Title = "Тест4",
                OriginalText = "Тест4"
            });

            var resultDoc5 = documentsRepository.Read(doc5.Id);
            resultDoc5.Title = "Тест41";
            resultDoc5.OriginalText = "Тест42";
            documentsRepository.Update(doc5.Id, doc5);
            var updatedDoc5 = documentsRepository.Read(doc5.Id);

            Assert.Equal("Тест41", updatedDoc5.Title);
            Assert.Equal("Тест42", updatedDoc5.OriginalText);
        }

        [Fact]
        public void TestDeleteDocumentsRepository()
        {
            Documents doc6;

            documentsRepository.Create(doc6 = new Documents
            {
                DateAdded = DateTime.Now,
                Title = "Тест5",
                OriginalText = "Тест5"
            });

            documentsRepository.Delete(doc6.Id);
            var deletedDoc6 = documentsRepository.Read(doc6.Id);

            Assert.Null(deletedDoc6);
        }
    }
}
