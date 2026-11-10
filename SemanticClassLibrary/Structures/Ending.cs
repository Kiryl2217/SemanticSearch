using SemanticClassLibrary.Enums;
using System.Text;

namespace SemanticClassLibrary.Structures
{
    public struct Ending
    {
        public string ending; // окончание
        public Gender gender; // род слова
        public Plurality plural; // число
        public Declension declension; // склонение
        public PartOfSpeech partOfSpeech; // часть речи

        public Ending(string ending, Gender gender,
            Plurality plural, Declension declension,
            PartOfSpeech partOfSpeech)
        {
            this.ending = ending;
            this.gender = gender;
            this.plural = plural;
            this.declension = declension;
            this.partOfSpeech = partOfSpeech;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder =
                new StringBuilder();

            return stringBuilder
                .Append(ending + ", ")
                .Append("часть речи: ")
                .Append(partOfSpeech)
                .ToString();
        }
    };
}
