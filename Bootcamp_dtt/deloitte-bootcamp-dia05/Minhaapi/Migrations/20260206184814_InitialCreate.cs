using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Minhaapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lotes_minerio",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_lote = table.Column<string>(type: "text", nullable: false),
                    mina_origem = table.Column<string>(type: "text", nullable: false),
                    localizacao_atual = table.Column<string>(type: "text", nullable: false),
                    teor_fe = table.Column<double>(type: "double precision", nullable: false),
                    umidade = table.Column<double>(type: "double precision", nullable: false),
                    si_o2 = table.Column<double>(type: "double precision", nullable: true),
                    p = table.Column<double>(type: "double precision", nullable: true),
                    toneladas = table.Column<double>(type: "double precision", nullable: false),
                    data_producao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotes_minerio", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lotes_minerio");
        }
    }
}
