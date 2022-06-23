using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UndisturbedLearning.DataAccess.Migrations
{
    public partial class AddedActivityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cause",
                table: "Appointments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ActivityId",
                table: "Appointments",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Activities_ActivityId",
                table: "Appointments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Activities_ActivityId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ActivityId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Cause",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Appointments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
