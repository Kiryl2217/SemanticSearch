namespace SemanticClassLibrary.Models
{
    public class Documents : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public string OriginalText { get; set; }
    }
}
