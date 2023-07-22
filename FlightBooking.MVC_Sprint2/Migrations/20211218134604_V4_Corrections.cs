using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightBooking.MVC_Sprint2.Migrations
{
    public partial class V4_Corrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgnetId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpiId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "UPI",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UPI_ID",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UPI",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UPI_ID",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "AgnetId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpiId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
