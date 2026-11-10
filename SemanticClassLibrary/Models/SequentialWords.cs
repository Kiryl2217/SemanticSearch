namespace SemanticClassLibrary.Models
{
    public class SequentialWords : Entity // Все последовательные слова из текста
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int DocumentsId { get; set; }
        public Documents Documents { get; set; }
    }
}
