using SemanticClassLibrary.Enums;
using System.Text;

namespace SemanticClassLibrary.Structures
{
    public struct Root
    {
        public string root; // корень
        public PartOfSpeech partOfSpeech; // часть речи

        public Root(string root,
                PartOfSpeech partOfSpeech)
        {
            this.root = root;
            this.partOfSpeech = partOfSpeech;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder =
                new StringBuilder();

            return stringBuilder
                 .Append(root + ", ")
                 .Append("часть речи: ")
                 .Append(partOfSpeech)
                 .ToString();
        }
    };
}
