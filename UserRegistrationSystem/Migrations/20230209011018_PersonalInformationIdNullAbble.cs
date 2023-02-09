using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class PersonalInformationIdNullAbble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_PersonalInformation_PersonalInformationId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalInformationId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_PersonalInformation_PersonalInformationId",
                table: "Accounts",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_PersonalInformation_PersonalInformationId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalInformationId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_PersonalInformation_PersonalInformationId",
                table: "Accounts",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
