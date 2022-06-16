using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmotionRecognition_FunTime.Migrations
{
    public partial class UpdateUserQuestionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "TextAnalyticModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "sentiment",
                table: "TextAnalyticModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasUser",
                table: "QuestionUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFollowing",
                table: "QuestionUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sentiment",
                table: "TextAnalyticModel");

            migrationBuilder.DropColumn(
                name: "HasUser",
                table: "QuestionUsers");

            migrationBuilder.DropColumn(
                name: "IsFollowing",
                table: "QuestionUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reason",
                table: "TextAnalyticModel",
                type: "int",
                nullable: true);
        }
    }
}
