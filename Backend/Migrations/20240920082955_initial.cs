using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserPass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    PolicyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverageStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CoverageEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsRenewed = table.Column<bool>(type: "bit", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.PolicyID);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RCProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_SupportDocuments_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempFormData",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetailsUserID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempFormData", x => x.FormId);
                    table.ForeignKey(
                        name: "FK_TempFormData_UserDetails_UserDetailsUserID",
                        column: x => x.UserDetailsUserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetailsUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_UserAddress_UserDetails_UserDetailsUserID",
                        column: x => x.UserDetailsUserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
     name: "PaymentDetails",
     columns: table => new
     {
         PaymentId = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         PremiumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
         PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
         PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
         UserID = table.Column<int>(type: "int", nullable: false),
         PolicyID = table.Column<int>(type: "int", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_PaymentDetails", x => x.PaymentId);

         table.ForeignKey(
             name: "FK_PaymentDetails_InsurancePolicies_PolicyID",
             column: x => x.PolicyID,
             principalTable: "InsurancePolicies",
             principalColumn: "PolicyID",
             onDelete: ReferentialAction.Cascade); // Keep cascade here

         table.ForeignKey(
             name: "FK_PaymentDetails_UserDetails_UserID",
             column: x => x.UserID,
             principalTable: "UserDetails",
             principalColumn: "UserID",
             onDelete: ReferentialAction.Restrict); // Use restrict here to avoid cycles
     });


            migrationBuilder.CreateTable(
      name: "VehicleDetails",
      columns: table => new
      {
          VehicleId = table.Column<int>(type: "int", nullable: false)
              .Annotation("SqlServer:Identity", "1, 1"),
          VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
          EngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
          EngineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          YearOfManufacture = table.Column<int>(type: "int", nullable: false),
          NumberOfSeats = table.Column<int>(type: "int", nullable: false),
          FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          ListPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
          LicensePlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
          VehicleCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
          ServiceHistory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
          LastServiceDate = table.Column<DateOnly>(type: "date", nullable: false),
          VehicleIdentificationNumber = table.Column<int>(type: "int", nullable: false),
          TransmissionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          NumberOfPreviousOwners = table.Column<int>(type: "int", nullable: false),
          RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
          UserID = table.Column<int>(type: "int", nullable: false),
          PolicyID = table.Column<int>(type: "int", nullable: false)
      },
      constraints: table =>
      {
          table.PrimaryKey("PK_VehicleDetails", x => x.VehicleId);
          table.ForeignKey(
              name: "FK_VehicleDetails_InsurancePolicies_PolicyID",
              column: x => x.PolicyID,
              principalTable: "InsurancePolicies",
              principalColumn: "PolicyID",
              onDelete: ReferentialAction.Cascade); // Keep cascade here
          table.ForeignKey(
              name: "FK_VehicleDetails_UserDetails_UserID",
              column: x => x.UserID,
              principalTable: "UserDetails",
              principalColumn: "UserID",
              onDelete: ReferentialAction.Restrict); // Change to Restrict or NoAction
      });


            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_UserID",
                table: "InsurancePolicies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_PolicyID",
                table: "PaymentDetails",
                column: "PolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_UserID",
                table: "PaymentDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportDocuments_UserID",
                table: "SupportDocuments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TempFormData_UserDetailsUserID",
                table: "TempFormData",
                column: "UserDetailsUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_UserDetailsUserID",
                table: "UserAddress",
                column: "UserDetailsUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_Email",
                table: "UserDetails",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserName",
                table: "UserDetails",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDetails_PolicyID",
                table: "VehicleDetails",
                column: "PolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDetails_UserID",
                table: "VehicleDetails",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "SupportDocuments");

            migrationBuilder.DropTable(
                name: "TempFormData");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "VehicleDetails");

            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
