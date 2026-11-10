using SemanticClassLibrary.Enums;
using SemanticClassLibrary.Structures;

namespace SemanticSearchLibrary.Classes
{
    public static class Constants
    {
        // Синонимы.
        public static string SYNONYMS =
            "узел см. стык, сопряжение, соединение, примыкание, крепление\n" +
            "стык см. узел, сопряжение, соединение, примыкание\n" +
            "соединение см. узел, сопряжение, стык, крепление\n" +
            "сопряжение см. узел, стык, соединение, примыкание\n" +
            "прочность см. надежность, сопротивление, устойчивость, долговечность\n" +
            "балка см. ригель, прогон, перемычка, перекрытие\n" +
            "ригель см. балка, прогон, перемычка\n" +
            "колонна см. стойка, опора, пилон\n" +
            "стойка см. колонна, опора, пилон\n" +
            "опора см. стойка, колонна, фундамент\n" +
            "сварка см. шов, наплавка, приварка\n" +
            "шов см. сварка, соединение, стык\n" +
            "болт см. метиз, шпилька, анкер, крепеж\n" +
            "анкер см. болт, закладная, фиксатор, шпилька\n" +
            "фланец см. фасонка, накладка, пластина\n" +
            "фасонка см. фланец, накладка, косынка, ребро, пластина\n" +
            "косынка см. фасонка, ребро, накладка\n" +
            "накладка см. фасонка, фланец, косынка\n" +
            "срез см. сдвиг, скалывание\n" +
            "сдвиг см. срез, скалывание\n" +
            "растяжение см. отрыв, разрыв\n" +
            "сжатие см. смятие, давление\n" +
            "смятие см. сжатие, давление\n" +
            "деформация см. прогиб, перемещение, осадка, податливость\n" +
            "прогиб см. деформация, перемещение\n" +
            "напряжение см. усилие, нагрузка, воздействие\n" +
            "нагрузка см. усилие, напряжение, воздействие\n" +
            "момент см. изгиб, кручение\n" +
            "шарнир см. шарнирный, податливый\n" +
            "жесткий см. рамный, неразрезной, фланцевый\n";

        // слова, которые не учитываются при поиске релевантного ответа
        public static string[] NEEDLESS_WORDS =
        {
            "мне", "тебе", "ему", "нам", "вам", "ними", "мной", "мною", "тобой", "тобою",

            "мой", "твой", "свой", "наш", "ваш", "его", "ее", "их", "себя", "себе", "собой", "собою",

            "это", "эта", "эти", "этот", "этого", "этой", "этих", "этому", "этим", "этом",

            "тот", "та", "то", "те", "того", "той", "тех", "тому", "тем", "том",

            "такой", "такая", "такое", "такие", "такого", "такой", "таких", "такому", "таким", "таком",

            "всякий", "каждый", "сам", "самый", "любой", "иной", "другой", "весь", "вся", "всё", "все",

            "кто", "что", "кого", "чего", "кому", "чему", "кем", "чем", "ком",

            "какой", "какая", "какое", "какие", "какого", "каких", "какому", "каким", "каком",

            "чей", "чья", "чье", "чьи", "чьего", "чьей", "чьих", "чьему", "чьим", "чьем",

            "который", "которая", "которое", "которые", "которого", "которых", "которому", "которым", "котором",
            "сколько", "столько", "насколько",

            "кто-то", "что-то", "какой-то", "где-то", "как-то", "когда-то", "почему-то",

            "кто-нибудь", "что-нибудь", "какой-нибудь", "где-нибудь", "как-нибудь", "когда-нибудь",

            "кто-либо", "что-либо", "какой-либо", "где-либо", "как-либо", "когда-либо",

            "некто", "нечто", "некоторый", "некий", "кое-кто", "кое-что", "кое-какой",

            "никто", "ничто", "никого", "ничего", "никому", "ничему", "никем", "ничем",

            "никакой", "никакая", "никакое", "никакие", "никакого", "никаких", "никакому", "никаким", "никаком",

            "ничей", "ничья", "ничье", "ничьи", "нигде", "никогда", "никуда", "никак",

            "как", "так", "чтобы", "чтоб", "если", "или", "либо", "зато", "однако", "хотя",

            "потому", "поэтому", "причем", "притом", "тоже", "также", "будто", "словно", "точно", "пусть",

            "там", "тут", "здесь", "везде", "всюду", "туда", "сюда", "оттуда", "отсюда",

            "всегда", "тогда", "потом", "сейчас", "теперь", "уже", "еще", "пока", "опять",

            "очень", "весьма", "совсем", "почти", "чуть", "едва", "слишком", "много", "мало",

            "ведь", "даже", "разве", "неужели", "только", "лишь", "просто", "прямо",

            "конечно", "наверное", "вероятно", "возможно", "видимо", "кажется", "значит",

            "вообще", "впрочем", "например", "скажем", "допустим", "пожалуй", "стало",

            "быть", "был", "была", "было", "были", "буду", "будешь", "будет", "будем", "будете", "будут",
            "есть", "суть", "бы", "б",

            "над", "под", "перед", "при", "про", "без", "для", "через", "сквозь",

            "между", "ради", "после", "кроме", "вместо", "около", "возле", "вдоль", "вне"
        };

        // буквы для проверки слова в меню разбор слова
        public static char[] RUS_LETTERS =
        {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з',
            'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
            'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
            'ъ', 'ы', 'ь', 'э', 'ю', 'я'
        };


        // Разделители, которые могут встретиться в тексте
        public static char[] EXCEPTION_ELEMENTS = new char[]
        {
            ' ', ',', '!', '.', '?', ':', ';', ')',
            '(', '[', ']', '-', '·', '>', '<', '«', '»',
            '\"', '"', '-', '/', '{', '}', '\\', '\n', '\r', '\t'
        };

        // Приставки
        public static List<Prefix> prefixes = new List<Prefix>()
        {
            // Базовые приставки
            new Prefix("в", PartOfSpeech.NOUN), new Prefix("в", PartOfSpeech.VERB), new Prefix("в", PartOfSpeech.OTHER),
            new Prefix("во", PartOfSpeech.NOUN), new Prefix("во", PartOfSpeech.VERB), new Prefix("во", PartOfSpeech.OTHER),
            new Prefix("вне", PartOfSpeech.NOUN), new Prefix("вне", PartOfSpeech.VERB), new Prefix("вне", PartOfSpeech.OTHER),
            new Prefix("внутри", PartOfSpeech.NOUN), new Prefix("внутри", PartOfSpeech.VERB), new Prefix("внутри", PartOfSpeech.OTHER),
            new Prefix("вы", PartOfSpeech.NOUN), new Prefix("вы", PartOfSpeech.VERB), new Prefix("вы", PartOfSpeech.OTHER),
            new Prefix("до", PartOfSpeech.NOUN), new Prefix("до", PartOfSpeech.VERB), new Prefix("до", PartOfSpeech.OTHER),
            new Prefix("за", PartOfSpeech.NOUN), new Prefix("за", PartOfSpeech.VERB), new Prefix("за", PartOfSpeech.OTHER),
            new Prefix("изо", PartOfSpeech.NOUN), new Prefix("изо", PartOfSpeech.VERB), new Prefix("изо", PartOfSpeech.OTHER),
            new Prefix("к", PartOfSpeech.NOUN), new Prefix("к", PartOfSpeech.VERB), new Prefix("к", PartOfSpeech.OTHER),
            new Prefix("ку", PartOfSpeech.NOUN), new Prefix("ку", PartOfSpeech.VERB), new Prefix("ку", PartOfSpeech.OTHER),
            new Prefix("на", PartOfSpeech.NOUN), new Prefix("на", PartOfSpeech.VERB), new Prefix("на", PartOfSpeech.OTHER),
            new Prefix("над", PartOfSpeech.NOUN), new Prefix("над", PartOfSpeech.VERB), new Prefix("над", PartOfSpeech.OTHER),
            new Prefix("надо", PartOfSpeech.NOUN), new Prefix("надо", PartOfSpeech.VERB), new Prefix("надо", PartOfSpeech.OTHER),
            new Prefix("наи", PartOfSpeech.NOUN), new Prefix("наи", PartOfSpeech.VERB), new Prefix("наи", PartOfSpeech.OTHER),
            new Prefix("не", PartOfSpeech.NOUN), new Prefix("не", PartOfSpeech.VERB), new Prefix("не", PartOfSpeech.OTHER),
            new Prefix("ни", PartOfSpeech.NOUN), new Prefix("ни", PartOfSpeech.VERB), new Prefix("ни", PartOfSpeech.OTHER),
            new Prefix("недо", PartOfSpeech.NOUN), new Prefix("недо", PartOfSpeech.VERB), new Prefix("недо", PartOfSpeech.OTHER),
            new Prefix("кое", PartOfSpeech.NOUN), new Prefix("кое", PartOfSpeech.VERB), new Prefix("кое", PartOfSpeech.OTHER),
            new Prefix("о", PartOfSpeech.NOUN), new Prefix("о", PartOfSpeech.VERB), new Prefix("о", PartOfSpeech.OTHER),
            new Prefix("об", PartOfSpeech.NOUN), new Prefix("об", PartOfSpeech.VERB), new Prefix("об", PartOfSpeech.OTHER),
            new Prefix("обо", PartOfSpeech.NOUN), new Prefix("обо", PartOfSpeech.VERB), new Prefix("обо", PartOfSpeech.OTHER),
            new Prefix("от", PartOfSpeech.NOUN), new Prefix("от", PartOfSpeech.VERB), new Prefix("от", PartOfSpeech.OTHER),
            new Prefix("ото", PartOfSpeech.NOUN), new Prefix("ото", PartOfSpeech.VERB), new Prefix("ото", PartOfSpeech.OTHER),
            new Prefix("пере", PartOfSpeech.NOUN), new Prefix("пере", PartOfSpeech.VERB), new Prefix("пере", PartOfSpeech.OTHER),
            new Prefix("по", PartOfSpeech.NOUN), new Prefix("по", PartOfSpeech.VERB), new Prefix("по", PartOfSpeech.OTHER),
            new Prefix("под", PartOfSpeech.NOUN), new Prefix("под", PartOfSpeech.VERB), new Prefix("под", PartOfSpeech.OTHER),
            new Prefix("подо", PartOfSpeech.NOUN), new Prefix("подо", PartOfSpeech.VERB), new Prefix("подо", PartOfSpeech.OTHER),
            new Prefix("поза", PartOfSpeech.NOUN), new Prefix("поза", PartOfSpeech.VERB), new Prefix("поза", PartOfSpeech.OTHER),
            new Prefix("после", PartOfSpeech.NOUN), new Prefix("после", PartOfSpeech.VERB), new Prefix("после", PartOfSpeech.OTHER),
            new Prefix("пред", PartOfSpeech.NOUN), new Prefix("пред", PartOfSpeech.VERB), new Prefix("пред", PartOfSpeech.OTHER),
            new Prefix("предо", PartOfSpeech.NOUN), new Prefix("предо", PartOfSpeech.VERB), new Prefix("предо", PartOfSpeech.OTHER),
            new Prefix("преди", PartOfSpeech.NOUN), new Prefix("преди", PartOfSpeech.VERB), new Prefix("преди", PartOfSpeech.OTHER),
            new Prefix("про", PartOfSpeech.NOUN), new Prefix("про", PartOfSpeech.VERB), new Prefix("про", PartOfSpeech.OTHER),
            new Prefix("противо", PartOfSpeech.NOUN), new Prefix("противо", PartOfSpeech.VERB), new Prefix("противо", PartOfSpeech.OTHER),
            new Prefix("с", PartOfSpeech.NOUN), new Prefix("с", PartOfSpeech.VERB), new Prefix("с", PartOfSpeech.OTHER),
            new Prefix("со", PartOfSpeech.NOUN), new Prefix("со", PartOfSpeech.VERB), new Prefix("со", PartOfSpeech.OTHER),
            new Prefix("у", PartOfSpeech.NOUN), new Prefix("у", PartOfSpeech.VERB), new Prefix("у", PartOfSpeech.OTHER),
    
            // Приставки на З и С 
            new Prefix("без", PartOfSpeech.NOUN), new Prefix("без", PartOfSpeech.VERB), new Prefix("без", PartOfSpeech.OTHER),
            new Prefix("бес", PartOfSpeech.NOUN), new Prefix("бес", PartOfSpeech.VERB), new Prefix("бес", PartOfSpeech.OTHER),
            new Prefix("воз", PartOfSpeech.NOUN), new Prefix("воз", PartOfSpeech.VERB), new Prefix("воз", PartOfSpeech.OTHER),
            new Prefix("вос", PartOfSpeech.NOUN), new Prefix("вос", PartOfSpeech.VERB), new Prefix("вос", PartOfSpeech.OTHER),
            new Prefix("вз", PartOfSpeech.NOUN), new Prefix("вз", PartOfSpeech.VERB), new Prefix("вз", PartOfSpeech.OTHER),
            new Prefix("вс", PartOfSpeech.NOUN), new Prefix("вс", PartOfSpeech.VERB), new Prefix("вс", PartOfSpeech.OTHER),
            new Prefix("из", PartOfSpeech.NOUN), new Prefix("из", PartOfSpeech.VERB), new Prefix("из", PartOfSpeech.OTHER),
            new Prefix("ис", PartOfSpeech.NOUN), new Prefix("ис", PartOfSpeech.VERB), new Prefix("ис", PartOfSpeech.OTHER),
            new Prefix("низ", PartOfSpeech.NOUN), new Prefix("низ", PartOfSpeech.VERB), new Prefix("низ", PartOfSpeech.OTHER),
            new Prefix("нис", PartOfSpeech.NOUN), new Prefix("нис", PartOfSpeech.VERB), new Prefix("нис", PartOfSpeech.OTHER),
            new Prefix("раз", PartOfSpeech.NOUN), new Prefix("раз", PartOfSpeech.VERB), new Prefix("раз", PartOfSpeech.OTHER),
            new Prefix("рас", PartOfSpeech.NOUN), new Prefix("рас", PartOfSpeech.VERB), new Prefix("рас", PartOfSpeech.OTHER),
            new Prefix("разо", PartOfSpeech.NOUN), new Prefix("разо", PartOfSpeech.VERB), new Prefix("разо", PartOfSpeech.OTHER),
            new Prefix("через", PartOfSpeech.NOUN), new Prefix("через", PartOfSpeech.VERB), new Prefix("через", PartOfSpeech.OTHER),
            new Prefix("черес", PartOfSpeech.NOUN), new Prefix("черес", PartOfSpeech.VERB), new Prefix("черес", PartOfSpeech.OTHER),
            new Prefix("чрез", PartOfSpeech.NOUN), new Prefix("чрез", PartOfSpeech.VERB), new Prefix("чрез", PartOfSpeech.OTHER),
            new Prefix("чрес", PartOfSpeech.NOUN), new Prefix("чрес", PartOfSpeech.VERB), new Prefix("чрес", PartOfSpeech.OTHER),
    
            // Специфические
            new Prefix("пре", PartOfSpeech.NOUN), new Prefix("пре", PartOfSpeech.VERB), new Prefix("пре", PartOfSpeech.OTHER),
            new Prefix("при", PartOfSpeech.NOUN), new Prefix("при", PartOfSpeech.VERB), new Prefix("при", PartOfSpeech.OTHER),
            
            // Сложные и двойные 
            new Prefix("безвоз", PartOfSpeech.NOUN), new Prefix("безвоз", PartOfSpeech.VERB), new Prefix("безвоз", PartOfSpeech.OTHER),
            new Prefix("взаимо", PartOfSpeech.NOUN), new Prefix("взаимо", PartOfSpeech.VERB), new Prefix("взаимо", PartOfSpeech.OTHER),
            new Prefix("изпод", PartOfSpeech.NOUN), new Prefix("изпод", PartOfSpeech.VERB), new Prefix("изпод", PartOfSpeech.OTHER),
            new Prefix("испод", PartOfSpeech.NOUN), new Prefix("испод", PartOfSpeech.VERB), new Prefix("испод", PartOfSpeech.OTHER),
            new Prefix("небез", PartOfSpeech.NOUN), new Prefix("небез", PartOfSpeech.VERB), new Prefix("небез", PartOfSpeech.OTHER),
            new Prefix("превос", PartOfSpeech.NOUN), new Prefix("превос", PartOfSpeech.VERB), new Prefix("превос", PartOfSpeech.OTHER),
            new Prefix("разъ", PartOfSpeech.NOUN), new Prefix("разъ", PartOfSpeech.VERB), new Prefix("разъ", PartOfSpeech.OTHER),
            new Prefix("сверх", PartOfSpeech.NOUN), new Prefix("сверх", PartOfSpeech.VERB), new Prefix("сверх", PartOfSpeech.OTHER),
    
            // Заимствованные приставки 
            new Prefix("а", PartOfSpeech.NOUN), new Prefix("а", PartOfSpeech.VERB), new Prefix("а", PartOfSpeech.OTHER),
            new Prefix("анти", PartOfSpeech.NOUN), new Prefix("анти", PartOfSpeech.VERB), new Prefix("анти", PartOfSpeech.OTHER),
            new Prefix("архи", PartOfSpeech.NOUN), new Prefix("архи", PartOfSpeech.VERB), new Prefix("архи", PartOfSpeech.OTHER),
            new Prefix("арх", PartOfSpeech.NOUN), new Prefix("арх", PartOfSpeech.VERB), new Prefix("арх", PartOfSpeech.OTHER),
            new Prefix("би", PartOfSpeech.NOUN), new Prefix("би", PartOfSpeech.VERB), new Prefix("би", PartOfSpeech.OTHER),
            new Prefix("гипер", PartOfSpeech.NOUN), new Prefix("гипер", PartOfSpeech.VERB), new Prefix("гипер", PartOfSpeech.OTHER),
            new Prefix("гипо", PartOfSpeech.NOUN), new Prefix("гипо", PartOfSpeech.VERB), new Prefix("гипо", PartOfSpeech.OTHER),
            new Prefix("де", PartOfSpeech.NOUN), new Prefix("де", PartOfSpeech.VERB), new Prefix("де", PartOfSpeech.OTHER),
            new Prefix("дез", PartOfSpeech.NOUN), new Prefix("дез", PartOfSpeech.VERB), new Prefix("дез", PartOfSpeech.OTHER),
            new Prefix("дис", PartOfSpeech.NOUN), new Prefix("дис", PartOfSpeech.VERB), new Prefix("дис", PartOfSpeech.OTHER),
            new Prefix("дизар", PartOfSpeech.NOUN), new Prefix("дизар", PartOfSpeech.VERB), new Prefix("дизар", PartOfSpeech.OTHER),
            new Prefix("евро", PartOfSpeech.NOUN), new Prefix("евро", PartOfSpeech.VERB), new Prefix("евро", PartOfSpeech.OTHER),
            new Prefix("им", PartOfSpeech.NOUN), new Prefix("им", PartOfSpeech.VERB), new Prefix("им", PartOfSpeech.OTHER),
            new Prefix("ин", PartOfSpeech.NOUN), new Prefix("ин", PartOfSpeech.VERB), new Prefix("ин", PartOfSpeech.OTHER),
            new Prefix("интер", PartOfSpeech.NOUN), new Prefix("интер", PartOfSpeech.VERB), new Prefix("интер", PartOfSpeech.OTHER),
            new Prefix("ир", PartOfSpeech.NOUN), new Prefix("ир", PartOfSpeech.VERB), new Prefix("ир", PartOfSpeech.OTHER),
            new Prefix("контр", PartOfSpeech.NOUN), new Prefix("контр", PartOfSpeech.VERB), new Prefix("контр", PartOfSpeech.OTHER),
            new Prefix("квази", PartOfSpeech.NOUN), new Prefix("квази", PartOfSpeech.VERB), new Prefix("квази", PartOfSpeech.OTHER),
            new Prefix("макро", PartOfSpeech.NOUN), new Prefix("макро", PartOfSpeech.VERB), new Prefix("макро", PartOfSpeech.OTHER),
            new Prefix("микро", PartOfSpeech.NOUN), new Prefix("микро", PartOfSpeech.VERB), new Prefix("микро", PartOfSpeech.OTHER),
            new Prefix("моно", PartOfSpeech.NOUN), new Prefix("моно", PartOfSpeech.VERB), new Prefix("моно", PartOfSpeech.OTHER),
            new Prefix("мульти", PartOfSpeech.NOUN), new Prefix("мульти", PartOfSpeech.VERB), new Prefix("мульти", PartOfSpeech.OTHER),
            new Prefix("нео", PartOfSpeech.NOUN), new Prefix("нео", PartOfSpeech.VERB), new Prefix("нео", PartOfSpeech.OTHER),
            new Prefix("поли", PartOfSpeech.NOUN), new Prefix("поли", PartOfSpeech.VERB), new Prefix("поли", PartOfSpeech.OTHER),
            new Prefix("пост", PartOfSpeech.NOUN), new Prefix("пост", PartOfSpeech.VERB), new Prefix("пост", PartOfSpeech.OTHER),
            new Prefix("прото", PartOfSpeech.NOUN), new Prefix("прото", PartOfSpeech.VERB), new Prefix("прото", PartOfSpeech.OTHER),
            new Prefix("псевдо", PartOfSpeech.NOUN), new Prefix("псевдо", PartOfSpeech.VERB), new Prefix("псевдо", PartOfSpeech.OTHER),
            new Prefix("ре", PartOfSpeech.NOUN), new Prefix("ре", PartOfSpeech.VERB), new Prefix("ре", PartOfSpeech.OTHER),
            new Prefix("супер", PartOfSpeech.NOUN), new Prefix("супер", PartOfSpeech.VERB), new Prefix("супер", PartOfSpeech.OTHER),
            new Prefix("суб", PartOfSpeech.NOUN), new Prefix("суб", PartOfSpeech.VERB), new Prefix("суб", PartOfSpeech.OTHER),
            new Prefix("транс", PartOfSpeech.NOUN), new Prefix("транс", PartOfSpeech.VERB), new Prefix("транс", PartOfSpeech.OTHER),
            new Prefix("ультра", PartOfSpeech.NOUN), new Prefix("ультра", PartOfSpeech.VERB), new Prefix("ультра", PartOfSpeech.OTHER),
            new Prefix("экстра", PartOfSpeech.NOUN), new Prefix("экстра", PartOfSpeech.VERB), new Prefix("экстра", PartOfSpeech.OTHER),
            new Prefix("эндо", PartOfSpeech.NOUN), new Prefix("эндо", PartOfSpeech.VERB), new Prefix("эндо", PartOfSpeech.OTHER),
            new Prefix("экзо", PartOfSpeech.NOUN), new Prefix("экзо", PartOfSpeech.VERB), new Prefix("экзо", PartOfSpeech.OTHER),
    
            // Строго определенные приставки
            new Prefix("взо", PartOfSpeech.VERB),
            new Prefix("пона", PartOfSpeech.VERB),
            new Prefix("понавы", PartOfSpeech.VERB),
            new Prefix("препод", PartOfSpeech.VERB),
            new Prefix("па", PartOfSpeech.NOUN),
            new Prefix("вице", PartOfSpeech.NOUN),
            new Prefix("экс", PartOfSpeech.NOUN),
            new Prefix("пра", PartOfSpeech.NOUN), new Prefix("пра", PartOfSpeech.OTHER)
        };

        // Суффиксы
        public static List<Suffix> suffixes = new List<Suffix>()
        {
            // Могут быть существительным и прилагательным/причастием
            new Suffix("к", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("к", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),

            new Suffix("ин", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ин", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER), 
    
            // Суффиксы существительных (Уменьшительно-ласкательные)
            new Suffix("ик", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ек", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ёк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ок", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ц", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ичк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ечк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("очк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("оньк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("еньк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ышк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ушк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("юшк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("онок", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("енок", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ёнок", SuffixType.DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("еньк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("оньк", SuffixType.DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("еств", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ств", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("тель", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("итель", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER), 
    
            // Суффиксы существительных (Профессии, деятели, лица)
            new Suffix("тель", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ник", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ниц", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("щик", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("чик", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("арь", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("яр", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ист", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ач", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ак", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("як", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ан", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("анин", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ец", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("иц", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("олог", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ман", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN), 
    
            // Суффиксы существительных (Отвлеченные понятия, свойства, собирательность)
            new Suffix("ость", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("есть", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("изн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("от", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ни", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ени", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ани", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("изм", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("ци", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ность", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("няк", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.NOUN),
            new Suffix("еств", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("ств", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN),
            new Suffix("тв", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.NOUN), 
    
            // Суффиксы прилагательных 
            new Suffix("н", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("енн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("онн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ов", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ев", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ск", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("цк", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ив", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("лив", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("чив", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ист", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("оват", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("еват", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("инск", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ичн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ическ", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("альн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("чат", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("юч", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ерн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER), 
    
            // Суффиксы глаголов
            new Suffix("а", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("я", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("и", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("е", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("у", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ю", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("о", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("и", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("а", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("я", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("е", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("у", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("о", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ю", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ова", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ева", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ирова", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ыва", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ива", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ну", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ся", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("сь", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("л", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("л", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("й", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("й", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ть", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ть", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ти", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.VERB),
            new Suffix("ти", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            
            // Глаголы инфинитивы (начальные формы без "ся")
            new Suffix("ить", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ать", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ять", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("еть", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("уть", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ють", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("овать", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("евать", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ывать", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ивать", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("нуть", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            
            // Глаголы возвратные (С частицами "ся"/"сь")
            new Suffix("иться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("аться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("яться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("еться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("уться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ются", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("оваться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("еваться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("ываться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("иваться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("нуться", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            
            // Возвратные суффиксы для прошедшего времени
            new Suffix("лся", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("лась", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("лось", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),
            new Suffix("лись", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.VERB),

            // Суффиксы причастий и деепричастий
            new Suffix("ущ", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ющ", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ащ", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ящ", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("вш", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ш", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ем", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("ом", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("им", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("нн", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("т", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("в", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("вши", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("учи", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("ючи", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER), 
    
            // Суффиксы наречий
            new Suffix("о", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("е", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("а", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("у", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("ски", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("ыми", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("енько", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("ча", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER), 
    
            // Суффиксы степеней сравнения 
            new Suffix("ее", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("ей", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITHOUT_ENDING, PartOfSpeech.OTHER),
            new Suffix("ейш", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER),
            new Suffix("айш", SuffixType.NOT_DIMINUTIVE, SuffixWithEnding.WITH_ENDING, PartOfSpeech.OTHER)
        };

        // Окончания
        public static List<Ending> endings = new List<Ending>()
        {
            // Окончания существительных
            new Ending("а", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("я", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("е", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("у", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("ю", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("ой", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("ей", Gender.NONE, Plurality.ONE, Declension.FIRST, PartOfSpeech.NOUN),
            new Ending("о", Gender.NONE, Plurality.ONE, Declension.SECOND, PartOfSpeech.NOUN),
            new Ending("ом", Gender.NONE, Plurality.ONE, Declension.SECOND, PartOfSpeech.NOUN),
            new Ending("ем", Gender.NONE, Plurality.ONE, Declension.SECOND, PartOfSpeech.NOUN),
            new Ending("и", Gender.FEMALE, Plurality.ONE, Declension.THIRD, PartOfSpeech.NOUN),
            new Ending("ью", Gender.FEMALE, Plurality.ONE, Declension.THIRD, PartOfSpeech.NOUN),
            new Ending("ь", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ы", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ов", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ев", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ам", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ям", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ами", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ями", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ах", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
            new Ending("ях", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.NOUN),
    
            // Окончания прилагательных, местоимений, причастий
            new Ending("ый", Gender.MALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ий", Gender.MALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ой", Gender.MALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ая", Gender.FEMALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("яя", Gender.FEMALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ую", Gender.FEMALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("юю", Gender.FEMALE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ое", Gender.NEUTER, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ее", Gender.NEUTER, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ые", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ие", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ых", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("их", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ым", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("им", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ыми", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ого", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("его", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ому", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER),
            new Ending("ему", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.OTHER), 
    
            // Глагольные окончания 
            new Ending("у", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ю", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ешь", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ишь", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ет", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ит", Gender.NONE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ем", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("им", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ете", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ите", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ут", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ют", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ат", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ят", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
            new Ending("те", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
    
            // Окончания глаголов прошедшего времени 
            new Ending("а", Gender.FEMALE, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("о", Gender.NEUTER, Plurality.ONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("и", Gender.NONE, Plurality.MANY, Declension.NONE, PartOfSpeech.VERB),
    
            // Инфинитив 
            new Ending("ть", Gender.NONE, Plurality.NONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("ти", Gender.NONE, Plurality.NONE, Declension.NONE, PartOfSpeech.VERB),
            new Ending("чь", Gender.NONE, Plurality.NONE, Declension.NONE, PartOfSpeech.VERB)
        };
    }
}
