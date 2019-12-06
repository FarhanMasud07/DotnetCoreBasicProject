using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRDHighSchool.Migrations
{
    public partial class PrdDotNetDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentName = table.Column<string>(nullable: false),
                    StudentId = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    Section = table.Column<string>(nullable: false),
                    FixedPrice = table.Column<double>(nullable: false),
                    Ammount = table.Column<double>(nullable: false),
                    CollectionDate = table.Column<string>(nullable: false),
                    PaymentType = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    DateofBirth = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentItems");
        }
    }
}
