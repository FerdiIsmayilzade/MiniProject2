using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class CreatedArchiveCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveCategories_Categories_CategoryId",
                table: "ArchiveCategories");

            migrationBuilder.DropIndex(
                name: "IX_ArchiveCategories_CategoryId",
                table: "ArchiveCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArchiveCategories_CategoryId",
                table: "ArchiveCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveCategories_Categories_CategoryId",
                table: "ArchiveCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
