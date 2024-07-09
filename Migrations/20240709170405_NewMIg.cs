using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ControlboxLibreriaAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewMIg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCategoria = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    FirebaseUserId = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.FirebaseUserId);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    LibroId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Autor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Resumen = table.Column<string>(type: "TEXT", nullable: true),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UrlImagen = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.LibroId);
                    table.ForeignKey(
                        name: "FK_Libro_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resena",
                columns: table => new
                {
                    ReseñaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirebaseUserId = table.Column<string>(type: "TEXT", nullable: false),
                    LibroId = table.Column<int>(type: "INTEGER", nullable: false),
                    Calificacion = table.Column<int>(type: "INTEGER", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", nullable: false),
                    FechaReseña = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioFirebaseUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resena", x => x.ReseñaId);
                    table.ForeignKey(
                        name: "FK_Resena_Libro_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libro",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resena_Usuario_UsuarioFirebaseUserId",
                        column: x => x.UsuarioFirebaseUserId,
                        principalTable: "Usuario",
                        principalColumn: "FirebaseUserId");
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "NombreCategoria" },
                values: new object[,]
                {
                    { 1, "Filosofía antigua." },
                    { 2, "Filosofía medieval." },
                    { 3, "Filosofía moderna." },
                    { 4, "Filosofía contemporánea." },
                    { 5, "Filosofía analítica." }
                });

            migrationBuilder.InsertData(
                table: "Libro",
                columns: new[] { "LibroId", "Autor", "CategoriaId", "Resumen", "Titulo", "UrlImagen" },
                values: new object[,]
                {
                    { 1, "Platón.", 1, "Es un diálogo filosófico que explora la justicia y el orden ideal de una ciudad-estado. A través de la conversación entre Sócrates y otros personajes, Platón describe una sociedad gobernada por filósofos-reyes, donde cada clase social desempeña su función adecuada. La obra también introduce conceptos como la teoría de las Ideas, el mito de la caverna y la educación como medio para alcanzar la virtud.", "La república.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Rep%C3%BAblica.webp.webp?alt=media&token=eee07e5c-d550-4ff0-a90b-b86338b50518" },
                    { 2, "San Agustín.", 2, "Es una obra teológica y filosófica que defiende el cristianismo y ofrece una visión de la historia como una lucha entre la Ciudad de Dios (la comunidad de los fieles) y la Ciudad del Hombre (la sociedad terrenal y pagana). San Agustín argumenta que la verdadera paz y justicia solo se encuentran en la Ciudad de Dios, que trasciende el mundo material y perdura eternamente.", "La ciudad de Dios.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Ciudad%20de%20Dios.webp.webp?alt=media&token=ea74b60a-be41-4c65-acb4-1d3c6fa4cc69" },
                    { 3, "Pedro Abelardo.", 2, "Traducido por primera vez al español, este libro de Pedro Abelardo aborda la crítica de los conceptos universales asumidos como realidades ontológicas. El filósofo medieval propone la comprensión del universal como ‘sermo’, esto es, significación derivada de la intelección como operación anímica fundada en un ‘status’, estado de cosas o situación fáctica que al coincidir con otras situaciones similares, permite la formación de una imagen universal. De esta manera logra salvar la legitimidad significativa del universal sin conferirle una realidad ontológica.", "Tratado sobre las intelecciones.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Sobre%20las%20Intelecciones.webp.webp?alt=media&token=91741859-3dfd-4703-ba39-2b6128e8b092" },
                    { 4, "René Descartes.", 3, "Es una obra filosófica que busca establecer una base sólida para el conocimiento. A través de seis meditaciones, Descartes duda de todo lo que puede ser cuestionado para llegar a certezas indudables. Propone la famosa máxima \"Cogito, ergo sum\" (\"Pienso, luego existo\") como la primera verdad indudable. Luego, argumenta la existencia de Dios como garante de la verdad y la fiabilidad del conocimiento claro y distinto. Descartes establece una dualidad entre mente y cuerpo, sentando las bases del racionalismo moderno.", "Meditaciones metafísicas.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Meditaciones%20Meta.webp.webp?alt=media&token=b43cea57-0e0c-47c0-bd72-ad4b37c15777" },
                    { 5, "Marco Aurelio.", 1, "Es una serie de reflexiones personales del emperador romano, escritas como un diario de autoayuda y filosofía. Influenciado por el estoicismo, Marco Aurelio aborda temas como la impermanencia de la vida, el control de las emociones, la aceptación del destino y la importancia de vivir conforme a la razón y la virtud. El libro ofrece sabiduría práctica sobre cómo mantener la paz interior y la fortaleza moral frente a las adversidades, destacando la necesidad de vivir en armonía con la naturaleza y los demás.", "Meditaciones.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Meditaciones.webp.webp?alt=media&token=9d045973-6204-433c-8124-25ae593599c5" },
                    { 6, "David Hume.", 3, "Es una obra filosófica que investiga la naturaleza y los límites del conocimiento humano. Hume aplica el empirismo para explorar temas como la percepción, la emoción, la moralidad y la identidad personal. Argumenta que todas las ideas provienen de impresiones sensoriales y que la razón es esclava de las pasiones. Critica la noción de causalidad como una mera costumbre de asociación mental y examina la naturaleza de las creencias y la moralidad desde una perspectiva psicológica. La obra es fundamental para la filosofía moderna, especialmente en la epistemología y la ética.", "Tratado de la naturaleza humana.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Tratado%20de%20la%20nat.webp.webp?alt=media&token=e59e451a-a890-4325-9b19-e9861590a85e" },
                    { 7, "Immanuel Kant.", 3, "Es una obra filosófica que analiza los fundamentos y límites del conocimiento humano. Kant distingue entre conocimiento a priori (independiente de la experiencia) y conocimiento a posteriori (basado en la experiencia). Introduce el concepto de las categorías del entendimiento, que son estructuras mentales innatas que organizan la experiencia sensorial. Kant argumenta que la realidad que percibimos (el fenómeno) está condicionada por estas categorías, mientras que la realidad en sí misma (el noumeno) es incognoscible. La obra busca reconciliar el racionalismo y el empirismo, estableciendo los límites de la razón y la ciencia.", "Crítica de la razón pura.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Kritik.webp.webp?alt=media&token=772625f1-8ff3-41bb-a2c6-cb40c0f8fc77" },
                    { 8, "Ludwig Wittgenstein.", 5, "Es una obra fundamental en la filosofía del lenguaje y la lógica. En este libro, Wittgenstein sostiene que el mundo se compone de hechos, no de cosas, y que el lenguaje refleja estos hechos a través de proposiciones. Cada proposición tiene una estructura lógica que corresponde a la estructura de la realidad. Wittgenstein presenta la idea de que los límites de nuestro lenguaje son los límites de nuestro mundo, concluyendo con la famosa proposición: \"De lo que no se puede hablar, hay que callar.\" El \"Tractatus\" busca delinear los límites del pensamiento y del lenguaje, influenciando profundamente la filosofía analítica.", "Tractatus logico-philosophicus", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/TLP.webp.webp?alt=media&token=f46f5a9c-603b-4a43-bc67-f32aaea96c29" },
                    { 9, "Ludwig Wittgenstein.", 4, "En este libro Wittgenstein revisa y critica las ideas expuestas en su \"Tractatus Logico-Philosophicus\". Aquí argumenta que el significado de las palabras se entiende mejor a través de su uso en el lenguaje cotidiano, introduciendo el concepto de \"juegos del lenguaje\". Rechaza la idea de que el lenguaje tiene una estructura lógica subyacente fija, destacando la diversidad y flexibilidad de las prácticas lingüísticas. La obra enfatiza el análisis de las formas de vida y la interpretación contextual del significado, marcando un giro hacia una filosofía del lenguaje más pragmática y menos formalista.", "Investigaciones filosóficas.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/IF.webp.webp?alt=media&token=1de7a952-d74f-40f4-87fe-8b10b6c08d80" },
                    { 10, "Michel Foucault.", 4, "Se trata de la transcripción de la última serie de conferencias dictadas por Michel Foucault en el Collège de France en 1984. En estas lecciones, Foucault explora la noción de la \"parresia\" o el \"decir veraz\", un concepto de la antigua filosofía griega que se refiere a la franqueza y la valentía para decir la verdad en contextos peligrosos o difíciles. Foucault examina cómo la parresia se relaciona con la ética personal, la política y la filosofía. A través del análisis de figuras históricas como Sócrates y Diógenes, Foucault discute cómo el acto de decir la verdad es un ejercicio de libertad y un desafío al poder establecido. \"El Coraje de la Verdad\" concluye con reflexiones sobre el papel del intelectual en la sociedad y la importancia de la verdad como práctica ética.", "El coraje de la verdad.", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/Coraje.webp.webp?alt=media&token=e204ad9f-3531-4f2f-87fa-ae77f245083b" },
                    { 11, "Julian Baggini.", 4, "Es un libro que explora las diferencias y similitudes en las perspectivas filosóficas y culturales alrededor del mundo. Baggini examina cómo diversas tradiciones filosóficas abordan temas universales como la verdad, la ética y la existencia. El libro invita a reflexionar sobre la diversidad de pensamiento humano y cómo las distintas culturas han desarrollado respuestas a las grandes preguntas de la vida.", "Cómo piensa el mundo", "https://firebasestorage.googleapis.com/v0/b/filobook-9c043.appspot.com/o/C%C3%B3mo%20piensa.webp.webp?alt=media&token=334f8dd9-7155-41fc-9f69-0638442e2161" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libro_CategoriaId",
                table: "Libro",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Resena_LibroId",
                table: "Resena",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_Resena_UsuarioFirebaseUserId",
                table: "Resena",
                column: "UsuarioFirebaseUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resena");

            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
