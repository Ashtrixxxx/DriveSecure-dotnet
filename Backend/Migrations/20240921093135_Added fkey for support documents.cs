using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class Addedfkeyforsupportdocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PolicyID",
                table: "SupportDocuments",
                type: "int",
                nullable: true, // Change this to true if you want to allow nulls
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupportDocuments_PolicyID",
                table: "SupportDocuments",
                column: "PolicyID");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportDocuments_InsurancePolicies_PolicyID",
                table: "SupportDocuments",
                column: "PolicyID",
                principalTable: "InsurancePolicies",
                principalColumn: "PolicyID",
                onDelete: ReferentialAction.NoAction); // Change this if you prefer a different behavior
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportDocuments_InsurancePolicies_PolicyID",
                table: "SupportDocuments");

            migrationBuilder.DropIndex(
                name: "IX_SupportDocuments_PolicyID",
                table: "SupportDocuments");

            migrationBuilder.DropColumn(
                name: "PolicyID",
                table: "SupportDocuments");
        }
    }
}
