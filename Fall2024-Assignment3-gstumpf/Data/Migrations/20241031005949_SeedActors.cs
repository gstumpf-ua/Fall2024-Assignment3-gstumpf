using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fall2024_Assignment3_gstumpf.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedActors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Age", "Gender", "IMDBLink", "Name", "PhotoUrl" },
                values: new object[,]
                {
                    { 1, 60, "Male", "https://www.imdb.com/name/nm0000206/", "Keanu Reeves", "https://example.com/keanu.jpg" },
                    { 2, 49, "Male", "https://www.imdb.com/name/nm0000138/", "Leonardo DiCaprio", "https://example.com/leonardo.jpg" },
                    { 3, 65, "Male", "https://www.imdb.com/name/nm0000600/", "Tim Robbins", "https://example.com/timrobbins.jpg" },
                    { 4, 50, "Male", "https://www.imdb.com/name/nm0000288/", "Christian Bale", "https://example.com/christianbale.jpg" },
                    { 5, 67, "Male", "https://www.imdb.com/name/nm0000158/", "Tom Hanks", "https://example.com/tomhanks.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Genre", "IMDBLink", "PosterUrl", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Sci-Fi", "https://www.imdb.com/title/tt0133093/", "https://example.com/matrix.jpg", "The Matrix", 1999 },
                    { 2, "Sci-Fi", "https://www.imdb.com/title/tt1375666/", "https://example.com/inception.jpg", "Inception", 2010 },
                    { 3, "Drama", "https://www.imdb.com/title/tt0111161/", "https://example.com/shawshank.jpg", "The Shawshank Redemption", 1994 },
                    { 4, "Action", "https://www.imdb.com/title/tt0468569/", "https://example.com/darkknight.jpg", "The Dark Knight", 2008 },
                    { 5, "Drama", "https://www.imdb.com/title/tt0109830/", "https://example.com/forrestgump.jpg", "Forrest Gump", 1994 }
                });

            migrationBuilder.InsertData(
                table: "MovieActors",
                columns: new[] { "ActorId", "MovieId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 2, 0 },
                    { 3, 3, 0 },
                    { 4, 4, 0 },
                    { 5, 5, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MovieActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MovieActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "MovieActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "MovieActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "MovieActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
