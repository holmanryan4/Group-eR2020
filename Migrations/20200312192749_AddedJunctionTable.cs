using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Migrations
{
    public partial class AddedJunctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Group_GroupId",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_GroupId",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "UserAccount");

            migrationBuilder.CreateTable(
                name: "UserAccountGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccountGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccountGroups_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountGroups_GroupId",
                table: "UserAccountGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountGroups_UserId",
                table: "UserAccountGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccountGroups");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "UserAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_GroupId",
                table: "UserAccount",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Group_GroupId",
                table: "UserAccount",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
