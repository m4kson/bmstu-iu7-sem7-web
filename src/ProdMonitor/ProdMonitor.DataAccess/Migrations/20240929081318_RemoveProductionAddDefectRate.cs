using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdMonitor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductionAddDefectRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Production",
                table: "AssemblyLines");

            migrationBuilder.AlterColumn<float>(
                name: "DefectRate",
                table: "AssemblyLines",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DefectRate",
                table: "AssemblyLines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "Production",
                table: "AssemblyLines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
