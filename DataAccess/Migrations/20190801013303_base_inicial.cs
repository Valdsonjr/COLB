using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class base_inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_REQUISICAO",
                columns: table => new
                {
                    NR_REQUISICAO = table.Column<long>(nullable: false),
                    DS_REQUISICAO = table.Column<string>(nullable: false),
                    DT_SOLICITACAO = table.Column<DateTime>(nullable: false),
                    NM_SOLICITANTE = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_REQUISICAO", x => x.NR_REQUISICAO);
                });

            migrationBuilder.CreateTable(
                name: "TB_SOLUCAO",
                columns: table => new
                {
                    CD_SOLUCAO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NM_SOLUCAO = table.Column<string>(nullable: false),
                    DS_SOLUCAO = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SOLUCAO", x => x.CD_SOLUCAO);
                });

            migrationBuilder.CreateTable(
                name: "TB_ORDEM_DE_LIBERACAO",
                columns: table => new
                {
                    NR_ORDEM_DE_LIBERACAO = table.Column<long>(nullable: false),
                    NR_REQUISICAO = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDEM_DE_LIBERACAO", x => x.NR_ORDEM_DE_LIBERACAO);
                    table.ForeignKey(
                        name: "FK_TB_ORDEM_DE_LIBERACAO_TB_REQUISICAO_NR_REQUISICAO",
                        column: x => x.NR_REQUISICAO,
                        principalTable: "TB_REQUISICAO",
                        principalColumn: "NR_REQUISICAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PROJETO",
                columns: table => new
                {
                    CD_PROJETO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NM_PROJETO = table.Column<string>(nullable: false),
                    DS_PROJETO = table.Column<string>(nullable: false),
                    CD_SOLUCAO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PROJETO", x => x.CD_PROJETO);
                    table.ForeignKey(
                        name: "FK_TB_PROJETO_TB_SOLUCAO_CD_SOLUCAO",
                        column: x => x.CD_SOLUCAO,
                        principalTable: "TB_SOLUCAO",
                        principalColumn: "CD_SOLUCAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PROJETO_AFETADO",
                columns: table => new
                {
                    CD_PROJETO = table.Column<int>(nullable: false),
                    NR_ORDEM_DE_LIBERACAO = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PROJETO_AFETADO", x => new { x.CD_PROJETO, x.NR_ORDEM_DE_LIBERACAO });
                    table.ForeignKey(
                        name: "FK_TB_PROJETO_AFETADO_TB_PROJETO_CD_PROJETO",
                        column: x => x.CD_PROJETO,
                        principalTable: "TB_PROJETO",
                        principalColumn: "CD_PROJETO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PROJETO_AFETADO_TB_ORDEM_DE_LIBERACAO_NR_ORDEM_DE_LIBERACAO",
                        column: x => x.NR_ORDEM_DE_LIBERACAO,
                        principalTable: "TB_ORDEM_DE_LIBERACAO",
                        principalColumn: "NR_ORDEM_DE_LIBERACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_SOLUCAO",
                columns: new[] { "CD_SOLUCAO", "DS_SOLUCAO", "NM_SOLUCAO" },
                values: new object[] { 1, "Api privada e batches", "ssc-serviços" });

            migrationBuilder.InsertData(
                table: "TB_SOLUCAO",
                columns: new[] { "CD_SOLUCAO", "DS_SOLUCAO", "NM_SOLUCAO" },
                values: new object[] { 2, "Sistema interno da SEAC", "ssc-modulos" });

            migrationBuilder.InsertData(
                table: "TB_SOLUCAO",
                columns: new[] { "CD_SOLUCAO", "DS_SOLUCAO", "NM_SOLUCAO" },
                values: new object[] { 3, "Websites e apis públicas", "ssc-canais" });

            migrationBuilder.InsertData(
                table: "TB_PROJETO",
                columns: new[] { "CD_PROJETO", "CD_SOLUCAO", "DS_PROJETO", "NM_PROJETO" },
                values: new object[,]
                {
                    { 1, 1, "Api Benefícios (Voucher)", "Api.Beneficios" },
                    { 2, 1, "Api Cartão", "Api.Cartao" },
                    { 3, 1, "Api Cliente", "Api.Cliente" },
                    { 4, 1, "Api Funcionário", "Api.Funcionario" },
                    { 5, 1, "Api Gerenciamento", "Api.Gerenciamento" },
                    { 6, 1, "Api Lojista", "Api.Lojista" },
                    { 7, 1, "Api Movimento", "Api.Movimento" },
                    { 8, 1, "Api Seguranca", "Api.Seguranca" },
                    { 9, 2, "Módulo Cliente", "SSC.Cliente" },
                    { 10, 2, "Módulo Lojista", "SSC.Lojista" },
                    { 11, 2, "Módulo Gerenciamento", "SSC.Gerenciamento" },
                    { 12, 3, "Portal Cliente", "PortalCliente" },
                    { 13, 3, "Portal Lojista", "PortalLojista" },
                    { 14, 3, "Api pública Assistente", "PortalAssistente" },
                    { 15, 3, "Api pública Benefícios", "PortalBeneficios" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ORDEM_DE_LIBERACAO_NR_REQUISICAO",
                table: "TB_ORDEM_DE_LIBERACAO",
                column: "NR_REQUISICAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROJETO_CD_SOLUCAO",
                table: "TB_PROJETO",
                column: "CD_SOLUCAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROJETO_AFETADO_NR_ORDEM_DE_LIBERACAO",
                table: "TB_PROJETO_AFETADO",
                column: "NR_ORDEM_DE_LIBERACAO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PROJETO_AFETADO");

            migrationBuilder.DropTable(
                name: "TB_PROJETO");

            migrationBuilder.DropTable(
                name: "TB_ORDEM_DE_LIBERACAO");

            migrationBuilder.DropTable(
                name: "TB_SOLUCAO");

            migrationBuilder.DropTable(
                name: "TB_REQUISICAO");
        }
    }
}
