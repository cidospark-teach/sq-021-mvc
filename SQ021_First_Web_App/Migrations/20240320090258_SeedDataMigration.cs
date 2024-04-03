using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQ021_First_Web_App.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO Dogs (Id, Name, Description, PhotoUrl) 
                    VALUES ('1', 'Bingo', 'Bingo is friendly fellow', '~/images/dog_1.jpg'), 
                           ('2', 'Lucky', 'Lucky is friendly fellow', '~/images/dog_2.jpg'), 
                           ('3', 'Gentle', 'Gentle is friendly fellow', '~/images/dog_3.jpg'), 
                           ('4', 'Jack', 'Jack is friendly fellow', '~/images/dog_4.jpg'), 
                           ('5', 'Fluffy', 'Fluffy is friendly fellow', '~/images/dog_5.jpg'), 
                           ('6', 'Alpha', 'Alpha is friendly fellow', '~/images/dog_6.jpg') 
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
