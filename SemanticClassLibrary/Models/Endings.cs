namespace SemanticClassLibrary.Models
{
    public class Endings : Entity
    {
        public int Id { get; set; }
        public string Ending { get; set; }
        public int GenderId { get; set; }
        public int PluralityId { get; set; }
        public int DeclensionId { get; set; }
        public int PartOfSpeechId { get; set; }
        public int UniqueWordsId { get; set; }
        public UniqueWords UniqueWords { get; set; }
    }
}
