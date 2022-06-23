using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UndisturbedLearning.DataAccess.Migrations
{
    public partial class AddedNecessaryAttributesAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Appointments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Virtual",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Virtual",
                table: "Appointments");
        }
    }
}
