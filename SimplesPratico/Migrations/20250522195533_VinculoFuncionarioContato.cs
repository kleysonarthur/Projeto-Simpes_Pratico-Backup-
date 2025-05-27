using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplesPratico.Migrations
{
    /// <inheritdoc />
    public partial class VinculoFuncionarioContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_FuncionarioId",
                table: "Clientes",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Funcionarios_FuncionarioId",
                table: "Clientes",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Funcionarios_FuncionarioId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_FuncionarioId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
