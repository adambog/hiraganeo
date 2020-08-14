using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiraganeoCore
{
    public class Hiraganeo
    {
        public enum Syllabaries
        {
            Hiragana,
            Katagana
        }
        public enum HiraganaParts
        {
            A, KA, SA, TA, NA, HA, MA, YA, RA, WA, N, GA, ZA, DA, BA, PA
        }

        public static Dictionary<HiraganaParts, Dictionary<string, string>> Hiragana = new Dictionary<HiraganaParts, Dictionary<string, string>>() {
                { HiraganaParts.A, new Dictionary<string, string>() { { "a", "あ" },{ "i", "い" },{ "u", "う" },{ "e", "え" },{ "o", "お" } } },
                { HiraganaParts.KA, new Dictionary<string, string>() { { "ka", "か" },{ "ki", "き" },{ "ku", "く" },{ "ke", "け" },{ "ko", "こ" } } },
                { HiraganaParts.SA, new Dictionary<string, string>() { { "sa", "さ" },{ "shi", "し" },{ "su", "す" },{ "se", "せ" },{ "so", "そ" } } },
                { HiraganaParts.TA, new Dictionary<string, string>() { { "ta", "た" },{ "chi", "ち" },{ "tsu", "つ" },{ "te", "て" },{ "to", "と" } } },
                { HiraganaParts.NA, new Dictionary<string, string>() { { "na", "な" },{ "ni", "に" },{ "nu", "ぬ" },{ "ne", "ね" },{ "no", "の" } } },
                { HiraganaParts.HA, new Dictionary<string, string>() { { "ha", "は" },{ "hi", "ひ" },{ "fu", "ふ" },{ "he", "へ" },{ "ho", "ほ" } } },
                { HiraganaParts.MA, new Dictionary<string, string>() { { "ma", "ま" },{ "mi", "み" },{ "mu", "む" },{ "me", "め" },{ "mo", "も" } } },

                { HiraganaParts.YA, new Dictionary<string, string>() { { "ya", "や" }, { "yu", "ゆ"},{ "yo", "よ" } } },

                { HiraganaParts.RA, new Dictionary<string, string>() { { "ra", "ら" },{ "ri", "り" },{ "ru", "る" },{ "re", "れ" },{ "ro", "ろ" } } },
                { HiraganaParts.WA, new Dictionary<string, string>() { { "wa", "わ" },{ "wi", "ゐ" },{ "we", "ゑ" },{ "wo", "を" } } },

                { HiraganaParts.N, new Dictionary<string, string>() { { "n", "ん" } } },

                { HiraganaParts.GA, new Dictionary<string, string>() { { "ga", "が" },{ "gi", "ぎ" },{ "gu", "ぐ" },{ "ge", "げ" },{ "go", "ご" } } },
                { HiraganaParts.ZA, new Dictionary<string, string>() { { "za", "ざ" },{ "ji", "じ" },{ "zu", "ず" },{ "ze", "ぜ" },{ "zo", "ぞ" } } },
                { HiraganaParts.DA, new Dictionary<string, string>() { { "da", "だ" },{ "(ji)", "ぢ" },{ "(zu)", "づ" },{ "de", "で" },{ "do", "ど" } } },
                { HiraganaParts.BA, new Dictionary<string, string>() { { "ba", "ば" },{ "bi", "び" },{ "bu", "ぶ" },{ "be", "べ" },{ "bo", "ぼ" } } },
                { HiraganaParts.PA, new Dictionary<string, string>() { { "pa", "ぱ" },{ "pi", "ぴ" },{ "pu", "ぷ" },{ "pe", "ぺ" },{ "po", "ぽ" } } }
            };

        public static List<HiraganaParts> HiraganaBasic = new List<HiraganaParts>()
        {
            HiraganaParts.A,
            HiraganaParts.KA,
            HiraganaParts.SA,
            HiraganaParts.TA,
            HiraganaParts.NA,
            HiraganaParts.HA,
            HiraganaParts.MA,
        };

        public static List<HiraganaParts> HiraganaVoiced = new List<HiraganaParts>()
        {
            HiraganaParts.GA,
            HiraganaParts.ZA,
            HiraganaParts.DA,
            HiraganaParts.BA,
            HiraganaParts.PA,
        };

        public static Dictionary<HiraganaParts, bool> EnabledSyllables = new Dictionary<HiraganaParts, bool>()
        {
            { HiraganaParts.A,  true },
            { HiraganaParts.KA, true },
            { HiraganaParts.SA, true },
            { HiraganaParts.TA, true },
            { HiraganaParts.NA, true },
            { HiraganaParts.HA, true },
            { HiraganaParts.MA, true },
            { HiraganaParts.YA, true },
            { HiraganaParts.RA, true },
            { HiraganaParts.WA, true },
            { HiraganaParts.N,  true },
            { HiraganaParts.GA, true },
            { HiraganaParts.ZA, true },
            { HiraganaParts.DA, true },
            { HiraganaParts.BA, true },
            { HiraganaParts.PA, true },
        };

        public static string GenerateText(IEnumerable<HiraganaParts> hiraganaSet)
        {
            StringBuilder output = new StringBuilder();

            Random rnd = new Random((int)DateTime.Now.Ticks);

            var maxSyllabsInWord = 4;
            var syllabsInWord = 0;

            var selectedSyllabels = Hiragana.Where(entry => EnabledSyllables[entry.Key] == true && hiraganaSet.Contains(entry.Key)).SelectMany(pair => pair.Value).Select(pair => pair.Key);

            if (selectedSyllabels.Count() == 0)
            {                
                return output.ToString();
            }

            for (int i = 0; i < 256; i++)
            {
                var sylab = selectedSyllabels.ElementAt(rnd.Next(0, selectedSyllabels.Count() - 1));

                //Console.Write(sylab);
                output.Append(sylab);
                syllabsInWord++;

                if (rnd.Next(0, 4) % 4 == 0 || syllabsInWord >= maxSyllabsInWord)
                {
                    //Console.Write(' ');
                    output.Append(' ');
                    syllabsInWord = 0;
                }
            }

            return output.ToString();
        }

        public static (string, string) GenerateTextWithHint(IEnumerable<HiraganaParts> hiraganaSet, Syllabaries selectedSyllabary = Syllabaries.Hiragana)
        {
            StringBuilder output = new StringBuilder();
            StringBuilder hint = new StringBuilder();

            Random rnd = new Random((int)DateTime.Now.Ticks);

            var maxSyllabsInWord = 4;
            var syllabsInWord = 0;

            var selectedSyllabels1 = Hiragana.Where(entry => EnabledSyllables[entry.Key] == true && hiraganaSet.Contains(entry.Key)).SelectMany(pair => pair.Value).Select(pair => pair.Key);
            var selectedSyllabels = Hiragana.Where(entry => EnabledSyllables[entry.Key] == true && hiraganaSet.Contains(entry.Key)).SelectMany(pair => pair.Value);

            if (selectedSyllabary == Syllabaries.Katagana)
            {
                output.Append("Not yet implemented!");
                return (output.ToString(), hint.ToString());
            }

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
}
