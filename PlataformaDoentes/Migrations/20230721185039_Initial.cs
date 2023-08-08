using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataConsulta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Especialidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Num_Processo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Genero = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Num_Processo);
                });

            migrationBuilder.CreateTable(
                name: "Consultas_Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPaciente = table.Column<int>(type: "INTEGER", nullable: false),
                    IdConsulta = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_Consultas_IdConsulta",
                        column: x => x.IdConsulta,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Num_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_Pacientes_IdConsulta",
                table: "Consultas_Pacientes",
                column: "IdConsulta");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_Pacientes_IdPaciente",
                table: "Consultas_Pacientes",
                column: "IdPaciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas_Pacientes");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
