using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQ021_First_Web_App.Migrations
{
    /// <inheritdoc />
    public partial class seedclaimsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO MyClaims (Id, Name) 
                    VALUES ('1', 'CanAdd'), 
                           ('2', 'CanEdit'), 
                           ('3', 'CanDelet')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
