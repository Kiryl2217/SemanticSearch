namespace SemanticClassLibrary.Models
{
    public class Roots : Entity
    {
        public int Id { get; set; }
        public string Root {  get; set; }
        public int PartOfSpeechId { get; set; }
        public int UniqueWordsId { get; set; }
        public UniqueWords UniqueWords { get; set; }
    }
}
