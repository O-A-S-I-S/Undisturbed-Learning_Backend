using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UndisturbedLearning.DataAccess.Migrations
{
    public partial class RemoveExtraStudentWorkshopRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentWorkshops");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentWorkshops",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWorkshops", x => new { x.StudentId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_StudentWorkshops_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentWorkshops_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorkshops_WorkshopId",
                table: "StudentWorkshops",
                column: "WorkshopId");
        }
    }
}
