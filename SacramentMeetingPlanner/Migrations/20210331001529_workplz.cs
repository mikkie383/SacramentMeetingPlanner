using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeetingPlanner.Migrations
{
    public partial class workplz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Benediction",
                table: "Planner");

            migrationBuilder.DropColumn(
                name: "Conducting",
                table: "Planner");

            migrationBuilder.DropColumn(
                name: "Invocation",
                table: "Planner");

            migrationBuilder.DropColumn(
                name: "Speaker",
                table: "Planner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Benediction",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Conducting",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Invocation",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Speaker",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
