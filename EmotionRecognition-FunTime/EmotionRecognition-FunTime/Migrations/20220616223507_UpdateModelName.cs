using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmotionRecognition_FunTime.Migrations
{
    public partial class UpdateModelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "time",
                table: "TextAnalyticModel",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "sentiment",
                table: "TextAnalyticModel",
                newName: "Sentiment");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TextAnalyticModel",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "TextAnalyticModel",
                newName: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "TextAnalyticModel",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Sentiment",
                table: "TextAnalyticModel",
                newName: "sentiment");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TextAnalyticModel",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "TextAnalyticModel",
                newName: "location");
        }
    }
}
