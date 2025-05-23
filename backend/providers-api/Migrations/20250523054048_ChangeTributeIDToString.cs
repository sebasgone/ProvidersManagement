using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace providers_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTributeIDToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TributeID",
                schema: "SchemaSPA",
                table: "Providers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "SchemaSPA",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TributeID",
                schema: "SchemaSPA",
                table: "Providers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                schema: "SchemaSPA",
                table: "Providers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
