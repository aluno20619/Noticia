using Microsoft.EntityFrameworkCore.Migrations;

namespace Noticia.Data.Migrations
{
    public partial class seedInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Imagens",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topicos",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Imagens",
                columns: new[] { "Id", "Legenda", "Nome" },
                values: new object[] { 1, "Fundação Maria Inácia Perdigão Vogado da Silva, em Reguengos de Monsaraz, onde morreram 18 pessoas infetadas com covid-19", "image.jpg" });

            migrationBuilder.InsertData(
                table: "Topicos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Covid19" });
        }
    }
}
