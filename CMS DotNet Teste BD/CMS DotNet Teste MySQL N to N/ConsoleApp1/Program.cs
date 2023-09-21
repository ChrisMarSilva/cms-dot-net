using Microsoft.EntityFrameworkCore;

Console.WriteLine("INI");
try
{
    using var context = new AppDbContext();

    //var contato1 = context.Contatos.AsNoTracking().FirstOrDefault(x => x.IdContato.Equals(Guid.Parse("47154052-3169-4403-B147-EC4A0944F93F")));
    //var contato2 = context.Contatos.AsNoTracking().FirstOrDefault(x => x.IdContato.Equals(Guid.Parse("00E10C50-65CC-47AF-BD5F-17692CB27989")));

    //var regra = new Regra(Guid.NewGuid(), "Regra 1", 1, "A");
    //regra.AdicionarContato(contato1.IdContato);
    //regra.AdicionarContato(contato2.IdContato);

    //context.Regras.Add(regra);
    //context.SaveChanges();
    //Console.WriteLine("salvo");


    var regra1 = context.Regras.AsNoTracking().Include(r  => r.Contatos).FirstOrDefault(x => x.IdRegra.Equals(Guid.Parse("287478A0-90BD-486D-BA4C-B0D77F28EE05")));
    Console.WriteLine($"Regra: {regra1!.NmRegra}");

    foreach (var contato in regra1.Contatos)
    {
        Console.WriteLine($"Contato: {contato!.NmContato}");
    }


}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}

public class AppDbContext : DbContext
{
    public DbSet<Regra> Regras { get; set; }
    public DbSet<Contato> Contatos { get; set; }
    public DbSet<RegraContato> RegraContatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = @"Data Source=jdsp108;Initial Catalog=JDNPC;User ID=jddesenv;Password=jddesenv;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        options.UseSqlServer(connectionString);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Regra
        mb.Entity<Regra>().ToTable("TbJdDje_Regra", tb => tb.HasComment("Regra a serem processada em uma comunicação processual."));
        mb.Entity<Regra>().HasKey(e => e.IdRegra).HasName("PkJdDje_Regra");
        mb.Entity<Regra>().Property(e => e.IdRegra).ValueGeneratedNever().HasComment("Identificador de uma regra.");
        mb.Entity<Regra>().Property(e => e.NmRegra).IsRequired().HasMaxLength(100).IsUnicode(false).HasComment("Nome da regra.");
        mb.Entity<Regra>().Property(e => e.StRegra).IsRequired().HasMaxLength(1).IsUnicode(false).IsFixedLength().HasComment("Situação da regra: A-ativa, I-inativa.");
        mb.Entity<Regra>().Property(e => e.TpAcaoRegra).HasComment("Tipo de ação da regra: 1- encaminhar e-mail.");
        mb.Entity<Regra>().HasMany(e => e.Contatos).WithMany(e => e.Regras).UsingEntity<RegraContato>();

        //Contato
        mb.Entity<Contato>().ToTable("TbJdDje_Contato", tb => tb.HasComment("Contato a enviar uma comunicação processual."));
        mb.Entity<Contato>().HasKey(e => e.IdContato).HasName("PkJdDje_Contato");
        mb.Entity<Contato>().Property(e => e.IdContato).ValueGeneratedNever().HasComment("Identificador do coxntato.");
        mb.Entity<Contato>().Property(e => e.EmailContato).IsRequired().HasMaxLength(100).IsUnicode(false).HasComment("E-mail do contato.");
        mb.Entity<Contato>().Property(e => e.NmContato).HasMaxLength(100).IsUnicode(false).HasComment("Nome do contato.");
        mb.Entity<Contato>().Property(e => e.StContato).IsRequired().HasMaxLength(1).IsUnicode(false).IsFixedLength().HasComment("Situação do contato: A-ativo, I-inativo.");
        mb.Entity<Contato>().HasMany(e => e.Regras).WithMany(e => e.Contatos).UsingEntity<RegraContato>();

        //RegraContato
        mb.Entity<RegraContato>().ToTable("TbJdDje_RegraContato");
        mb.Entity<RegraContato>().HasKey(bc => new { bc.IdRegra, bc.IdContato }).HasName("PkJdDje_RegraContato");
    }
}

public class Regra
{
    public Regra() { }

    public Regra(Guid idRegra, string nmRegra, int tpAcaoRegra, string stRegra) : this()
    {
        IdRegra = idRegra;
        NmRegra = nmRegra;
        TpAcaoRegra = tpAcaoRegra;
        StRegra = stRegra;
    }

    public Guid IdRegra { get; set; } = Guid.NewGuid();
    public string NmRegra { get; set; } = string.Empty;
    public int TpAcaoRegra { get; set; }
    public string StRegra { get; set; } = string.Empty;
    public virtual ICollection<Contato> Contatos { get; set; } = new HashSet<Contato>();
    public ICollection<RegraContato> RegraContatos { get; } = new HashSet<RegraContato>();

    public void AdicionarContato(Guid idContato)
    {
        RegraContatos.Add(new RegraContato(IdRegra, idContato));
    }
}

public class Contato
{
    public Contato() { }

    public Contato(Guid idContato, string nmContato, string emailContato, string stContato) : this()
    {
        IdContato = idContato;
        NmContato = nmContato;
        EmailContato = emailContato;
        StContato = stContato;
    }

    public Guid IdContato { get; set; } = Guid.NewGuid();
    public string NmContato { get; set; } = string.Empty;
    public string EmailContato { get; set; } = string.Empty;
    public string StContato { get; set; } = string.Empty;
    public virtual ICollection<Regra> Regras { get; set; } = new HashSet<Regra>();
    public ICollection<RegraContato> RegraContatos { get; } = new HashSet<RegraContato>();
}

public class RegraContato
{
    public RegraContato(Guid idRegra, Guid idContato)
    {
        IdRegra = idRegra;
        IdContato = idContato;
    }

    public Guid IdRegra { get; set; }
    public Guid IdContato { get; set; }
}
