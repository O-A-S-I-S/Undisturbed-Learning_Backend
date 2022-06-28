using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UndisturbedLearning.DataAccess.Migrations
{
    public partial class AddedCauseEntityAndReminderToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cause",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Reminder",
                table: "Appointments",
                newName: "ReminderStudent");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Reports",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Reports",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Brief",
                table: "Reports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Appointments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CauseDescription",
                table: "Appointments",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(140)",
                oldMaxLength: 140);

            migrationBuilder.AddColumn<int>(
                name: "CauseId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ReminderPsychopedagogist",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Causes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Causes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CauseId",
                table: "Appointments",
                column: "CauseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Causes_CauseId",
                table: "Appointments",
                column: "CauseId",
                principalTable: "Causes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Causes_CauseId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Causes");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CauseId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CauseId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ReminderPsychopedagogist",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ReminderStudent",
                table: "Appointments",
                newName: "Reminder");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Reports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Reports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Brief",
                table: "Reports",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Appointments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "CauseDescription",
                table: "Appointments",
                type: "nvarchar(140)",
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AddColumn<string>(
                name: "Cause",
                table: "Appointments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
