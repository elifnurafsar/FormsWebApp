using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class BugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Form_formid",
                table: "Field");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Users_usere_mail",
                table: "Form");

            migrationBuilder.DropIndex(
                name: "IX_Form_usere_mail",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "usere_mail",
                table: "Form");

            migrationBuilder.RenameColumn(
                name: "formid",
                table: "Field",
                newName: "Formid");

            migrationBuilder.RenameIndex(
                name: "IX_Field_formid",
                table: "Field",
                newName: "IX_Field_Formid");

            migrationBuilder.AlterColumn<int>(
                name: "Formid",
                table: "Field",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Form_Formid",
                table: "Field",
                column: "Formid",
                principalTable: "Form",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Form_Formid",
                table: "Field");

            migrationBuilder.RenameColumn(
                name: "Formid",
                table: "Field",
                newName: "formid");

            migrationBuilder.RenameIndex(
                name: "IX_Field_Formid",
                table: "Field",
                newName: "IX_Field_formid");

            migrationBuilder.AddColumn<string>(
                name: "usere_mail",
                table: "Form",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "formid",
                table: "Field",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Form_usere_mail",
                table: "Form",
                column: "usere_mail");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Form_formid",
                table: "Field",
                column: "formid",
                principalTable: "Form",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Form_Users_usere_mail",
                table: "Form",
                column: "usere_mail",
                principalTable: "Users",
                principalColumn: "e_mail",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
