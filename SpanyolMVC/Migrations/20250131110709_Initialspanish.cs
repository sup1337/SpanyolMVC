using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpanyolMVC.Migrations
{
    /// <inheritdoc />
    public partial class Initialspanish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hungarian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    English = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Italian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    French = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    German = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Infinitive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gerund = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Participle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imperfect1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imperfect2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imperfect3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imperfect4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imperfect5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imperfect6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indefinite1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indefinite2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indefinite3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indefinite4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indefinite5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indefinite6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Future1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Future2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Future3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Future4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Future5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Future6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditional1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditional2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditional3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditional4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditional5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditional6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctivePresent1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctivePresent2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctivePresent3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctivePresent4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctivePresent5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctivePresent6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctiveImperfect1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctiveImperfect2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctiveImperfect3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctiveImperfect4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctiveImperfect5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjunctiveImperfect6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativePositive2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativePositive3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativePositive4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativePositive5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativePositive6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativeNegative2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativeNegative3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativeNegative4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativeNegative5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImperativeNegative6 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
