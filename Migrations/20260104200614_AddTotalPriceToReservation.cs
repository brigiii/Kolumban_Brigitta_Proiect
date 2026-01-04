using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolumban_Brigitta_Proiect.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Reservation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Reservation");
        }
    }
}
