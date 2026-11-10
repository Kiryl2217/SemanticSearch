using SemanticClassLibrary.Enums;
using System.Text;

namespace SemanticClassLibrary.Structures
{
    public struct Prefix
    {
        public string prefix; // приставка
        public PartOfSpeech partOfSpeech; // часть речи

        public Prefix(string prefix,
            PartOfSpeech partOfSpeech)
        {
            this.prefix = prefix;
            this.partOfSpeech = partOfSpeech;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder =
                new StringBuilder();

            return stringBuilder
                .Append(prefix + ", ")
                .Append("часть речи: ")
                .Append(partOfSpeech)
                .ToString();
        }
    };
}
