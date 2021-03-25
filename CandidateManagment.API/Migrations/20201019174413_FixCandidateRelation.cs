using Microsoft.EntityFrameworkCore.Migrations;

namespace CandidateManagment.API.Migrations
{
    public partial class FixCandidateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_CandidateEmployers_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateEmployers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Candidates_AspNetUsers_UserId",
                table: "Tbl_Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_CandidateSkills_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateSkills");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Candidates_UserId",
                table: "Tbl_Candidates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tbl_Candidates");

            migrationBuilder.AddColumn<int>(
                name: "Tbl_CandidateId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Tbl_CandidateId",
                table: "AspNetUsers",
                column: "Tbl_CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tbl_Candidates_Tbl_CandidateId",
                table: "AspNetUsers",
                column: "Tbl_CandidateId",
                principalTable: "Tbl_Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_CandidateEmployers_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateEmployers",
                column: "CandidateId",
                principalTable: "Tbl_Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_CandidateSkills_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateSkills",
                column: "CandidateId",
                principalTable: "Tbl_Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tbl_Candidates_Tbl_CandidateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_CandidateEmployers_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateEmployers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_CandidateSkills_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateSkills");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Tbl_CandidateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Tbl_CandidateId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tbl_Candidates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Candidates_UserId",
                table: "Tbl_Candidates",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_CandidateEmployers_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateEmployers",
                column: "CandidateId",
                principalTable: "Tbl_Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Candidates_AspNetUsers_UserId",
                table: "Tbl_Candidates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_CandidateSkills_Tbl_Candidates_CandidateId",
                table: "Tbl_CandidateSkills",
                column: "CandidateId",
                principalTable: "Tbl_Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
