using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopcornBackend.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "Singers",
                columns: table => new
                {
                    SingerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singers", x => x.SingerId);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlternateMobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "U"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: true),
                    SubscriptionStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubscriptionEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproved = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId");
                });

            migrationBuilder.CreateTable(
                name: "ClientsMedia",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsMedia", x => new { x.ClientId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_ClientsMedia_MediaTypes_MediaId",
                        column: x => x.MediaId,
                        principalTable: "MediaTypes",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsMedia_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoviePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MoviePoster = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MovieDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_MediaCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MediaCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SongLyrics = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SongType = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SongGeneration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SongPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SongPoster = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SongDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Songs_MediaCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MediaCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Songs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShows",
                columns: table => new
                {
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TvShowName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TvShowDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TvShowPoster = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TvShowPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShows", x => x.TvShowId);
                    table.ForeignKey(
                        name: "FK_TvShows_MediaCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MediaCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShows_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavMovies",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavMovies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_FavMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId");
                    table.ForeignKey(
                        name: "FK_FavMovies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavSongs",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavSongs", x => new { x.UserId, x.SongId });
                    table.ForeignKey(
                        name: "FK_FavSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId");
                    table.ForeignKey(
                        name: "FK_FavSongs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongSingers",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    SingerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongSingers", x => new { x.SongId, x.SingerId });
                    table.ForeignKey(
                        name: "FK_SongSingers_Singers_SingerId",
                        column: x => x.SingerId,
                        principalTable: "Singers",
                        principalColumn: "SingerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongSingers_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavTvShow",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavTvShow", x => new { x.UserId, x.TvShowId });
                    table.ForeignKey(
                        name: "FK_FavTvShow_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId");
                    table.ForeignKey(
                        name: "FK_FavTvShow_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientsMedia_MediaId",
                table: "ClientsMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_FavMovies_MovieId",
                table: "FavMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_FavSongs_SongId",
                table: "FavSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_FavTvShow_TvShowId",
                table: "FavTvShow",
                column: "TvShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieName",
                table: "Movies",
                column: "MovieName");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_CategoryId",
                table: "Songs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SongLyrics",
                table: "Songs",
                column: "SongLyrics");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SongName",
                table: "Songs",
                column: "SongName");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UserId",
                table: "Songs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SongSingers_SingerId",
                table: "SongSingers",
                column: "SingerId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_CategoryId",
                table: "TvShows",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_TvShowName",
                table: "TvShows",
                column: "TvShowName");

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_UserId",
                table: "TvShows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionId",
                table: "Users",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientsMedia");

            migrationBuilder.DropTable(
                name: "FavMovies");

            migrationBuilder.DropTable(
                name: "FavSongs");

            migrationBuilder.DropTable(
                name: "FavTvShow");

            migrationBuilder.DropTable(
                name: "SongSingers");

            migrationBuilder.DropTable(
                name: "MediaTypes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "TvShows");

            migrationBuilder.DropTable(
                name: "Singers");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "MediaCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
