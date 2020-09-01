using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiraganeoApp.Syllabaries
{
    public enum SyllabaryType
    {
        Hiragana,
        Katakana,
        Kenji
    }
    public enum SyllabaryParts
    {
        A, KA, SA, TA, NA, HA, MA, YA, RA, WA, N, GA, ZA, DA, BA, PA
    }

    public class Generators
    {
        public static (string, string) GeneratePracticeTextWithHint(Dictionary<SyllabaryParts, bool> selectedParts, SyllabaryType selectedType)
        {
            StringBuilder output = new StringBuilder();
            StringBuilder hint = new StringBuilder();

            Random rnd = new Random((int)DateTime.Now.Ticks);

            var maxSyllabsInWord = 4;
            var syllabsInWord = 0;

            // var selectedSyllabels1 = Hiragana.Syllables.Where(entry => /*EnabledSyllables[entry.Key]*/ true == true && selectedParts.Contains(entry.Key)).SelectMany(pair => pair.Value).Select(pair => pair.Key);
            // var selectedSyllabels = Hiragana.Syllables.Where(entry => /*EnabledSyllables[entry.Key]*/ true == true && selectedParts.Contains(entry.Key)).SelectMany(pair => pair.Value);

            var selectedSyllabels = new List<KeyValuePair<string, string>>();

            Dictionary<SyllabaryParts, Dictionary<string, string>> selectedSyllabary = new Dictionary<SyllabaryParts, Dictionary<string, string>>();

            switch (selectedType)
            {
                case SyllabaryType.Hiragana:
                    selectedSyllabary = Hiragana.Syllables;
                    break;
                case SyllabaryType.Katakana:
                    selectedSyllabary = Katakana.Syllables;
                    break;
            }

            selectedSyllabels = selectedSyllabary
                .Where(entry => selectedParts.GetValueOrDefault(entry.Key))
                .SelectMany(partEntry => partEntry.Value)
                .ToList();
            
            if (selectedSyllabels.Count() == 0)
            {
                return (output.ToString(), hint.ToString());
            }

            for (int i = 0; i < 256; i++)
            {
                var sylab = selectedSyllabels.ElementAt(rnd.Next(0, selectedSyllabels.Count() - 1));

                output.Append(sylab.Key);
                hint.Append(sylab.Value);
                syllabsInWord++;

                if (rnd.Next(0, 4) % 4 == 0 || syllabsInWord >= maxSyllabsInWord)
                {
                    output.Append(' ');
                    hint.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    syllabsInWord = 0;
                }
            }

            return (output.ToString(), hint.ToString());
        }
    }
    public class Hiragana
    {
        public static Dictionary<SyllabaryParts, Dictionary<string, string>> Syllables = new Dictionary<SyllabaryParts, Dictionary<string, string>>() {
            { SyllabaryParts.A, new Dictionary<string, string>() { { "a", "あ" },{ "i", "い" },{ "u", "う" },{ "e", "え" },{ "o", "お" } } },
            { SyllabaryParts.KA, new Dictionary<string, string>() { { "ka", "か" },{ "ki", "き" },{ "ku", "く" },{ "ke", "け" },{ "ko", "こ" } } },
            { SyllabaryParts.SA, new Dictionary<string, string>() { { "sa", "さ" },{ "shi", "し" },{ "su", "す" },{ "se", "せ" },{ "so", "そ" } } },
            { SyllabaryParts.TA, new Dictionary<string, string>() { { "ta", "た" },{ "chi", "ち" },{ "tsu", "つ" },{ "te", "て" },{ "to", "と" } } },
            { SyllabaryParts.NA, new Dictionary<string, string>() { { "na", "な" },{ "ni", "に" },{ "nu", "ぬ" },{ "ne", "ね" },{ "no", "の" } } },
            { SyllabaryParts.HA, new Dictionary<string, string>() { { "ha", "は" },{ "hi", "ひ" },{ "fu", "ふ" },{ "he", "へ" },{ "ho", "ほ" } } },
            { SyllabaryParts.MA, new Dictionary<string, string>() { { "ma", "ま" },{ "mi", "み" },{ "mu", "む" },{ "me", "め" },{ "mo", "も" } } },

            { SyllabaryParts.YA, new Dictionary<string, string>() { { "ya", "や" },               { "yu", "ゆ"},                { "yo", "よ" } } },

            { SyllabaryParts.RA, new Dictionary<string, string>() { { "ra", "ら" },{ "ri", "り" },{ "ru", "る" },{ "re", "れ" },{ "ro", "ろ" } } },
            { SyllabaryParts.WA, new Dictionary<string, string>() { { "wa", "わ" },{ "wi", "ゐ" },               { "we", "ゑ" },{ "wo", "を" } } },

            { SyllabaryParts.N, new Dictionary<string, string>() {                                                              { "n", "ん" } } },

            { SyllabaryParts.GA, new Dictionary<string, string>() { { "ga", "が" },{ "gi", "ぎ" },{ "gu", "ぐ" },{ "ge", "げ" },{ "go", "ご" } } },
            { SyllabaryParts.ZA, new Dictionary<string, string>() { { "za", "ざ" },{ "ji", "じ" },{ "zu", "ず" },{ "ze", "ぜ" },{ "zo", "ぞ" } } },
            { SyllabaryParts.DA, new Dictionary<string, string>() { { "da", "だ" },{ "(ji)", "ぢ" },{ "(zu)", "づ" },{ "de", "で" },{ "do", "ど" } } },
            { SyllabaryParts.BA, new Dictionary<string, string>() { { "ba", "ば" },{ "bi", "び" },{ "bu", "ぶ" },{ "be", "べ" },{ "bo", "ぼ" } } },
            { SyllabaryParts.PA, new Dictionary<string, string>() { { "pa", "ぱ" },{ "pi", "ぴ" },{ "pu", "ぷ" },{ "pe", "ぺ" },{ "po", "ぽ" } } }
        };
    }

    public class Katakana
    {
        public static Dictionary<SyllabaryParts, Dictionary<string, string>> Syllables = new Dictionary<SyllabaryParts, Dictionary<string, string>>()
        {
            { SyllabaryParts.A, new Dictionary<string, string>() { { "a",  "ア" }, { "i",  "イ" }, { "u",  "ウ" }, { "e",  "エ" }, { "o",  "オ" } } },
            { SyllabaryParts.KA, new Dictionary<string, string>() { { "ka", "カ" }, { "ki", "キ" }, { "ku", "ク" }, { "ke", "ケ" }, { "ko", "コ" } } },
            { SyllabaryParts.SA, new Dictionary<string, string>() { { "sa", "サ" }, { "shi","シ" }, { "su", "ス" }, { "se", "セ" }, { "so", "ソ" } } },
            { SyllabaryParts.TA, new Dictionary<string, string>() { { "ta", "タ" }, { "chi","チ" }, { "tsu","ツ" }, { "te", "テ" }, { "to", "ト" } } },
            { SyllabaryParts.NA, new Dictionary<string, string>() { { "na", "ナ" }, { "ni", "ニ" }, { "nu", "ヌ" }, { "ne", "ネ" }, { "no", "ノ" } } },
            { SyllabaryParts.HA, new Dictionary<string, string>() { { "ha", "ハ" }, { "hi", "ヒ" }, { "fu", "フ" }, { "he", "ヘ" }, { "ho", "ホ" } } },
            { SyllabaryParts.MA, new Dictionary<string, string>() { { "ma", "マ" }, { "mi", "ミ" }, { "mu", "ム" }, { "me", "メ" }, { "mo", "モ" } } },

            { SyllabaryParts.YA, new Dictionary<string, string>() { { "ya", "ヤ" },                 { "yu", "ユ" },                 { "yo", "ヨ" } } },

            { SyllabaryParts.RA, new Dictionary<string, string>() { { "ra", "ラ" }, { "ri", "リ" }, { "ru", "ル" }, { "re", "レ" }, { "ro", "ロ" } } },
            { SyllabaryParts.WA, new Dictionary<string, string>() { { "wa", "ワ" }, { "wi", "ヰ" },                 { "we", "ヱ" }, { "wo", "ヲ" } } },

            { SyllabaryParts.N, new Dictionary<string, string>() {                                                                 { "n", "ン" } } },

            { SyllabaryParts.GA, new Dictionary<string, string>() { { "ga", "ガ" }, { "gi",  "ギ" }, { "gu",   "グ" }, { "ge", "ゲ" }, { "go", "ゴ" } } },
            { SyllabaryParts.ZA, new Dictionary<string, string>() { { "za", "ザ" }, { "ji",  "ジ" }, { "zu",   "ズ" }, { "ze", "ゼ" }, { "zo", "ゾ" } } },
            { SyllabaryParts.DA, new Dictionary<string, string>() { { "da", "ダ" }, { "(ji)","ヂ" }, { "(zu)", "ヅ" }, { "de", "デ" }, { "do", "ド" } } },
            { SyllabaryParts.BA, new Dictionary<string, string>() { { "ba", "バ" }, { "bi",  "ビ" }, { "bu",   "ブ" }, { "be", "ベ" }, { "bo", "ボ" } } },
            { SyllabaryParts.PA, new Dictionary<string, string>() { { "pa", "パ" }, { "pi",  "ピ" }, { "pu",   "プ" }, { "pe", "ペ" }, { "po", "ポ" } } },
        };
    }
}
