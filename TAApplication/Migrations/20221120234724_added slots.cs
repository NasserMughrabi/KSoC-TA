using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAApplication.Migrations
{
    public partial class addedslots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayAndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    UserIDId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Slot_AspNetUsers_UserIDId",
                        column: x => x.UserIDId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slot_UserIDId",
                table: "Slot",
                column: "UserIDId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slot");
        }
    }
}
