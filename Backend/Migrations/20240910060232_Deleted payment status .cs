using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Deletedpaymentstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentHistory");

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PremiumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserDetailsUserID = table.Column<int>(type: "int", nullable: true),
                    PolicyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_InsurancePolicies_PolicyID",
                        column: x => x.PolicyID,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyID");
                    table.ForeignKey(
                        name: "FK_PaymentDetails_UserDetails_UserDetailsUserID",
                        column: x => x.UserDetailsUserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_PolicyID",
                table: "PaymentDetails",
                column: "PolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_UserDetailsUserID",
                table: "PaymentDetails",
                column: "UserDetailsUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyID = table.Column<int>(type: "int", nullable: true),
                    UserDetailsUserID = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PremiumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_InsurancePolicies_PolicyID",
                        column: x => x.PolicyID,
                        principalTable: "InsurancePolicies",
                        principalColumn: "PolicyID");
                    table.ForeignKey(
                        name: "FK_PaymentHistory_UserDetails_UserDetailsUserID",
                        column: x => x.UserDetailsUserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_PolicyID",
                table: "PaymentHistory",
                column: "PolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_UserDetailsUserID",
                table: "PaymentHistory",
                column: "UserDetailsUserID");
        }
    }
}
