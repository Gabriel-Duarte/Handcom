using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handcom.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "AspNetUsers",
                type: "varchar(Max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(Max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "AspNetUsers",
                type: "varchar(Max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(Max)",
                oldNullable: true);
        }
    }
}
