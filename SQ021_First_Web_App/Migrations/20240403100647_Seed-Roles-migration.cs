using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQ021_First_Web_App.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES ('1', 'regular', 'REGULAR', '7e873450-ec0b-4961-9db9-6257541797fe'), 
                           ('2', 'editor', 'EDITOR', 'c88ebfe5-a2d7-4bb4-97ef-3071c72dc3b1'), 
                           ('3', 'admin', 'ADMIN', '2aa54d2d-6609-4d82-a465-c7e94df5d481')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
