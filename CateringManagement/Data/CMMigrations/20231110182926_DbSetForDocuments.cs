using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringManagement.Data.CMMigrations
{
    /// <inheritdoc />
    public partial class DbSetForDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_UploadedFile_FileContentID",
                table: "FileContent");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFile_Functions_FunctionID",
                table: "UploadedFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedFile",
                table: "UploadedFile");

            migrationBuilder.RenameTable(
                name: "UploadedFile",
                newName: "UploadedFiles");

            migrationBuilder.RenameIndex(
                name: "IX_UploadedFile_FunctionID",
                table: "UploadedFiles",
                newName: "IX_UploadedFiles_FunctionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedFiles",
                table: "UploadedFiles",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_UploadedFiles_FileContentID",
                table: "FileContent",
                column: "FileContentID",
                principalTable: "UploadedFiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFiles_Functions_FunctionID",
                table: "UploadedFiles",
                column: "FunctionID",
                principalTable: "Functions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_UploadedFiles_FileContentID",
                table: "FileContent");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFiles_Functions_FunctionID",
                table: "UploadedFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedFiles",
                table: "UploadedFiles");

            migrationBuilder.RenameTable(
                name: "UploadedFiles",
                newName: "UploadedFile");

            migrationBuilder.RenameIndex(
                name: "IX_UploadedFiles_FunctionID",
                table: "UploadedFile",
                newName: "IX_UploadedFile_FunctionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedFile",
                table: "UploadedFile",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_UploadedFile_FileContentID",
                table: "FileContent",
                column: "FileContentID",
                principalTable: "UploadedFile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFile_Functions_FunctionID",
                table: "UploadedFile",
                column: "FunctionID",
                principalTable: "Functions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
