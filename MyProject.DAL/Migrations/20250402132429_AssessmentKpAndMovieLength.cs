using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AssessmentKpAndMovieLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AssessmentKinopoisk",
                table: "Movies",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Movies",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentKinopoisk",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Movies");
        }
    }
}
