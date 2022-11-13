using Microsoft.EntityFrameworkCore.Migrations;

namespace infrastructure.Migrations
{
    public partial class changeRelashion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttrebiuteValue_Attribute_AttributeAttrebiuteId",
                schema: "Shop",
                table: "AttrebiuteValue");

            migrationBuilder.AlterColumn<int>(
                name: "AttributeAttrebiuteId",
                schema: "Shop",
                table: "AttrebiuteValue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttrebiuteValue_Attribute_AttributeAttrebiuteId",
                schema: "Shop",
                table: "AttrebiuteValue",
                column: "AttributeAttrebiuteId",
                principalSchema: "Shop",
                principalTable: "Attribute",
                principalColumn: "AttrebiuteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttrebiuteValue_Attribute_AttributeAttrebiuteId",
                schema: "Shop",
                table: "AttrebiuteValue");

            migrationBuilder.AlterColumn<int>(
                name: "AttributeAttrebiuteId",
                schema: "Shop",
                table: "AttrebiuteValue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AttrebiuteValue_Attribute_AttributeAttrebiuteId",
                schema: "Shop",
                table: "AttrebiuteValue",
                column: "AttributeAttrebiuteId",
                principalSchema: "Shop",
                principalTable: "Attribute",
                principalColumn: "AttrebiuteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
