using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class ResidentialAddressIdNullAble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_ResidentialAddresses_ResidentialAddressId",
                table: "PersonalInformation");

            migrationBuilder.AlterColumn<int>(
                name: "ResidentialAddressId",
                table: "PersonalInformation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformation_ResidentialAddresses_ResidentialAddressId",
                table: "PersonalInformation",
                column: "ResidentialAddressId",
                principalTable: "ResidentialAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_ResidentialAddresses_ResidentialAddressId",
                table: "PersonalInformation");

            migrationBuilder.AlterColumn<int>(
                name: "ResidentialAddressId",
                table: "PersonalInformation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformation_ResidentialAddresses_ResidentialAddressId",
                table: "PersonalInformation",
                column: "ResidentialAddressId",
                principalTable: "ResidentialAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
