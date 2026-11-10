using SemanticClassLibrary.Structures;

namespace SemanticSearchLibrary.Classes
{
    public class VECT
    {
        public string word;
        public List<Prefix> wordPrefixes;
        public List<Root> roots;
        public List<Suffix> wordSuffixes;
        public List<Ending> wordEndings;
    }
}
