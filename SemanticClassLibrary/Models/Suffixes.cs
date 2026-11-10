namespace SemanticClassLibrary.Models
{
    public class Suffixes : Entity
    {
        public int Id { get; set; }
        public string Suffix { get; set; }
        public int SuffixTypeId { get; set; }
        public int SuffixWithEndingId { get; set; }
        public int PartOfSpeechId { get; set; }
        public int UniqueWordsId { get; set; }
        public UniqueWords UniqueWords { get; set; }
    }
}
