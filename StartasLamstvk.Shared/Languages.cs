using System.Globalization;

namespace StartasLamstvk.Shared
{
    public static class Languages
    {
        public const string En = "en";
        public const string Lt = "lt";

        public static readonly List<CultureInfo> CultureLanguages = new()
        {
            new CultureInfo(En),
            new CultureInfo(Lt)
        };

        public static readonly List<(string LanguageCode, string DisplayName)> SupportedLanguages = new()
        {
            (En, "English"),
            (Lt, "Lietuvių")
        };
    }
}
