﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnerIdFk",
                table: "medicineMovement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_medicineMovement_PartnerIdFk",
                table: "medicineMovement",
                column: "PartnerIdFk");

            migrationBuilder.AddForeignKey(
                name: "FK_medicineMovement_partner_PartnerIdFk",
                table: "medicineMovement",
                column: "PartnerIdFk",
                principalTable: "partner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medicineMovement_partner_PartnerIdFk",
                table: "medicineMovement");

            migrationBuilder.DropIndex(
                name: "IX_medicineMovement_PartnerIdFk",
                table: "medicineMovement");

            migrationBuilder.DropColumn(
                name: "PartnerIdFk",
                table: "medicineMovement");
        }
    }
}