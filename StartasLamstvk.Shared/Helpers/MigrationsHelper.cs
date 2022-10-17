using Microsoft.EntityFrameworkCore.Migrations;

namespace StartasLamstvk.Shared.Helpers
{
    public static class MigrationsHelper
    {
        public static void AddEnumAndTranslations<T>(
            this MigrationBuilder migrationBuilder,
            T enumId,
            string enText,
            string ltText)
            where T : Enum
        {
            InsertEnum(migrationBuilder, enumId);
            AddTranslation(migrationBuilder, enumId, "en", enText);
            AddTranslation(migrationBuilder, enumId, "lt", ltText);
        }

        private static void InsertEnum<T>(MigrationBuilder migrationBuilder, T enumId)
            where T : Enum
        {
            migrationBuilder.InsertData(table: GetEnumTableName(enumId),
                column: "Id",
                value: Convert.ToInt32(enumId));
        }

        private static string GetEnumTableName<T>(T enumId)
        {
            var enumName = enumId.GetType().Name;
            var enumTableName = enumName.EndsWith("y")
                ? $"{enumName.Replace($"{enumName[enumName.LastIndexOf("y", StringComparison.Ordinal)]}", "ies")}"
                : enumName.EndsWith("s")
                    ? $"Enum{enumName}es"
                    : $"Enum{enumName}s";

            return enumTableName;
        }

        private static void AddTranslation<T>(MigrationBuilder migrationBuilder, T enumId, string languageCode, string text)
            where T : Enum
        {
            migrationBuilder.InsertData(
                table: $"{enumId.GetType().Name}Translations",
                columns: new[] { $"{enumId.GetType().Name}Id", "LanguageCode", "Text" },
                values: new object[] { Convert.ToInt32(enumId), languageCode, text });
        }
    }
}
