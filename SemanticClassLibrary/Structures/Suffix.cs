using SemanticClassLibrary.Enums;
using System.Text;

namespace SemanticClassLibrary.Structures
{
    public struct Suffix
    {
        public string suffix; // суффикс
        public SuffixType suffixType; // уменьшительно-ласкательный суффикс или нет
        public SuffixWithEnding suffixWithEnding; // суффикс с окончанием или без него
        public PartOfSpeech partOfSpeech; // часть речи

        public Suffix(string suffix, SuffixType suffixType,
             SuffixWithEnding suffixWithEnding, PartOfSpeech partOfSpeech)
        {
            this.suffix = suffix;
            this.suffixType = suffixType;
            this.suffixWithEnding = suffixWithEnding;
            this.partOfSpeech = partOfSpeech;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder =
                new StringBuilder();

            return stringBuilder
                .Append(suffix + ", ")
                .Append("часть речи: ")
                .Append(partOfSpeech)
                .ToString();
        }
    };
}
