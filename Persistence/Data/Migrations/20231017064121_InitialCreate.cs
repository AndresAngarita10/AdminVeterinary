using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "breed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_breed", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "laboratory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laboratory", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "partnerType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partnerType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rolName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "specialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialty", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "specie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specie", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "typeMovement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeMovement", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GenderIdfk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_gender_GenderIdfk",
                        column: x => x.GenderIdfk,
                        principalTable: "gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantityAvalible = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    price = table.Column<double>(type: "double", nullable: false),
                    LaboratoryIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicine_laboratory_LaboratoryIdFk",
                        column: x => x.LaboratoryIdFk,
                        principalTable: "laboratory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "partner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpecialtyIdFk = table.Column<int>(type: "int", nullable: false),
                    GenderIdFk = table.Column<int>(type: "int", nullable: false),
                    PartnerTypeIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partner_gender_GenderIdFk",
                        column: x => x.GenderIdFk,
                        principalTable: "gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partner_partnerType_PartnerTypeIdFk",
                        column: x => x.PartnerTypeIdFk,
                        principalTable: "partnerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partner_specialty_SpecialtyIdFk",
                        column: x => x.SpecialtyIdFk,
                        principalTable: "specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicineMovement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    dateMovement = table.Column<DateOnly>(type: "date", nullable: false),
                    TypeMovementFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicineMovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicineMovement_typeMovement_TypeMovementFk",
                        column: x => x.TypeMovementFk,
                        principalTable: "typeMovement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "refreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refreshToken_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userRol",
                columns: table => new
                {
                    UserIdFk = table.Column<int>(type: "int", nullable: false),
                    RolIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRol", x => new { x.UserIdFk, x.RolIdFk });
                    table.ForeignKey(
                        name: "FK_userRol_rol_RolIdFk",
                        column: x => x.RolIdFk,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRol_user_UserIdFk",
                        column: x => x.UserIdFk,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicinePartner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MedicineIdFk = table.Column<int>(type: "int", nullable: false),
                    PartnerIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicinePartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicinePartner_medicine_MedicineIdFk",
                        column: x => x.MedicineIdFk,
                        principalTable: "medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicinePartner_partner_PartnerIdFk",
                        column: x => x.PartnerIdFk,
                        principalTable: "partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datebirth = table.Column<DateOnly>(type: "date", nullable: false),
                    UserOwnerId = table.Column<int>(type: "int", nullable: false),
                    SpeciesIdFk = table.Column<int>(type: "int", nullable: false),
                    BreedIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pet_breed_BreedIdFk",
                        column: x => x.BreedIdFk,
                        principalTable: "breed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pet_partner_UserOwnerId",
                        column: x => x.UserOwnerId,
                        principalTable: "partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pet_specie_SpeciesIdFk",
                        column: x => x.SpeciesIdFk,
                        principalTable: "specie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detailMovement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    price = table.Column<double>(type: "double", nullable: false),
                    MedicineIdFk = table.Column<int>(type: "int", nullable: false),
                    MedicineMovementIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailMovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detailMovement_medicineMovement_MedicineMovementIdFk",
                        column: x => x.MedicineMovementIdFk,
                        principalTable: "medicineMovement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detailMovement_medicine_MedicineIdFk",
                        column: x => x.MedicineIdFk,
                        principalTable: "medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "quote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    hour = table.Column<TimeOnly>(type: "time", nullable: false),
                    reason = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PetIdFk = table.Column<int>(type: "int", nullable: false),
                    VeterinarianIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quote_partner_VeterinarianIdFk",
                        column: x => x.VeterinarianIdFk,
                        principalTable: "partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quote_pet_PetIdFk",
                        column: x => x.PetIdFk,
                        principalTable: "pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicalTreatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dateStartTreatment = table.Column<DateOnly>(type: "date", nullable: false),
                    QuoteIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicalTreatment_quote_QuoteIdFk",
                        column: x => x.QuoteIdFk,
                        principalTable: "quote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "descriptionMedicalTreatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dose = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    administrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    observation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicineIdFk = table.Column<int>(type: "int", nullable: false),
                    MedicalTreatmentIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptionMedicalTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_descriptionMedicalTreatment_medicalTreatment_MedicalTreatmen~",
                        column: x => x.MedicalTreatmentIdFk,
                        principalTable: "medicalTreatment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_descriptionMedicalTreatment_medicine_MedicineIdFk",
                        column: x => x.MedicineIdFk,
                        principalTable: "medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_descriptionMedicalTreatment_MedicalTreatmentIdFk",
                table: "descriptionMedicalTreatment",
                column: "MedicalTreatmentIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_descriptionMedicalTreatment_MedicineIdFk",
                table: "descriptionMedicalTreatment",
                column: "MedicineIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_detailMovement_MedicineIdFk",
                table: "detailMovement",
                column: "MedicineIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_detailMovement_MedicineMovementIdFk",
                table: "detailMovement",
                column: "MedicineMovementIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicalTreatment_QuoteIdFk",
                table: "medicalTreatment",
                column: "QuoteIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicine_LaboratoryIdFk",
                table: "medicine",
                column: "LaboratoryIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicineMovement_TypeMovementFk",
                table: "medicineMovement",
                column: "TypeMovementFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicinePartner_MedicineIdFk",
                table: "medicinePartner",
                column: "MedicineIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_medicinePartner_PartnerIdFk",
                table: "medicinePartner",
                column: "PartnerIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_partner_GenderIdFk",
                table: "partner",
                column: "GenderIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_partner_PartnerTypeIdFk",
                table: "partner",
                column: "PartnerTypeIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_partner_SpecialtyIdFk",
                table: "partner",
                column: "SpecialtyIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_pet_BreedIdFk",
                table: "pet",
                column: "BreedIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_pet_SpeciesIdFk",
                table: "pet",
                column: "SpeciesIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_pet_UserOwnerId",
                table: "pet",
                column: "UserOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_quote_PetIdFk",
                table: "quote",
                column: "PetIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_quote_VeterinarianIdFk",
                table: "quote",
                column: "VeterinarianIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_refreshToken_UserId",
                table: "refreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_GenderIdfk",
                table: "user",
                column: "GenderIdfk");

            migrationBuilder.CreateIndex(
                name: "IX_userRol_RolIdFk",
                table: "userRol",
                column: "RolIdFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "descriptionMedicalTreatment");

            migrationBuilder.DropTable(
                name: "detailMovement");

            migrationBuilder.DropTable(
                name: "medicinePartner");

            migrationBuilder.DropTable(
                name: "refreshToken");

            migrationBuilder.DropTable(
                name: "userRol");

            migrationBuilder.DropTable(
                name: "medicalTreatment");

            migrationBuilder.DropTable(
                name: "medicineMovement");

            migrationBuilder.DropTable(
                name: "medicine");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "quote");

            migrationBuilder.DropTable(
                name: "typeMovement");

            migrationBuilder.DropTable(
                name: "laboratory");

            migrationBuilder.DropTable(
                name: "pet");

            migrationBuilder.DropTable(
                name: "breed");

            migrationBuilder.DropTable(
                name: "partner");

            migrationBuilder.DropTable(
                name: "specie");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "partnerType");

            migrationBuilder.DropTable(
                name: "specialty");
        }
    }
}
