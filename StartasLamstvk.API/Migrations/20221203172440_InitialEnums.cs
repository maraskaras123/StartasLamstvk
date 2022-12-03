using Microsoft.EntityFrameworkCore.Migrations;
using StartasLamstvk.Shared.Models.Enum;
using StartasLamstvk.Shared.Helpers;

#nullable disable

namespace StartasLamstvk.API.Migrations
{
    public partial class InitialEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddEnumAndTranslations(EnumRole.Admin, "Administrator", "Administratorius");
            //migrationBuilder.AddEnumAndTranslations(EnumRole.Director, "Race Director", "Varžybų vadovas");
            //migrationBuilder.AddEnumAndTranslations(EnumRole.Marshal, "Marshal", "Teisėjas");

            migrationBuilder.AddEnumAndTranslations(EnumRaceType.AutoCircuit, "Auto Circuit", "Automobilių žiedinės");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.AutoCross, "Auto Cross", "Autokrosas");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.Drift, "Drift", "Driftas");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.HardEnduro, "Hard Enduro", "Hard Enduro");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.Kart, "Karting", "Kartingai");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.MotoCircuit, "Moto Circuit", "Motociklų žiedinės");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.MotoCross, "Moto Cross", "Motokrosas");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.Rally, "Rally", "Ralis");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.Slalom, "Slalom", "Slalomas");
            migrationBuilder.AddEnumAndTranslations(EnumRaceType.SuperMoto, "Supermoto", "Supermoto");

            migrationBuilder.AddEnumAndTranslations(EnumLasfCategory.Intern, "Intern", "Stažuotojas");
            migrationBuilder.AddEnumAndTranslations(EnumLasfCategory.Third, "Third", "Trečia");
            migrationBuilder.AddEnumAndTranslations(EnumLasfCategory.Second, "Second", "Antra");
            migrationBuilder.AddEnumAndTranslations(EnumLasfCategory.First, "First", "Pirma");
            migrationBuilder.AddEnumAndTranslations(EnumLasfCategory.National, "National", "Nacionalinė");
            migrationBuilder.AddEnumAndTranslations(EnumLasfCategory.International, "International", "Tarptautinė");

            migrationBuilder.AddEnumAndTranslations(EnumMotoCategory.HardEnduroA, "Hard Enduro A", "Hard Enduro A");
            migrationBuilder.AddEnumAndTranslations(EnumMotoCategory.HardEnduroB, "Hard Enduro B", "Hard Enduro B");
            migrationBuilder.AddEnumAndTranslations(EnumMotoCategory.MotoCrossA, "Moto Cross A", "Motokrosas A");
            migrationBuilder.AddEnumAndTranslations(EnumMotoCategory.MotoCrossB, "Moto Cross B", "Motokrosas B");
            migrationBuilder.AddEnumAndTranslations(EnumMotoCategory.SuperMotoA, "Supermoto A", "Supermoto A");
            migrationBuilder.AddEnumAndTranslations(EnumMotoCategory.SuperMotoB, "Supermoto B", "Supermoto B");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
