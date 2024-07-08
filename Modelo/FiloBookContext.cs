using ControlboxLibreriaAPI.Entities;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;

namespace ControlboxLibreriaAPI.Modelo
{
    public class FiloBookContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; } = null!;
        public DbSet<Libro> Libro { get; set; } = null!;
        public DbSet<Resena> Resena { get; set; } = null!;
        public DbSet<Categoria> Categoria { get; set; } = null!;

        public FiloBookContext(DbContextOptions<FiloBookContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=Database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data for Categoria
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { CategoriaId = 1, NombreCategoria = "Filosofía antigua." },
                new Categoria { CategoriaId = 2, NombreCategoria = "Filosofía medieval." },
                new Categoria { CategoriaId = 3, NombreCategoria = "Filosofía moderna." },
                new Categoria { CategoriaId = 4, NombreCategoria = "Filosofía contemporánea." },
                new Categoria { CategoriaId = 5, NombreCategoria = "Filosofía analítica." }
            );

            // Seed initial data for Libro
            modelBuilder.Entity<Libro>().HasData(
                new Libro
                {
                    LibroId = 1,
                    Titulo = "La república.",
                    Autor = "Platón.",
                    Resumen = "Es un diálogo filosófico que explora la justicia y el orden ideal de una ciudad-estado. A través de la conversación entre Sócrates y otros personajes, Platón describe una sociedad gobernada por filósofos-reyes, donde cada clase social desempeña su función adecuada. La obra también introduce conceptos como la teoría de las Ideas, el mito de la caverna y la educación como medio para alcanzar la virtud.",
                    CategoriaId = 1,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 2,
                    Titulo = "La ciudad de Dios.",
                    Autor = "San Agustín.",
                    Resumen = "Es una obra teológica y filosófica que defiende el cristianismo y ofrece una visión de la historia como una lucha entre la Ciudad de Dios (la comunidad de los fieles) y la Ciudad del Hombre (la sociedad terrenal y pagana). San Agustín argumenta que la verdadera paz y justicia solo se encuentran en la Ciudad de Dios, que trasciende el mundo material y perdura eternamente.",
                    CategoriaId = 2,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 3,
                    Titulo = "Tratado sobre las intelecciones.",
                    Autor = "Pedro Abelardo.",
                    Resumen = "Traducido por primera vez al español, este libro de Pedro Abelardo aborda la crítica de los conceptos universales asumidos como realidades ontológicas. El filósofo medieval propone la comprensión del universal como ‘sermo’, esto es, significación derivada de la intelección como operación anímica fundada en un ‘status’, estado de cosas o situación fáctica que al coincidir con otras situaciones similares, permite la formación de una imagen universal. De esta manera logra salvar la legitimidad significativa del universal sin conferirle una realidad ontológica.",
                    CategoriaId = 2,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 4,
                    Titulo = "Meditaciones metafísicas.",
                    Autor = "René Descartes.",
                    Resumen = "Es una obra filosófica que busca establecer una base sólida para el conocimiento. A través de seis meditaciones, Descartes duda de todo lo que puede ser cuestionado para llegar a certezas indudables. Propone la famosa máxima \"Cogito, ergo sum\" (\"Pienso, luego existo\") como la primera verdad indudable. Luego, argumenta la existencia de Dios como garante de la verdad y la fiabilidad del conocimiento claro y distinto. Descartes establece una dualidad entre mente y cuerpo, sentando las bases del racionalismo moderno.",
                    CategoriaId = 3,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 5,
                    Titulo = "Meditaciones.",
                    Autor = "Marco Aurelio.",
                    Resumen = "Es una serie de reflexiones personales del emperador romano, escritas como un diario de autoayuda y filosofía. Influenciado por el estoicismo, Marco Aurelio aborda temas como la impermanencia de la vida, el control de las emociones, la aceptación del destino y la importancia de vivir conforme a la razón y la virtud. El libro ofrece sabiduría práctica sobre cómo mantener la paz interior y la fortaleza moral frente a las adversidades, destacando la necesidad de vivir en armonía con la naturaleza y los demás.",
                    CategoriaId = 1,
                    UrlImagen = ""
                },
                new Libro
                {
                LibroId = 6,
                    Titulo = "Tratado de la naturaleza humana.",
                    Autor = "David Hume.",
                    Resumen = "Es una obra filosófica que investiga la naturaleza y los límites del conocimiento humano. Hume aplica el empirismo para explorar temas como la percepción, la emoción, la moralidad y la identidad personal. Argumenta que todas las ideas provienen de impresiones sensoriales y que la razón es esclava de las pasiones. Critica la noción de causalidad como una mera costumbre de asociación mental y examina la naturaleza de las creencias y la moralidad desde una perspectiva psicológica. La obra es fundamental para la filosofía moderna, especialmente en la epistemología y la ética.",
                    CategoriaId = 3,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 7,
                    Titulo = "Crítica de la razón pura.",
                    Autor = "Immanuel Kant.",
                    Resumen = "Es una obra filosófica que analiza los fundamentos y límites del conocimiento humano. Kant distingue entre conocimiento a priori (independiente de la experiencia) y conocimiento a posteriori (basado en la experiencia). Introduce el concepto de las categorías del entendimiento, que son estructuras mentales innatas que organizan la experiencia sensorial. Kant argumenta que la realidad que percibimos (el fenómeno) está condicionada por estas categorías, mientras que la realidad en sí misma (el noumeno) es incognoscible. La obra busca reconciliar el racionalismo y el empirismo, estableciendo los límites de la razón y la ciencia.",
                    CategoriaId = 3,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 8,
                    Titulo = "Tractatus logico-philosophicus",
                    Autor = "Ludwig Wittgenstein.",
                    Resumen = "Es una obra fundamental en la filosofía del lenguaje y la lógica. En este libro, Wittgenstein sostiene que el mundo se compone de hechos, no de cosas, y que el lenguaje refleja estos hechos a través de proposiciones. Cada proposición tiene una estructura lógica que corresponde a la estructura de la realidad. Wittgenstein presenta la idea de que los límites de nuestro lenguaje son los límites de nuestro mundo, concluyendo con la famosa proposición: \"De lo que no se puede hablar, hay que callar.\" El \"Tractatus\" busca delinear los límites del pensamiento y del lenguaje, influenciando profundamente la filosofía analítica.",
                    CategoriaId = 5,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 8,
                    Titulo = "Investigaciones filosóficas.",
                    Autor = "Ludwig Wittgenstein.",
                    Resumen = "En este libro Wittgenstein revisa y critica las ideas expuestas en su \"Tractatus Logico-Philosophicus\". Aquí argumenta que el significado de las palabras se entiende mejor a través de su uso en el lenguaje cotidiano, introduciendo el concepto de \"juegos del lenguaje\". Rechaza la idea de que el lenguaje tiene una estructura lógica subyacente fija, destacando la diversidad y flexibilidad de las prácticas lingüísticas. La obra enfatiza el análisis de las formas de vida y la interpretación contextual del significado, marcando un giro hacia una filosofía del lenguaje más pragmática y menos formalista.",
                    CategoriaId = 4,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 9,
                    Titulo = "El coraje de la verdad.",
                    Autor = "Michel Foucault.",
                    Resumen = "Se trata de la transcripción de la última serie de conferencias dictadas por Michel Foucault en el Collège de France en 1984. En estas lecciones, Foucault explora la noción de la \"parresia\" o el \"decir veraz\", un concepto de la antigua filosofía griega que se refiere a la franqueza y la valentía para decir la verdad en contextos peligrosos o difíciles. Foucault examina cómo la parresia se relaciona con la ética personal, la política y la filosofía. A través del análisis de figuras históricas como Sócrates y Diógenes, Foucault discute cómo el acto de decir la verdad es un ejercicio de libertad y un desafío al poder establecido. \"El Coraje de la Verdad\" concluye con reflexiones sobre el papel del intelectual en la sociedad y la importancia de la verdad como práctica ética.",
                    CategoriaId = 4,
                    UrlImagen = ""
                },
                new Libro
                {
                    LibroId = 10,
                    Titulo = "Cómo piensa el mundo",
                    Autor = "Julian Baggini.",
                    Resumen = "Es un libro que explora las diferencias y similitudes en las perspectivas filosóficas y culturales alrededor del mundo. Baggini examina cómo diversas tradiciones filosóficas abordan temas universales como la verdad, la ética y la existencia. El libro invita a reflexionar sobre la diversidad de pensamiento humano y cómo las distintas culturas han desarrollado respuestas a las grandes preguntas de la vida.",
                    CategoriaId = 4,
                    UrlImagen = ""
                }

            );
        }



    }
}