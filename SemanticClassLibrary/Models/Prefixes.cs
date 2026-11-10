namespace SemanticClassLibrary.Models
{
    public class Prefixes : Entity
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public int PartOfSpeechId { get; set; }
        public int UniqueWordsId { get; set; }
        public UniqueWords UniqueWords { get; set; }
    }
}
