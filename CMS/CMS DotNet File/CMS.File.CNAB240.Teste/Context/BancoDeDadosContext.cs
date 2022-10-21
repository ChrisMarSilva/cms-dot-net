using CMS.EF.Performance.Console.CNAB240.Entity;
using CMS.EF.Performance.Console.CNAB240.Mappings;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CMS.EF.Performance.CNAB240.Context
{
    public class BancoDeDadosContext : DbContext
    {

        // public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole(); });
        public DbSet<JDArquivo> Arquivos { get; set; }
        public DbSet<JDCompe> Compes { get; set; }
        public DbSet<JDRegistro> Registros { get; set; }
        public DbSet<JDResposta> Respostas { get; set; }

        public BancoDeDadosContext() : base("Name=DbConnJDSPB") // DbConnJDSPB // DbConnJD // DbConnNote
        {
            //Database.SetInitializer<BancoDeDadosContext>(new CreateDatabaseIfNotExists<BancoDeDadosContext>());
            //Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext]
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //Database.Log = comando => System.Console.WriteLine(comando); //System.Diagnostics.Debug.WriteLine(comando);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();// Nao colocar o nomes das tabelas no plural
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Disable cascade delete 
            modelBuilder.Configurations.Add<JDArquivo>(new JDArquivoMapping());
            modelBuilder.Configurations.Add<JDCompe>(new JDCompeMapping());
            modelBuilder.Configurations.Add<JDRegistro>(new JDRegistroMapping());
            modelBuilder.Configurations.Add<JDResposta>(new JDRespostaMapping());
            base.OnModelCreating(modelBuilder);
        }

    }
}
