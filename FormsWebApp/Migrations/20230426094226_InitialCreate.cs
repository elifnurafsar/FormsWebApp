using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    e_mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.e_mail);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<bool>(type: "bit", nullable: false),
                    user_e_mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usere_mail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.id);
                    table.ForeignKey(
                        name: "FK_Form_Users_usere_mail",
                        column: x => x.usere_mail,
                        principalTable: "Users",
                        principalColumn: "e_mail",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    required = table.Column<bool>(type: "bit", nullable: false),
                    form_id = table.Column<int>(type: "int", nullable: false),
                    formid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.id);
                    table.ForeignKey(
                        name: "FK_Field_Form_formid",
                        column: x => x.formid,
                        principalTable: "Form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_formid",
                table: "Field",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_Form_usere_mail",
                table: "Form",
                column: "usere_mail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
