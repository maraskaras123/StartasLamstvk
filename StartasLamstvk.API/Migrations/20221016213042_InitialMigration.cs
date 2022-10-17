using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartasLamstvk.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LasfCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LasfCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotoCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LasfCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LasfCategoryId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LasfCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LasfCategoryTranslations_LasfCategories_LasfCategoryId",
                        column: x => x.LasfCategoryId,
                        principalTable: "LasfCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotoCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotoCategoryId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotoCategoryTranslations_MotoCategories_MotoCategoryId",
                        column: x => x.MotoCategoryId,
                        principalTable: "MotoCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LasfCategoryId = table.Column<int>(type: "int", nullable: true),
                    MotoCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_LasfCategories_LasfCategoryId",
                        column: x => x.LasfCategoryId,
                        principalTable: "LasfCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_MotoCategories_MotoCategoryId",
                        column: x => x.MotoCategoryId,
                        principalTable: "MotoCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RaceTypeTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceTypeId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceTypeTranslations_RaceTypes_RaceTypeId",
                        column: x => x.RaceTypeId,
                        principalTable: "RaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleTranslations_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Championship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaceTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_RaceTypes_RaceTypeId",
                        column: x => x.RaceTypeId,
                        principalTable: "RaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Events_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RaceTypeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferences_RaceTypes_RaceTypeId",
                        column: x => x.RaceTypeId,
                        principalTable: "RaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceOfficials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceOfficials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceOfficials_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceOfficials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRacePreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRacePreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRacePreferences_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRacePreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mileage = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    Passenger1Id = table.Column<int>(type: "int", nullable: true),
                    Passenger2Id = table.Column<int>(type: "int", nullable: true),
                    Passenger3Id = table.Column<int>(type: "int", nullable: true),
                    Passenger4Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crews_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crews_RaceOfficials_DriverId",
                        column: x => x.DriverId,
                        principalTable: "RaceOfficials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Crews_RaceOfficials_Passenger1Id",
                        column: x => x.Passenger1Id,
                        principalTable: "RaceOfficials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Crews_RaceOfficials_Passenger2Id",
                        column: x => x.Passenger2Id,
                        principalTable: "RaceOfficials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Crews_RaceOfficials_Passenger3Id",
                        column: x => x.Passenger3Id,
                        principalTable: "RaceOfficials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Crews_RaceOfficials_Passenger4Id",
                        column: x => x.Passenger4Id,
                        principalTable: "RaceOfficials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    RaceOfficialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wages_RaceOfficials_RaceOfficialId",
                        column: x => x.RaceOfficialId,
                        principalTable: "RaceOfficials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_DriverId",
                table: "Crews",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_EventId",
                table: "Crews",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_Passenger1Id",
                table: "Crews",
                column: "Passenger1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_Passenger2Id",
                table: "Crews",
                column: "Passenger2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_Passenger3Id",
                table: "Crews",
                column: "Passenger3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_Passenger4Id",
                table: "Crews",
                column: "Passenger4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorId",
                table: "Events",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ManagerId",
                table: "Events",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RaceTypeId",
                table: "Events",
                column: "RaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LasfCategoryTranslations_LasfCategoryId",
                table: "LasfCategoryTranslations",
                column: "LasfCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MotoCategoryTranslations_MotoCategoryId",
                table: "MotoCategoryTranslations",
                column: "MotoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceOfficials_EventId",
                table: "RaceOfficials",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceOfficials_UserId",
                table: "RaceOfficials",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceTypeTranslations_RaceTypeId",
                table: "RaceTypeTranslations",
                column: "RaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTranslations_RoleId",
                table: "RoleTranslations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_RaceTypeId",
                table: "UserPreferences",
                column: "RaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId",
                table: "UserPreferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRacePreferences_EventId",
                table: "UserRacePreferences",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRacePreferences_UserId",
                table: "UserRacePreferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LasfCategoryId",
                table: "Users",
                column: "LasfCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MotoCategoryId",
                table: "Users",
                column: "MotoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Wages_RaceOfficialId",
                table: "Wages",
                column: "RaceOfficialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "LasfCategoryTranslations");

            migrationBuilder.DropTable(
                name: "MotoCategoryTranslations");

            migrationBuilder.DropTable(
                name: "RaceTypeTranslations");

            migrationBuilder.DropTable(
                name: "RoleTranslations");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "UserRacePreferences");

            migrationBuilder.DropTable(
                name: "Wages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RaceOfficials");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "RaceTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LasfCategories");

            migrationBuilder.DropTable(
                name: "MotoCategories");
        }
    }
}
